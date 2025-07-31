namespace Delegate
{
	public delegate void ShowLog(string message);

	internal class Program
	{
		private static void Warning(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine(msg);
			Console.ResetColor();
		}

		private static void Error(string msg)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine(msg);
			Console.ResetColor();
		}

		private static int Tong(int a, int b) => a + b;

		private static void Hieu(int a, int b, ShowLog log)
		{
			int kq = a - b;
			Console.ForegroundColor = ConsoleColor.Green;
			log?.Invoke("Tong la: " + kq);
		}

		private static void Main(string[] args)
		{
			Func<int, int, int> func1;
			int a = 8, b = 10;
			func1 = Tong;
			Hieu(9, 2, Warning);
			Console.WriteLine("Tổng là " + func1.Invoke(a, b));
		}
	}
}