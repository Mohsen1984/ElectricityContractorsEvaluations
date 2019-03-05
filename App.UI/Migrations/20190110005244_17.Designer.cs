﻿// <auto-generated />
using System;
using App.UI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.UI.Migrations
{
    [DbContext(typeof(EvaluationContext))]
    [Migration("20190110005244_17")]
    partial class _17
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("App.UI.Models.ContractInfoModel", b =>
                {
                    b.Property<int>("ContractId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContractNo");

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<int?>("ProjectTreeRef");

                    b.Property<int?>("ReginalPowerCorpRef");

                    b.Property<byte?>("State");

                    b.Property<string>("Title");

                    b.HasKey("ContractId");

                    b.HasIndex("ProjectTreeRef");

                    b.HasIndex("ReginalPowerCorpRef");

                    b.ToTable("ContractInfos","dbo");
                });

            modelBuilder.Entity("App.UI.Models.EducationalLevelModel", b =>
                {
                    b.Property<int>("EducationalLevelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<byte?>("State");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.HasKey("EducationalLevelId");

                    b.ToTable("EducationalLevels","dbo");
                });

            modelBuilder.Entity("App.UI.Models.EvaluationFactorTreeModel", b =>
                {
                    b.Property<int>("EvaluationFactorId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<int?>("EvaluationFactorRef");

                    b.Property<string>("EvaluationFactorType")
                        .HasMaxLength(5);

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<byte?>("State");

                    b.Property<string>("Test")
                        .HasMaxLength(5);

                    b.Property<string>("Title")
                        .HasMaxLength(100);

                    b.HasKey("EvaluationFactorId");

                    b.HasIndex("EvaluationFactorRef");

                    b.ToTable("EvaluationFactorTrees","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ExternalOrganizationModel", b =>
                {
                    b.Property<int>("ExternalOrganizationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<int?>("ExternalOrgTypeRef");

                    b.Property<string>("Fax")
                        .HasMaxLength(200);

                    b.Property<string>("Mail")
                        .HasMaxLength(200);

                    b.Property<string>("MainAddress")
                        .HasMaxLength(1000);

                    b.Property<string>("Manager")
                        .HasMaxLength(200);

                    b.Property<string>("ManagerMobile")
                        .HasMaxLength(200);

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<string>("RegisterCode")
                        .HasMaxLength(200);

                    b.Property<byte?>("State");

                    b.Property<string>("SubAddress")
                        .HasMaxLength(1000);

                    b.Property<string>("Tel")
                        .HasMaxLength(200);

                    b.Property<string>("Title");

                    b.HasKey("ExternalOrganizationId");

                    b.HasIndex("ExternalOrgTypeRef");

                    b.ToTable("ExternalOrganizations","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ExternalOrgTypeModel", b =>
                {
                    b.Property<int>("ExternalOrgTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<byte?>("State");

                    b.Property<string>("Title");

                    b.HasKey("ExternalOrgTypeId");

                    b.ToTable("ExternalOrgTypes","dbo");
                });

            modelBuilder.Entity("App.UI.Models.OUTreeModel", b =>
                {
                    b.Property<int>("OUTreeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<int?>("OUTreeRef");

                    b.Property<int?>("ReginalPowerCorpRef");

                    b.Property<byte?>("State");

                    b.Property<string>("Title");

                    b.HasKey("OUTreeId");

                    b.HasIndex("OUTreeRef");

                    b.HasIndex("ReginalPowerCorpRef");

                    b.ToTable("OUTrees","dbo");
                });

            modelBuilder.Entity("App.UI.Models.PersonModel", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(1000);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<int?>("EducationLevelRef");

                    b.Property<string>("Email")
                        .HasMaxLength(200);

                    b.Property<string>("FatherName")
                        .HasMaxLength(100);

                    b.Property<string>("FistName")
                        .HasMaxLength(200);

                    b.Property<int?>("IDNumber");

                    b.Property<string>("LastName")
                        .HasMaxLength(200);

                    b.Property<string>("MobileNo")
                        .HasMaxLength(10);

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<string>("NationalID")
                        .HasMaxLength(10);

                    b.Property<byte?>("State");

                    b.Property<string>("Tel")
                        .HasMaxLength(10);

                    b.HasKey("PersonId");

                    b.HasIndex("EducationLevelRef");

                    b.ToTable("Persons","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ProjectInfoModel", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<string>("ProjectNo");

                    b.Property<int?>("ProjectTreeRef");

                    b.Property<int?>("ReginalPowerCorpRef");

                    b.Property<byte?>("State");

                    b.Property<string>("Title");

                    b.HasKey("ProjectId");

                    b.HasIndex("ProjectTreeRef");

                    b.HasIndex("ReginalPowerCorpRef");

                    b.ToTable("ProjectInfos","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ProjectTreeModel", b =>
                {
                    b.Property<int>("ProjectTreeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<bool?>("IsTemplate");

                    b.Property<string>("Level");

                    b.Property<string>("LevelCode");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<int?>("ProjectTreeRef");

                    b.Property<int?>("ReginalPowerCorpRef");

                    b.Property<byte?>("State");

                    b.Property<string>("Title");

                    b.HasKey("ProjectTreeId");

                    b.HasIndex("ProjectTreeRef");

                    b.HasIndex("ReginalPowerCorpRef");

                    b.ToTable("ProjectTrees","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ReginalPowerCorpModel", b =>
                {
                    b.Property<int?>("ReginalPowerCorpId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Fax")
                        .HasMaxLength(200);

                    b.Property<string>("Mail")
                        .HasMaxLength(200);

                    b.Property<string>("MainAddress")
                        .HasMaxLength(1000);

                    b.Property<string>("Manager")
                        .HasMaxLength(200);

                    b.Property<string>("ManagerMobile")
                        .HasMaxLength(200);

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<string>("RegisterCode")
                        .HasMaxLength(200);

                    b.Property<byte?>("State");

                    b.Property<string>("SubAddress")
                        .HasMaxLength(1000);

                    b.Property<string>("Tel")
                        .HasMaxLength(200);

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.HasKey("ReginalPowerCorpId");

                    b.ToTable("ReginalPowerCorps","dbo");
                });

            modelBuilder.Entity("App.UI.Models.RoleOrgModel", b =>
                {
                    b.Property<int>("RoleOrgId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Created")
                        .HasMaxLength(10);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(80);

                    b.Property<string>("Description");

                    b.Property<string>("Modified")
                        .HasMaxLength(10);

                    b.Property<string>("ModifyBy")
                        .HasMaxLength(80);

                    b.Property<byte?>("State");

                    b.Property<string>("Title")
                        .HasMaxLength(200);

                    b.HasKey("RoleOrgId");

                    b.ToTable("RoleOrgs","dbo");
                });

            modelBuilder.Entity("App.UI.Models.ContractInfoModel", b =>
                {
                    b.HasOne("App.UI.Models.ProjectTreeModel", "ProjectTree")
                        .WithMany()
                        .HasForeignKey("ProjectTreeRef");

                    b.HasOne("App.UI.Models.ReginalPowerCorpModel", "ReginalPowerCorp")
                        .WithMany()
                        .HasForeignKey("ReginalPowerCorpRef");
                });

            modelBuilder.Entity("App.UI.Models.EvaluationFactorTreeModel", b =>
                {
                    b.HasOne("App.UI.Models.EvaluationFactorTreeModel", "EvaluationFactorParent")
                        .WithMany()
                        .HasForeignKey("EvaluationFactorRef");
                });

            modelBuilder.Entity("App.UI.Models.ExternalOrganizationModel", b =>
                {
                    b.HasOne("App.UI.Models.ExternalOrgTypeModel", "ExternalOrgType")
                        .WithMany()
                        .HasForeignKey("ExternalOrgTypeRef");
                });

            modelBuilder.Entity("App.UI.Models.OUTreeModel", b =>
                {
                    b.HasOne("App.UI.Models.OUTreeModel", "OUTree")
                        .WithMany()
                        .HasForeignKey("OUTreeRef");

                    b.HasOne("App.UI.Models.ReginalPowerCorpModel", "ReginalPowerCorp")
                        .WithMany()
                        .HasForeignKey("ReginalPowerCorpRef");
                });

            modelBuilder.Entity("App.UI.Models.PersonModel", b =>
                {
                    b.HasOne("App.UI.Models.EducationalLevelModel", "EducationalLevel")
                        .WithMany()
                        .HasForeignKey("EducationLevelRef");
                });

            modelBuilder.Entity("App.UI.Models.ProjectInfoModel", b =>
                {
                    b.HasOne("App.UI.Models.ProjectTreeModel", "ProjectTree")
                        .WithMany()
                        .HasForeignKey("ProjectTreeRef");

                    b.HasOne("App.UI.Models.ReginalPowerCorpModel", "ReginalPowerCorp")
                        .WithMany()
                        .HasForeignKey("ReginalPowerCorpRef");
                });

            modelBuilder.Entity("App.UI.Models.ProjectTreeModel", b =>
                {
                    b.HasOne("App.UI.Models.ProjectTreeModel", "ProjectTreechild")
                        .WithMany()
                        .HasForeignKey("ProjectTreeRef");

                    b.HasOne("App.UI.Models.ReginalPowerCorpModel", "ReginalPowerCorp")
                        .WithMany()
                        .HasForeignKey("ReginalPowerCorpRef");
                });
#pragma warning restore 612, 618
        }
    }
}
