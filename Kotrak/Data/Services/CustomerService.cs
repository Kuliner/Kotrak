using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kotrak.Data.Services
{
    public class CustomerService
    {
        #region Fields

        private DbModelContainer _dbContext;
        private LogManager _logger;

        #endregion Fields

        #region Constructors

        public CustomerService(DbModelContainer db, LogManager logger)
        {
            _dbContext = db;
            _logger = logger;
        }

        #endregion Constructors

        #region Methods

        public bool Add(Customer entity)
        {
            try
            {
                if (entity.Name == null)
                    throw new Exception("Name field must not be null");

                _dbContext.Customers.Add(entity);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public List<Customer> Get()
        {
            try
            {
                return _dbContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return null;
            }
        }

        public Customer Get(string name)
        {
            try
            {
                return _dbContext.Customers.FirstOrDefault(x => x.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return null;
            }
        }

        public Customer Get(int id)
        {
            try
            {
                return _dbContext.Customers.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return null;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                var found = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
                if (found == null)
                    throw new Exception($"DB:{ this.GetType().ToString()} entity with id {id} not found!");
                _dbContext.Customers.Remove(found);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public bool Remove(Customer entity)
        {
            try
            {
                _dbContext.Customers.Remove(entity);
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        public bool Update(Customer entity)
        {
            try
            {
                if (entity.Name == null)
                    throw new Exception("Name field must not be null");

                var found = _dbContext.Customers.FirstOrDefault(x => x.Id == entity.Id);
                if (found == null)
                    throw new Exception($"DB:{ this.GetType().ToString()} entity with id {entity.Id.ToString()} not found!");

                found = entity;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.StackTrace);
                return false;
            }
        }

        #endregion Methods
    }
}
