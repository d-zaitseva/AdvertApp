using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdvertApp.Persistance.Migrations
{
    public partial class GetAdvertsProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var createProcSql = @"CREATE PROCEDURE [dbo].[GetAdverts]
									@PageSize INT,
									@PageNumber INT,
									@SortBy INT,
									@SortAsc BIT

									AS
									BEGIN
									SELECT *, COUNT(*) OVER()
									FROM (SELECT * 
									FROM [dbo].[Adverts]) a
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
									return;
									END";
            migrationBuilder.Sql(createProcSql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dropProcSql = "DROP PROCEDURE [dbo].[GetAdverts]";
            migrationBuilder.Sql(dropProcSql);
        }
    }
}
