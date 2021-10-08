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
    public class ApplicantEducationService : ApplicantEducation.ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;
        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }


        public override Task<Empty> CreateApplicantEducation(ApplicantEducationReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantEducationPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationReply reply, ServerCallContext context)
        {

            var pocos = CreateApplicantEducationPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<ApplicantEducationReply> ReadApplicantEducation(ApplicantEducationRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));

            return new Task<ApplicantEducationReply>(() => new ApplicantEducationReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Major = poco.Major,
                CertficateDiploma = poco.CertificateDiploma,
                StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate),
                CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime((DateTime)poco.CompletionDate),
                CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent
            });
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantEducationPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantEducationPoco[] CreateApplicantEducationPocos(ApplicantEducationReply reply)
        {
            return new ApplicantEducationPoco[]
            {
                new ApplicantEducationPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Applicant = Guid.Parse(reply.Applicant),
                    Major = reply.Major,
                    CertificateDiploma = reply.CertficateDiploma,
                    StartDate = reply.StartDate.ToDateTime(),
                    CompletionDate = reply.CompletionDate.ToDateTime(),
                    CompletionPercent = (byte)reply.CompletionPercent,
                }
            };
        }
    }
}
