
using LunchOrder.Data.Data.Repository.IRepository;
using LunchOrder.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchOrder.Data.Data.Repository
{
    public class ApplicationUserRepository:Repository<User>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
    }
}
