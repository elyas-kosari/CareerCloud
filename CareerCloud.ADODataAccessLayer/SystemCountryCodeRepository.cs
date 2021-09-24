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
    public class SystemCountryCodeRepository : Initializer, IDataRepository<SystemCountryCodePoco>
    {
        public SystemCountryCodeRepository()
            : base()
        {
        }
        public void Add(params SystemCountryCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[System_Country_Codes]
                                        ([Code]
                                        ,[Name])
                                    VALUES
                                        (@Code
                                        ,@Name)";

                cmd.Parameters.AddWithValue("@Code", item.Code);
                cmd.Parameters.AddWithValue("@Name", item.Name);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemCountryCodePoco> GetAll(params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            var systemCountryCodePoco = new List<SystemCountryCodePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[System_Country_Codes]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SystemCountryCodePoco();
                temp.Code = reader.GetString(0);
                temp.Name = reader.GetString(1);

                systemCountryCodePoco.Add(temp);
            }

            _connection.Close();

            return systemCountryCodePoco;
        }

        public IList<SystemCountryCodePoco> GetList(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemCountryCodePoco GetSingle(Expression<Func<SystemCountryCodePoco, bool>> where, params Expression<Func<SystemCountryCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SystemCountryCodePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[System_Country_Codes]
                                    WHERE Code=@Code";

                cmd.Parameters.AddWithValue("@Code", item.Code);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SystemCountryCodePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[System_Country_Codes]
                                    SET [Code] = @Code
                                        ,[Name] = @Name
                                    WHERE [Code] = @Code";

                cmd.Parameters.AddWithValue("@Code", item.Code);
                cmd.Parameters.AddWithValue("@Name", item.Name);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
