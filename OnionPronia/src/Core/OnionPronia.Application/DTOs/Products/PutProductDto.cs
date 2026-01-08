using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Application.DTOs.Products
{
    public record PutProductDto(
        string Name,
        decimal Price,
        string SKU,
        string Description,
        long CategoryId,
        ICollection<long> TagIds //1 2 3           1 2 4 5
        );
   
}
