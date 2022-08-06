using System;
using System.Collections.Generic;
using System.Text;

namespace Travordle
{
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public class Route
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
		private string _DestinationCity;
		private string _StartingCity;
		private string _Mode;
		private double _Price = 0;
		private string? _URL;
		private double _Lat = 0;
		private double _Long = 0;

		public string DestinationCity
		{
			get
			{
				return _DestinationCity;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(nameof(value));
				}

				_DestinationCity = value;	//would have liked to add code here to normalize city imputs such as normalizing metropolitain areas (st paul/minneapolis=>twin cities)
			}
		}

		public double Price
		{
			get
			{
				return (Math.Round(_Price));
			}
			set
			{
				_Price = value;	
			}
		}

		public string StartingCity
		{
			get
			{
				return _StartingCity;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(nameof(value));
				}
				_StartingCity = value;
			}
		}

		public string Mode
		{
			get
			{
				return _Mode;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException(nameof(value));
				}
				_Mode = value;
			}
		}
		public string URL
		{
			get
			{
				return _URL;
			}
			set
			{
				_URL = value;
			}
		}
		public double Lat
		{
			get
			{
				return _Lat;
			}
			set
			{
				_Lat = value;
			}
		}
		public double Long
		{
			get
			{
				return _Long;
			}
			set
			{
				_Long = value;
			}
		}
		public Route(string destinationCity, string startingCity, string mode, double? price, double? lat, double? lon, string? url)
		{
				
			DestinationCity = destinationCity;
			StartingCity = startingCity;
			Mode = mode;
			Price = (double)price;
			Lat = (double)lat;
			Long = (double)lon;
			URL = url;

		}
		public string PrintRoute()
        {
			return (StartingCity+" => "+DestinationCity+" for $"+Price+" "+URL);
        }

		//operator overriding for comparing if routes are same, does not check starting city because assumption is route file contains only the local metro area (which might be a different city than user's input)
        public static bool operator ==(Route a, Route b) 
		{
			try
			{
				if ((a.DestinationCity == b.DestinationCity) && (a.Price == b.Price) && (a.Mode == b.Mode))
					return true;
					}
			catch(System.NullReferenceException)
			{
				return false;
					}
			return false;
				}
		public static bool operator !=(Route a, Route b)
		{
			if ((a.DestinationCity != b.DestinationCity) || (a.Price != b.Price) || (a.Mode != b.Mode))
				return true;
			return false;
		}
	}
}
