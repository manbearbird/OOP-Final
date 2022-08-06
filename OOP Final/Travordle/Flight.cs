using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Travordle
{
	public class Flight : RouteGeneration
	{
		public static List<Route> GetRouteList(string startcity)
		{
			string path = @"..\..\..\Travordle\RouteLists\Flights\"+startcity+".csv";
			var list = new List<Route>();
			foreach (string line in File.ReadLines(path))
			{
				var section = line.Split(',');
				list.Add(new Route(section[2], section[0], "Flight", Convert.ToDouble(section[6]), Convert.ToDouble(section[3]), Convert.ToDouble(section[4]), section[1]));
			}
			return list;
		}
	}
}
