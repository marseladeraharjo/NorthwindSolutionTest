using AutoMapper;
using NorthwindWebMvc.Basic.Models;
using NorthwindWebMvc.Basic.Models.Dto;
using NorthwindWebMvc.Basic.Repository;
using NorthwindWebMvc.Basic.RepositoryContext;

namespace NorthwindWebMvc.Basic.Service
{
	public class ProductService : IProductService
	{
		private readonly IRepositoryBase<Product> _repositoryBase;
		private readonly IMapper _mapper;
		private readonly RepositoryDbContext _context;


        public ProductService(IRepositoryBase<Product> repositoryBase, IMapper mapper, RepositoryDbContext context)
        {
            _repositoryBase = repositoryBase;
            _mapper = mapper;
            _context = context;
        }

        public void Create(ProductDto entity)
		{
			var product = _mapper.Map<Product>(entity);
			_repositoryBase.Create(product);
			_repositoryBase.Save();
		}

		public void Delete(ProductDto entity)
		{
			var product = _mapper.Map<Product>(entity);
			_repositoryBase.Delete(product);
			_repositoryBase.Save();
		}

		public async Task<IEnumerable<ProductDto>> FindAll(bool trackChanges)
		{
			var products = await _repositoryBase.FindAll(false);
			var productDto = _mapper.Map<IEnumerable<ProductDto>>(products);

			return productDto;
		}

		public async Task<ProductDto> FindById(int id, bool trackChanges)
		{
			var product = await _repositoryBase.FindById(id, false);
			var productDto = _mapper.Map<ProductDto>(product);
			return productDto;
		}

		public void Update(ProductDto entity)
		{
			var product = _mapper.Map<Product>(entity);
			_repositoryBase.Update(product);
			_repositoryBase.Save();
		}
	}
}
