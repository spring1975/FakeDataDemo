using System.ComponentModel.DataAnnotations;
using PoultryPopulation.Entities.Abstracts;
using PoultryPopulation.Entities.Interfaces;

namespace PoultryPopulation.Entities
{
    public class Address: EntityBase, IIdentified<int>
    {
        public Address(string streetAddress, string streetAddress2, string city, string state, string postalCode)
        {
            StreetAddress = streetAddress;
            StreetAddress2 = streetAddress2;
            City = city;
            State = state;
            PostalCode = postalCode;
        }
        [Key]
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

    }
}
