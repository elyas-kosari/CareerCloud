using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCode.SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;
        public SystemCountryCodeService()
        {
            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodeReply reply, ServerCallContext context)
        {
            var pocos = CreateSystemCountryCodePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }


        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodeReply reply, ServerCallContext context)
        {
            var pocos = CreateSystemCountryCodePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SystemCountryCodeReply> ReadSystemCountryCode(SystemCountryCodeRequest request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Code);
            return new Task<SystemCountryCodeReply>(() => new SystemCountryCodeReply()
            {
                Code = poco.Code,
                Name = poco.Name
            });
        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodeReply reply, ServerCallContext context)
        {
            var pocos = CreateSystemCountryCodePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SystemCountryCodePoco[] CreateSystemCountryCodePocos(SystemCountryCodeReply reply)
        {
            return new SystemCountryCodePoco[]
            {
                new SystemCountryCodePoco()
                {
                    Code = reply.Code,
                    Name = reply.Name
                }
            };
        }
    }
}
