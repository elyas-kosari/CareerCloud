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
    public class SecurityLoginsRoleRepository : Initializer, IDataRepository<SecurityLoginsRolePoco>
    {
        public SecurityLoginsRoleRepository()
            : base()
        {
        }
        public void Add(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Security_Logins_Roles]
                                        ([Id]
                                        ,[Login]
                                        ,[Role])
                                    VALUES
                                        (@Id
                                        ,@Login
                                        ,@Role)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Role", item.Role);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityLoginsRolePoco> GetAll(params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            var securityLoginsRolePoco = new List<SecurityLoginsRolePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Security_Logins_Roles]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SecurityLoginsRolePoco();
                temp.Id = reader.GetGuid(0);
                temp.Login = reader.GetGuid(1);
                temp.Role = reader.GetGuid(2);
                temp.TimeStamp = (byte[])reader[3];

                securityLoginsRolePoco.Add(temp);
            }

            _connection.Close();

            return securityLoginsRolePoco;
        }

        public IList<SecurityLoginsRolePoco> GetList(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityLoginsRolePoco GetSingle(Expression<Func<SecurityLoginsRolePoco, bool>> where, params Expression<Func<SecurityLoginsRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Security_Logins_Roles]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityLoginsRolePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Security_Logins_Roles]
                                    SET [Id] = @Id
                                        ,[Login] = @Login
                                        ,[Role] = @Role
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Applicant", item.Login);
                cmd.Parameters.AddWithValue("@Major", item.Role);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
