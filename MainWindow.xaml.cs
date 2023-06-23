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

namespace MoonCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Selected_Date.SelectedDate = DateTime.Today;
            double age = AgeMoon();
            age_moon.Text = age.ToString("0.00");
            phase_moon.Text = PhaseMoon(age);
            direction_moon.Text = DirectionMoon(age);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            slider_text.Text = $"Знаков после запятой: {slider.Value.ToString("0")}";
            //double age = AgeMoon();
            //age_moon.Text = age.ToString(Format_Age());

        }

        private string Format_Age()
        {
            string form;
            if (slider.Value.ToString("0") == "0") form = "0";
            else if (slider.Value.ToString("0") == "1") form = "0.0";
            else if (slider.Value.ToString("0") == "2") form = "0.00";
            else if (slider.Value.ToString("0") == "3") form = "0.000";
            else if (slider.Value.ToString("0") == "4") form = "0.0000";
            else if (slider.Value.ToString("0") == "5") form = "0.00000";
            else if (slider.Value.ToString("0") == "6") form = "0.000000";
            else if (slider.Value.ToString("0") == "7") form = "0.0000000";
            else if (slider.Value.ToString("0") == "8") form = "0.00000000";
            else if (slider.Value.ToString("0") == "9") form = "0.000000000";
            else form = "0.0000000000";
            return form;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double age = AgeMoon();
            age_moon.Text = age.ToString(Format_Age());
            phase_moon.Text = PhaseMoon(age);
            direction_moon.Text = DirectionMoon(age);
        }

        private void Calendar_SelectedDataChanged(object sender, SelectionChangedEventArgs e)
        {
            double age = AgeMoon();
            age_moon.Text = age.ToString(Format_Age());
            phase_moon.Text = PhaseMoon(age);
            direction_moon.Text = DirectionMoon(age);
        }

        private double AgeMoon()
        {
            DateTime dateTime = Selected_Date.SelectedDate.Value;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;
            decimal temp = (12 - month) / 10;
            int jy = (int)(year - Math.Floor(temp));
            int jm = month + 9;
            if (jm > 9) jm = jm - 12;
            temp = (decimal)(365.25 * (jy + 4712));
            int k1 = (int)Math.Floor(temp);
            temp = (decimal)(30.6 * jm + 0.5);
            int k2 = (int)Math.Floor(temp);
            temp = Math.Floor((decimal)(jy / 100 + 49));
            int k3 = (int)Math.Floor(temp * 0.75m) - 38;
            int jd = k1 + k2 + day + 59;
            double ip = Normilize((jd - 2451550.1) / 29.530588853);
            double ageMoon = ip * 29.53;
            return ageMoon;
        }

        private string PhaseMoon(double ageMoon)
        {
            string phaseMoon;
            if (ageMoon < 1.84566) phaseMoon = "NEW";
            else if (ageMoon < 5.53699) phaseMoon = "Waxing crescent";
            else if (ageMoon < 9.22831) phaseMoon = "First quarter";
            else if (ageMoon < 12.91963) phaseMoon = "Waxing gibbous";
            else if (ageMoon < 16.61096) phaseMoon = "FULL";
            else if (ageMoon < 20.30228) phaseMoon = "Waning gibbous";
            else if (ageMoon < 23.99361) phaseMoon = "Last quarter";
            else if (ageMoon < 27.68493) phaseMoon = "Waning crescent";
            else phaseMoon = "NEW";
            return phaseMoon;
        }

        private string DirectionMoon(double ageMoon)
        {
            if (ageMoon < 16.61096) return "Растущая Луна";
            else return "Убывающая Луна";
        }

        private static double Normilize(double value)
        {
            value = value % 1;
            if (value < 0)
            {
                value += 1;
            }
            return value;
        }
    }
}
