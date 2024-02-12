using Northwind.Contract.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Service.Abstraction.Base
{
    public interface IServiceManager
    {
        IServiceEntityBase<CategoryDto> CategoryService { get; }
    }
}
