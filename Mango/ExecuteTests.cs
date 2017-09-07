using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace Mango
{
	[TestFixture]
	public class ExecuteTests
	{
		[Test]
		public void CircularDependcy() {
			Dependency circular = new Dependency();
			circular.add_direct("A", new string[] { "B" });
			circular.add_direct("B", new string[] { "C", "D" });
			circular.add_direct("C", new string[] { "D" });
			circular.add_direct("D", new string[] { "B" });

			Assert.Throws<CircularDepencyException>(() => circular.dependcies_for("A"));
			Assert.Throws<CircularDepencyException>(() => circular.dependcies_for("B"));
			Assert.Throws<CircularDepencyException>(() => circular.dependcies_for("C"));
			Assert.Throws<CircularDepencyException>(() => circular.dependcies_for("D"));
			Console.WriteLine("Circular Test Passed");
		}

		[Test]
		public void TestExample() {
			/* example question */
			Console.WriteLine("---------- Mango Test Example ----------");
			Dependency dep = new Dependency();
			dep.add_direct("A", new string[] { "B", "C" });
			dep.add_direct("B", new string[] { "C", "E" });
			dep.add_direct("C", new string[] { "G" });
			dep.add_direct("D", new string[] { "A", "F" });
			dep.add_direct("E", new string[] { "F" });
			dep.add_direct("F", new string[] { "H" });

			Console.WriteLine("Testing for A");
			List<string> resultsA = dep.dependcies_for("A");

			dep.dependcies_for("B");
			dep.dependcies_for("C");
			dep.dependcies_for("D");
			dep.dependcies_for("E");
			dep.dependcies_for("F");

			Console.WriteLine("---------- END Mango Test Example ----------");
		}

		[Test]
		public void self() {
			Dependency selfTest = new Dependency();
			selfTest.add_direct("A", new string[] { "A" });

			Assert.Throws<CircularDepencyException>(() => selfTest.dependcies_for("A"));
		}

	}
}
