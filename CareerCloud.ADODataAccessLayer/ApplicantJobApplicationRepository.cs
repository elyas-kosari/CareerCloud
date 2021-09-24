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
    public class ApplicantJobApplicationRepository : Initializer, IDataRepository<ApplicantJobApplicationPoco>
    {
        public ApplicantJobApplicationRepository()
            : base()
        {
        }
        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Job]
                                        ,[Application_Date])
                                    VALUES
                                        (@Id
                                        ,@Applicant
                                        ,@Job
                                        ,@Application_Date)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            var applicantJobApplicationPoco = new List<ApplicantJobApplicationPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Job_Applications]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantJobApplicationPoco();
                temp.Id = reader.GetGuid(0);
                temp.Applicant = reader.GetGuid(1);
                temp.Job = reader.GetGuid(2);
                temp.ApplicationDate = reader.GetDateTime(3);
                temp.TimeStamp = (byte[]) reader[4];

                applicantJobApplicationPoco.Add(temp);
            }

            _connection.Close();

            return applicantJobApplicationPoco;
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                    SET  [Applicant] = @Applicant
                                        ,[Job] = @Job
                                        ,[Application_Date] = @Application_Date
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Application_Date", item.ApplicationDate);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
