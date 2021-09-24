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
    public class SecurityLoginRepository : Initializer, IDataRepository<SecurityLoginPoco>
    {
        public SecurityLoginRepository()
            : base()
        {
        }
        public void Add(params SecurityLoginPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins]
                                        ([Id]
                                        ,[Login]
                                        ,[Password]
                                        ,[Created_Date]
                                        ,[Password_Update_Date]
                                        ,[Agreement_Accepted_Date]
                                        ,[Is_Locked]
                                        ,[Is_Inactive]
                                        ,[Email_Address]
                                        ,[Phone_Number]
                                        ,[Full_Name]
                                        ,[Force_Change_Password]
                                        ,[Prefferred_Language])
                                    VALUES
                                        (@Id
                                        ,@Login
                                        ,@Password
                                        ,@Created_Date
                                        ,@Password_Update_Date
                                        ,@Agreement_Accepted_Date
                                        ,@Is_Locked
                                        ,@Is_Inactive
                                        ,@Email_Address
                                        ,@Phone_Number
                                        ,@Full_Name
                                        ,@Force_Change_Password
                                        ,@Prefferred_Language)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Password", item.Password);
                cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginPoco> GetAll(params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            var securityLoginPoco = new List<SecurityLoginPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Security_Logins]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SecurityLoginPoco();
                temp.Id = reader.GetGuid(0);
                temp.Login = reader.GetString(1);
                temp.Password = reader.GetString(2);
                temp.Created = reader.GetDateTime(3);
                temp.PasswordUpdate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                temp.AgreementAccepted = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                temp.IsLocked = reader.GetBoolean(6);
                temp.IsInactive = reader.GetBoolean(7);
                temp.EmailAddress = reader.GetString(8);
                temp.PhoneNumber = reader.IsDBNull(9) ? (string)null : reader.GetString(9);
                temp.FullName = reader.GetString(10);
                temp.ForceChangePassword = reader.GetBoolean(11);
                temp.PrefferredLanguage = reader.IsDBNull(12) ? (string)null : reader.GetString(12);
                temp.TimeStamp = (byte[])reader[13];

                securityLoginPoco.Add(temp);
            }

            _connection.Close();

            return securityLoginPoco;
        }

        public IList<SecurityLoginPoco> GetList(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginPoco GetSingle(Expression<Func<SecurityLoginPoco, bool>> where, params Expression<Func<SecurityLoginPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Security_Logins]
                                    SET [Id] = @Id
                                        ,[Login] = @Login
                                        ,[Password] = @Password
                                        ,[Created_Date] = @Created_Date
                                        ,[Password_Update_Date] = @Password_Update_Date
                                        ,[Agreement_Accepted_Date] = @Agreement_Accepted_Date
                                        ,[Is_Locked] = @Is_Locked
                                        ,[Is_Inactive] = @Is_Inactive
                                        ,[Email_Address] = @Email_Address
                                        ,[Phone_Number] = @Phone_Number
                                        ,[Full_Name] = @Full_Name
                                        ,[Force_Change_Password] = @Force_Change_Password
                                        ,[Prefferred_Language] = @Prefferred_Language
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Password", item.Password);
                cmd.Parameters.AddWithValue("@Created_Date", item.Created);
                cmd.Parameters.AddWithValue("@Password_Update_Date", item.PasswordUpdate);
                cmd.Parameters.AddWithValue("@Agreement_Accepted_Date", item.AgreementAccepted);
                cmd.Parameters.AddWithValue("@Is_Locked", item.IsLocked);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);
                cmd.Parameters.AddWithValue("@Email_Address", item.EmailAddress);
                cmd.Parameters.AddWithValue("@Phone_Number", item.PhoneNumber);
                cmd.Parameters.AddWithValue("@Full_Name", item.FullName);
                cmd.Parameters.AddWithValue("@Force_Change_Password", item.ForceChangePassword);
                cmd.Parameters.AddWithValue("@Prefferred_Language", item.PrefferredLanguage);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
