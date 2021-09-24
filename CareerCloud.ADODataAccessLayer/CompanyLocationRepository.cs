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
    public class CompanyLocationRepository : Initializer, IDataRepository<CompanyLocationPoco>
    {
        public CompanyLocationRepository()
            : base()
        {
        }
        public void Add(params CompanyLocationPoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Company_Locations]
                                        ([Id]
                                        ,[Company]
                                        ,[Country_Code]
                                        ,[State_Province_Code]
                                        ,[Street_Address]
                                        ,[City_Town]
                                        ,[Zip_Postal_Code])
                                    VALUES
                                        (@Id
                                        ,@Company
                                        ,@Country_Code
                                        ,@State_Province_Code
                                        ,@Street_Address
                                        ,@City_Town
                                        ,@Zip_Postal_Code)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                cmd.Parameters.AddWithValue("@City_Town", item.City);
                cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<CompanyLocationPoco> GetAll(params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            var companyLocationPoco = new List<CompanyLocationPoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Company_Locations]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new CompanyLocationPoco();
                temp.Id = reader.GetGuid(0);
                temp.Company = reader.GetGuid(1);
                temp.CountryCode = reader.GetString(2);
                temp.Province = reader.GetString(3);
                temp.Street = reader.GetString(4);
                temp.City = reader.IsDBNull(5) ? (string)null : reader.GetString(5);
                temp.PostalCode = reader.IsDBNull(6) ? (string)null : reader.GetString(6);
                temp.TimeStamp = (byte[])reader[7];

                companyLocationPoco.Add(temp);
            }

            _connection.Close();

            return companyLocationPoco;
        }

        public IList<CompanyLocationPoco> GetList(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public CompanyLocationPoco GetSingle(Expression<Func<CompanyLocationPoco, bool>> where, params Expression<Func<CompanyLocationPoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params CompanyLocationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Company_Locations]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params CompanyLocationPoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Company_Locations]
                                    SET [Id] = @Id
                                        ,[Company] = @Company
                                        ,[Country_Code] = @Country_Code
                                        ,[State_Province_Code] = @State_Province_Code
                                        ,[Street_Address] = @Street_Address
                                        ,[City_Town] = @City_Town
                                        ,[Zip_Postal_Code] = @Zip_Postal_Code
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Company", item.Company);
                cmd.Parameters.AddWithValue("@Country_Code", item.CountryCode);
                cmd.Parameters.AddWithValue("@State_Province_Code", item.Province);
                cmd.Parameters.AddWithValue("@Street_Address", item.Street);
                cmd.Parameters.AddWithValue("@City_Town", item.City);
                cmd.Parameters.AddWithValue("@Zip_Postal_Code", item.PostalCode);

                cmd.ExecuteNonQuery();
            }

            _connection.Close();
        }
    }
}
