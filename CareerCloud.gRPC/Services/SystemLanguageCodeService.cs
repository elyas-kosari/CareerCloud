using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System.Threading.Tasks;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCode.SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;
        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodeReply reply, ServerCallContext context)
        {

            var pocos = CreateSystemLanguageCodePocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodeReply reply, ServerCallContext context)
        {
            var pocos = CreateSystemLanguageCodePocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<SystemLanguageCodeReply> ReadSystemLanguageCode(SystemLanguageCodeRequest request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.LanguageId);
            return new Task<SystemLanguageCodeReply>(() => new SystemLanguageCodeReply()
            {
                LanguageId = poco.LanguageID.ToString(),
                Name = poco.Name,
                NativeName = poco.NativeName
            });
        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodeReply reply, ServerCallContext context)
        {
            var pocos = CreateSystemLanguageCodePocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private SystemLanguageCodePoco[] CreateSystemLanguageCodePocos(SystemLanguageCodeReply reply)
        {
            return new SystemLanguageCodePoco[]
            {
                new SystemLanguageCodePoco()
                {
                    LanguageID = reply.LanguageId,
                    Name = reply.Name,
                    NativeName = reply.NativeName
                }
            };
        }
    }
}
