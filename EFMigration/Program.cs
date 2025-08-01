namespace EFMigration
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			/*
			 Các lệnh của EF Migration
			1. dotnet ef migrations add [MigrationName]   - Tạo migration
			2. dotnet ef migrations list                  - Xem danh sách các migration
			3. dotnet ef migrations remove				  - Xóa migration version mới nhất
			4. dotnet ef database update				  - Cập nhật lên database tới version mới nhất hoặc thêm tên version muốn update ở cuối
			5. dotnet ef database remove -f				  - Xóa database
			6. Với khóa chính thì phải thay đổi tên luôn (string sang int) rồi version sau đổi tên lại
			7. dotnet ef migrations script				  - Tạo file script từ V0 - Vcuoi (script Name1 Name2)
			8. dotnet ef migrations script -o migration.sql     - Tạo file SQL

			** Tạo migration từ DB có sẵn
			1. dotnet ef dbcontext scaffold -o Models -d "sqlConnectstring" "Microsoft.EntityFrameworkCore.SqlServer"
			2. dotnet ef migrations add V0
			3. dotnet ef migrations script -o 1.sql
			4. Vô file này rồi insert bảng và dữ liệu vào bảng Migration trên SQL server
			5. dotnet ef migrations update. End
			 */
		}
	}
}