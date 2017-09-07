using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango
{
	public class Dependency
	{

		List<Library> dependcyList = new List<Library>();
	
		///<summary>
		///<para>converts strings to Library object</para>
		///<para>adds Library object to dependcyList</para>
		///</summary>
		public void add_direct(string item,string[] dependencies)
		{
			Console.Write(item + " | ");
			//if we don't have a name (index [0]), then return
			if (dependencies.Length == 0)
			{
				Console.WriteLine("");
				return;
			}
			
			Library lib = getLibrary(item);			

			foreach (string name in dependencies) {
				Console.Write(name);
				Library dependency = getLibrary(name);
				lib.addDependency(dependency);
				//lib.addResolvedDepency(dependency);
			}
			Console.WriteLine("");
		
		}

		public List<string> dependcies_for(string lookup) {
			//get library
			Library lib = getLibrary(lookup);

			List<string> resolvedDependencies = new List<string>();
			List<string> visitedDependencies = new List<string>();

			resolveDepencies(lib, null, resolvedDependencies, visitedDependencies);
			Console.WriteLine("Dependencies for {0}", lookup);
			foreach (string s in resolvedDependencies)
			{
				Console.Write("{0} ", s);
			}
			Console.WriteLine("");

			
			return resolvedDependencies;
		}

		/// <summary>
		/// <para>Recursively traverse the library and each of its dependents</para>
		/// <para>If the dependent does not exist, we can add to the origin library</para>
		/// <para>If the dependent does not exist, but we have already traversed this library, then we can assume circular dependency since we have already traversed ths pathy</para>
		/// </summary>
		/// <param name="lib"></param>
		/// <param name="origin"></param>
		/// <param name="resolvedDependencies"></param>
		/// <param name="visitedDependencies"></param>
		private void resolveDepencies(Library lib, Library origin, List<string> resolvedDependencies,List<string> traversedDependencies)
		{
			if (origin == null)
			{
				origin = lib;
			}
			traversedDependencies.Add(lib.Name);

			foreach (Library dependcy in lib.DirectDependencies)
			{
				bool dependencyExists = resolvedDependencies.Any(item => item == dependcy.Name);
				bool hasBeenTraversed = traversedDependencies.Any(item => item == dependcy.Name);
				if (!dependencyExists)
				{

					if (hasBeenTraversed)
					{
						string error = "Circular Reference found for " + dependcy.Name + " - " + lib.Name;
						throw new CircularDepencyException(error);
					}

					resolveDepencies(dependcy, origin, resolvedDependencies, traversedDependencies);
					resolvedDependencies.Add(dependcy.Name);
				}

							

			}
		}

		///<summary>
		///<para>query our depencyList for library.  If it does not exist,create the library and add it to list.</para>
		///<para></para>
		///</summary>
		private Library getLibrary(string name)
		{
			//check to see if this Library already exists
			Library lib = dependcyList.Where(item => item.Name == name).FirstOrDefault();

			if (lib == null)
			{
				lib = new Library(name);
				dependcyList.Add(lib);
			}

			return lib;
		}
	}
	
}
