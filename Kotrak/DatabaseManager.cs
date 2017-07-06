using Kotrak.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotrak
{
    public class DatabaseManager
    {
        private DbModelContainer _dbContext;

        public DatabaseManager(DbModelContainer dbContext, CustomerService customerService)
        {
            _dbContext = dbContext;
            CustomerService = customerService;
        }

        public CustomerService CustomerService { get; set; }
    }
}