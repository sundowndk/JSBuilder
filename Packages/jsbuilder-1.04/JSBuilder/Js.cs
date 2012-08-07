using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;

using Toolbox;

namespace JSBuilder2
{
	public class Js
	{
		#region Private Fields
		private string _name;
		private string _path;
		#endregion
		
		#region Public Fields
		public string Name
		{
			get
			{
				return this._name;
			}
		}
		#endregion
		
		#region Constructor
		private Js ()
		{
		}
		#endregion
		
		#region Public Methods
		public List<string> Build ()
		{
			return Toolbox.IO.ReadTextFile (this._path, Encoding.UTF8);
		}
		#endregion
		
		#region Public Static Methods
		public static Js Load (string Path)
		{
			Js result = new Js ();
			result._name = System.IO.Path.GetFileNameWithoutExtension (Path);
			result._path = Path;
			
			return result;
		}	
		#endregion
	}
}

