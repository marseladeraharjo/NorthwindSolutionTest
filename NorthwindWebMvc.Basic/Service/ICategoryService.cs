﻿namespace NorthwindWebMvc.Basic.Service
{
    public interface ICategoryService<TEntityDto>
    {

        Task<IEnumerable<TEntityDto>> FindAll(bool trackChanges);
        Task<TEntityDto> FindById(int id, bool trackChanges);
        void Create(TEntityDto entity);
        void Update(TEntityDto entity);
        void Delete(TEntityDto entity);

    }
}
