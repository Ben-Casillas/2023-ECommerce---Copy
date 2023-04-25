using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ILogger<OrdersController> _logger;
    private readonly IOrderRepository _ordersRepository;

    public OrdersController(ILogger<OrdersController> logger, IOrderRepository ordersRepository)
    {
        _logger = logger;
        _ordersRepository = ordersRepository;
    }


    [HttpPost]
    //[Authorize]
    public async Task<IActionResult> Create(OrderDTOCreate orderDTOCreate)
    {
        String userGuid = "E8E369C0-960B-4584-9A81-F9FF9F98DBD6";

        try
        {
            //var userGuid = HttpContext.User.Claims.Where(x => x.Type == "UserGuid").FirstOrDefault()?.Value;
            //var userGuid = HttpContext.User.Claims.Where(x => x.Type == "Name").FirstOrDefault()?.Value;

            //Console.WriteLine(userGuid);

            if (String.IsNullOrEmpty(userGuid)) throw new Exception("it was null...");

            var orderGuid = await _ordersRepository.Create(orderDTOCreate, new Guid(userGuid));

            return Ok(new
            {
                Success = true,
                Message = "Order created.",
                OrderGuid = orderGuid
            });

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet]
    [Route("withitems")] //returns all order items
    public async Task<IActionResult> GetAllWithItems()
    {
        try
        {
            var Data = _ordersRepository.GetAllWithItems();
            _ordersRepository.GetAllWithItems();
            return Ok(new
            {
                Success = true,
                Message = "All Order items returned.",
                Data
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return StatusCode(500, ex.Message);
        }
    }

}