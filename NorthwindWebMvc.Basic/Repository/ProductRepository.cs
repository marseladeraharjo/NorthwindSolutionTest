﻿using Microsoft.EntityFrameworkCore;
using NorthwindWebMvc.Basic.Models;
using NorthwindWebMvc.Basic.RepositoryContext;

namespace NorthwindWebMvc.Basic.Repository
{
	public class ProductRepository : IRepositoryBase<Product>
	{
		private readonly RepositoryDbContext _context;

		public ProductRepository(RepositoryDbContext repositoryDbContext)
		{
			_context = repositoryDbContext;
		}

		public void Create(Product entity)
		{
			_context.Products.Add(entity);
		}

		public void Delete(Product entity)
		{
			_context.Products.Remove(entity);
		}

		public async Task<IQueryable<Product>> FindAll(bool trackChanges)
		{
			return !trackChanges ? _context.Products.AsNoTracking() : _context.Products;
		}

		public async Task<Product> FindById(int id, bool trackChanges)
		{
			return await _context.Products.FindAsync(id);
		}

		public void Save()
		{
			_context.SaveChanges();
		}

		public void Update(Product entity)
		{
			_context.Products.Update(entity);
		}
	}
}
