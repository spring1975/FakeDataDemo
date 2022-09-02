using System;
using System.Collections.Generic;
using PoultryPopulation.Model;

namespace PoultryPopulation.Persistence.Models
{
    public class Owner: EntityBase
    {
        public Owner(int id, string name, string phoneNumber, Address address)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Address = address;
        }

        [Obsolete("For EF/tests only.", error: true)]
        protected Owner() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<ChickenCoop> ChickenCoops { get; set; }
        public virtual ICollection<Chicken> Chickens { get; set; }
    }
}
