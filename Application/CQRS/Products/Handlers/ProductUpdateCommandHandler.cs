using Application.CQRS.Products.Commands;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.CQRS.Products.Handlers
{
    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand, Product>
    {
        private readonly IProductRepository _productRepository;
        public ProductUpdateCommandHandler(IProductRepository productRepository)
        {
             _productRepository = productRepository ?? 
               throw new ArgumentException(nameof(productRepository));
        }
        public async Task<Product> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            
           
            if (product == null)
            {
                throw new ApplicationException($"Error creating entity.");
            }
            else
            {
                product.Description = request.Description;
                product.Name = request.Name;
                product.Price = request.Price;
                product.Stock = request.Stock;
                product.CategoryId = request.CategoryId;

                return await _productRepository.UpdateAsync(product);
            }
        }
    }
}
