using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango
{
	public class Library
	{
		public string Name { get; private set; }
		public List<Library> DirectDependencies { get; set; }

		public Library(string name)
		{
			this.Name = name;
			this.DirectDependencies = new List<Library>();
		}

		public void addDependency(Library lib)
		{
			this.DirectDependencies.Add(lib);
		}

	}
}
