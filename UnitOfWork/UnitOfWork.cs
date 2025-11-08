using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Repositories.Interfaces;

namespace Backend.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public IUserRepository Users { get; }

        public UnitOfWork(AppDbContext dbContext, IUserRepository users)
        {
            _dbContext = dbContext;
            Users = users;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }        
    }
}