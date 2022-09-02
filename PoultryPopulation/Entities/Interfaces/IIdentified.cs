namespace PoultryPopulation.Entities.Interfaces
{
    public interface IIdentified<TKey>
    {
        TKey Id { get; set; }
    }
}
