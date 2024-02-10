using NorthwindWebMvc.Basic.Models.Dto;

namespace NorthwindWebMvc.Basic.Service
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> FindAll(bool trackChanges);
		Task<ProductDto> FindById(int id, bool trackChanges);
		void Create(ProductDto entity);
		void Update(ProductDto entity);
		void Delete(ProductDto entity);
	}
}
