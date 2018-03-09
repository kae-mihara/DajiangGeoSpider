﻿using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dajiangspider
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel()
        {
            _level = "1,2,3";
            _lat = "39";
            _lng = "112";
            _radius = "100000";
            _drone = "spark";
            _country = "CN";
            
        }

        public async Task RequestAsync()
        {
            Processing = true;
            Result = "";
            CurrentUri = 
                $"{baseUri}" +
                $"lat={Lat}" +
                $"&lng={Lng}" +
                $"&country={Country}" +
                $"&search_radius={Radius}" +
                $"&level={Level}" +
                $"&drone={Drone}" +
                $"&zones_mode=total";
            var message = await client.GetAsync(CurrentUri, System.Net.Http.HttpCompletionOption.ResponseHeadersRead);
            Result = $"{message.StatusCode}";
            if (message.IsSuccessStatusCode)
            {
                File.WriteAllText($"{_uri}.json",await message.Content.ReadAsStringAsync());
                Result = $"已保存到{Environment.CurrentDirectory}";
            }
            Processing = false;
        }

        private const string baseUri = @"https://www.dji.com/cn/api/geo/areas?";

        private System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new System.Net.Http.HttpClientHandler()
        {
            UseCookies = true,
            AllowAutoRedirect = true,
            
        });
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

        private string _lat;
        public string Lat
        {
            get => _lat;
            set { _lat = value; OnPropertyChanged("Lat"); }
        }
        private string _lng;
        public string Lng
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
        private string  _country;
        public string  Country
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