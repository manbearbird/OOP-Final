using System;
using System.Collections.Generic;
using System.Text;

namespace Travordle
{
	//Due to cutbacks in program goals the point in this inheritence was lost, if I was pulling files from the web in some way
	//then any helper functions would go here for all route types to benifit from. As is each type does differ slightly in its
	//GetRouteList() because each csv file has slightly different order or information within it
	public abstract class RouteGeneration
	{
		public RouteGeneration()
		{
			throw new NotImplementedException();
		}

		public virtual List<Route> GetRouteList()
		{
			throw new NotImplementedException();
		}
	}
}
