using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango
{
	class CircularDepencyException : Exception
	{
		public CircularDepencyException()
		{
		}

		public CircularDepencyException(string message)
        : base(message)
		{

		}

		public CircularDepencyException(string message, Exception inner)
		: base(message, inner)
		{

		}
	}
}
