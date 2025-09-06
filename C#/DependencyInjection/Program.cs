using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
	internal interface IClassB
	{
		public void ActionB();
	}

	internal interface IClassC
	{
		public void ActionC();
	}

	internal class ClassC : IClassC
	{
		public ClassC() => Console.WriteLine("ClassC is created");

		public void ActionC() => Console.WriteLine("Action in ClassC");
	}

	internal class ClassB : IClassB
	{
		private IClassC c_dependency;

		public ClassB(IClassC classc)
		{
			c_dependency = classc;
			Console.WriteLine("ClassB is created");
		}

		public void ActionB()
		{
			Console.WriteLine("Action in ClassB");
			c_dependency.ActionC();
		}
	}

	internal class ClassA
	{
		private IClassB b_dependency;

		public ClassA(IClassB classb)
		{
			b_dependency = classb;
			Console.WriteLine("ClassA is created");
		}

		public void ActionA()
		{
			Console.WriteLine("Action in ClassA");
			b_dependency.ActionB();
		}
	}

	internal class ClassC1 : IClassC
	{
		public ClassC1() => Console.WriteLine("ClassC1 is created");

		public void ActionC()
		{
			Console.WriteLine("Action in C1");
		}
	}

	internal class ClassB1 : IClassB
	{
		private IClassC c_dependency;

		public ClassB1(IClassC classc)
		{
			c_dependency = classc;
			Console.WriteLine("ClassB1 is created");
		}

		public void ActionB()
		{
			Console.WriteLine("Action in B1");
			c_dependency.ActionC();
		}
	}

	internal class ClassB2 : IClassB
	{
		private IClassC c_dependency;
		private string message;

		public ClassB2(IClassC classc, string mgs)
		{
			c_dependency = classc;
			message = mgs;
			Console.WriteLine("ClassB2 is created");
		}

		public void ActionB()
		{
			Console.WriteLine(message);
			c_dependency.ActionC();
		}
	}

	public class Program
	{
		private static void Main(string[] args)
		{
			var services = new ServiceCollection();
			// Đăng ký dịch vụ
			services.AddSingleton<IClassC, ClassC1>();
			services.AddSingleton<IClassB, ClassB1>();
			services.AddSingleton<ClassA>();

			var provider = services.BuildServiceProvider();

			// Gọi dịch vụ
			var classA = provider.GetService<ClassA>();
			classA.ActionA();
		}
	}
}