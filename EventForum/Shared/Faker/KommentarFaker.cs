using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using EventForum.Shared.Aggregates.Beitrag.ValueObjects;

namespace EventForum.Shared.Faker
{
    public class KommentarFaker
    {
        private readonly Faker<KommentarData> _faker;

        public KommentarFaker()
        {
            _faker = new Faker<KommentarData>("de")
                .RuleFor(k => k.Name, f => f.Name.FullName())
                .RuleFor(k => k.Betreff, f => f.Lorem.Sentence(2, 3))
                .RuleFor(k => k.Inhalt, f => f.Lorem.Sentences(f.Random.Number(1, 5)))
                .RuleFor(k => k.MetaInfo, f => new KommentarMetaInfo { UserId = f.Random.Guid().ToString()})
                ;
        }

        public IEnumerable<KommentarData> Generate(int n) => _faker.Generate(n);
    }
}
