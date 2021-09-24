using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantJobApplicationLogic : BaseLogic<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationLogic(IDataRepository<ApplicantJobApplicationPoco> repository)
            : base(repository)
        {
        }

        public override void Add(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantJobApplicationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantJobApplicationPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (poco.ApplicationDate > DateTime.Today)
                {
                    exceptions.Add(new ValidationException(110, "Application date cannot be after today!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
