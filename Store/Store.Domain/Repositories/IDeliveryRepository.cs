namespace Store.Domain.Repositories
{
    public interface IDeliveryRepository
    {
        decimal Get(string zipCode);
    }
}