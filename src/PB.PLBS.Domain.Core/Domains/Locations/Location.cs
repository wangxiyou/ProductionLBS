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
        private string m_Addresss;
        private double m_Longitude;
        private double m_Latitude;
        private bool m_IsEmpty;

        public static Location Empty = CreateEmpty();

        private Location(string address,double log,double lat,bool isEmpty)
        {
            m_Addresss = address;
            m_Longitude = log;
            m_Latitude = lat;
            m_IsEmpty = isEmpty;
        }

        public string Addresss { get => string.IsNullOrEmpty(m_Addresss)?string.Empty:m_Addresss; set => m_Addresss = value; }
        public double Longitude { get => m_Longitude; set => m_Longitude = value; }
        public double Latitude { get => m_Latitude; set => m_Latitude = value; }
        public bool IsEmpty { get => m_IsEmpty; set => m_IsEmpty = value; }

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
