using System;
using PoultryPopulation.Entities;

namespace PoultryPopulation.ViewModels
{
    public class RoosterRoster
    {
        public string RoosterName { get; set; }
        public DateTime ShiftStart { get; set; }
        public DateTime ShiftEnd { get; set; }
        public string ChickenCoopName { get; set; }
        public string CoopLocation { get; set; }
    }
}
