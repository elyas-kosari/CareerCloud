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
    public class CompanyJobDescriptionService : CompanyJobDescription.CompanyJobDescriptionBase
    {
        private readonly CompanyJobDescriptionLogic _logic;
        public CompanyJobDescriptionService()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobDescriptionPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobDescriptionPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyJobDescriptionReply> ReadCompanyJobDescription(CompanyJobDescriptionRequest request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobDescriptionReply>(() => new CompanyJobDescriptionReply()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                JobName = poco.JobName,
                JobDescriptions = poco.JobDescriptions
            });
        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobDescriptionPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobDescriptionPoco[] CreateCompanyJobDescriptionPocos(CompanyJobDescriptionReply reply)
        {
            return new CompanyJobDescriptionPoco[]
            {
                new CompanyJobDescriptionPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Job = Guid.Parse(reply.Job),
                    JobName = reply.JobName,
                    JobDescriptions = reply.JobDescriptions
                }
            };
        }
    }
}
