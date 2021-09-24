using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyJobEducationLogic : BaseLogic<CompanyJobEducationPoco>
    {
        public CompanyJobEducationLogic(IDataRepository<CompanyJobEducationPoco> repository)
            : base(repository)
        {
        }

        public override void Add(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyJobEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyJobEducationPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(200, "Major name cannot be null or empty!"));
                }
                else if (poco.Major.Length < 2)
                {
                    exceptions.Add(new ValidationException(200, "Major name must be at least 2 characters long!"));
                }

                if (poco.Importance < 0)
                {
                    exceptions.Add(new ValidationException(201, "Importance cannot be negative!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
