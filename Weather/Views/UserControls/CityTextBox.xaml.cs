using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace Weather.Views.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CityTextBox.xaml
    /// </summary>
    public partial class CityTextBox : UserControl
    {
        public CityTextBox()
        {
            InitializeComponent();
            
        }
        //обраточик изменения текста в CityTB
        private void CityTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            //если строка пустая или нулевая то показывать предложении ввода, иначе — скрывать
            if (string.IsNullOrEmpty(CityTB.Text))
            {
                CityPlaceHolder.Visibility = Visibility.Visible;
            }
            else CityPlaceHolder.Visibility = Visibility.Hidden;
        }

        //обработчик нажатия на кнопку Forecast
        private void ForecastBtn_Click(object sender, RoutedEventArgs e)
        {
            
            //доступ к главному 
            MainWindow main = (MainWindow)Window.GetWindow(this);
            try
            {
                //попытка получить метеоданные
                WeatherModel wm = WeatherAPI.GetForecast(CityTB.Text.Trim());
                //отображение на главном окне
                main.Temperature.Text = $"Temperature: {(int)(wm.main.temp - 273.15)}°C";
                main.Description.Text = $"Description: {wm.weather[0].description}";
                main.WindSpeed.Text = $"Wind speed: {wm.wind.speed}m/s";
            }
            catch
            {
                //вывод сообщения с ошибкой
                MessageBox.Show("The city coordinates not found", "Error",MessageBoxButton.OK,MessageBoxImage.Error);
                //отображение на главном экране
                CityTB.Text = null;
                main.Temperature.Text = $"Temperature:";
                main.Description.Text = $"Description:";
                main.WindSpeed.Text = $"Wind speed:";
            }
        }
    }
}
