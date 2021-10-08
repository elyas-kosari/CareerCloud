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
    public class SecurityLoginsRoleService : SecurityLoginsRole.SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;
        public SecurityLoginsRoleService()
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }

        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsRolePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsRolePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SecurityLoginsRoleReply> ReadSecurityLoginsRole(SecurityLoginsRoleRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginsRoleReply>(() => new SecurityLoginsRoleReply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Role = poco.Role.ToString(),
            });
        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginsRolePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginsRolePoco[] CreateSecurityLoginsRolePocos(SecurityLoginsRoleReply reply)
        {
            return new SecurityLoginsRolePoco[]
            {
                new SecurityLoginsRolePoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Login = Guid.Parse(reply.Login),
                    Role = Guid.Parse(reply.Role)
                }
            };
        }
    }
}
