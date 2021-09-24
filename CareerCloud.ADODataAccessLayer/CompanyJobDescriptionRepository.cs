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
    public class CompanyJobDescriptionRepository : Initializer, IDataRepository<CompanyJobDescriptionPoco>
    {
        public CompanyJobDescriptionRepository()
            : base()
        {
        }
        public void Add(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Jobs_Descriptions]
                                        ([Id]
                                        ,[Job]
                                        ,[Job_Name]
                                        ,[Job_Descriptions])
                                    VALUES
                                        (@Id
                                        ,@Job
                                        ,@Job_Name
                                        ,@Job_Descriptions)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobDescriptionPoco> GetAll(params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            var companyJobDescriptionPoco = new List<CompanyJobDescriptionPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Jobs_Descriptions]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyJobDescriptionPoco();
                temp.Id = reader.GetGuid(0);
                temp.Job = reader.GetGuid(1);
                temp.JobName = reader.GetString(2);
                temp.JobDescriptions = reader.GetString(3);
                temp.TimeStamp = (byte[])reader[4];

                companyJobDescriptionPoco.Add(temp);
            }

            _connection.Close();

            return companyJobDescriptionPoco;
        }

        public IList<CompanyJobDescriptionPoco> GetList(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobDescriptionPoco GetSingle(Expression<Func<CompanyJobDescriptionPoco, bool>> where, params Expression<Func<CompanyJobDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Jobs_Descriptions]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobDescriptionPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Jobs_Descriptions]
                                    SET [Id] = @Id
                                        ,[Job] = @Job
                                        ,[Job_Name] = @Job_Name
                                        ,[Job_Descriptions] = @Job_Descriptions
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Job_Name", item.JobName);
                cmd.Parameters.AddWithValue("@Job_Descriptions", item.JobDescriptions);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
