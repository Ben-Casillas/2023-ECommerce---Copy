
public interface IOrderRepository{
    
    public Task<Guid> Create(OrderDTOCreate orderDTOCreate, Guid userGuid);
    public Task<IEnumerable<Order>> GetAllProducts();


    public IEnumerable<Order> GetAllWithItems();
}