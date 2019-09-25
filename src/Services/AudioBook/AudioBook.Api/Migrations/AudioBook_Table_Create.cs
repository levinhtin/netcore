using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace AudioBook.API.Migrations
{
    [Migration(20190612161801)]
    public class AudioBook_Table_Create : Migration
    {
        public override void Up()
        {
            Create.Table("Category")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("Author")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("DateOfBirth").AsDate().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("Reader")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBookInfo")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("ImageBackground").AsString()
                .WithColumn("Views").AsInt32()
                .WithColumn("Rate").AsInt32()
                .WithColumn("Duration").AsInt32()
                .WithColumn("IsActive").AsBoolean().WithDefaultValue(0)
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBookTrack")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("PathFile").AsString()
                .WithColumn("Duration").AsInt32()
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBookInfo", "Id")
                .WithColumn("ReaderId").AsInt32().ForeignKey("Reader", "Id").Nullable()
                .WithColumn("IsActive").AsBoolean().WithDefaultValue(0)
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Categories")
                .WithColumn("CategoryId").AsInt32().ForeignKey("Category", "Id")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBookInfo", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Authors")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBookInfo", "Id")
                .WithColumn("AuthorId").AsInt32().ForeignKey("Author", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Readers")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBookInfo", "Id")
                .WithColumn("ReaderId").AsInt32().ForeignKey("Reader", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            // Seed Data
            #region Category
            Insert.IntoTable("Category").Row(new { Name = "Kinh tế", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Văn học", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Tôn giáo", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Chính trị", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Lịch sử", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Khoa học", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            Insert.IntoTable("Category").Row(new { Name = "Giải trí", CreatedAt = DateTime.Now, CreatedBy = "Migrator" });
            #endregion
        }

        public override void Down()
        {
            Delete.Table("AudioBook_Categories");
            Delete.Table("AudioBookTrack");
            Delete.Table("AudioBookInfo");
            Delete.Table("Reader");
            Delete.Table("Author");
            Delete.Table("Category");
        }
    }
}
