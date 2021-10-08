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
    public class ApplicantResumeService : ApplicantResume.ApplicantResumeBase
    {
        private readonly ApplicantResumeLogic _logic;
        public ApplicantResumeService()
        {
            _logic = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        public override Task<Empty> CreateApplicantResume(ApplicantResumeReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantResumePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantResume(ApplicantResumeReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantResumePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<ApplicantResumeReply> ReadApplicantResume(ApplicantResumeRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantResumeReply>(() => new ApplicantResumeReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Resume = poco.Resume,
                LastUpdated = poco.LastUpdated is null ? null : Timestamp.FromDateTime((DateTime)poco.LastUpdated)
            });
        }

        public override Task<Empty> UpdateApplicantResume(ApplicantResumeReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantResumePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantResumePoco[] CreateApplicantResumePocos(ApplicantResumeReply reply)
        {
            return new ApplicantResumePoco[]
            {
                new ApplicantResumePoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Applicant = Guid.Parse(reply.Applicant),
                    Resume = reply.Resume,
                    LastUpdated = reply.LastUpdated.ToDateTime()
                }
            };
        }
    }
}
