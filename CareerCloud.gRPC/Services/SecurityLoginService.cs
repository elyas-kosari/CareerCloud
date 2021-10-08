using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLogin.SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;
        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SecurityLoginReply> ReadSecurityLogin(SecurityLoginRequest request, ServerCallContext context)
        {
            var poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<SecurityLoginReply>(() => new SecurityLoginReply()
            {
                Id = poco.Id.ToString(),
                Login = poco.Login.ToString(),
                Password = poco.Password,
                CreatedDate = Timestamp.FromDateTime((DateTime)poco.Created),
                PasswordUpdateDate = poco.PasswordUpdate is null ? null : Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                AgreementAccepted = poco.AgreementAccepted is null ? null : Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage
            });
        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginReply reply, ServerCallContext context)
        {
            var pocos = CreateSecurityLoginPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SecurityLoginPoco[] CreateSecurityLoginPocos(SecurityLoginReply reply)
        {
            return new SecurityLoginPoco[]
            {
                new SecurityLoginPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Login = reply.Login,
                    Password = reply.Password,
                    Created = reply.CreatedDate.ToDateTime(),
                    PasswordUpdate = reply.PasswordUpdateDate.ToDateTime(),
                    AgreementAccepted = reply.AgreementAccepted.ToDateTime(),
                    IsLocked = reply.IsLocked,
                    IsInactive = reply.IsInactive,
                    EmailAddress = reply.EmailAddress,
                    PhoneNumber = reply.PhoneNumber,
                    FullName = reply.FullName,
                    ForceChangePassword =reply.ForceChangePassword,
                    PrefferredLanguage = reply.PrefferredLanguage
                }
            };
        }
    }
}
