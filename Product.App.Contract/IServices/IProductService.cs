using Product.App.Contract.Dto;

namespace Product.App.Contract.IServices
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProduct(int id);
        ProductDto GetProductForEdit(int id);
        int Add(ProductDto product);
    }
}
