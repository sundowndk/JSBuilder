using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

namespace JSBuilder2
{	
	public class Solution
	{		
		#region Private Fields
		private string _name;
		private string _path;
		private bool _buildjsm;
		private string _outputdirectory;
		private List<Project> _projects;
		#endregion
		
		#region Public Fields
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		public bool BuildJSM
		{
			get
			{
				return this._buildjsm;
			}
		}
		
		public string OutputDirectory
		{
			get
			{
				return this._outputdirectory;
			}
		}
		
		public List<Project> Projects
		{
			get
			{
				return this._projects;
			}
		}
		#endregion
		
		#region Constructor
		private Solution ()
		{
			this._name = string.Empty;
			this._buildjsm = false;
			this._outputdirectory = string.Empty;			
			this._projects = new List<Project> ();			
		}	
		#endregion
		
		#region Public Methods
		public void Build (string OutputDirectory)
		{
			if (!System.IO.Path.IsPathRooted (OutputDirectory))
			{
				this._outputdirectory = System.IO.Path.GetDirectoryName (this._path) +"/"+ OutputDirectory;
			}			
			
			this._outputdirectory = OutputDirectory;
			
			Build ();
		}
		
		public void Build ()
		{			
			Console.WriteLine ("Bulding solution '"+ this._name +"' ...");												
			
			#region PROJECTS
			if (this._projects.Count > 0)
			{
				foreach (Project project in this._projects)
				{									
					List<string> result = new List<string> ();
					result.Add ("// ---------------------------------------------------------------------------------------------------------------");
					result.Add ("// PROJECT: "+ project.Name);
					result.Add ("// ---------------------------------------------------------------------------------------------------------------");				
			
					foreach (string line in project.Build (0))
					{
						result.Add (line);
					}
					
					Console.WriteLine (string.Empty);
					Console.WriteLine ("Writing '"+ this._outputdirectory + project.Name +".js' ...");			
					Toolbox.IO.WriteTextFile (this._outputdirectory + project.Name +".js", result, Encoding.ASCII);

//					TextWriter textfile = new StreamWriter (this._outputdirectory + project.Name +".js", false, Encoding.ASCII);
//					foreach (string line in result)
//					{
//						textfile.WriteLine (line);
//					}
//					textfile.Close ();
//					textfile.Dispose ();


					if (this._buildjsm)
					{
						result.Insert (0, "var EXPORTED_SYMBOLS = [\""+ project.Name +"\"];");

						Console.WriteLine (string.Empty);
						Console.WriteLine ("Writing '"+ this._outputdirectory + project.Name +".jsm' ...");			
						Toolbox.IO.WriteTextFile (this._outputdirectory + project.Name +".jsm", result, Encoding.ASCII);
					}
				}
			}
			#endregion
		}
		#endregion
		
		#region Public Static Methods
		public static Solution Load (string Path)
		{							
			Solution result = new Solution ();
			result._path = Path;
			
			XmlDocument xml = new XmlDocument ();
			xml.Load (result._path);
			
			XmlElement root = xml.DocumentElement;
			
			result._name = root.Attributes["name"].Value;				
			result._outputdirectory = root.Attributes["outputdirectory"].Value;

			if (root.HasAttribute ("buildjsm"))
			{
				if (root.Attributes["buildjsm"].Value == "true")
				{
					result._buildjsm = true;
				}
			}
			
			if (!System.IO.Path.IsPathRooted (result._outputdirectory))
			{
				result._outputdirectory = System.IO.Path.GetDirectoryName (result._path) +"/"+ result._outputdirectory;
			}
			
			foreach (XmlNode node in root.ChildNodes)
			{							
				switch (node.Name.ToLower ())
				{
					case "project":
					{						
						result._projects.Add (Project.Parse (node.ChildNodes, node.Attributes["name"].Value, result._path));
						
						break;
					}
					
					case "include":
					{
						break;
					}
				}
			}
			
			return result;
		}
		#endregion
	}
}

