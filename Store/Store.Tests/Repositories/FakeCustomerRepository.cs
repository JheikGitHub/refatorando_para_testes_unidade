using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeCustomerRepository : ICustomerRepository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Jheik Alves", "jheikalves7@gmail.com");

            return null;
        }
    }
}