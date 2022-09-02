using System;
using System.Collections.Generic;
using AutoBogus;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryCloningFactory.Fakes
{
    public class LookupEntityBaseFaker<TEntity> : AutoFaker<TEntity> where TEntity : EntityBase, IIdentified<int>
    {
        private readonly List<TEntity> _entities = new List<TEntity>();
        private int MinimumSet { get; }

        public List<TEntity> Entities
        {
            get
            {
                if (_entities.Count == 0 && MinimumSet != 0)
                {
                    Generate(MinimumSet);
                }
                return _entities;
            }
        } 

        internal LookupEntityBaseFaker(int minimumSet)
        {
            MinimumSet = minimumSet;
            base.RuleFor(b => b.Id, f => f.IndexFaker);
            base.RuleFor(b => b.Created, f => DateTimeOffset.Now);
            base.RuleFor(b => b.CreatedBy, f => "FAKE DATA MAKER");
            base.RuleFor(b => b.Modified, f => null);
            base.RuleFor(b => b.ModifiedBy, f => null);
        }

        public override TEntity Generate(string ruleSets = null)
        {
            TEntity newlyGenerated = base.Generate(ruleSets);
            
            _entities.Add(newlyGenerated);

            return newlyGenerated;
        }

    }
}