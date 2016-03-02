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
using System.IO.Ports;

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
        byte red, green, blue;
        double redMax, greenMax, blueMax;
        int counter;
        byte bowRed, bowBlue, bowGreen;
        SerialPort sp = new SerialPort();
        //string buffer { get; set; }
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
        RadialGradientBrush cornerBrush = new RadialGradientBrush();
        GradientStop cornerBrushColor = new GradientStop();
        GradientStop cornerBrushBlack = new GradientStop();
        //private bool connected;
        //private string connectedPort;

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
            
            if(fromcbx == "Solid Breathing")
            {
                OpenColor();
                InitializeTimer();
                rectOuter.Visibility = Visibility.Visible;
                rectInner.Visibility = Visibility.Visible;
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
            }
            else if(fromcbx == "Solid Rainbow")
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
                UpdateColor(red, green, blue);
                FillRectangle();
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
            UpdateColor(red, green, blue);
            FillRectangle();
            updateBrush.Color = solidBrush.Color;
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
                UpdateColor(red, green, blue);
                FillRectangle();
                updateBrush.Color = Color.FromRgb(red, green, blue);
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
                UpdateColor(bowRed, bowGreen, bowBlue);
                FillRectangle();
                updateBrush.Color = Color.FromRgb(bowRed, bowGreen, bowBlue);
            }
        }

        private void StopTimer()
        {
            Timer1.Stop();
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

        public void CreateBrushes()
        {
            // Top Rectangle Brush
            topBrush.StartPoint = new Point(0, 1);
            topBrush.EndPoint = new Point(0, 0);
            topBrushColor.Color = Color.FromRgb(255, 255, 255);
            topBrushColor.Offset = 0.0;
            topBrushBlack.Color = Colors.Black;
            topBrushBlack.Offset = 1.0;
            topBrush.GradientStops.Add(topBrushColor);
            topBrush.GradientStops.Add(topBrushBlack);

            // Left Rectangle Brush
            leftBrush.StartPoint = new Point(1, 0);
            leftBrush.EndPoint = new Point(0, 0);
            leftBrushColor.Color = Color.FromRgb(255, 255, 255);
            leftBrushColor.Offset = 0.0;
            leftBrushBlack.Color = Colors.Black;
            leftBrushBlack.Offset = 1.0;
            leftBrush.GradientStops.Add(leftBrushColor);
            leftBrush.GradientStops.Add(leftBrushBlack);

            // Bottom Rectangle Brush
            bottomBrush.StartPoint = new Point(0, 0);
            bottomBrush.EndPoint = new Point(0, 1);
            bottomBrushColor.Color = Color.FromRgb(255, 255, 255);
            bottomBrushColor.Offset = 0.0;
            bottomBrushBlack.Color = Colors.Black;
            bottomBrushBlack.Offset = 1.0;
            bottomBrush.GradientStops.Add(bottomBrushColor);
            bottomBrush.GradientStops.Add(bottomBrushBlack);

            // Right Rectangle Brush
            rightBrush.StartPoint = new Point(0, 0);
            rightBrush.EndPoint = new Point(1, 0);
            rightBrushColor.Color = Color.FromRgb(255, 255, 255);
            rightBrushColor.Offset = 0.0;
            rightBrushBlack.Color = Colors.Black;
            rightBrushBlack.Offset = 1.0;
            rightBrush.GradientStops.Add(rightBrushColor);
            rightBrush.GradientStops.Add(rightBrushBlack);

            // Corner Brush
            cornerBrush.GradientOrigin = new Point(0.5, 0.5);
            cornerBrushColor.Color = Color.FromRgb(255, 255, 255);
            cornerBrushColor.Offset = 0.0;
            cornerBrushBlack.Color = Colors.Black;
            cornerBrushBlack.Offset = 1.0;
            cornerBrush.GradientStops.Add(cornerBrushColor);
            cornerBrush.GradientStops.Add(cornerBrushBlack);
        }

        private void UpdateColor(byte red, byte green, byte blue)
        {
            topBrushColor.Color = Color.FromRgb(red, green, blue);
            leftBrushColor.Color = Color.FromRgb(red, green, blue);
            rightBrushColor.Color = Color.FromRgb(red, green, blue);
            bottomBrushColor.Color = Color.FromRgb(red, green, blue);
            cornerBrushColor.Color = Color.FromRgb(red, green, blue);
        }

        private void FillRectangle()
        {
            rectTop.Fill = topBrush;
            rectLeft.Fill = leftBrush;
            rectRight.Fill = rightBrush;
            rectBottom.Fill = bottomBrush;
            ellTopLeft.Fill = cornerBrush;
            ellTopRight.Fill = cornerBrush;
            ellBottomLeft.Fill = cornerBrush;
            ellBottomRight.Fill = cornerBrush;
        }

        private Queue<byte> receivedData = new Queue<byte>();
        //TESTING AREA FOR ARDUINO INTEGRATION
        private void CONNECT_Click(object sender, RoutedEventArgs e)
        {
            //buffer = "";
            //connected = false;
            try
            {
                sp.PortName = "COM3";
                sp.ReadTimeout = 500;
                sp.BaudRate = 9600;
                sp.DataReceived += new SerialDataReceivedEventHandler(dataReceived);
                sp.Open();
                sp.Write("HEY~");
            }
            catch(Exception)
            {
                MessageBox.Show("ERROR");
            }
            //if (connected == true)
            //{
            //    CONNECT.Visibility = Visibility.Hidden;
            //    labelStatus.Content = "Connected";
            //} 
        }

        private void ON_Click(object sender, RoutedEventArgs e)
        {
            sp.Write("1~");
        }

        private void OFF_Click(object sender, RoutedEventArgs e)
        {
            sp.Write("0~");
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //buffer += sp.ReadExisting();
            //if(buffer.Contains("84652345shae"))
            //{
                //connected = true;
                //connectedPort = sp.PortName;
            //}
        }
    }
}
