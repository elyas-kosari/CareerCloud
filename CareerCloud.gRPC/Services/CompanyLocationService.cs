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

    public class CompanyLocationService : CompanyLocation.CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;
        public CompanyLocationService()
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyLocationPocos(reply);

            _logic.Add(pocos);

            return new Task<Empty>(() => new Empty());
        }
        public override Task<Empty> DeleteCompanyLocation(CompanyLocationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyLocationPocos(reply);

            _logic.Delete(pocos);

            return new Task<Empty>(() => new Empty());
        }

        public override Task<CompanyLocationReply> ReadCompanyLocation(CompanyLocationRequest request, ServerCallContext context)
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));
            return new Task<CompanyLocationReply>(() => new CompanyLocationReply()
            {
                Id = poco.Id.ToString(),
                Company = poco.Company.ToString(),
                CountryCode = poco.CountryCode,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode
            });
        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationReply reply, ServerCallContext context)
        {
            var pocos = CreateCompanyLocationPocos(reply);

            _logic.Update(pocos);

            return new Task<Empty>(() => new Empty());
        }

        private CompanyLocationPoco[] CreateCompanyLocationPocos(CompanyLocationReply reply)
        {
            return new CompanyLocationPoco[]
            {
                new CompanyLocationPoco()
                {
                    Id = Guid.Parse(reply.Id),
                    Company = Guid.Parse(reply.Company),
                    CountryCode = reply.CountryCode,
                    Province = reply.Province,
                    Street = reply.Street,
                    City = reply.City,
                    PostalCode = reply.PostalCode
                }
             };
        }
    }
}
