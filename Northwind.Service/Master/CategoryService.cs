using Mapster;
using Northwind.Contract.Dtos;
using Northwind.Domain.Repositories;
using Northwind.Service.Abstraction.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Domain.Exceptions;
using Northwind.Domain.Entities.Master;

namespace Northwind.Service.Master
{
    public class CategoryService : IServiceEntityBase<CategoryDto>
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<CategoryDto> CreateAsync(CategoryDto entity)
        {
            var category = entity.Adapt<Category>();
            _repositoryManager.CategoryRepository.CreateEntity(category);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();

            return category.Adapt<CategoryDto>();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repositoryManager.CategoryRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }

            _repositoryManager.CategoryRepository.DeleteEntity(category);
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync(bool trackChanges)
        {
            var categories = await _repositoryManager.CategoryRepository.GetAllEntity(false);
            var categoryDtos = categories.Adapt<IEnumerable<CategoryDto>>();
            return categoryDtos;
        }

        public async Task<CategoryDto> GetByIdAsync(int id, bool trackChanges)
        {
            var category = await _repositoryManager.CategoryRepository.GetEntityById(id, false);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }
            var categoryDto = category.Adapt<CategoryDto>();
            return categoryDto;
        }

        public async Task UpdateAsync(int id, CategoryDto entity)
        {
            var category = await _repositoryManager.CategoryRepository.GetEntityById(id, true);
            if (category == null)
            {
                throw new EntityNotFoundException(id);
            }

            category.CategoryName = entity.CategoryName;
            category.Description = entity.Description;
            await _repositoryManager.UnitOfWork.SaveChangesAsync();
        }
    }
}
