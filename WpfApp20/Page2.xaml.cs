using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using WpfApp20.Models;

namespace Wpf.CartesianChart.Basic_Bars
{
    public partial class BasicColumn : UserControl, INotifyPropertyChanged
    {
        forecastContext db;

        private bool _series2019Visibility;
        private bool _series2020Visibility;


        public BasicColumn()
        {
            InitializeComponent();

            //ставим на свойства по умолчанию true
            Series2019Visibility = true;
            Series2020Visibility = true;


            db = new forecastContext();
            db.forecasts.Load(); // загружаем данные


            List<float> l19 = db.forecasts.Select(p => p.qty19).Distinct().ToList();
            List<float> l20 = db.forecasts.Select(p => p.qty20).Distinct().ToList();

            List<DateTime> listDates;// = new List<DateTime>(); 

            listDates = db.forecasts.Select(p => p.Date).OrderBy(p => p).Distinct().ToList();

            SeriesCollection = new SeriesCollection();

            //Выборка Value1
            Dictionary<string, float> dateControl = new Dictionary<string, float>();

            foreach (var date in listDates)
            {
                var f = db.forecasts.Where(p => p.Date == date && p.InventLocationId.Contains("04_ОСН")).Select(p => p.qty19).ToList();
                if (f.Count > 0)
                {
                    dateControl.Add(date.ToShortDateString(), f[0]);
                }
                else
                    dateControl.Add(date.ToShortDateString(), 0); //нет данных по дате
            }

            Values1 = dateControl.Values.AsChartValues();
            dateControl.Clear();


            //Выборка Value2
            foreach (var m in listDates)
            {
                var f = db.forecasts.Where(p => p.Date == m && p.InventLocationId.Contains("09_ОСН")).Select(p => p.qty19).ToList();
                if (f.Count > 0)
                {
                    dateControl.Add(m.ToShortDateString(), f[0]);
                }
                else
                    dateControl.Add(m.ToShortDateString(), 0); //нет данных по дате
            }

            Values2 = dateControl.Values.AsChartValues();
            dateControl.Clear();


            //Выборка Value3
            foreach (var m in listDates)
            {
                var f = db.forecasts.Where(p => p.Date == m && p.InventLocationId.Contains("04_ОСН")).Select(p => p.qty20).ToList();
                if (f.Count > 0)
                {
                    dateControl.Add(m.ToShortDateString(), f[0]);
                }
                else
                    dateControl.Add(m.ToShortDateString(), 0); //нет данных по дате
            }

            Values3 = dateControl.Values.AsChartValues();
            dateControl.Clear();


            //Выборка Value4
            foreach (var m in listDates)
            {
                var f = db.forecasts.Where(p => p.Date == m && p.InventLocationId.Contains("09_ОСН")).Select(p => p.qty20).ToList();
                if (f.Count > 0)
                {
                    dateControl.Add(m.ToShortDateString(), f[0]);
                }
                else
                    dateControl.Add(m.ToShortDateString(), 0); //нет данных по дате
            }

            Values4 = dateControl.Values.AsChartValues();

            //список дат на оси x
            Labels = dateControl.Keys.ToArray();
            Step = 1;

            DataContext = this;
        }


        public SeriesCollection SeriesCollection { get; set; }

        public ChartValues<float> Values1 { get; set; }
        public ChartValues<float> Values2 { get; set; }
        public ChartValues<float> Values3 { get; set; }
        public ChartValues<float> Values4 { get; set; }

        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public int Step { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Series2019Visibility
        {
            get { return _series2019Visibility; }
            set
            {
                _series2019Visibility = value;
                OnPropertyChanged("Series2019Visibility");
            }
        }

        public bool Series2020Visibility
        {
            get { return _series2020Visibility; }
            set
            {
                _series2020Visibility = value;
                OnPropertyChanged("Series2020Visibility");
            }
        }


    }
}