using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository)
            : base(repository)
        {
        }

        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }

        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            var exceptions = new List<ValidationException>();
            var websiteExtensions = new string[] { ".ca", ".com", ".biz" };

            foreach (var poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "Company website domain must be '.ca', '.com' or '.biz'"));
                }
                else if (!poco.CompanyWebsite.Contains(websiteExtensions[0]) &&
                        !poco.CompanyWebsite.Contains(websiteExtensions[1]) &&
                        !poco.CompanyWebsite.Contains(websiteExtensions[2]))
                {
                    exceptions.Add(new ValidationException(600, "Company website domain must be '.ca', '.com' or '.biz'"));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Phone number is not in correct format like 416-555-1234!"));
                }
                else
                {
                    var phoneNumberParts = poco.ContactPhone.Split('-');
                    if (phoneNumberParts.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, "Phone number is not in correct format like 416-555-1234!"));
                    }
                    else if (phoneNumberParts[0].Length != 3 ||
                             phoneNumberParts[1].Length != 3 ||
                             phoneNumberParts[2].Length != 4)
                    {
                        exceptions.Add(new ValidationException(601, "Phone number is not in correct format like 416-555-1234!"));
                    }
                }
            }

            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }
}
