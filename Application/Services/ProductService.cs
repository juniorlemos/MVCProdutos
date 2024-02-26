using Application.CQRS.Products.Commands;
using Application.CQRS.Products.Queries;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public ProductService(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productQuery = new GetProductsQuery();
            
            if(productQuery == null)
            {
                throw new Exception($"Entity could not be loaded");      
            }
            var result = await _mediator.Send(productQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var productIdByQuery = new GetProductByIdQuery(id);

            if (productIdByQuery == null)
            {
                throw new Exception($"Entity could not be loaded");
            }
            var result = await _mediator.Send(productIdByQuery);
            return _mapper.Map<ProductDTO>(result);
        }
        public async Task InsertAsync(ProductDTO productDTO)
        {
            var productCreateCommand = _mapper.Map<ProductCreateCommand>(productDTO);
            await _mediator.Send(productCreateCommand);
        }

        public async Task UpdateAsync(ProductDTO productDTO)
        {
            var productUpdateCommand = _mapper.Map<ProductUpdateCommand>(productDTO);
            await _mediator.Send(productUpdateCommand);
        }

        public async Task DeleteAsync(int id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id);
            if(productRemoveCommand == null)
            {
                throw new Exception($"Entity could not be loaded");
             }

            await _mediator.Send(productRemoveCommand);
        }
    }
}
