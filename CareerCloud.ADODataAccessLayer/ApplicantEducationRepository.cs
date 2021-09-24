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

    public class ApplicantEducationRepository : Initializer, IDataRepository<ApplicantEducationPoco>
    {
        public ApplicantEducationRepository()
            : base()
        {
        }
        public void Add(params ApplicantEducationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Educations]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Major]
                                        ,[Certificate_Diploma]
                                        ,[Start_Date]
                                        ,[Completion_Date]
                                        ,[Completion_Percent])
                                    VALUES
                                        (@Id
                                        ,@Applicant
                                        ,@Major
                                        ,@Certificate_Diploma
                                        ,@Start_Date
                                        ,@Completion_Date
                                        ,@Completion_Percent)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantEducationPoco> GetAll(params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            var applicantEducationPoco = new List<ApplicantEducationPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Educations]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantEducationPoco();
                temp.Id = reader.GetGuid(0);
                temp.Applicant = reader.GetGuid(1);
                temp.Major = reader.GetString(2);
                temp.CertificateDiploma = reader.GetString(3);
                temp.StartDate = reader.GetDateTime(4);
                temp.CompletionDate = reader.GetDateTime(5);
                temp.CompletionPercent = reader.GetByte(6);
                temp.TimeStamp = (byte[]) reader[7];

                applicantEducationPoco.Add(temp);
            }

            _connection.Close();

            return applicantEducationPoco;
        }

        public IList<ApplicantEducationPoco> GetList(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantEducationPoco GetSingle(Expression<Func<ApplicantEducationPoco, bool>> where, params Expression<Func<ApplicantEducationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantEducationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Educations]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantEducationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Educations]
                                    SET [Id] = @Id
                                        ,[Applicant] = @Applicant
                                        ,[Major] = @Major
                                        ,[Certificate_Diploma] = @Certificate_Diploma
                                        ,[Start_Date] = @Start_Date
                                        ,[Completion_Date] = @Completion_Date
                                        ,[Completion_Percent] = @Completion_Percent
                                    WHERE Id = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Major", item.Major);
                cmd.Parameters.AddWithValue("@Certificate_Diploma", item.CertificateDiploma);
                cmd.Parameters.AddWithValue("@Start_Date", item.StartDate);
                cmd.Parameters.AddWithValue("@Completion_Date", item.CompletionDate);
                cmd.Parameters.AddWithValue("@Completion_Percent", item.CompletionPercent);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }
    }
}
