using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfile.CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;
        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<Empty> CreateCompanyProfile(CompanyProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyProfilePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyProfile(CompanyProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyProfilePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyProfileReply> ReadCompanyProfile(CompanyProfileRequest request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyProfileReply>(() => new CompanyProfileReply()
            {
                Id = poco.Id.ToString(),
                RegistrationDate = Timestamp.FromDateTime((DateTime)poco.RegistrationDate),
                CompanyWebsite = poco.CompanyWebsite,
                ContactPhone = poco.ContactPhone,
                ContactName = poco.ContactName,
                CompanyLogo = ByteString.CopyFrom(poco.CompanyLogo)
            });
        }

        public override Task<Empty> UpdateCompanyProfile(CompanyProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyProfilePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyProfilePoco[] CreateCompanyProfilePocos(CompanyProfileReply reply)
        {
            return new CompanyProfilePoco[]
            {
                new CompanyProfilePoco()
                {
                    Id = Guid.Parse(reply.Id),
                    RegistrationDate = reply.RegistrationDate.ToDateTime(),
                    CompanyWebsite = reply.CompanyWebsite,
                    ContactPhone = reply.ContactPhone,
                    ContactName = reply.ContactName,
                    CompanyLogo = reply.CompanyLogo.ToByteArray()
                }
            };
        }
    }
}
