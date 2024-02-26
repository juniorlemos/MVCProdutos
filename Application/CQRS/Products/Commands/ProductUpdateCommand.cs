using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Commands
{
    public class ProductUpdateCommand : ProductCommand
    {
        public int Id { get; set; }
    }
}
