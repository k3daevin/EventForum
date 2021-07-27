using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using EventFlow;
using EventFlow.Extensions;
using EventFlow.Configuration;
using EventForum.Shared.Aggregates.Beitrag.Commands;
using EventForum.Shared.Aggregates.Beitrag.Events;
using EventForum.Shared.Aggregates.Beitrag.ReadModels;
using System;
using EventForum.Shared.Aggregates.Beitrag.Queries;
using System.Collections.Generic;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;
using EventFlow.Queries;
using EventForum.Shared.Aggregates.Beitrag;
using EventForum.Shared.Subscriber;
using EventFlow.Subscribers;
using EventForum.Server.Hubs;
using EventFlow.MongoDB;
using EventFlow.MongoDB.Extensions;
using EventFlow.MongoDB.EventStore;
using EventFlow.MongoDB.ReadStores;

namespace EventForum.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            AddEventFlowResolver(services, EventForumStore.Mongo2Go);
            services.AddSignalR();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(opt =>
            {

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(opt =>
            {

            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventForum API V1");

            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapHub<BeitragHub>("/beitragHub");
                endpoints.MapFallbackToFile("index.html");
            });
        }

        public enum EventForumStore
        {
            InMemory,
            Mongo2Go,
        }
        public IServiceCollection AddEventFlowResolver(IServiceCollection services, EventForumStore eventForumStore)
        {

            var eventFlowOptions = EventFlowOptions.New;
            eventFlowOptions.Configure(c =>
            {
                c.IsAsynchronousSubscribersEnabled = true;

            });
            eventFlowOptions.AddCommands(new[]
            {
                typeof(ErstelleBeitragCommand),
                typeof(FuegeKommentarHinzuCommand),
            });
            eventFlowOptions.AddCommandHandlers(new[]
            {
                typeof(ErstelleBeitragCommandHandler),
                typeof(FuegeKommentarHinzuCommandHandler),
            });
            eventFlowOptions.AddEvents(new[]
            {
                typeof(BeitragErstelltEvent),
                typeof(KommentarHinzugefuegtEvent),
            });
            eventFlowOptions.AddAsynchronousSubscriber<BeitragAggregate, BeitragId, KommentarHinzugefuegtEvent, KommentarHinzugefuegtSubscriber>();

            var subscriberEventContainer = new SubscriberEventContainer();

            eventFlowOptions.RegisterServices(register =>
            {
                register.Register<SubscriberEventContainer>(context => subscriberEventContainer);

            });
            services.AddSingleton<SubscriberEventContainer>(sp => subscriberEventContainer);
            services.AddSingleton<BeitragHubContext>();

            switch (eventForumStore)
            {
                case EventForumStore.InMemory:
                    eventFlowOptions.UseInMemoryReadStoreFor<BeitragReadModel>();
                    eventFlowOptions.UseInMemoryReadStoreFor<VorschauReadModel>();
                    eventFlowOptions.AddQueryHandler<InMemoryAlleBeitragIdsQueryHandler, AlleBeitraegeQuery, IEnumerable<string>>();
                    eventFlowOptions.AddQueryHandler<InMemoryVorschauQueryHandler, VorschauQuery, IEnumerable<VorschauReadModel>>();
                    break;
                case EventForumStore.Mongo2Go:
                    var runner = Mongo2Go.MongoDbRunner.Start();
                    var mongoClient = new MongoDB.Driver.MongoClient(runner.ConnectionString);
                    eventFlowOptions.ConfigureMongoDb(mongoClient, "forum");

                    eventFlowOptions.UseMongoDbEventStore();
                    eventFlowOptions.UseMongoDbReadModel<BeitragReadModel>();
                    eventFlowOptions.UseMongoDbReadModel<VorschauReadModel>();
                    eventFlowOptions.AddQueryHandler<MongoDbAlleBeitragIdsQueryHandler, AlleBeitraegeQuery, IEnumerable<string>>();
                    eventFlowOptions.AddQueryHandler<MongoDbVorschauQueryHandler, VorschauQuery, IEnumerable<VorschauReadModel>>();
                    services.AddSingleton(runner);
                    break;
                default:
                    throw new NotImplementedException();
            }

            var resolver = eventFlowOptions.CreateResolver();


            services.AddSingleton<ICommandBus>(s => resolver.Resolve<ICommandBus>());
            services.AddSingleton<IQueryProcessor>(s => resolver.Resolve<IQueryProcessor>());



            return services;
        }
    }
}
