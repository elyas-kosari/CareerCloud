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
    public class CompanyDescriptionRepository : Initializer, IDataRepository<CompanyDescriptionPoco>
    {
        public CompanyDescriptionRepository()
            : base()
        {
        }
        public void Add(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Descriptions]
                                        ([Id]
                                        ,[Company]
                                        ,[LanguageID]
                                        ,[Company_Name]
                                        ,[Company_Description])
                                    VALUES
                                        (@Id
                                        ,@Company
                                        ,@LanguageID
                                        ,@Company_Name
                                        ,@Company_Description)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyDescriptionPoco> GetAll(params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            var companyDescriptionPoco = new List<CompanyDescriptionPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Descriptions]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyDescriptionPoco();
                temp.Id = reader.GetGuid(0);
                temp.Company = reader.GetGuid(1);
                temp.LanguageId = reader.GetString(2);
                temp.CompanyName = reader.GetString(3);
                temp.CompanyDescription = reader.GetString(4);
                temp.TimeStamp = (byte[])reader[5];

                companyDescriptionPoco.Add(temp);
            }

            _connection.Close();

            return companyDescriptionPoco;
        }

        public IList<CompanyDescriptionPoco> GetList(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyDescriptionPoco GetSingle(Expression<Func<CompanyDescriptionPoco, bool>> where, params Expression<Func<CompanyDescriptionPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Descriptions]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyDescriptionPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Descriptions]
                                    SET [Id] = @Id
                                        ,[Company] = @Company
                                        ,[LanguageID] = @LanguageID
                                        ,[Company_Name] = @Company_Name
                                        ,[Company_Description] = @Company_Description
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@LanguageID", item.LanguageId);
                cmd.Parameters.AddWithValue("@Company_Name", item.CompanyName);
                cmd.Parameters.AddWithValue("@Company_Description", item.CompanyDescription);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
