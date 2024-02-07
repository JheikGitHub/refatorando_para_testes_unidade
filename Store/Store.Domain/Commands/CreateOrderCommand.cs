using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interface;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderCommand() => Items = [];

        public CreateOrderCommand(string customer, string zipCode, string promoCode, IList<CreateOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            PromoCode = promoCode;
            Items = items;
        }

        public string Customer { get; set; }
        public string ZipCode { get; set; }
        public string PromoCode { get; set; }
        public IList<CreateOrderItemCommand> Items { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<CreateOrderCommand>()
                .Requires()
                .IsFalse(Customer.Length != 11, "Customer", "Cliente inválido")
                .IsFalse(ZipCode.Length != 8, nameof(ZipCode), "CEP inválido.")
            );
        }
    }
}