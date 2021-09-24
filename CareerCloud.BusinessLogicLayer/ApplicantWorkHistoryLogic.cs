using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantWorkHistoryLogic : BaseLogic<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryLogic(IDataRepository<ApplicantWorkHistoryPoco> repository)
            : base(repository)
        {
        }

        public override void Add(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantWorkHistoryPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantWorkHistoryPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyName))
                {
                    exceptions.Add(new ValidationException(105, "Company name cannoot be empty!"));
                }
                else if (poco.CompanyName.Length <= 2)
                {
                    exceptions.Add(new ValidationException(105, "Company name must at least have 2 characters!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
