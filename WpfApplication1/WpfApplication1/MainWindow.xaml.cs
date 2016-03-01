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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SolidColorBrush updateBrush = new SolidColorBrush();
        SolidColorBrush solidBrush = new SolidColorBrush();
        System.Windows.Threading.DispatcherTimer Timer1 = new System.Windows.Threading.DispatcherTimer();
        System.Timers.Timer Timer2 = new System.Timers.Timer();
        byte red, green, blue;
        double redMax, greenMax, blueMax;
        int counter;

        public MainWindow()
        {
            InitializeComponent();
            Timer1.Interval = TimeSpan.FromMilliseconds(5);
            Timer1.Tick += new EventHandler(Timer1_Tick);
            redMax = 120;
            greenMax = 255;
            blueMax = 50;
            red = Convert.ToByte(redMax);
            green = Convert.ToByte(greenMax);
            blue = Convert.ToByte(blueMax);
            updateBrush.Color = Color.FromRgb(red, green, blue);
            solidBrush.Color = Color.FromRgb(red, green, blue);
            rectColor.Fill = updateBrush;
            counter = 100;
            rectOuter.Fill = solidBrush;
            textValRed.Text = Convert.ToString(red);
            textValGreen.Text = Convert.ToString(green);
            textValBlue.Text = Convert.ToString(blue);
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {

        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fromcbx = comboBox.SelectedValue.ToString();
            string[] choiceSplit = fromcbx.Split(':');
            fromcbx = choiceSplit[1].Substring(1);
            
            if(fromcbx == "Breathing")
            {
                InitializeTimer();
                rectOuter.Visibility = Visibility.Visible;
                rectInner.Visibility = Visibility.Visible;
            }
            else if(fromcbx == "Rainbow")
            {
                StopTimer();
                rectOuter.Visibility = Visibility.Hidden;
                rectInner.Visibility = Visibility.Hidden;
            }
            else if(fromcbx == "Solid")
            {
                StopTimer();
                counter = 100;
                rectOuter.Visibility = Visibility.Visible;
                rectInner.Visibility = Visibility.Visible;
                rectColor.Fill = solidBrush;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Timer1.Interval = TimeSpan.FromMilliseconds(slider.Value);
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        bool goingUp = true;
        private void InitializeTimer()
        {
            Timer1.Start();
        }

        
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (goingUp)
            {
                counter++;
                if(counter > 100)
                {
                    counter = 99;
                    goingUp = false;
                }
            }
            else
            {
                counter--;
                if (counter < 0)
                {
                    counter = 1;
                    goingUp = true;
                }
            }
            byte red = Convert.ToByte(counter * (redMax / 100));
            byte green = Convert.ToByte(counter * (greenMax / 100));
            byte blue = Convert.ToByte(counter * (blueMax / 100));
            updateBrush.Color = Color.FromRgb(red, green, blue);
            rectColor.Fill = updateBrush;

            //labelTest.Content = DateTime.Now.ToLongTimeString();
        }

        private void StopTimer()
        {
            Timer1.Stop();
        }
    }
}
