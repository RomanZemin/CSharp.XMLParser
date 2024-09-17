using CST.Application.Interfaces;
using CST.Domain.Entities;
using CST.Persistence.Data;
using CST.Persistence.Parsers;

namespace CST.Persistence.Services
{
    public class OrderService : IOrderService
    {
        private readonly XmlParser _xmlParser;
        private readonly string _connectionString;

        public OrderService(string connectionString)
        {
            _xmlParser = new XmlParser();
            _connectionString = connectionString;
        }

        public void ProcessOrders(string xmlPath)
        {
            var xmlDocument = _xmlParser.LoadXml(xmlPath);

            using (var context = new AppDbContext(_connectionString))
            {
                foreach (var orderElement in xmlDocument.Descendants("order"))
                {
                    var userElement = orderElement.Element("user");
                    var fullName = _xmlParser.GetElementValue(userElement, "fio");
                    var email = _xmlParser.GetElementValue(userElement, "email");

                    var user = context.Users.FirstOrDefault(u => u.Email == email);
                    if (user == null)
                    {
                        user = new User
                        {
                            Username = fullName,
                            Email = email,
                            PasswordHash = "default_hash",
                            Address = "default_address",
                            CreatedAt = DateTime.UtcNow
                        };
                        context.Users.Add(user);
                        context.SaveChanges();
                    }

                    var orderDate = _xmlParser.ParseDate(_xmlParser.GetElementValue(orderElement, "reg_date"));
                    var totalPrice = _xmlParser.ParseDecimal(_xmlParser.GetElementValue(orderElement, "sum"));

                    var order = new Order
                    {
                        OrderDate = orderDate,
                        TotalPrice = totalPrice,
                        UserID = user.UserID
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();

                    foreach (var productElement in orderElement.Descendants("product"))
                    {
                        var productName = _xmlParser.GetElementValue(productElement, "name");
                        var productPrice = _xmlParser.ParseDecimal(_xmlParser.GetElementValue(productElement, "price"));
                        var productQuantity = _xmlParser.ParseInt(_xmlParser.GetElementValue(productElement, "quantity"));

                        var product = context.Products.FirstOrDefault(p => p.Name == productName);
                        if (product == null)
                        {
                            product = new Product
                            {
                                Name = productName,
                                Price = productPrice,
                                StockQuantity = 0,
                                Category = "default_category",
                                CreatedAt = DateTime.UtcNow
                            };
                            context.Products.Add(product);
                            context.SaveChanges();
                        }

                        var orderItem = new OrderItem
                        {
                            OrderID = order.OrderID,
                            ProductID = product.ProductID,
                            Quantity = productQuantity,
                            Price = productPrice
                        };
                        context.OrderItems.Add(orderItem);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
