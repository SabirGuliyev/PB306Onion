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
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repository,
            ICategoryRepository categoryRepository,
            ITagRepository tagRepository,
            IMapper mapper)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
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

        public async Task CreateProductAsync(PostProductDto productDto)
        {


            bool result = await _repository.AnyAsync(p =>p.Name==productDto.Name);
            if (result)
            {
                throw new Exception("Entity already exists");
            }

            bool categoryResult = await _categoryRepository.AnyAsync(c => c.Id == productDto.CategoryId);

            if (!categoryResult)
            {
                throw new Exception("Category does not exist");
            }

   
            var tags=await _tagRepository.GetAll(t=>productDto.TagIds.Contains(t.Id)).ToListAsync();

            if(tags.Count!=productDto.TagIds.Count())
            {
                throw new Exception("Tag does not exist");
            }


            Product product = _mapper.Map<Product>(productDto);     

            _repository.Add(product);

            await _repository.SaveChangesAsync();
        }
    }
}
