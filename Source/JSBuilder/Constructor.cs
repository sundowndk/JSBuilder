using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace JSBuilder2
{
	public class Constructor
	{		
		#region Private Fields
		private string _name;
		private string _variables;
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
		
		public string Variables
		{
			get
			{
				return this._variables;
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
		private Constructor ()
		{
			this._name = string.Empty;	
			this._variables = string.Empty;
			this._jses = new List<Js> ();
		}
		#endregion
		
		#region Public Methods
		public List<string> Build (int Depth)
		{
			List<string> result = new List<string> ();
			
			#region TAB
			string tab = string.Empty;
			for (int i = 0; i < Depth; i++) 
			{
				tab += "\t";
			}		
			#endregion
									
			#region JS
			if (this._jses.Count > 0)
			{
				int count = 0;
				foreach (Js js in this._jses)
				{
					Console.WriteLine ("\t\t\t\t'"+ js.Name +"'...");			
					count++;
								
					foreach (string line in js.Build ())
					{
						result.Add (tab + line);	
					}				
						
//					if (count < this._jses.Count)
//					{
//						result[result.Count - 1] += ","; 
//						result.Add (string.Empty);
//					}
				}
			}
			#endregion
			
			return result;
		}
		#endregion
		
		#region Public Static Methods
		public static Constructor Parse (XmlNodeList Nodes, string Name, string Variables, string Path)
		{
			Constructor result = new Constructor ();
			result._name = Name;
			result._variables = Variables;
			
			foreach (XmlNode node in Nodes)
			{
				switch (node.Name.ToLower ())
				{					
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

