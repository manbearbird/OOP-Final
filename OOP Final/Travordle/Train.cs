using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Travordle
{
	public class Train : RouteGeneration
	{
		public static List<Route> GetRouteList(string startcity)
		{
			string path = @"..\..\..\Travordle\RouteLists\Trains\"+startcity+".csv";
			var list = new List<Route>();
			foreach (string line in File.ReadLines(path))
			{
				var section = line.Split(',');
				list.Add(new Route(section[1], section[0], "Train", Convert.ToDouble(section[5]), Convert.ToDouble(section[2]), Convert.ToDouble(section[3]), null));
			}
			return list;
		}
	}
}
