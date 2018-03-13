using GMap.NET.MapProviders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace dajiangspider
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private static readonly string DefaultPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\DjiGeo\";

        private const string baseUri = @"https://www.dji.com/cn/api/geo/areas?";

        private HttpClient client = new HttpClient(new HttpClientHandler()
        {
            UseCookies = true,
            AllowAutoRedirect = true,
        });

        FlySafe _flySafe = FlySafe.CreateInstance();

        List<ForbiddenArea> tempList = new List<ForbiddenArea>();

        public MainWindowViewModel()
        {
            _level = "1,2,3";
            _lat = 39;
            _lng = 112;
            _radius = "100000";
            _drone = "spark";
            _country = "CN";

            client.DefaultRequestHeaders.Add("accept", @"application/json");
            client.DefaultRequestHeaders.Add("user-agent", @"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36 Edge/16.16299");

            if (!Directory.Exists(DefaultPath))
            {
                Directory.CreateDirectory(DefaultPath);
            }
        }

        public async Task RequestAsync()
        {
            Processing = true;
            Result = "";
            CurrentUri =
                $"{baseUri}" +
                $"lng={Lng}" +
                $"&lat={Lat}" +
                $"&country={Country}" +
                $"&search_radius={Radius}" +
                $"&drone={Drone}" +
                $"&level={Level}" +
                $"&zones_mode=total";

            var message = await client.GetAsync(CurrentUri, HttpCompletionOption.ResponseHeadersRead);
            
            Console.WriteLine("当前的网络接口API:{0}", CurrentUri);
            Result = $"{message.StatusCode}";


            if (message.IsSuccessStatusCode)
            {
                var content = await message.Content.ReadAsStringAsync();
                File.WriteAllText($"{DefaultPath}{Lat:f2}_{Lng:f2}.json", content);
                Result = $"已保存到\n{DefaultPath}";

                _flySafe.ForbiddenAreasData = JsonConvert.DeserializeObject<ForbiddenArea>(content);

                tempList.Add(_flySafe.ForbiddenAreasData);
            }

            Processing = false;
        }

        private bool _processing;
        public bool Processing
        {
            get => _processing;
            set { _processing = value; OnPropertyChanged("Processing"); }
        }

        private string _uri;
        public string CurrentUri
        {
            get => _uri;
            set { _uri = value; OnPropertyChanged("CurrentUri"); }
        }

        private string _result;
        public string Result
        {
            get => _result;
            set { _result = value; OnPropertyChanged("Result"); }
        }

        private string _level;
        public string Level
        {
            get => _level;
            set { _level = value; OnPropertyChanged(nameof(Level)); }
        }

        private double _lat;
        public double Lat
        {
            get => _lat;
            set { _lat = value; OnPropertyChanged("Lat"); }
        }

        private double _lng;
        public double Lng
        {
            get => _lng;
            set { _lng = value; OnPropertyChanged("Lng"); }
        }

        private string _radius;
        public string Radius
        {
            get => _radius;
            set { _radius = value; OnPropertyChanged("Radius"); }
        }

        private string _drone;
        public string Drone
        {
            get => _drone;
            set { _drone = value; OnPropertyChanged("Drone"); }
        }

        private string _country;
        public string Country
        {
            get => _country;
            set { _country = value; OnPropertyChanged("Country"); }
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
