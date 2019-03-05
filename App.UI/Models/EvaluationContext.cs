using Microsoft.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace App.UI.Models
{
    public class EvaluationContext : DbContext
    {
        public EvaluationContext(DbContextOptions<EvaluationContext> options)
            : base(options)
        {
          
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<ReginalPowerCorpModel> ReginalPowerCorps { get; set; }
        public DbSet<ProjectTreeModel> ProjectTrees { get; set; }
        public DbSet<OUTreeModel> OUTrees { get; set; }
        public DbSet<ExternalOrgTypeModel> ExternalOrgTypes { get; set; }
        public DbSet<ExternalOrganizationModel> ExternalOrganizations { get; set; }
        public DbSet<RoleOrgModel> RoleOrgs { get; set; }
        public DbSet<PersonModel> Persons { get; set; }
        public DbSet<EducationalLevelModel> EducationalLevels { get; set; }
        public DbSet<ContractInfoModel> ContractInfos { get; set; }
        public DbSet<ProjectInfoModel> ProjectInfos { get; set; }
        public DbSet<EvaluationFactorTreeModel> EvaluationFactorTrees { get; set; }
        public DbSet<TaskListModel> TaskLists { get; set; }


        public DbSet<EvaluationPeriodModel> EvaluationPeriods { get; set; }
        public DbSet<EvaluationProjectModel> EvaluationProjects { get; set; }

        public DbSet<EVFormTemplateModel> EVFormTemplates { get; set; }
        public DbSet<EVFormTemplateItemModel> EVFormTemplateItems { get; set; }

        public DbSet<EVFormModel> EVForms { get; set; }
        public DbSet<EVFormItemModel> EVFormItems { get; set; }

        public DbSet<ServiceTemplateTreeModel> ServiceTemplateTrees { get; set; }
        public DbSet<ServiceTreeModel> ServiceTrees { get; set; }

        public DbSet<ProjectMemberModel> ProjectMembers { get; set; }

    }
}