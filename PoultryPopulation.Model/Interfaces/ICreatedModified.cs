using System;

namespace PoultryPopulation.Model.Interfaces
{
    public interface ICreatedModified
    {
        DateTimeOffset? Created { get; set; }
        DateTimeOffset? Modified { get; set; }
    }
}
