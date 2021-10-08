using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescription.CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;
        public CompanyDescriptionService()
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyDescriptionPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyDescriptionPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyDescriptionReply> ReadCompanyDescription(CompanyDescriptionRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyDescriptionReply>(() => new CompanyDescriptionReply()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                LanguageId = poco.LanguageId.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription
            });
        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyDescriptionPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyDescriptionPoco[] CreateCompanyDescriptionPocos(CompanyDescriptionReply reply)
        {
            return new CompanyDescriptionPoco[]
            {
                new CompanyDescriptionPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Company =  Guid.Parse(reply.Company),
                    LanguageId =  reply.LanguageId,
                    CompanyName = reply.CompanyName,
                    CompanyDescription = reply.CompanyDescription
                }
            };
        }
    }
}
