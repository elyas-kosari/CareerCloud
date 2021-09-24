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
    public class CompanyJobEducationRepository : Initializer, IDataRepository<CompanyJobEducationPoco>
    {
        public CompanyJobEducationRepository()
            : base()
        {   
        }
        public void Add(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Educations]
                                        ([Id]
                                        ,[Job]
                                        ,[Major]
                                        ,[Importance])
                                    VALUES
                                        (@Id
                                        ,@Job
                                        ,@Major
                                        ,@Importance)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Importance", item.Importance);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobEducationPoco> GetAll(params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            var companyJobEducationPoco = new List<CompanyJobEducationPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Job_Educations]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyJobEducationPoco();
                temp.Id = reader.GetGuid(0);
                temp.Job = reader.GetGuid(1);
                temp.Major = reader.GetString(2);
                temp.Importance = reader.GetInt16(3);
                temp.TimeStamp = (byte[])reader[4];

                companyJobEducationPoco.Add(temp);
            }

            _connection.Close();

            return companyJobEducationPoco;
        }

        public IList<CompanyJobEducationPoco> GetList(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobEducationPoco GetSingle(Expression<Func<CompanyJobEducationPoco, bool>> where, params Expression<Func<CompanyJobEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Educations]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobEducationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Job_Educations]
                                    SET [Id] = @Id
                                        ,[Job] = @Job
                                        ,[Major] = @Major
                                        ,[Importance] = @Importance
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Major", item.Major);

                cmd.Parameters.AddWithValue("@Importance", item.Importance);
                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
