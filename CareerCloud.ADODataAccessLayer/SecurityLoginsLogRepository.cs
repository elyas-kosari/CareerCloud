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
    public class SecurityLoginsLogRepository : Initializer, IDataRepository<SecurityLoginsLogPoco>
    {
        public SecurityLoginsLogRepository()
            : base()
        {
        }
        public void Add(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Log]
                                        ([Id]
                                        ,[Login]
                                        ,[Source_IP]
                                        ,[Logon_Date]
                                        ,[Is_Succesful])
                                    VALUES
                                        (@Id
                                        ,@Login
                                        ,@Source_IP
                                        ,@Logon_Date
                                        ,@Is_Succesful)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsLogPoco> GetAll(params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            var securityLoginsLogPoco = new List<SecurityLoginsLogPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Security_Logins_Log]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SecurityLoginsLogPoco();
                temp.Id = reader.GetGuid(0);
                temp.Login = reader.GetGuid(1);
                temp.SourceIP = reader.GetString(2);
                temp.LogonDate = reader.GetDateTime(3);
                temp.IsSuccesful = reader.GetBoolean(4);

                securityLoginsLogPoco.Add(temp);
            }

            _connection.Close();

            return securityLoginsLogPoco;
        }

        public IList<SecurityLoginsLogPoco> GetList(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsLogPoco GetSingle(Expression<Func<SecurityLoginsLogPoco, bool>> where, params Expression<Func<SecurityLoginsLogPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Log]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginsLogPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _connection;
                cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Log]
                                    SET [Id] = @Id
                                        ,[Login] = @Login
                                        ,[Source_IP] = @Source_IP
                                        ,[Logon_Date] = @Logon_Date
                                        ,[Is_Succesful] = @Is_Succesful
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Source_IP", item.SourceIP);
                cmd.Parameters.AddWithValue("@Logon_Date", item.LogonDate);
                cmd.Parameters.AddWithValue("@Is_Succesful", item.IsSuccesful);
                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
