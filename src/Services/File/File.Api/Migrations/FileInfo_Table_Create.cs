using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace File.Api.Migrations
{
    [Migration(20190612090500)]
    public class FileInfo_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("AppFile")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(255).NotNullable()
                .WithColumn("AppType").AsString(100).NotNullable()
                .WithColumn("Length").AsInt32().NotNullable()
                .WithColumn("Extension").AsString(10).NotNullable()
                .WithColumn("Path").AsString(2000).NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable();

            Create.Table("AppFileExtendProp")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("AppFileId").AsInt32().ForeignKey("AppFile", "Id")
                .WithColumn("PropName").AsString(100).NotNullable()
                .WithColumn("PropValue").AsString(500).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("CreatedBy").AsString().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("AppFileExtendProp");
            Delete.Table("AppFile");
        }
    }
}
