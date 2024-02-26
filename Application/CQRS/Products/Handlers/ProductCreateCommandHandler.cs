using Application.CQRS.Products.Commands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers
{
    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductCreateCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Description = request.Description,
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            if (product == null)
            {
                throw new ApplicationException($"Error creating entity.");
            }
            else 
            { 
                   product.CategoryId = request.CategoryId;
                   return await _productRepository.InsertAsync( product );
            }
        }
    }
}
