using System;
using JSBuilder2;

namespace test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Solution s = Solution.Load ("/home/rvp/Skrivebord/jsbuildertest/test.jsb");
			s.Build ();
			
		}
	}
}
