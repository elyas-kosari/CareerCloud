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
    public class CompanyJobSkillService : CompanyJobSkill.CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;
        public CompanyJobSkillService()
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobSkillPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobSkillPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyJobSkillReply> ReadCompanyJobSkill(CompanyJobSkillRequest request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyJobSkillReply>(() => new CompanyJobSkillReply()
            {
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Skill = poco.Skill,
                SkillLevel = poco.SkillLevel,
                Importance = poco.Importance
            });
        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyJobSkillPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyJobSkillPoco[] CreateCompanyJobSkillPocos(CompanyJobSkillReply reply)
        {
            return new CompanyJobSkillPoco[]
            {
                new CompanyJobSkillPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Job = Guid.Parse(reply.Job),
                    Skill = reply.Skill,
                    SkillLevel = reply.SkillLevel,
                    Importance = reply.Importance
                }
            };
        }
    }
}
