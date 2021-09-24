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
    public class ApplicantSkillRepository : Initializer, IDataRepository<ApplicantSkillPoco>
    {
        public ApplicantSkillRepository()
            : base()
        {
        }
        public void Add(params ApplicantSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Skills]
                                        ([Id]
                                        ,[Applicant]
                                        ,[Skill]
                                        ,[Skill_Level]
                                        ,[Start_Month]
                                        ,[Start_Year]
                                        ,[End_Month]
                                        ,[End_Year])
                                    VALUES
                                        (@Id
                                        ,@Applicant
                                        ,@Skill
                                        ,@Skill_Level
                                        ,@Start_Month
                                        ,@Start_Year
                                        ,@End_Month
                                        ,@End_Year)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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

        public IList<ApplicantSkillPoco> GetAll(params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            var applicantSkillPoco = new List<ApplicantSkillPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Skills]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantSkillPoco();
                temp.Id = reader.GetGuid(0);
                temp.Applicant = reader.GetGuid(1);
                temp.Skill = reader.GetString(2);
                temp.SkillLevel = reader.GetString(3);
                temp.StartMonth = reader.GetByte(4);
                temp.StartYear = reader.GetInt32(5);
                temp.EndMonth = reader.GetByte(6);
                temp.EndYear = reader.GetInt32(7);
                temp.TimeStamp = (byte[])reader[8];

                applicantSkillPoco.Add(temp);
            }

            _connection.Close();

            return applicantSkillPoco;
        }

        public IList<ApplicantSkillPoco> GetList(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantSkillPoco GetSingle(Expression<Func<ApplicantSkillPoco, bool>> where, params Expression<Func<ApplicantSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantSkillPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Skills]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantSkillPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Skills]
                                    SET [Id] = @Id
                                        ,[Applicant] = @Applicant
                                        ,[Skill] = @Skill
                                        ,[Skill_Level] = @Skill_Level
                                        ,[Start_Month] = @Start_Month
                                        ,[Start_Year] = @Start_Year
                                        ,[End_Month] = @End_Month
                                        ,[End_Year] = @End_Year
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Applicant);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
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
