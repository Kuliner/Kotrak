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
        public DatabaseManager(CustomerService customerService)
        {
            CustomerService = customerService;
        }

        public CustomerService CustomerService { get; set; }
    }
}