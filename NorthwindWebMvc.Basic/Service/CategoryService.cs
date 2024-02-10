using AutoMapper;
using NorthwindWebMvc.Basic.Models;
using NorthwindWebMvc.Basic.Models.Dto;
using NorthwindWebMvc.Basic.Repository;

namespace NorthwindWebMvc.Basic.Service
{
    public class CategoryService : ICategoryService<CategoryDto>
    {
        private readonly IRepositoryBase<Category> _repositoryBase;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryBase<Category> repositoryBase, IMapper mapper)
        {
            _repositoryBase = repositoryBase;
            _mapper = mapper;
        }

        public void Create(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            _repositoryBase.Create(category);
            _repositoryBase.Save();
        }

        public void Delete(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            _repositoryBase.Delete(category);
            _repositoryBase.Save();
        }


        public async Task<IEnumerable<CategoryDto>> FindAll(bool trackChanges)
        {
            var categories = await _repositoryBase.FindAll(false);
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categories).ToList();
            return categoryDto;
        }



        public async Task<CategoryDto> FindById(int id, bool trackChanges)
        {
            var category = await _repositoryBase.FindById(id, false);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public void Update(CategoryDto entity)
        {
            var category = _mapper.Map<Category>(entity);
            _repositoryBase.Update(category);
            _repositoryBase.Save();
        }
    }
}
