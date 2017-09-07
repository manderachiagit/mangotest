using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango
{
	class Program
	{
		static void Main(string[] args)
		{

			Console.WriteLine("---------- Circular Dependcy Test ----------");
			Dependency circular = new Dependency();
			circular.add_direct("A", new string[] { "B" });
			circular.add_direct("B", new string[] { "C", "D" });
			circular.add_direct("C", new string[] { "D" });
			circular.add_direct("D", new string[] { "B" });

			try
			{
				circular.dependcies_for("A");
				Console.WriteLine("Circular Test failed");
			} catch (CircularDepencyException e) {
				Console.WriteLine(e.Message);
				Console.WriteLine("Circular Test Passed");
			}
			Console.WriteLine("---------- END Circular Dependcy Test ----------");
			Console.WriteLine("");


			Console.WriteLine("---------- Mango Test Example ----------");
			Dependency dep = new Dependency();
			dep.add_direct("A", new string[] { "B", "C" });
			dep.add_direct("B", new string[] { "C", "E" });
			dep.add_direct("C", new string[] { "G" });
			dep.add_direct("D", new string[] { "A", "F" });
			dep.add_direct("E", new string[] { "F" });
			dep.add_direct("F", new string[] { "H" });

			string[] expectedA = new string[] { "B", "C", "E", "F", "G", "H" };
			string[] actualA = dep.dependcies_for("A").ToArray();
			bool _A = !expectedA.Except(actualA).Any(); //true


			dep.dependcies_for("B");
			dep.dependcies_for("C");
			dep.dependcies_for("D");
			dep.dependcies_for("E");
			dep.dependcies_for("F");

			Console.WriteLine("---------- END Mango Test Example ----------");
			Console.WriteLine("");

			Console.WriteLine("---------- Self Reference Test ----------");
			Dependency selfTest = new Dependency();
			selfTest.add_direct("A", new string[] { "A" });

			try
			{
				selfTest.dependcies_for("A");
				Console.WriteLine("Self refereence failed");
			}
			catch (CircularDepencyException e)
			{
				Console.WriteLine(e.Message);
				Console.WriteLine("Circular Test Passed");
			}
			Console.WriteLine("---------- END Self Reference Test ----------");
			Console.WriteLine("");

			Console.WriteLine("---------- Empty Dependency Test ----------");
			Dependency emptyTest = new Dependency();
			emptyTest.add_direct("A", new string[] {  });
			emptyTest.add_direct("B", new string[] { "A" });

			emptyTest.dependcies_for("A");
			emptyTest.dependcies_for("B");
			Console.WriteLine("---------- END Empty Dependency Test ----------");
			Console.WriteLine("");
			Console.WriteLine("Done");
			Console.Read();

		}
	}
}
