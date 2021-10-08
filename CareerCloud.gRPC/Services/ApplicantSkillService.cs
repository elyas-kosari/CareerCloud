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
    public class ApplicantSkillService : ApplicantSkill.ApplicantSkillBase
    {
        private readonly ApplicantSkillLogic _logic;
        public ApplicantSkillService()
        {
            _logic = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        public override Task<Empty> CreateApplicantSkill(ApplicantSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantSkillPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteApplicantSkill(ApplicantSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantSkillPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<ApplicantSkillReply> ReadApplicantSkill(ApplicantSkillRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<ApplicantSkillReply>(() => new ApplicantSkillReply()
            {
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel,
                StartMonth = poco.StartMonth,
                StartYear = poco.StartYear,
                EndMonth = poco.EndMonth,
                EndYear = poco.EndMonth
            });
        }

        public override Task<Empty> UpdateApplicantSkill(ApplicantSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateApplicantSkillPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private ApplicantSkillPoco[] CreateApplicantSkillPocos(ApplicantSkillReply reply)
        {
            return new ApplicantSkillPoco[]
            {
                new ApplicantSkillPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Applicant = Guid.Parse(reply.Applicant),
                    Skill = reply.Skill,
                    SkillLevel = reply.SkillLevel,
                    StartMonth = (byte)reply.StartMonth,
                    StartYear = reply.StartYear,
                    EndMonth = (byte)reply.EndMonth,
                    EndYear = (byte)reply.EndYear
                }
            };
        }
    }
}
