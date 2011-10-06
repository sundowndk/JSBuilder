using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace JSBuilder2
{
	public class Project
	{
		#region Private Fields
		private string _name;		
		private List<Class> _classes;
		private List<Js> _jses;
		#endregion

		#region Public Fields
		public string Name
		{
			get
			{
				return this._name;
			}
		}
		
		public List<Class> Classes
		{
			get
			{
				return this._classes;
			}
		}
		
		public List<Js> Jses
		{
			get
			{
				return this._jses;
			}
		}		
		#endregion
		
		#region Constructor
		private Project ()
		{
			this._name = string.Empty;						
			this._classes = new List<Class> ();
			this._jses = new List<Js> ();
		}
		#endregion
		
		#region Public Methods
		public List<string> Build (int Depth)
		{
			Console.WriteLine ("\tBuilding project '"+ this._name +"'");			
			List<string> result = new List<string> (); 	
			
			#region TAB
			string tab = string.Empty;
			for (int i = 0; i < Depth; i++) 
			{
				tab += "\t";
			}
			#endregion
			
			#region CLASS
			if (this._classes.Count > 0)
			{
				int count = 0;
				foreach (Class class_ in this._classes)
				{
					Console.WriteLine ("\t\tIncluding class '"+ class_.Name +"'...");
					count++;
											
					result.Add (tab +"// ---------------------------------------------------------------------------------------------------------------");
					result.Add (tab +"// CLASS: "+ class_.Name);
					result.Add (tab +"// ---------------------------------------------------------------------------------------------------------------");
					if (Depth < 1)
					{
						result.Add (tab + "var "+ class_.Name +" =");
					}
					else
					{
						result.Add (tab + class_.Name +" :");
					}
					result.Add (tab + "{");
					
					foreach (string line in class_.Build (Depth + 1))
					{
						result.Add (tab + line);
					}
					
					if (count < this._classes.Count)
					{
						result.Add (tab + "},");
						result.Add (string.Empty);
					}
					else
					{
						result.Add (tab + "}");	
						
						if (this._jses.Count > 0)
						{
							result.Add (string.Empty);
						}
					}
				}
			}
			#endregion
			
			#region JS
			if (this._jses.Count > 0)
			{
				Console.WriteLine ("\t\t\tIncluding js");
												
				foreach (Js js in this._jses)
				{
					Console.WriteLine ("\t\t\t\t'"+ js.Name +"'...");				
					
					foreach (string line in js.Build ())
					{
						result.Add (tab + line);
					}
				}		
			}
			#endregion
							
			return result;
		}
		#endregion
		
		#region Public Static Methods
		public static Project Parse (XmlNodeList Nodes, string Name, string Path)
		{
			Project result = new Project ();			
			result._name = Name;
			
			foreach (XmlNode node in Nodes)
			{
				switch (node.Name.ToLower ())
				{
					case "class":
					{						
						result._classes.Add (Class.Parse (node.ChildNodes, node.Attributes["name"].Value, Path));
					
						break;
					}
						
					case "js":
					{
						result._jses.Add (Js.Load (System.IO.Path.GetDirectoryName (Path) +"/"+ node.Attributes["file"].Value));
						break;
					}							
				}
			}
			
			return result;
		}
		#endregion
	}
}

