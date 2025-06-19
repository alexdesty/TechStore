namespace TechStore.Api.DTO;

public class ProductToPropertyDTO
{
    public int Id { get; set; } 

    public int ProductId { get; set; }

    public int PropertyId {  get; set; }

    public string Value { get; set; } = string.Empty;
}
