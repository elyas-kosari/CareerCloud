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
    public class ApplicantJobApplicationService : ApplicantJobApplication.ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logic;
        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantJobApplicationPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantJobApplicationPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<ApplicantJobApplicationReply> ReadApplicantJobApplication(ApplicantJobApplicationRequest request, ServerCallContext context)
        {

            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantJobApplicationReply>(() => new ApplicantJobApplicationReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
                ApplicationDate = Timestamp.FromDateTime((DateTime)poco.ApplicationDate)
            });
        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantJobApplicationPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantJobApplicationPoco[] CreateApplicantJobApplicationPocos(ApplicantJobApplicationReply reply)
        {
            return new ApplicantJobApplicationPoco[]
            {
                new ApplicantJobApplicationPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Applicant = Guid.Parse(reply.Applicant),
                    Job = Guid.Parse(reply.Job),
                    ApplicationDate = reply.ApplicationDate.ToDateTime()
                }
            };
        }
    }
}
