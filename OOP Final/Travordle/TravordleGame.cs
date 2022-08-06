using System;
using System.Collections.Generic;
using System.Text;

//built as an API that can be interacted with, returning key value pairs of <int (describing game state), string(detailed message)> 
//for connecting frontend game logic, int values correspond as:
//0+ Game in progress, int represents current guess #
//-1=Game Status Update
//-2=Error
//string messages are crafted for direct display to user

namespace Travordle
{
	public class TravordleGame
	{

		private string _StartCity = "";
		private Random rnd = new Random();
		private List<Route> RouteList = new List<Route>();
		private Route? SelectedRoute;
		private int _GuessCount;
		private bool _ActiveGame;

		public int GuessCount { get; }
		public bool ActiveGame { get; }

		public TravordleGame()
		{
			ActiveGame = false;
		}

		public KeyValuePair<int, string> NewGame(string city)
		{
			RouteList.Clear();
			_ActiveGame = true;	
			_GuessCount = 0;
			RouteList.AddRange(Train.GetRouteList(city));
			RouteList.AddRange(Flight.GetRouteList(city));
			RouteList.AddRange(Bus.GetRouteList(city));
			if (RouteList.Count == 0)
				return new KeyValuePair<int, string>(8, "No routes found from "+city+".");
			SelectedRoute = RouteList.ElementAt(rnd.Next(RouteList.Count));
			return new KeyValuePair<int, string>(0, "Route found from "+city+" to a destination for $"+ SelectedRoute.Price+".");
		}
		public string StartCity
		{
			get
			{
				return _StartCity;
			}
			set
			{
				_StartCity = value;
			}
		}
		//method overloading to accept string inputs
		
		public KeyValuePair<int, string> RouteGuess(string city, string mode)
		{
			if (RouteList.Exists(x => x.DestinationCity == city && x.Mode == mode))
			{
				Route dest = RouteList.Find(x => x.DestinationCity == city && x.Mode == mode);
				if (dest == SelectedRoute)
				{
					_ActiveGame = false;
					return new KeyValuePair<int, string>(-1, "Correct in " + GuessCount + " guesses! " + SelectedRoute.PrintRoute());
				}
				_GuessCount++;
				return new KeyValuePair<int, string>(GuessCount, RouteDif(SelectedRoute, dest));
			}
			if (RouteList.Exists(x => x.DestinationCity == city))
				return new KeyValuePair<int, string>(-1, "No such route found to "+city+". Try again!");
			return new KeyValuePair<int, string>(-1, "No such city found. Try Again!");
		}

		public KeyValuePair<int, string> PrintRoutes()
		{
			return new KeyValuePair<int, string>(-1, "not yet implmented");
		}
		public KeyValuePair<int, string> PrintRoutes(string type) //overloading if user wants to specify a set of routes to print (flights/buses/trains)
		{
			return new KeyValuePair<int, string>(-1, "not yet implmented");
		}

		public KeyValuePair<int, string> PrintCities()
        {
			return new KeyValuePair<int, string>(-1, "not yet implmented");
		}

		public KeyValuePair<int, string> PrintCities(string type)
		{
			return new KeyValuePair<int, string>(-1, "not yet implmented");//overloading if user wants to specify a set of routes to print (flights/buses/trains)
		}


		private static string RouteDif(Route a, Route b)
		{
			if ((a.StartingCity == b.StartingCity) && (a.DestinationCity == b.DestinationCity) && (a.Price == b.Price) && (a.Mode == b.Mode))
				return "Correct!" + a.URL;
			if (a.Lat == 0 || a.Long == 0 || b.Lat == 0 || b.Long == 0)
				return ModeCompare(a.Mode, b.Mode);
			return "Destination city is to the " + DegreeBearing(b.Lat, b.Long, a.Lat, a.Long) + " " + ModeCompare(a.Mode, b.Mode);
		}

		//Support functions for route comparisons;
		private static string ModeCompare(string a, string b)
		{
			if (a == "Bus" && b == "Bus")
				return "Route is by Bus!";
			if (a == "Train" && b == "Train")
				return "Route is by Train!";
			if (a == "Flight" && b == "Flight")
				return "Route is by Flight!";
			if (b == "Flight" || b == "Train" || b == "Bus")
				return "Route isn't by "+ b;
			return "Route Guess needs to be Bus, Train, or Flight";
		}
		private static string DegreeBearing(double lat1, double lon1, double lat2, double lon2)
		{
			var dLon = ToRad(lon2-lon1);
			var dPhi = Math.Log(
				Math.Tan(ToRad(lat2)/2+Math.PI/4)/Math.Tan(ToRad(lat1)/2+Math.PI/4));
			if (Math.Abs(dLon) > Math.PI)
				dLon = dLon > 0 ? -(2*Math.PI-dLon) : (2*Math.PI+dLon);
			return ToBearing(Math.Atan2(dLon, dPhi));
		}

		private static double ToRad(double degrees)
		{
			return degrees * (Math.PI / 180);
		}

		private static double ToDegrees(double radians)
		{
			return radians * 180 / Math.PI;
		}

		private static string ToBearing(double radians)
		{
			// convert radians to degrees (as bearing: 0...360), and converts to cardinal direction
			if (((ToDegrees(radians) +360) + 22.5) % 360 < 45)
				return "North";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 90)
				return "NorthEast";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 135)
				return "East";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 180)
				return "SouthEast";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 225)
				return "South";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 270)
				return "SouthWest";
			if (((ToDegrees(radians) +360) + 22.5) % 360  < 315)
				return "West";
			return "NorthWest";
		}
	}
}
