using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLog.SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;
        public SecurityLoginsLogService()
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogReply request, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsLogPocos(request);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogReply request, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsLogPocos(request);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SecurityLoginsLogReply> ReadSecurityLoginsLog(SecurityLoginsLogRequest request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsLogReply>(() => new SecurityLoginsLogReply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                SourceIp = poco.SourceIP,
                LogonDate = Timestamp.FromDateTime((DateTime)poco.LogonDate),
                IsSuccesful = poco.IsSuccesful
            });
        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogReply request, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsLogPocos(request);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginsLogPoco[] CreateSecurityLoginsLogPocos(SecurityLoginsLogReply request)
        {
            return new SecurityLoginsLogPoco[]
            {
                new SecurityLoginsLogPoco()
                {
                    Id = Guid.Parse(request.Id),
                    Login = Guid.Parse(request.Login),
                    SourceIP = request.SourceIp,
                    LogonDate = request.LogonDate.ToDateTime(),
                    IsSuccesful = request.IsSuccesful                    
                }
            };
        }
    }
}
