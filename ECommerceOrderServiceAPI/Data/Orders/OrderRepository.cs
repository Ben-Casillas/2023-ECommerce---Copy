using Dapper;
using System.Data;

public class OrderRepository : IOrderRepository
{
    private readonly DapperContext _context;
    public OrderRepository(DapperContext context)
    {
        _context = context;
    }

//post is working
    public async Task<Guid> Create(OrderDTOCreate orderDTOCreate, Guid userGuid)
    {
        string sqlQueryOrders = "INSERT INTO Orders (OrderGuid, UserGuid, BasketGuid, CreatedDate) VALUES (@OrderGuid, @UserGuid, @BasketGuid, @CreatedDate)";
        var parametersOrders = new DynamicParameters();

        Guid orderGuid = Guid.NewGuid();
        //Guid userGuid = Guid.NewGuid(); //TODO

        parametersOrders.Add("OrderGuid", orderGuid, DbType.Guid);
        parametersOrders.Add("UserGuid", userGuid, DbType.Guid);
        parametersOrders.Add("BasketGuid", orderDTOCreate.BasketGuid, DbType.Guid);
        parametersOrders.Add("CreatedDate", DateTime.UtcNow, DbType.DateTime);

        using (var connection = _context.CreateConnection())
        {
            await connection.ExecuteAsync(sqlQueryOrders, parametersOrders);
        }

        if (orderDTOCreate.ReadBasket == true) //reads basket
        {
            Console.WriteLine("read basket");

            var client = new HttpClient();
            var basket = await client.GetFromJsonAsync<Basket>($"http://localhost:8080/basket/{orderDTOCreate.BasketGuid}"); //($"http://localhost:8082/basket/{orderDTOCreate.BasketGuid}")
            //var basket = await client.GetStringAsync($"http://localhost:8082/basket/{orderDTOCreate.BasketGuid}");
            //Console.WriteLine(basket);

            if (basket != null && basket.Items != null)
            {
                Console.WriteLine(basket);
                orderDTOCreate.Items = basket.Items;

                foreach (ItemDTOCreate item in basket.Items)
                {
                    //Console.WriteLine(item.ItemUuid);
                }
            }

        }

        string sqlQueryItems = "INSERT INTO Items (ItemUuid, OrderGuid, Title, Description, UnitPrice ) VALUES (@ItemUuid, @OrderGuid, @Title, @Description, @UnitPrice)";

        foreach (ItemDTOCreate item in orderDTOCreate.Items)
        {

            //Console.WriteLine(item.ItemGuid);
            //Console.WriteLine(item.PublishedDate);

            var parametersItems = new DynamicParameters();
            parametersItems.Add("ItemUuid", item.ItemUuid, DbType.Guid);
            parametersItems.Add("OrderGuid", orderGuid, DbType.Guid);
            parametersItems.Add("Title", item.Title, DbType.String);
            parametersItems.Add("Description", item.Description, DbType.String);
            parametersItems.Add("UnitPrice", item.unitPrice, DbType.Decimal);

            // even if they pass in the created date from the ItemService or BasketService, I'm setting a new one here, since it represents the creation of this record
            //parametersItems.Add("CreatedDate", /*item.CreatedDate == null ? */ DateTime.UtcNow /*: item.CreatedDate*/, DbType.DateTime);

            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sqlQueryItems, parametersItems);
            }
        }

        return orderGuid;
    }

    public IEnumerable<Order> GetAllWithItems()
    {
        

        string sql = "SELECT * FROM Orders AS O INNER JOIN Items AS B ON O.OrderGuid = B.OrderGuid;"; //get all items

        using (var connection = _context.CreateConnection())
        {
            var orderDictionary = new Dictionary<Guid, Order>();

            var ordersList = connection.Query<Order, Item, Order>(
                sql,
                (order, orderItem) =>
                {
                    Order orderEntry;

                    if (!orderDictionary.TryGetValue(order.OrderGuid, out orderEntry))
                    {
                        orderEntry = order;
                        orderEntry.Items = orderEntry.Items ?? new List<Item>();
                        orderDictionary.Add(orderEntry.OrderGuid, orderEntry);
                    }

                    orderEntry.Items.Add(orderItem);
                    return orderEntry;
                },
                splitOn: "ItemUuid")
            .Distinct()
            .ToList();

            Console.WriteLine("Orders Count:" + ordersList.Count);

            foreach (Order order in ordersList)
            {
                //Console.WriteLine("orderID:" + order.OrderGuid);
                //Console.WriteLine(string.Join(",", order.Items.AsList<Item>()));
            }

            return ordersList;

        }
    }
}
