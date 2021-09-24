using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class ApplicantResumeLogic : BaseLogic<ApplicantResumePoco>
    {
        public ApplicantResumeLogic(IDataRepository<ApplicantResumePoco> repository)
            : base(repository)
        {
        }

        public override void Add(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(ApplicantResumePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(ApplicantResumePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.Resume))
                {
                    exceptions.Add(new ValidationException(113, "Applicant must have a resume. Resume cannot be empty!"));
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
