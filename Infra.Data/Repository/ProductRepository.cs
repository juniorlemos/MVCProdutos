using Domain.Entities;
using Domain.Interfaces.Repository;
using Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context){ }
        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category)
                .SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
