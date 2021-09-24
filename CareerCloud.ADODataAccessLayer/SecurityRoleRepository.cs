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
    public class SecurityRoleRepository : Initializer, IDataRepository<SecurityRolePoco>
    {
        public SecurityRoleRepository()
            : base()
        {
        }
        public void Add(params SecurityRolePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Security_Roles]
                                        ([Id]
                                        ,[Role]
                                        ,[Is_Inactive])
                                    VALUES
                                        (@Id
                                        ,@Role
                                        ,@Is_Inactive)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Role", item.Role);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityRolePoco> GetAll(params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            var securityRolePoco = new List<SecurityRolePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Security_Roles]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SecurityRolePoco();
                temp.Id = reader.GetGuid(0);
                temp.Role = reader.GetString(1);
                temp.IsInactive = reader.GetBoolean(2);

                securityRolePoco.Add(temp);
            }

            _connection.Close();

            return securityRolePoco;
        }

        public IList<SecurityRolePoco> GetList(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SecurityRolePoco GetSingle(Expression<Func<SecurityRolePoco, bool>> where, params Expression<Func<SecurityRolePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SecurityRolePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Security_Roles]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SecurityRolePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Security_Roles]
                                    SET [Id] = @Id
                                        ,[Role] = @Role
                                        ,[Is_Inactive] = @Is_Inactive
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Role", item.Role);
                cmd.Parameters.AddWithValue("@Is_Inactive", item.IsInactive);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
