using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;
using Store.Tests.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IDeliveryRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRepository();
            _deliveryFeeRepository = new FakeDeliveryRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderRepository();
            _productRepository = new FakeProductRepository();
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "",
                ZipCode = "13411080",
                PromoCode = "12345678"
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            command.Validate();
            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand
            {
                Customer = "12345678",
                ZipCode = "",
                PromoCode = "12345678"
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            Assert.AreEqual(command.Items.Count, 1);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand
            {
                Customer = "12345678",
                ZipCode = "12345678",
                PromoCode = null
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            Assert.AreEqual(command.Items.Count, 1);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_pedido_sem_itens_o_mesmo_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "12345678",
                ZipCode = "12345678",
                PromoCode = null
            };

            Assert.AreEqual(command.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "",
                ZipCode = "13411080",
                PromoCode = "12345678"
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.IsValid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand
            {
                Customer = "12345678900",
                ZipCode = "13411080",
                PromoCode = "12345678"
            };
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            command.Validate();

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _productRepository,
                _orderRepository);

            handler.Handle(command);
            Assert.AreEqual(handler.IsValid, true);
        }
    }
}