using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class SystemCountryCodeLogic
    {
        private IDataRepository<SystemCountryCodePoco> _repository;
        public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository)
        {
            _repository = repository;
        }

        public void Add(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Add(pocos);
        }

        public void Delete(SystemCountryCodePoco[] pocos)
        {
            _repository.Remove(pocos);
        }

        public SystemCountryCodePoco Get(string code)
        {
            return _repository.GetSingle(c => c.Code == code);
        }

        public List<SystemCountryCodePoco> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public void Update(SystemCountryCodePoco[] pocos)
        {
            Verify(pocos);
            _repository.Update(pocos);
        }

        protected void Verify(SystemCountryCodePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Code))
                {
                    exceptions.Add(new ValidationException(900, "Country code cannot be null or empty!"));
                }

                if (string.IsNullOrEmpty(poco.Name))
                {
                    exceptions.Add(new ValidationException(901, "Country name cannot be null or empty!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
