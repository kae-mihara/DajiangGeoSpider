using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dajiangspider
{
    public static class GMapEx
    {
        public static PointLatLng ToLatLng(this double[] p)
        {
            if (p.Length < 2) return new PointLatLng();
            return new PointLatLng(p[0],p[1]);
        }
    }
}
