namespace Aplication.DTOs.Product
{
    public class ProductUpdateDTO : ProductBaseDTO
    {
        public int ProductId { get; set; }
        public int CategoriesId { get; set; }

    }
}
