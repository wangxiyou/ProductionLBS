using System;
using System.Collections.Generic;
using System.Text;

namespace PB.PLBS.Domain
{
    /// <summary>
    /// 描述一个地理位置
    /// </summary>
    [Serializable]
    public struct Location
    {
        private string _addresss;
        private double _longitude;
        private double _latitude;
        private bool _isEmpty;

        public static Location Empty = CreateEmpty();

        private Location(string address,double log,double lat,bool isEmpty)
        {
            _addresss = address;
            _longitude = log;
            _latitude = lat;
            _isEmpty = isEmpty;
        }

        public string Addresss { get => string.IsNullOrEmpty(_addresss)?string.Empty:_addresss; set => _addresss = value; }
        public double Longitude { get => _longitude; set => _longitude = value; }
        public double Latitude { get=> _latitude; set=> _latitude = value; }
        public bool IsEmpty { get => _isEmpty; set => _isEmpty = value; }

        public static Location CreateEmpty()
        {
            Location result = new Location(string.Empty, 0, 0, true);
            return result;
        }

        public static Location Create(string address, double log, double lat)
        {
            Location result = new Location(address, log, lat, false);
            return result;
        }
    }
}
