using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities
{
    public class Owner: EntityBase, IIdentified<int>
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        public Owner(int id, string name, string phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        [Obsolete("For EF/tests only.", error: true)]
        protected Owner() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual Address Address { get; set; }

        public virtual ICollection<ChickenCoop> ChickenCoops { get; set; }

        public virtual ICollection<Chicken> Chickens { get; set; }
    }
}
