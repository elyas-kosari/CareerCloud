using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
    {
        public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository)
            : base(repository)
        {
        }

        public override void Add(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantEducationPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantEducationPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Major))
                {
                    exceptions.Add(new ValidationException(107, "Major cannot be null or empty!"));
                }
                else if (poco.Major.Length < 3)
                {
                    exceptions.Add(new ValidationException(107, "Major cannot be less than 3 characters long!"));
                }

                if (poco.StartDate > DateTime.Today)
                {
                    exceptions.Add(new ValidationException(108, "Start date cannot be in the future!"));
                }

                if (poco.CompletionDate < poco.StartDate)
                {
                    exceptions.Add(new ValidationException(109, "The end date cannot be before it starts!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
