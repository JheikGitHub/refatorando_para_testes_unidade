using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeDeliveryRepository : IDeliveryRepository
    {
        public decimal Get(string zipCode)
        {
            // var randon = new Random();
            // return randon.Next(0, 10);
            return 10;
        }
    }
}