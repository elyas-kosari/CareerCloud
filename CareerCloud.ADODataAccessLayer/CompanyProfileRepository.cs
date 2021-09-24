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
    public class CompanyProfileRepository : Initializer, IDataRepository<CompanyProfilePoco>
    {
        public CompanyProfileRepository()
            : base()
        {
        }
        public void Add(params CompanyProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Profiles]
                                        ([Id]
                                        ,[Registration_Date]
                                        ,[Company_Website]
                                        ,[Contact_Phone]
                                        ,[Contact_Name]
                                        ,[Company_Logo])
                                    VALUES
                                        (@Id
                                        ,@Registration_Date
                                        ,@Company_Website
                                        ,@Contact_Phone
                                        ,@Contact_Name
                                        ,@Company_Logo)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyProfilePoco> GetAll(params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            var companyProfilePoco = new List<CompanyProfilePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Profiles]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyProfilePoco();
                temp.Id = reader.GetGuid(0);
                temp.RegistrationDate = reader.GetDateTime(1);
                temp.CompanyWebsite = reader.IsDBNull(2) ? (string)null : reader.GetString(2);
                temp.ContactPhone = reader.GetString(3);
                temp.ContactName = reader.IsDBNull(4) ? (string)null : reader.GetString(4);
                temp.CompanyLogo = reader.IsDBNull(5) ? (byte[])null : (byte[])reader[5];
                temp.TimeStamp = (byte[])reader[6];

                companyProfilePoco.Add(temp);
            }

            _connection.Close();

            return companyProfilePoco;
        }

        public IList<CompanyProfilePoco> GetList(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyProfilePoco GetSingle(Expression<Func<CompanyProfilePoco, bool>> where, params Expression<Func<CompanyProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyProfilePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Profiles]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyProfilePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Profiles]
                                    SET [Id] = @Id
                                        ,[Registration_Date] = @Registration_Date
                                        ,[Company_Website] = @Company_Website
                                        ,[Contact_Phone] = @Contact_Phone
                                        ,[Contact_Name] = @Contact_Name
                                        ,[Company_Logo] = @Company_Logo
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Registration_Date", item.RegistrationDate);
                cmd.Parameters.AddWithValue("@Company_Website", item.CompanyWebsite);
                cmd.Parameters.AddWithValue("@Contact_Phone", item.ContactPhone);
                cmd.Parameters.AddWithValue("@Contact_Name", item.ContactName);
                cmd.Parameters.AddWithValue("@Company_Logo", item.CompanyLogo);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
