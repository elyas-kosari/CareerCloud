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
    public class ApplicantResumeRepository : Initializer, IDataRepository<ApplicantResumePoco>
    {
        public ApplicantResumeRepository()
            : base()
        {
        }
        public void Add(params ApplicantResumePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Resumes]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Resume]
                                        ,[Last_Updated])
                                    VALUES
                                        (@Id
                                        ,@Applicant
                                        ,@Resume
                                        ,@Last_Updated)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantResumePoco> GetAll(params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            var applicantResumePoco = new List<ApplicantResumePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Resumes]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantResumePoco();
                temp.Id = reader.GetGuid(0);
                temp.Applicant = reader.GetGuid(1);
                temp.Resume = reader.GetString(2);
                temp.LastUpdated = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);

                applicantResumePoco.Add(temp);
            }

            _connection.Close();

            return applicantResumePoco;
        }

        public IList<ApplicantResumePoco> GetList(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantResumePoco GetSingle(Expression<Func<ApplicantResumePoco, bool>> where, params Expression<Func<ApplicantResumePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantResumePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Resumes]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantResumePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Resumes]
                                    SET  [Applicant] = @Applicant
                                        ,[Resume] = @Resume
                                        ,[Last_Updated] = @Last_Updated
                                    WHERE[Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Resume", item.Resume);
                cmd.Parameters.AddWithValue("@Last_Updated", item.LastUpdated);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
