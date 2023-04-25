using System;
using System.ComponentModel.DataAnnotations;

public class OrderDTOCreate
{
    [Required]
    public Guid BasketGuid { get; set; }

    [Required]
    public Boolean ReadBasket {get; set;}
    
    //[Required]
    public List<ItemDTOCreate>? Items { get; set; }
}