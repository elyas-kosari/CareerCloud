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
    public class ApplicantWorkHistoryRepository : Initializer, IDataRepository<ApplicantWorkHistoryPoco>
    {
        public ApplicantWorkHistoryRepository()
            : base()
        {
        }
        public void Add(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Work_History]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Company_Name]
                                        ,[Country_Code]
                                        ,[Location]
                                        ,[Job_Title]
                                        ,[Job_Description]
                                        ,[Start_Month]
                                        ,[Start_Year]
                                        ,[End_Month]
                                        ,[End_Year])
                                    VALUES
                                        (@Id
                                        ,@Applicant
                                        ,@Company_Name
                                        ,@Country_Code
                                        ,@Location
                                        ,@Job_Title
                                        ,@Job_Description
                                        ,@Start_Month
                                        ,@Start_Year
                                        ,@End_Month
                                        ,@End_Year)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@Location", item.Location);
                cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantWorkHistoryPoco> GetAll(params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            var applicantWorkHistoryPoco = new List<ApplicantWorkHistoryPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Work_History]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantWorkHistoryPoco();
                temp.Id = reader.GetGuid(0);
                temp.Applicant = reader.GetGuid(1);
                temp.CompanyName = reader.GetString(2);
                temp.CountryCode = reader.GetString(3);
                temp.Location = reader.GetString(4);
                temp.JobTitle = reader.GetString(5);
                temp.JobDescription = reader.GetString(6);
                temp.StartMonth = reader.GetInt16(7);
                temp.StartYear = reader.GetInt32(8);
                temp.EndMonth = reader.GetInt16(9);
                temp.EndYear = reader.GetInt32(10);
                temp.TimeStamp = (byte[])reader[11];

                applicantWorkHistoryPoco.Add(temp);
            }

            _connection.Close();

            return applicantWorkHistoryPoco;
        }

        public IList<ApplicantWorkHistoryPoco> GetList(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantWorkHistoryPoco GetSingle(Expression<Func<ApplicantWorkHistoryPoco, bool>> where, params Expression<Func<ApplicantWorkHistoryPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Work_History]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantWorkHistoryPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Work_History]
                                    SET [Id] = @Id
                                        ,[Applicant] = @Applicant
                                        ,[Company_Name] = @Company_Name
                                        ,[Country_Code] = @Country_Code
                                        ,[Location] = @Location
                                        ,[Job_Title] = @Job_Title
                                        ,[Job_Description] = @Job_Description
                                        ,[Start_Month] = @Start_Month
                                        ,[Start_Year] = @Start_Year
                                        ,[End_Month] = @End_Month
                                        ,[End_Year] = @End_Year
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@Location", item.Location);
                cmd.Parameters.AddWithValue("@Job_Title", item.JobTitle);
                cmd.Parameters.AddWithValue("@Job_Description", item.JobDescription);
                cmd.Parameters.AddWithValue("@Start_Month", item.StartMonth);
                cmd.Parameters.AddWithValue("@Start_Year", item.StartYear);
                cmd.Parameters.AddWithValue("@End_Month", item.EndMonth);
                cmd.Parameters.AddWithValue("@End_Year", item.EndYear);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
