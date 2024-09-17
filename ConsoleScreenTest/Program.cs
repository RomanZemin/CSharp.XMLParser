using CST.Persistence.Services;

namespace CST.Presentation.ConsoleScreenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Укажите путь к XML файлу
            string xmlPath = "C:\\Users\\mvide\\source\\repos\\ConsoleScreenTest\\Data.xml";

            // Задайте строку подключения
            string connectionString = "Host=localhost;Port=5433;Database=Shop;Username=postgres;Password=admin12";

            var orderService = new OrderService(connectionString);
            orderService.ProcessOrders(xmlPath);

            Console.WriteLine("Данные успешно загружены.");
        }
    }
}
