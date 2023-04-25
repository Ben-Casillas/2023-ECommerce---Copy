using System.ComponentModel.DataAnnotations;

public class ItemDTOCreate
{
    [Required]
    public Guid ItemUuid { get; set; }

    [Required]
    public String Title { get; set; }

    [Required]
    public String Description { get; set; }

    [Required]
    public double unitPrice { get; set; }

}