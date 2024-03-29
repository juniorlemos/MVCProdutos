﻿using Domain.Entities;
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
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context){ }
    }
}
