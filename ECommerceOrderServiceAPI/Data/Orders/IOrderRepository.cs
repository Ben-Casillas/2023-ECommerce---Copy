
public interface IOrderRepository{
    
    public Task<Guid> Create(OrderDTOCreate orderDTOCreate, Guid userGuid);

    public IEnumerable<Order> GetAllWithItems();
}