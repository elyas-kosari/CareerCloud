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
    public class SystemLanguageCodeRepository : Initializer, IDataRepository<SystemLanguageCodePoco>
    {
        public SystemLanguageCodeRepository()
            : base()
        {
        }
        public void Add(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[System_Language_Codes]
                                        ([LanguageID]
                                        ,[Name]
                                        ,[Native_Name])
                                    VALUES
                                        (@LanguageID
                                        ,@Name
                                        ,@Native_Name)";

                cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<SystemLanguageCodePoco> GetAll(params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            var systemLanguageCodePoco = new List<SystemLanguageCodePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[System_Language_Codes]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new SystemLanguageCodePoco();
                temp.LanguageID = reader.GetString(0);
                temp.Name = reader.GetString(1);
                temp.NativeName = reader.GetString(2);

                systemLanguageCodePoco.Add(temp);
            }

            _connection.Close();

            return systemLanguageCodePoco;
        }

        public IList<SystemLanguageCodePoco> GetList(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public SystemLanguageCodePoco GetSingle(Expression<Func<SystemLanguageCodePoco, bool>> where, params Expression<Func<SystemLanguageCodePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[System_Language_Codes]
                                    WHERE LanguageID=@LanguageID";

                cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params SystemLanguageCodePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[System_Language_Codes]
                                    SET  [Name] = @Name
                                        ,[Native_Name] = @Native_Name
                                    WHERE [LanguageID] = @LanguageID";

                cmd.Parameters.AddWithValue("@LanguageID", item.LanguageID);
                cmd.Parameters.AddWithValue("@Name", item.Name);
                cmd.Parameters.AddWithValue("@Native_Name", item.NativeName);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
