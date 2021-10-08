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
    public class CompanyJobEducationService : CompanyJobEducation.CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;
        public CompanyJobEducationService()
        {
            _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobEducationPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobEducationPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyJobEducationReply> ReadCompanyJobEducation(CompanyJobEducationRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobEducationReply>(() => new CompanyJobEducationReply()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Major,
                Importance = poco.Importance
            });
        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobEducationPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobEducationPoco[] CreateCompanyJobEducationPocos(CompanyJobEducationReply reply)
        {
            return new CompanyJobEducationPoco[]
            {
                new CompanyJobEducationPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Job = Guid.Parse(reply.Job),
                    Major = reply.Major,
                    Importance = (short) reply.Importance
                }
            };
        }
    }
}
