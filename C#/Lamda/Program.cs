namespace Lamda
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Func<int, int, int> tong = (a, b) =>
			{
				return a + b;
			};
			Console.WriteLine(tong?.Invoke(5, 3));
		}
	}
}