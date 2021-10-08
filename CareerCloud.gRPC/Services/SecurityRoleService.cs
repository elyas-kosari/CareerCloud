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
    public class SecurityRoleService : SecurityRole.SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;
        public SecurityRoleService()
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        public override Task<Empty> CreateSecurityRole(SecurityRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityRolePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityRolePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SecurityRoleReply> ReadSecurityRole(SecurityRoleRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityRoleReply>(() => new SecurityRoleReply()
            {
                Id = poco.Id.ToString(),
                Role = poco.Role.ToString(),
                IsInactive = poco.IsInactive
            });
        }

        public override Task<Empty> UpdateSecurityRole(SecurityRoleReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityRolePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SecurityRolePoco[] CreateSecurityRolePocos(SecurityRoleReply reply)
        {
            return new SecurityRolePoco[]
            {
                new SecurityRolePoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Role = reply.Role,
                    IsInactive = reply.IsInactive
                }
            };
        }
    }
}
