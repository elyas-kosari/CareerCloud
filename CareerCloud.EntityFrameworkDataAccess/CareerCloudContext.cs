using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using CareerCloud.Pocos;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace CareerCloud.EntityFrameworkDataAccess
{
    public class CareerCloudContext : DbContext
    {

        public DbSet<ApplicantEducationPoco> ApplicantEducations { get; set; }
        public DbSet<ApplicantJobApplicationPoco> ApplicantJobApplications { get; set; }
        public DbSet<ApplicantProfilePoco> ApplicantProfiles { get; set; }
        public DbSet<ApplicantResumePoco> ApplicantResumes { get; set; }
        public DbSet<ApplicantSkillPoco> ApplicantSkills { get; set; }
        public DbSet<ApplicantWorkHistoryPoco> ApplicantWorkHistories { get; set; }
        public DbSet<CompanyDescriptionPoco> CompanyDescriptions { get; set; }
        public DbSet<CompanyJobDescriptionPoco> CompanyJobDescriptions { get; set; }
        public DbSet<CompanyJobEducationPoco> CompanyJobEducations { get; set; }
        public DbSet<CompanyJobPoco> CompanyJobs { get; set; }
        public DbSet<CompanyJobSkillPoco> CompanyJobSkills { get; set; }
        public DbSet<CompanyLocationPoco> CompanyLocations { get; set; }
        public DbSet<CompanyProfilePoco> CompanyProfiles { get; set; }
        public DbSet<SecurityLoginPoco> SecurityLogins { get; set; }
        public DbSet<SecurityLoginsLogPoco> SecurityLoginsLogs { get; set; }
        public DbSet<SecurityLoginsRolePoco> SecurityLoginsRoles { get; set; }
        public DbSet<SecurityRolePoco> SecurityRoles { get; set; }
        public DbSet<SystemCountryCodePoco> SystemCountryCodes { get; set; }
        public DbSet<SystemLanguageCodePoco> SystemLanguageCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            string _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;

            optionsBuilder.UseSqlServer(_connStr);

            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicantEducationPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantEducations)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantJobApplicationPoco>()
                .HasOne(c => c.CompanyJob)
                .WithMany(a => a.ApplicantJobApplications)
                .HasForeignKey(a => a.Job);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(se => se.SecurityLogin)
                .WithMany(a => a.ApplicantProfiles)
                .HasForeignKey(a => a.Login);

            modelBuilder.Entity<ApplicantProfilePoco>()
                .HasOne(sy => sy.SystemCountryCode)
                .WithMany(a => a.ApplicantProfiles)
                .HasForeignKey(a => a.Country);

            modelBuilder.Entity<ApplicantResumePoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantResumes)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantSkillPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantSkills)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(a => a.ApplicantProfile)
                .WithMany(a => a.ApplicantWorkHistorys)
                .HasForeignKey(a => a.Applicant);

            modelBuilder.Entity<ApplicantWorkHistoryPoco>()
                .HasOne(sy => sy.SystemCountryCode)
                .WithMany(a => a.ApplicantWorkHistories)
                .HasForeignKey(a => a.CountryCode);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(c => c.CompanyProfile)
                .WithMany(c => c.CompanyDescriptions)
                .HasForeignKey(c => c.Company);

            modelBuilder.Entity<CompanyDescriptionPoco>()
                .HasOne(sy => sy.SystemLanguageCode)
                .WithMany(c => c.CompanyDescriptions)
                .HasForeignKey(c => c.LanguageId);

            modelBuilder.Entity<CompanyJobDescriptionPoco>()
                .HasOne(c => c.CompanyJob)
                .WithMany(c => c.CompanyJobDescriptions)
                .HasForeignKey(c => c.Job);

            modelBuilder.Entity<CompanyJobEducationPoco>()
                .HasOne(c => c.CompanyJob)
                .WithMany(c => c.CompanyJobEducations)
                .HasForeignKey(c => c.Job);

            modelBuilder.Entity<CompanyJobPoco>()
                .HasOne(c => c.CompanyProfile)
                .WithMany(c => c.CompanyJobs)
                .HasForeignKey(c => c.Company);

            modelBuilder.Entity<CompanyJobSkillPoco>()
                .HasOne(c => c.CompanyJob)
                .WithMany(c => c.CompanyJobSkills)
                .HasForeignKey(c => c.Job);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(c => c.CompanyProfile)
                .WithMany(c => c.CompanyLocations)
                .HasForeignKey(c => c.Company);

            modelBuilder.Entity<CompanyLocationPoco>()
                .HasOne(sy => sy.SystemCountryCode)
                .WithMany(c => c.CompanyLocations)
                .HasForeignKey(c => c.CountryCode);

            modelBuilder.Entity<SecurityLoginsLogPoco>()
                .HasOne(se => se.SecurityLogin)
                .WithMany(se => se.SecurityLoginsLogs)
                .HasForeignKey(se => se.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(se => se.SecurityLogin)
                .WithMany(se => se.SecurityLoginsRoles)
                .HasForeignKey(se => se.Login);

            modelBuilder.Entity<SecurityLoginsRolePoco>()
                .HasOne(se => se.SecurityRole)
                .WithMany(se => se.SecurityLoginsRoles)
                .HasForeignKey(se => se.Role);
        }
    }
}
