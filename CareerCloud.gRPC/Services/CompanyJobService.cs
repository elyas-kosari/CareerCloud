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
    public class CompanyJobService : CompanyJob.CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;
        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyJob(CompanyJobReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyJobReply> ReadCompanyJob(CompanyJobRequest request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobReply>(() => new CompanyJobReply()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                ProfileCreated = Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                IsInactive = poco.IsInactive,
                IsCompanyHidden = poco.IsCompanyHidden
            });
        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobPoco[] CreateCompanyJobPocos(CompanyJobReply reply)
        {
            return new CompanyJobPoco[]
            {
                new CompanyJobPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Company = Guid.Parse(reply.Company),
                    ProfileCreated = reply.ProfileCreated.ToDateTime(),
                    IsInactive = reply.IsInactive,
                    IsCompanyHidden = reply.IsCompanyHidden
                }
            };
        }
    }
}
