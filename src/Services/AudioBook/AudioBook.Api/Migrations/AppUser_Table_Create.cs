using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace AudioBook.API.Migrations
{
    [Migration(20180430121801)]
    public class AppUser_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("AppUser")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Username").AsString()
                .WithColumn("Email").AsString()
                .WithColumn("FirstName").AsString()
                .WithColumn("LastName").AsString()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AppRole")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AppUserRole")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("AppUserId").AsInt32().ForeignKey("AppUser", "Id")
                .WithColumn("AppRoleId").AsInt32().ForeignKey("AppRole", "Id")
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("AppUser");
        }
    }
}
