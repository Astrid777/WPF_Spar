using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp20.Models;

namespace WpfApp20
{


    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {

        forecastContext db;
        public event CancelEventHandler Closing;

        private List<forecast> forecastsList;

        public Page1()
        {
            InitializeComponent();

            db = new forecastContext();
            db.forecasts.Load(); // загружаем данные

            forecastsList = db.forecasts.ToList();

            forecastGrid.ItemsSource = db.forecasts.Local.ToBindingList(); // устанавливаем привязку к кэшу
            Closing += MainWindow_Closing;

            //выбрать список всех точек
            comboBox1.ItemsSource = selectInventId();

            //посчитать выборку
            getCalculations(getIndex());

        }

        private List<string> selectInventId()
        {
            List<string> lstResult = forecastsList.Select(p => p.InventLocationId).Distinct().ToList();
            lstResult.Add("Все");

            return lstResult;
        }


        private List<forecast> getIndex()
        {
            List<forecast> forecasts = new List<forecast>();

            if (comboBox1.SelectedValue.ToString() == "Все")
                forecasts = forecastsList;
            else
                forecasts = forecastsList.Where(p => p.InventLocationId == comboBox1.SelectedValue.ToString()).ToList();

            return forecasts;

        }

        private void getCalculations(List<forecast> forecasts)
        {
            //Avg(qty19)
            tb19Res.Text = forecasts.Average(p => p.qty19).ToString();

            //Avg(qty20)
            tb20Res.Text = forecasts.Average(p => p.qty20).ToString();


            List<float> f20 = new List<float>();

            //ошибка прогноза avg( abs(QTY20 - FORECAST20)/ QTY20))
            foreach (var f in getIndex())
            {
                if (f.qty20 == 0)
                    f20.Add(0);
                else
                    f20.Add(Math.Abs(f.qty20 - f.forecast20) / f.qty20);
            }

            forecastRes.Text = f20.Average().ToString();

        }


        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var frc = new List<forecast>();

            frc = getIndex();
            forecastGrid.ItemsSource = frc;

            getCalculations(frc);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        //сохранит после внесения изменений
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (forecastGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < forecastGrid.SelectedItems.Count; i++)
                {
                    forecast phone = forecastGrid.SelectedItems[i] as forecast;
                    if (phone != null)
                    {
                        db.forecasts.Remove(phone);
                    }
                }
            }
            db.SaveChanges();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
