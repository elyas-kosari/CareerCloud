using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace CareerCloud.ADODataAccessLayer
{
    public class CompanyJobSkillRepository : Initializer, IDataRepository<CompanyJobSkillPoco>
    {
        public CompanyJobSkillRepository()
            : base()
        {   
        }
        public void Add(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Job_Skills]
                                        ([Id]
                                        ,[Job]
                                        ,[Skill]
                                        ,[Skill_Level]
                                        ,[Importance])
                                    VALUES
                                        (@Id
                                        ,@Job
                                        ,@Skill
                                        ,@Skill_Level
                                        ,@Importance)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                cmd.Parameters.AddWithValue("@Importance", item.Importance);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyJobSkillPoco> GetAll(params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            var companyJobSkillPoco = new List<CompanyJobSkillPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Job_Skills]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyJobSkillPoco();
                temp.Id = reader.GetGuid(0);
                temp.Job = reader.GetGuid(1);
                temp.Skill = reader.GetString(2);
                temp.SkillLevel = reader.GetString(3);
                temp.Importance = reader.GetInt32(4);
                temp.TimeStamp = (byte[])reader[5];

                companyJobSkillPoco.Add(temp);
            }

            _connection.Close();

            return companyJobSkillPoco;
        }

        public IList<CompanyJobSkillPoco> GetList(System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyJobSkillPoco GetSingle(System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, bool>> where, params System.Linq.Expressions.Expression<Func<CompanyJobSkillPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Job_Skills]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyJobSkillPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Job_Skills]
                                    SET [Id] = @Id
                                        ,[Job] = @Job
                                        ,[Skill] = @Skill
                                        ,[Skill_Level] = @Skill_Level
                                        ,[Importance] = @Importance
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Job", item.Job);
                cmd.Parameters.AddWithValue("@Skill", item.Skill);
                cmd.Parameters.AddWithValue("@Skill_Level", item.SkillLevel);
                cmd.Parameters.AddWithValue("@Importance", item.Importance);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
