using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionPronia.Application.DTOs.Products;
using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionPronia.Persistence.Implementations.Services
{
    internal class ProductService:IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IReadOnlyList<GetProductItemDto>> GetAllAsync(int page, int take)
        {
            IReadOnlyList<Product> products=await _repository.GetAll(
                page: page,
                take: take,
                includes:nameof(Product.Category)
                ).ToListAsync();

            return _mapper.Map<IReadOnlyList<GetProductItemDto>>(products);
        }
        public async Task<GetProductDto> GetByIdAsync(long id)
        {
            Product product = await _repository.GetByIdAsync(id,"ProductTags.Tag",nameof(Product.Category));

            if (product is null) throw new Exception("Entity not found");

            return _mapper.Map<GetProductDto>(product);
        }
    }
}
