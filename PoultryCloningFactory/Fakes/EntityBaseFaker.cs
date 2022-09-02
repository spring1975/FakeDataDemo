using System;
using System.Collections.Generic;
using AutoBogus;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryCloningFactory.Fakes
{
    public class EntityBaseFaker<TEntity> : AutoFaker<TEntity> where TEntity : EntityBase, IIdentified<int>
    {
        internal EntityBaseFaker()
        {
            base.RuleFor(b => b.Id, f => f.IndexFaker);
            base.RuleFor(b => b.Created, f => DateTimeOffset.Now);
            base.RuleFor(b => b.CreatedBy, f => "FAKE DATA MAKER");
            base.RuleFor(b => b.Modified, f => null);
            base.RuleFor(b => b.ModifiedBy, f => null);
        }


        public List<TEntity> Entities { get; } = new List<TEntity>();

        public override TEntity Generate(string ruleSets = null)
        {
            TEntity newlyGenerated = base.Generate(ruleSets);
            Entities.Add(newlyGenerated);

            return newlyGenerated;
        }
    }
}