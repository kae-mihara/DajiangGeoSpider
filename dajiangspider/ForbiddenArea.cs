using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dajiangspider
{
    /// <summary>
    /// 序列化/反序列化方式
    /// </summary>
    public enum SerializeType
    {
        Binary = 0,
        SOAP = 1,
        XML = 2,
    }

    [Serializable]
    public class ForbiddenArea
    {
        /// <summary>
        /// 国家
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 中心点纬度
        /// </summary>
        public double lat { get; set; }

        /// <summary>
        /// 中心点经度
        /// </summary>
        public double lng { get; set; }

        /// <summary>
        /// 未知
        /// </summary>
        public double radius { get; set; }

        public List<main_area> areas { get; set; }

        public string status { get; set; }

        public DJIHeader header { get; set; }

        public ForbiddenArea()
        {
            areas = new List<main_area>();
            //areas = new main_area();
            header = new DJIHeader();
        }
    }

    [Serializable]
    public class DJIHeader
    {
        public string date { get; set; }

        [JsonProperty(PropertyName = "content-type")]
        public string contentType { get; set; }

        [JsonProperty(PropertyName = "transfer-encoding")]
        public string transferEncoding { get; set; }

        public string connection { get; set; }

        [JsonProperty(PropertyName = "x-frame-options")]
        public string frameOptions { get; set; }

        [JsonProperty(PropertyName = "x-xss-protection")]
        public string xssProtection { get; set; }

        [JsonProperty(PropertyName = "x-content-type-options")]
        public string contentTYpeOptions { get; set; }

        public string etag { get; set; }

        [JsonProperty(PropertyName = "cache-control")]
        public string cacheControl { get; set; }

        [JsonProperty(PropertyName = "x-request-id")]
        public string requestId { get; set; }

        [JsonProperty(PropertyName = "x-runtime")]
        public string runtime { get; set; }

        [JsonProperty(PropertyName = "x-nw-upstream-latency")]
        public string upstreamLatency { get; set; }

        [JsonProperty(PropertyName = "x-nw-proxy-latency")]
        public string proxyLatency { get; set; }

        public string via { get; set; }
    }

    public class main_area
    {
        public int level { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        public double radius { get; set; }

        public string name { get; set; }

        public int area_id { get; set; }

        public int type { get; set; }

        public int shape { get; set; }

        public int begin_at { get; set; }

        public int end_at { get; set; }

        public string country { get; set; }

        public string polygon_points { get; set; }

        public string color { get; set; }

        public string city { get; set; }

        public string url { get; set; }

        public List<sub_area> sub_areas { get; set; }

        public main_area()
        {
            sub_areas = new List<sub_area>();
        }
    }

    public class sub_area
    {
        public int shape { get; set; }

        public List<double[][]> polygon_points { get; set; } = new List<double[][]>();

        public string color { get; set; }
    }
}
