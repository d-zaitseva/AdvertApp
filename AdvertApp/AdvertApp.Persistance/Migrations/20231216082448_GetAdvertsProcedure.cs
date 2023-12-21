using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertApp.Persistance.Migrations
{
    public partial class GetAdvertsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE PROCEDURE [dbo].[GetAdverts]
					(
					@PageSize int,
					@PageNumber int,
					@SortBy int,
					@SortAsc bit,
					@FullTextSearch nvarchar(max),
					@AdvertStatus int,
					@CreatedAt date,
					@UpdatedAt date,
					@MinRating int,
					@MaxRaiting int
					)
				AS
				declare @likeFilterOn bit = LEN(ISNULL(@FullTextSearch, ''))
				declare @fullTextSearchExp nvarchar(max) = CONCAT('%',@FullTextSearch,'%')

				SELECT *, count(*) over() as TotalCount
				FROM (SELECT * FROM [dbo].[Adverts]) a

				WHERE (@AdvertStatus IS NULL OR a.[Status] = @AdvertStatus)
					AND (@CreatedAt IS NULL OR a.[Audit_CreatedAt] BETWEEN CONCAT(@CreatedAt,' 00:00:00.000') AND CONCAT(@CreatedAt,' 23:59:59.999'))
					AND (@UpdatedAt IS NULL OR a.[Audit_UpdatedAt] BETWEEN CONCAT(@CreatedAt,' 00:00:00.000') AND CONCAT(@CreatedAt,' 23:59:59.999'))
					AND (@MinRating IS NULL OR a.[Rating] >= @MinRating)
					AND (@MaxRaiting IS NULL OR a.[Rating] <= @MaxRaiting)
					AND (@likeFilterOn = 0
					OR a.[AuthorName] LIKE @fullTextSearchExp
					OR a.[Text] LIKE @fullTextSearchExp)

				ORDER BY
					case when @SortBy = 0 and @SortAsc = 1 then a.[Number] end asc,
					case when @SortBy = 1 and @SortAsc = 1 then a.[AuthorName] end asc,
					case when @SortBy = 2 and @SortAsc = 1 then a.[Rating] end asc,
					case when @SortBy = 3 and @SortAsc = 1 then a.[Audit_CreatedAt] end asc,
					case when @SortBy = 4 and @SortAsc = 1 then a.[Audit_UpdatedAt] end asc,
					case when @SortBy = 5 and @SortAsc = 1 then a.[ExpiredAt] end asc,
					case when @SortBy = 6 and @SortAsc = 1 then a.[Status] end asc,

					case when @SortBy = 0 and @SortAsc = 0 then a.[Number] end desc,
					case when @SortBy = 1 and @SortAsc = 0 then a.[AuthorName] end desc,
					case when @SortBy = 2 and @SortAsc = 0 then a.[Rating] end desc,
					case when @SortBy = 3 and @SortAsc = 0 then a.[Audit_CreatedAt] end desc,
					case when @SortBy = 4 and @SortAsc = 0 then a.[Audit_UpdatedAt] end desc,
					case when @SortBy = 5 and @SortAsc = 0 then a.[ExpiredAt] end desc,
					case when @SortBy = 6 and @SortAsc = 0 then a.[Status] end desc

				offset @PageSize * (@PageNumber - 1) rows
				fetch next @PageSize rows only option (recompile);
				return;";

            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROCEDURE [dbo].[GetAdverts]";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
