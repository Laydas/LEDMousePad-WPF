using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        byte bowRed, bowBlue, bowGreen;

        public MainWindow()
        {
            CreateBrushes();
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
            comboBox.SelectedIndex = 0;
            sliderRed.Value = redMax;
            sliderGreen.Value = greenMax;
            sliderBlue.Value = blueMax;
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
                OpenColor();
                InitializeTimer();
                rectOuter.Visibility = Visibility.Visible;
                rectInner.Visibility = Visibility.Visible;
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
            }
            else if(fromcbx == "Rainbow")
            {
                CloseColor();
                InitializeTimer();
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
                bowRed = 255;
                bowGreen = 0;
                bowBlue = 0;
            }
            else if(fromcbx == "Solid")
            {
                StopTimer();
                OpenColor();
                counter = 100;
                slider.Visibility = Visibility.Hidden;
                labelSpeed.Visibility = Visibility.Hidden;
                updateBrush.Color = solidBrush.Color;
                rectColor.Fill = updateBrush;
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Timer1.Interval = TimeSpan.FromMilliseconds(slider.Value);
        }

        private void textValRed_TextChanged(object sender, TextChangedEventArgs e)
        {
            int redVal = Convert.ToInt32(textValRed.Text);
            if(redVal > 255)
            {
                textValRed.Text = "255";
            }
            red = Convert.ToByte(textValRed.Text);
            solidBrush.Color = Color.FromRgb(red, green, blue);
        }

        private void textValGreen_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Convert.ToInt32(textValGreen.Text) > 255)
            {
                textValGreen.Text = "255";
            }
            green = Convert.ToByte(textValGreen.Text);
            solidBrush.Color = Color.FromRgb(red, green, blue);
        }

        private void textValBlue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Convert.ToInt32(textValBlue.Text) > 255)
            {
                textValBlue.Text = "255";
            }
            blue = Convert.ToByte(textValBlue.Text);
            solidBrush.Color = Color.FromRgb(red, green, blue);
        }

        private void InitializeTimer()
        {
            Timer1.Start();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            redMax = Convert.ToDouble(textValRed.Text);
            greenMax = Convert.ToDouble(textValGreen.Text);
            blueMax = Convert.ToDouble(textValBlue.Text);
            counter = 100;
            updateBrush.Color = solidBrush.Color;
            topBrushColor.Color = Color.FromRgb(red, green, blue);
            rectTop.Fill = topBrush;
        }

        bool goingUp = true;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex == 1)
            {
                if (goingUp)
                {
                    counter++;
                    if (counter > 100)
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
            }

            else if (comboBox.SelectedIndex == 2)
            {

                if (bowRed == 255 && bowGreen < 255 && bowBlue == 0)
                {
                    bowGreen = (byte)(bowGreen + 5);
                }
                else if(bowRed > 0 && bowGreen == 255)
                {
                    bowRed = (byte)(bowRed - 5);
                }
                else if(bowGreen == 255 && bowBlue < 255)
                {
                    bowBlue = (byte)(bowBlue + 5);
                }
                else if(bowGreen > 0 && bowBlue == 255)
                {
                    bowGreen = (byte)(bowGreen - 5);
                }
                else if(bowBlue == 255 && bowRed < 255)
                {
                    bowRed = (byte)(bowRed + 5);
                }
                else if(bowRed == 255 && bowBlue > 0)
                {
                    bowBlue = (byte)(bowBlue - 5);
                }
                updateBrush.Color = Color.FromRgb(bowRed, bowGreen, bowBlue);
                rectColor.Fill = updateBrush;
            }
        }

        private void StopTimer()
        {
            Timer1.Stop();
            rectColor.Fill = solidBrush;
        }
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[0-9]+");
            e.Handled = !regex.IsMatch(e.Text);
        }
        private void CloseColor()
        {
            rectOuter.Visibility = Visibility.Hidden;
            rectInner.Visibility = Visibility.Hidden;
            buttonUpdate.Visibility = Visibility.Hidden;
            rectRed.Visibility = Visibility.Hidden;
            rectGreen.Visibility = Visibility.Hidden;
            rectBlue.Visibility = Visibility.Hidden;
            textValRed.Visibility = Visibility.Hidden;
            textValGreen.Visibility = Visibility.Hidden;
            textValBlue.Visibility = Visibility.Hidden;
            sliderRed.Visibility = Visibility.Hidden;
            sliderGreen.Visibility = Visibility.Hidden;
            sliderBlue.Visibility = Visibility.Hidden;
        }
        private void OpenColor()
        {
            rectOuter.Visibility = Visibility.Visible;
            rectInner.Visibility = Visibility.Visible;
            buttonUpdate.Visibility = Visibility.Visible;
            rectRed.Visibility = Visibility.Visible;
            rectGreen.Visibility = Visibility.Visible;
            rectBlue.Visibility = Visibility.Visible;
            textValRed.Visibility = Visibility.Visible;
            textValGreen.Visibility = Visibility.Visible;
            textValBlue.Visibility = Visibility.Visible;
            sliderRed.Visibility = Visibility.Visible;
            sliderGreen.Visibility = Visibility.Visible;
            sliderBlue.Visibility = Visibility.Visible;
        }


        LinearGradientBrush topBrush = new LinearGradientBrush();
        GradientStop topBrushColor = new GradientStop();
        GradientStop topBrushBlack = new GradientStop();
        LinearGradientBrush leftBrush = new LinearGradientBrush();
        GradientStop leftBrushColor = new GradientStop();
        GradientStop leftBrushBlack = new GradientStop();
        LinearGradientBrush rightBrush = new LinearGradientBrush();
        GradientStop rightBrushColor = new GradientStop();
        GradientStop rightBrushBlack = new GradientStop();
        LinearGradientBrush bottomBrush = new LinearGradientBrush();
        GradientStop bottomBrushColor = new GradientStop();
        GradientStop bottomBrushBlack = new GradientStop();
        RadialGradientBrush topLeft = new RadialGradientBrush();
        GradientStop topLeftColor = new GradientStop();
        GradientStop topLeftBlack= new GradientStop();
        RadialGradientBrush topRight = new RadialGradientBrush();
        GradientStop topRightColor= new GradientStop();
        GradientStop topRightBlack= new GradientStop();
        RadialGradientBrush bottomLeft = new RadialGradientBrush();
        GradientStop bottomLeftColor= new GradientStop();
        GradientStop bottomLeftBlack= new GradientStop();
        RadialGradientBrush bottomRight = new RadialGradientBrush();
        GradientStop bottomRightColor= new GradientStop();
        GradientStop bottomRightBlack= new GradientStop();

        public void CreateBrushes()
        {
            topBrush.StartPoint = new Point(0, 1);
            topBrush.EndPoint = new Point(0, 0);
            topBrushColor.Color = Color.FromRgb(255, 0, 0);
            topBrushColor.Offset = 0.0;
            topBrushBlack.Color = Colors.Black;
            topBrushBlack.Offset = 1.0;
            topBrush.GradientStops.Add(topBrushColor);
            topBrush.GradientStops.Add(topBrushBlack);
            // add all the other directional brushes to this list
        }
    }
}
