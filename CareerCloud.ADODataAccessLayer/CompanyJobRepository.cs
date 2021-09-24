using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobRepository : Initializer, IDataRepository<CompanyJobPoco>
    {
        public CompanyJobRepository()
            : base()
        {
        }
        public void Add(params CompanyJobPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs]
                                        ([Id]
                                        ,[Company]
                                        ,[Profile_Created]
                                        ,[Is_Inactive]
                                        ,[Is_Company_Hidden])
                                    VALUES
                                        (@Id
                                        ,@Company
                                        ,@Profile_Created
                                        ,@Is_Inactive
                                        ,@Is_Company_Hidden)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobPoco> GetAll(params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            var companyJobPoco = new List<CompanyJobPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Jobs]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyJobPoco();
                temp.Id = reader.GetGuid(0);
                temp.Company = reader.GetGuid(1);
                temp.ProfileCreated = reader.GetDateTime(2);
                temp.IsInactive = reader.GetBoolean(3);
                temp.IsCompanyHidden = reader.GetBoolean(4);
                temp.TimeStamp = (byte[])reader[5];

                companyJobPoco.Add(temp);
            }

            _connection.Close();

            return companyJobPoco;
        }

        public IList<CompanyJobPoco> GetList(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobPoco GetSingle(Expression<Func<CompanyJobPoco, bool>> where, params Expression<Func<CompanyJobPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs]
                                    SET [Id] = @Id
                                        ,[Company] = @Company
                                        ,[Profile_Created] = @Profile_Created
                                        ,[Is_Inactive] = @Is_Inactive
                                        ,[Is_Company_Hidden] = @Is_Company_Hidden
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Profile_Created", item.ProfileCreated);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Is_Company_Hidden", item.IsCompanyHidden);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
