using AutoMapper;
using Product.App.Contract.Dto;
using Product.App.Contract.IServices;
using Product.Infrastructure.Repositories.Product;
using Product.Model.Models;

namespace Product.App.Services
{
    public class ProductService : IProductService
    {
        
        private readonly IProductRepository<Products> productRepository;
        private readonly IMapper mapper;
        public ProductService(IProductRepository<Products> productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        public int Add(ProductDto dto)
        {

            var products = MapToProduct(dto);
            return productRepository.Add(products);
        }

        public ProductDto GetProduct(int id)
        {
            var product = productRepository.GetById(id);

            return mapper.Map<ProductDto>(product);


        }

        public ProductDto GetProductForEdit(int id)
        {
            var product = productRepository.Get().First(x => x.Id == id);
            return new ()
            {
                Name = product.Name,
                ProductDate = product.ProductDate,
                ManufacturePhone = product.ManufacturePhone,
                ManufactureEmail = product.ManufactureEmail,
                IsAvailable = product.IsAvailable
            };

        }

        public List<ProductDto> GetProducts()
        {

            var products = productRepository.Get().OrderByDescending(x => x.Id).Take(10).ToList();

            return mapper.Map<List<ProductDto>>(products);

        }

        private static Products MapToProduct(ProductDto dto)
        {
            return new Products()
            {
                Name = dto.Name,
                ProductDate = dto.ProductDate,
                ManufacturePhone = dto.ManufacturePhone,
                ManufactureEmail = dto.ManufactureEmail,
                IsAvailable = dto.IsAvailable
            };

        }
    }
}
