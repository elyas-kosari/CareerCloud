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
    public class ApplicantProfileService : ApplicantProfile.ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;
        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantProfilePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantProfilePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<ApplicantProfileReply> ReadApplicantProfile(ApplicantProfileRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantProfileReply>(() => new ApplicantProfileReply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                CurrentSalary = (double)poco.CurrentSalary,
                CurrentRate = (double)poco.CurrentRate,
                Currency = poco.Currency,
                Country = poco.Country,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode
            });
        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfileReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantProfilePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantProfilePoco[] CreateApplicantProfilePocos(ApplicantProfileReply reply)
        {
            return new ApplicantProfilePoco[]
            {
                new ApplicantProfilePoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Login = Guid.Parse(reply.Login),
                    CurrentSalary = (decimal)reply.CurrentSalary,
                    CurrentRate = (decimal)reply.CurrentRate,
                    Currency = reply.Currency,
                    Country = reply.Country,
                    Province = reply.Province,
                    Street = reply.Street,
                    City = reply.City,
                    PostalCode = reply.PostalCode
                }
            };
        }
    }
}
