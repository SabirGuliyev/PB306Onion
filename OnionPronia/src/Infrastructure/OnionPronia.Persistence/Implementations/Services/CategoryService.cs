using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionPronia.Application.DTOs;
using OnionPronia.Application.Interfaces.Repositories;
using OnionPronia.Application.Interfaces.Services;
using OnionPronia.Domain.Entities;


namespace OnionPronia.Persistence.Implementations.Services
{
    internal class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<GetCategoryItemDto>> GetAllAsync(int page, int take)
        {
            var categories = await _repository
                .GetAll(
                  sort: c => c.Name,
                  page: page,
                  take: take,
                  includes: nameof(Category.Products)
                  ).ToListAsync();


            return _mapper.Map<IReadOnlyList<GetCategoryItemDto>>(categories);


            //return await _repository
            //.GetAll(
            //  sort: c => c.Name,
            //  page: page,
            //  take: take
            //  ).Select(c => new GetCategoryItemDto(c.Id, c.Name, c.Products.Count)).ToListAsync();

        }


        public async Task<GetCategoryDto> GetByIdAsync(int? id)
        {
            Category? category = await _repository.GetByIdAsync(id.Value, nameof(Category.Products));


            if (category == null) throw new Exception("Category not found");

            return _mapper.Map<GetCategoryDto>(category);

                //new GetCategoryDto(
                //    category.Id,
                //    category.Name,
                //    ProductDtos: category.Products.Select(p => new GetProductInCategoryDto(p.Id, p.Name, p.Price))
                //);
        }

        public async Task CreateAsync(PostCategoryDto categoryDto)
        {

            bool result = await _repository.AnyAsync(c=>c.Name==categoryDto.Name);
            if (result)
                throw new Exception($"Category named:{categoryDto.Name} already exists");


            Category category = _mapper.Map<Category>(categoryDto);

            //Category category = new()
            //{
            //    Name = categoryDto.Name,
            //    CreatedAt=DateTime.Now
            //};
            _repository.Add(category);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, PutCategoryDto categoryDto)
        {
            bool result = await _repository.AnyAsync(c => c.Name == categoryDto.Name && c.Id!=id);
            if (result)
                throw new Exception($"Category named:{categoryDto.Name} already exists");

            Category? existed = await _repository.GetByIdAsync(id);

            if (existed is null) throw new Exception("Category not found");

            existed = _mapper.Map(categoryDto, existed); //Var olani deyishdirir

            //existed = _mapper.Map<Category>(categoryDto); Yenisi yaradir


          

            _repository.Update(existed);
            await _repository.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            Category? existed = await _repository.GetByIdAsync(id);

            if (existed is null) throw new Exception("Category not found");

            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
        }
        public async Task SoftDeleteAsync(int id)
        {
            Category? existed = await _repository.GetByIdAsync(id);

            if (existed is null) throw new Exception("Category not found");

            existed.IsDeleted = true;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
        }

    }
}
