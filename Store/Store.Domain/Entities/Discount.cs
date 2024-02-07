
using Flunt.Validations;

namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public Discount(decimal amount, DateTime expireDate)
        {
            AddNotifications(
                new Contract<Discount>()
                .Requires()
                .IsGreaterThan(amount, 0, "Amount", "A quantia não pode ser menor que zero.")
            );
            
            Amount = amount;
            ExpireDate = expireDate;
        }

        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public bool IsValidDiscount()
        {
            return DateTime.Compare(DateTime.Now, ExpireDate) < 0;
        }

        public decimal Value()
        {
            if (IsValidDiscount())
                return Amount;
            else
                return 0;
        }
    }
}