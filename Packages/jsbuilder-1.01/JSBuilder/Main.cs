using System;

using System.IO;
using System.Xml;
namespace JSBuilder2
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			
			
			if (args.Length > 0)
			{		
				string path = string.Empty;
				if (!System.IO.Path.IsPathRooted (args[0]))
				{
					path = System.Environment.CurrentDirectory + "/" + args[0];
				}
				else
				{
					path = args[0];
				}
				
				Solution solution = Solution.Load (path);
				
				if (args[1] != string.Empty)
				{
					solution.Build (args[1]);	
				}				
				else
				{
					solution.Build ();
				}				
			}
			else
			{
				Console.WriteLine ("Syntax: inputfile");
			}
		}				
	}
}

