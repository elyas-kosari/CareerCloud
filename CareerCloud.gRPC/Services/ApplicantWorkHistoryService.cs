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
    public class ApplicantWorkHistoryService : ApplicantWorkHistory.ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;
        public ApplicantWorkHistoryService()
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantWorkHistoryPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantWorkHistoryPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<ApplicantWorkHistoryReply> ReadApplicantWorkHistory(ApplicantWorkHistoryRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantWorkHistoryReply>(() => new ApplicantWorkHistoryReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CompanyName = poco.CompanyName,
                CountryCode = poco.CountryCode,
                Location = poco.Location,
                JobTitle = poco.JobTitle,
                JobDescription = poco.JobDescription,
                StartMonth = poco.StartMonth,
                StartYear = poco.StartYear,
                EndMonth = poco.EndMonth,
                EndYear = poco.EndMonth
            });
        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantWorkHistoryPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantWorkHistoryPoco[] CreateApplicantWorkHistoryPocos(ApplicantWorkHistoryReply reply)
        {
            return new ApplicantWorkHistoryPoco[]
            {
                new ApplicantWorkHistoryPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Applicant = Guid.Parse(reply.Applicant),
                    CompanyName = reply.CompanyName,
                    CountryCode = reply.CountryCode,
                    Location = reply.Location,
                    JobTitle = reply.JobTitle,
                    JobDescription = reply.JobDescription,
                    StartMonth = (byte)reply.StartMonth,
                    StartYear = reply.StartYear,
                    EndMonth = (byte)reply.EndMonth,
                    EndYear = (byte)reply.EndYear
                }
            };
        }
    }
}
