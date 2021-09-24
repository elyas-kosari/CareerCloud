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
    public class ApplicantProfileRepository : Initializer, IDataRepository<ApplicantProfilePoco>
    {
        public ApplicantProfileRepository()
        {
        }
        public void Add(params ApplicantProfilePoco[] items)
        {
            _connection.Open();
            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"INSERT INTO [dbo].[Applicant_Profiles]
                                        ([Id]
                                        ,[Login]
                                        ,[Current_Salary]
                                        ,[Current_Rate]
                                        ,[Currency]
                                        ,[Country_Code]
                                        ,[State_Province_Code]
                                        ,[Street_Address]
                                        ,[City_Town]
                                        ,[Zip_Postal_Code])
                                    VALUES
                                        (@Id
                                        ,@Login
                                        ,@Current_Salary
                                        ,@Current_Rate
                                        ,@Currency
                                        ,@Country_Code
                                        ,@State_Province_Code
                                        ,@Street_Address
                                        ,@City_Town
                                        ,@Zip_Postal_Code)";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", item.Currency);
                cmd.Parameters.AddWithValue("@Country_Code", item.Country);
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

        public IList<ApplicantProfilePoco> GetAll(params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            var applicantProfilePoco = new List<ApplicantProfilePoco>();
            _connection.Open();

            var cmd = new SqlCommand();
            cmd.Connection = _connection;

            cmd.CommandText = @"SELECT *
                                FROM [dbo].[Applicant_Profiles]";

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var temp = new ApplicantProfilePoco();
                temp.Id = reader.GetGuid(0);
                temp.Login= reader.GetGuid(1);
                temp.CurrentSalary = reader.GetDecimal(2);
                temp.CurrentRate = reader.GetDecimal(3);
                temp.Currency = reader.GetString(4);
                temp.Country = reader.GetString(5);
                temp.Province = reader.GetString(6);
                temp.Street = reader.GetString(7);
                temp.City = reader.GetString(8);
                temp.PostalCode = reader.GetString(9);
                temp.TimeStamp = (byte[])reader[10];

                applicantProfilePoco.Add(temp);
            }

            _connection.Close();

            return applicantProfilePoco;
        }

        public IList<ApplicantProfilePoco> GetList(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantProfilePoco GetSingle(Expression<Func<ApplicantProfilePoco, bool>> where, params Expression<Func<ApplicantProfilePoco, object>>[] navigationProperties)
        {
            return GetAll().AsQueryable().Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantProfilePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Profiles]
                                    WHERE Id=@Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);

                cmd.ExecuteNonQuery();
            }
            _connection.Close();
        }

        public void Update(params ApplicantProfilePoco[] items)
        {
            _connection.Open();

            foreach (var item in items)
            {
                var cmd = new SqlCommand();
                cmd.Connection = _connection;

                cmd.CommandText = @"UPDATE [dbo].[Applicant_Profiles]
                                    SET  [Login] = @Login
                                        ,[Current_Salary] = @Current_Salary
                                        ,[Current_Rate] = @Current_Rate
                                        ,[Currency] = @Currency
                                        ,[Country_Code] = @Country_Code
                                        ,[State_Province_Code] = @State_Province_Code
                                        ,[Street_Address] = @Street_Address
                                        ,[City_Town] = @City_Town
                                        ,[Zip_Postal_Code] = @Zip_Postal_Code
                                    WHERE [Id] = @Id";

                cmd.Parameters.AddWithValue("@Id", item.Id);
                cmd.Parameters.AddWithValue("@Login", item.Login);
                cmd.Parameters.AddWithValue("@Current_Salary", item.CurrentSalary);
                cmd.Parameters.AddWithValue("@Current_Rate", item.CurrentRate);
                cmd.Parameters.AddWithValue("@Currency", item.Currency);
                cmd.Parameters.AddWithValue("@Country_Code", item.Country);
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
