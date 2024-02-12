using Northwind.Contract.Dtos;
using Northwind.Domain.Repositories;
using Northwind.Service.Abstraction.Base;
using Northwind.Service.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Service.Base
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IServiceEntityBase<CategoryDto>> _categoryService;

        public ServiceManager(IRepositoryManager repositoryManager)
        {
            _categoryService = new Lazy<IServiceEntityBase<CategoryDto>>(() => new CategoryService(repositoryManager));
        }

        public IServiceEntityBase<CategoryDto> CategoryService => _categoryService.Value;
    }
}
