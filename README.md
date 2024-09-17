# ConsoleScreenTest

Этот проект является тестовым заданием для компании СКРИН. Он представляет собой консольное приложение на C#, которое загружает данные о покупках из XML файла в базу данных.

## Структура проекта

Проект использует принципы чистой архитектуры и имеет следующую структуру:

- `CST.Core/CST.Application/Interfaces/IOrderService.cs` — Интерфейс сервиса заказа.
- `CST.Core/CST.Domain/Entities/Order.cs` — Сущности домена (Order, OrderItem, Product, User).
- `CST.Infrastructure/CST.Persistence/Data/AppDbContext.cs` — Контекст базы данных.
- `CST.Infrastructure/CST.Persistence/Parser/XmlParser.cs` — Парсер XML.
- `CST.Infrastructure/CST.Persistence/Services/OrderService.cs` — Сервис обработки заказов.
- `CST.Presentation/ConsoleScreenTest/Program.cs` — Точка входа в приложение.
- `CST.Presentation/ConsoleScreenTest/Data.xml` — Пример XML файла с данными.

## Инструкции по запуску

1. **Откройте решение**:
   Перейдите в каталог `ConsoleScreenTest` и откройте файл `ConsoleScreenTest.sln` в вашей IDE (например, Visual Studio).

2. **Настройте строку подключения**:
   В файле `Program.cs` задайте строку подключения к вашей базе данных PostgreSQL:
   ```csharp
   string connectionString = "Host=localhost;Port=5433;Database=Shop;Username=postgres;Password=admin12";
3. **Подготовьте XML файл:** Убедитесь, что файл `Data.xml` находится в том же каталоге, что и `Program.cs`.
4. **Запустите приложение:** Сборка и запуск приложения происходит как обычно в вашей IDE или через командную строку:
```bash
dotnet build
dotnet run
 ```

## Скрипт создания базы данных
Если у вас возникают проблемы с созданием базы данных, вы можете использовать следующий SQL скрипт для создания таблиц:
```sql
CREATE TABLE Users (
    UserID SERIAL PRIMARY KEY,
    Username VARCHAR(100) NOT NULL,
    Email VARCHAR(255) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL,
    Address VARCHAR(255),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Products (
    ProductID SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Description TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT NOT NULL,
    Category VARCHAR(100),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Orders (
    OrderID SERIAL PRIMARY KEY,
    UserID INT NOT NULL,
    TotalPrice DECIMAL(10, 2),
    OrderDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(50) DEFAULT 'pending',
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE OrderItems (
    OrderItemID SERIAL PRIMARY KEY,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10, 2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
```
## Особенности проекта

- **Entity Framework Core** используется для работы с базой данных.
- **Npgsql** используется в качестве провайдера базы данных PostgreSQL.
- Код структурирован в соответствии с принципами чистой архитектуры, что обеспечивает модульность и удобство поддержки.
