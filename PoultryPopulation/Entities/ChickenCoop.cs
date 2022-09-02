using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities
{
    public class ChickenCoop : EntityBase, IIdentified<int>
    {
        public ChickenCoop(
            string name,
            int ownerId)
        {
            Name = name;
            OwnerId = ownerId;
        }

        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public ChickenCoop(
            int id,
            string name,
            int ownerId,
            Owner owner,
            ICollection<Chicken> housedChickens)
        {
            Id = id;
            Name = name;
            OwnerId = ownerId;
        }

        [Obsolete("For EF/tests only.", error: true)]
        protected ChickenCoop() { }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual Owner Owner { get; set; }
        public virtual ICollection<Chicken> HousedChickens { get; set; }
    }
}
