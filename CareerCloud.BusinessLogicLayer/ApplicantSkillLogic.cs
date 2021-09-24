using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantSkillLogic : BaseLogic<ApplicantSkillPoco>
    {
        public ApplicantSkillLogic(IDataRepository<ApplicantSkillPoco> repository)
            : base(repository)
        {
        }

        public override void Add(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantSkillPoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantSkillPoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (poco.StartMonth > 12)
                {
                    exceptions.Add(new ValidationException(101, "Start month cannot be greater than 12!"));
                }

                if (poco.StartYear < 1900)
                {
                    exceptions.Add(new ValidationException(103, "Start year cannot be before 1900-01-01!"));
                }

                if (poco.EndMonth > 12)
                {
                    exceptions.Add(new ValidationException(102, "A year has 12 months. End month cannot be greater than 12!"));
                }

                if (poco.EndYear < poco.StartYear)
                {
                    exceptions.Add(new ValidationException(104, "End year cannot be before Start year!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
