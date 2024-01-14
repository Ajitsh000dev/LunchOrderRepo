using LunchOrder.Data.Data;
using LunchOrder.Data.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Data.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            ApplicationUser = new ApplicationUserRepository(_context);
           
        }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
