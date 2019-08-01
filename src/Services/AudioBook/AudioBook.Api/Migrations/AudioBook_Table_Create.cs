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

            Create.Table("AudioBook")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString().Nullable()
                .WithColumn("ImageBackground").AsString()
                .WithColumn("Views").AsInt32()
                .WithColumn("Rate").AsInt32()
                .WithColumn("Duration").AsInt32()
                .WithColumn("IsActive").AsBoolean()
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
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBook", "Id")
                .WithColumn("ReaderId").AsInt32().ForeignKey("Reader", "Id")
                .WithColumn("IsActive").AsBoolean()
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Categories")
                .WithColumn("CategoryId").AsInt32().ForeignKey("Category", "Id")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBook", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Authors")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBook", "Id")
                .WithColumn("AuthorId").AsInt32().ForeignKey("Author", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();

            Create.Table("AudioBook_Readers")
                .WithColumn("AudioBookId").AsInt32().ForeignKey("AudioBook", "Id")
                .WithColumn("ReaderId").AsInt32().ForeignKey("Reader", "Id")
                .WithColumn("CreatedAt").AsDateTime()
                .WithColumn("CreatedBy").AsString()
                .WithColumn("ModifiedAt").AsDateTime().Nullable()
                .WithColumn("ModifiedBy").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table("AudioBook_Categories");
            Delete.Table("AudioBookTrack");
            Delete.Table("AudioBook");
            Delete.Table("Reader");
            Delete.Table("Author");
            Delete.Table("Category");
        }
    }
}
