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
        int counter, count, place;
        byte bowRed, bowBlue, bowGreen;
        SerialPort sp = new SerialPort();
        string buffer { get; set; }
        private bool connected;
        string toPadString, padMode, padColor;
        bool consecutiveAttempt;
        LinearGradientBrush brushTL, brushT, brushTR, brushR, brushBR, brushB, brushBL, brushL;
        GradientStop stopTL1, stopTL2;
        GradientStop stopT1, stopT2, stopT3, stopT4, stopT5, stopT6;
        GradientStop stopTR1, stopTR2;
        GradientStop stopR1, stopR2, stopR3, stopR4, stopR5, stopR6;
        GradientStop stopBR1, stopBR2;
        GradientStop stopB1, stopB2, stopB3, stopB4, stopB5, stopB6;
        GradientStop stopBL1, stopBL2;
        public Color sendColor;
        GradientStop stopL1, stopL2, stopL3, stopL4, stopL5, stopL6;

        private void btnDirection_Click(object sender, RoutedEventArgs e)
        {
            if(rI == 1)
            {
                rI = -1;
            }
            else
            {
                rI = 1;
            }
        }

        Color newColor;
        Color[] points = new Color[24];
        Color[] staticPoints = new Color[24];
        SolidColorBrush[] buttonBrush = new SolidColorBrush[24];
        byte[,] now = new byte[24, 3];
        int[,] range = new int[24, 3];
        float[,] step = new float[24, 3];
        float[,] current = new float[24, 3];
        int rI = 1;

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
            counter = 100;
            rectOuter.Fill = solidBrush;
            textValRed.Text = Convert.ToString(red);
            textValGreen.Text = Convert.ToString(green);
            textValBlue.Text = Convert.ToString(blue);
            comboBox.SelectedIndex = 0;
            sliderRed.Value = redMax;
            sliderGreen.Value = greenMax;
            sliderBlue.Value = blueMax;
            connected = false;
            ConnectSerial();
            WaitConnect();
            padColor = "12025550";
            createBrushes();
        }

        private void colorRects()
        {
            rectTL.Fill = brushTL;
            rectT.Fill = brushT;
            rectTR.Fill = brushTR;
            rectR.Fill = brushR;
            rectBR.Fill = brushBR;
            rectB.Fill = brushB;
            rectBL.Fill = brushBL;
            rectL.Fill = brushL;
        }

        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            createBrushes();
            int count = 0;
            foreach (var childButton in colorButtons.Children.OfType<Button>())
            {
                childButton.Background = buttonBrush[count];
                count++;
            }
        }

        private void openCP(string name)
        {
            int i = Convert.ToInt32(name);
            sendColor = staticPoints[i - 1];
            var dialog = new ColorPicker(sendColor);
            dialog.Top = this.Top + (this.Height / 2) - (dialog.Height / 2);
            dialog.Left = this.Left + (this.Width / 2) - (dialog.Width / 2);
            if(dialog.ShowDialog() == true)
            {
                newColor = dialog.NewColor;
            }
            if (newColor.R == 0 && newColor.G == 0 & newColor.B == 0)
            {
                newColor = staticPoints[i - 1];
            }
            else
            {
                staticPoints[i - 1] = newColor;
            }
        }

        private void btnColor_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            openCP(content);
            (sender as Button).Background = new SolidColorBrush(newColor);
        }


        private void createBrushes()
        {
            brushTL = new LinearGradientBrush();
            brushT = new LinearGradientBrush();
            brushTR = new LinearGradientBrush();
            brushR = new LinearGradientBrush();
            brushBR = new LinearGradientBrush();
            brushB = new LinearGradientBrush();
            brushBL = new LinearGradientBrush();
            brushL = new LinearGradientBrush();
            stopTL1 = new GradientStop();
            stopTL2 = new GradientStop();
            stopT1 = new GradientStop();
            stopT2 = new GradientStop();
            stopT3 = new GradientStop();
            stopT4 = new GradientStop();
            stopT5 = new GradientStop();
            stopT6 = new GradientStop();
            stopTR1 = new GradientStop();
            stopTR2 = new GradientStop();
            stopR1 = new GradientStop();
            stopR2 = new GradientStop();
            stopR3 = new GradientStop();
            stopR4 = new GradientStop();
            stopR5 = new GradientStop();
            stopR6 = new GradientStop();
            stopBR1 = new GradientStop();
            stopBR2 = new GradientStop();
            stopB1 = new GradientStop();
            stopB2 = new GradientStop();
            stopB3 = new GradientStop();
            stopB4 = new GradientStop();
            stopB5 = new GradientStop();
            stopB6 = new GradientStop();
            stopBL1 = new GradientStop();
            stopBL2 = new GradientStop();
            stopL1 = new GradientStop();
            stopL2 = new GradientStop();
            stopL3 = new GradientStop();
            stopL4 = new GradientStop();
            stopL5 = new GradientStop();
            stopL6 = new GradientStop();

            brushTL.StartPoint = new Point(0, 1);
            brushTL.EndPoint = new Point(1, 0);
            brushT.StartPoint = new Point(0, 0);
            brushT.EndPoint = new Point(1, 0);
            brushTR.StartPoint = new Point(0, 0);
            brushTR.EndPoint = new Point(1, 1);
            brushR.StartPoint = new Point(1, 0);
            brushR.EndPoint = new Point(1, 1);
            brushBR.StartPoint = new Point(1, 0);
            brushBR.EndPoint = new Point(0, 1);
            brushB.StartPoint = new Point(1, 1);
            brushB.EndPoint = new Point(0, 1);
            brushBL.StartPoint = new Point(1, 1);
            brushBL.EndPoint = new Point(0, 0);
            brushL.StartPoint = new Point(0, 1);
            brushL.EndPoint = new Point(0, 0);
            stopTL1.Offset = 0;
            stopTL2.Offset = 1;
            stopT1.Offset = 0;
            stopT2.Offset = 0.2;
            stopT3.Offset = 0.4;
            stopT4.Offset = 0.6;
            stopT5.Offset = 0.8;
            stopT6.Offset = 1;
            stopTR1.Offset = 0;
            stopTR2.Offset = 1;
            stopR1.Offset = 0;
            stopR2.Offset = 0.2;
            stopR3.Offset = 0.4;
            stopR4.Offset = 0.6;
            stopR5.Offset = 0.8;
            stopR6.Offset = 1;
            stopBR1.Offset = 0;
            stopBR2.Offset = 1;
            stopB1.Offset = 0;
            stopB2.Offset = 0.2;
            stopB3.Offset = 0.4;
            stopB4.Offset = 0.6;
            stopB5.Offset = 0.8;
            stopB6.Offset = 1;
            stopBL1.Offset = 0;
            stopBL2.Offset = 1;
            stopL1.Offset = 0;
            stopL2.Offset = 0.2;
            stopL3.Offset = 0.4;
            stopL4.Offset = 0.6;
            stopL5.Offset = 0.8;
            stopL6.Offset = 1;

            // preset all the points to form a simple rainbow
            points[0] = Color.FromRgb(255, 0, 0);
            points[1] = Color.FromRgb(224, 32, 0);
            points[2] = Color.FromRgb(192, 64, 0);
            points[3] = Color.FromRgb(160, 96, 0);
            points[4] = Color.FromRgb(128, 128, 0);
            points[5] = Color.FromRgb(96, 160, 0);
            points[6] = Color.FromRgb(64, 192, 0);
            points[7] = Color.FromRgb(32, 224, 0);
            points[8] = Color.FromRgb(0, 255, 0);
            points[9] = Color.FromRgb(0, 224, 32);
            points[10] = Color.FromRgb(0, 192, 64);
            points[11] = Color.FromRgb(0, 160, 96);
            points[12] = Color.FromRgb(0, 128, 128);
            points[13] = Color.FromRgb(0, 96, 160);
            points[14] = Color.FromRgb(0, 64, 192);
            points[15] = Color.FromRgb(0, 32, 224);
            points[16] = Color.FromRgb(0, 0, 255);
            points[17] = Color.FromRgb(32, 0, 224);
            points[18] = Color.FromRgb(64, 0, 192);
            points[19] = Color.FromRgb(96, 0, 160);
            points[20] = Color.FromRgb(128, 0, 128);
            points[21] = Color.FromRgb(160, 0, 96);
            points[22] = Color.FromRgb(192, 0, 64);
            points[23] = Color.FromRgb(224, 0, 32);

            staticPoints[0] = points[0];
            staticPoints[1] = points[1];
            staticPoints[2] = Color.FromRgb(192, 64, 0);
            staticPoints[3] = Color.FromRgb(160, 96, 0);
            staticPoints[4] = Color.FromRgb(128, 128, 0);
            staticPoints[5] = Color.FromRgb(96, 160, 0);
            staticPoints[6] = Color.FromRgb(64, 192, 0);
            staticPoints[7] = Color.FromRgb(32, 224, 0);
            staticPoints[8] = Color.FromRgb(0, 255, 0);
            staticPoints[9] = Color.FromRgb(0, 224, 32);
            staticPoints[10] = Color.FromRgb(0, 192, 64);
            staticPoints[11] = Color.FromRgb(0, 160, 96);
            staticPoints[12] = Color.FromRgb(0, 128, 128);
            staticPoints[13] = Color.FromRgb(0, 96, 160);
            staticPoints[14] = Color.FromRgb(0, 64, 192);
            staticPoints[15] = Color.FromRgb(0, 32, 224);
            staticPoints[16] = Color.FromRgb(0, 0, 255);
            staticPoints[17] = Color.FromRgb(32, 0, 224);
            staticPoints[18] = Color.FromRgb(64, 0, 192);
            staticPoints[19] = Color.FromRgb(96, 0, 160);
            staticPoints[20] = Color.FromRgb(128, 0, 128);
            staticPoints[21] = Color.FromRgb(160, 0, 96);
            staticPoints[22] = Color.FromRgb(192, 0, 64);
            staticPoints[23] = Color.FromRgb(224, 0, 32);

            for (int i=0; i < 24; i++)
            {
                buttonBrush[i] = new SolidColorBrush(points[i]);
                now[i, 0] = staticPoints[i].R;
                now[i, 1] = staticPoints[i].G;
                now[i, 2] = staticPoints[i].B;
            }

            stopTL2.Color = points[0];
            stopT1.Color = points[0];
            stopT2.Color = points[1];
            stopT3.Color = points[2];
            stopT4.Color = points[3];
            stopT5.Color = points[4];
            stopT6.Color = points[5];
            stopTR1.Color = points[6];
            stopTR2.Color = Color.FromRgb(64, 192, 0);
            stopR1.Color = Color.FromRgb(64, 192, 0);
            stopR2.Color = Color.FromRgb(32, 224, 0);
            stopR3.Color = Color.FromRgb(0, 255, 0);
            stopR4.Color = Color.FromRgb(0, 224, 32);
            stopR5.Color = Color.FromRgb(0, 192, 64);
            stopR6.Color = Color.FromRgb(0, 160, 96);
            stopBR1.Color = Color.FromRgb(0, 160, 96);
            stopBR2.Color = Color.FromRgb(0, 128, 128);
            stopB1.Color = Color.FromRgb(0, 128, 128);
            stopB2.Color = Color.FromRgb(0, 96, 160);
            stopB3.Color = Color.FromRgb(0, 64, 192);
            stopB4.Color = Color.FromRgb(0, 32, 224);
            stopB5.Color = Color.FromRgb(0, 0, 255);
            stopB6.Color = Color.FromRgb(32, 0, 224);
            stopBL1.Color = Color.FromRgb(32, 0, 224);
            stopBL2.Color = Color.FromRgb(64, 0, 192);
            stopL1.Color = Color.FromRgb(64, 0, 192);
            stopL2.Color = Color.FromRgb(96, 0, 160);
            stopL3.Color = Color.FromRgb(128, 0, 128);
            stopL4.Color = Color.FromRgb(160, 0, 96);
            stopL5.Color = Color.FromRgb(192, 0, 64);
            stopL6.Color = Color.FromRgb(224, 0, 32);
            stopTL1.Color = Color.FromRgb(224, 0, 32);

            brushTL.GradientStops.Add(stopTL1);
            brushTL.GradientStops.Add(stopTL2);
            brushT.GradientStops.Add(stopT1);
            brushT.GradientStops.Add(stopT2);
            brushT.GradientStops.Add(stopT3);
            brushT.GradientStops.Add(stopT4);
            brushT.GradientStops.Add(stopT5);
            brushT.GradientStops.Add(stopT6);
            brushTR.GradientStops.Add(stopTR1);
            brushTR.GradientStops.Add(stopTR2);
            brushR.GradientStops.Add(stopR1);
            brushR.GradientStops.Add(stopR2);
            brushR.GradientStops.Add(stopR3);
            brushR.GradientStops.Add(stopR4);
            brushR.GradientStops.Add(stopR5);
            brushR.GradientStops.Add(stopR6);
            brushBR.GradientStops.Add(stopBR1);
            brushBR.GradientStops.Add(stopBR2);
            brushB.GradientStops.Add(stopB1);
            brushB.GradientStops.Add(stopB2);
            brushB.GradientStops.Add(stopB3);
            brushB.GradientStops.Add(stopB4);
            brushB.GradientStops.Add(stopB5);
            brushB.GradientStops.Add(stopB6);
            brushBL.GradientStops.Add(stopBL1);
            brushBL.GradientStops.Add(stopBL2);
            brushL.GradientStops.Add(stopL1);
            brushL.GradientStops.Add(stopL2);
            brushL.GradientStops.Add(stopL3);
            brushL.GradientStops.Add(stopL4);
            brushL.GradientStops.Add(stopL5);
            brushL.GradientStops.Add(stopL6);
        }

        public async void WaitConnect()
        {
            await Task.Delay(1000);
            if(connected == true)
            {
                sp.Close();
                textStatus.Text = "Connected: " + sp.PortName;
                buttonToPad.IsEnabled = true;
            }
            else
            {
                ConnectSerial();
                WaitConnect();
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fromcbx = comboBox.SelectedValue.ToString();
            string[] choiceSplit = fromcbx.Split(':');
            fromcbx = choiceSplit[1].Substring(1);
            
            if(fromcbx == "Solid Breathing")
            {
                closeColorButtons();
                OpenColor();
                InitializeTimer();
                rectOuter.Visibility = Visibility.Visible;
                rectInner.Visibility = Visibility.Visible;
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
                padMode = "0SB";
            }
            else if(fromcbx == "Solid Rainbow")
            {
                closeColorButtons();
                CloseColor();
                InitializeTimer();
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
                bowRed = 255;
                bowGreen = 0;
                bowBlue = 0;
                padMode = "0SR";
            }
            else if(fromcbx == "Solid")
            {
                closeColorButtons();
                StopTimer();
                OpenColor();
                counter = 100;
                slider.Visibility = Visibility.Hidden;
                labelSpeed.Visibility = Visibility.Hidden;
                updateBrush.Color = solidBrush.Color;
                //UpdateColor(red, green, blue);
                FillRectangle();
                padMode = "00S";
            }
            else if(fromcbx == "Rotating Rainbow")
            {
                CloseColor();
                place = 0;
                InitializeTimer();
                slider.Visibility = Visibility.Visible;
                labelSpeed.Visibility = Visibility.Visible;
                padMode = "0RR";
                openColorButtons();
                colorRects();
            }
        }

        private void openColorButtons()
        {
            int count = 0;
            foreach(var childButton in colorButtons.Children.OfType<Button>()){
                childButton.Visibility = Visibility.Visible;
                childButton.Background = buttonBrush[count];
                count++;
            }
            btnDirection.Visibility = Visibility.Visible;
            buttonReset.Visibility = Visibility.Visible;
        }

        private void closeColorButtons()
        {
            foreach(var childButton in colorButtons.Children.OfType<Button>())
            {
                childButton.Visibility = Visibility.Hidden;
            }
            btnDirection.Visibility = Visibility.Hidden;
            buttonReset.Visibility = Visibility.Hidden;
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
            padColor = CreateMousePadColorString();
        }

        private void buttonToPad_Click(object sender, RoutedEventArgs e)
        {
            sp.Open();
            toPadString = padMode;
            toPadString += padColor;
            toPadString += CreateMousePadSliderSpeed();
            sp.Write(toPadString);
            sp.Close();
            MessageBox.Show(toPadString);
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
            else if(comboBox.SelectedIndex == 3)
            {
                if (count == 25)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        int j = i + 1 * rI;
                        if (j < 0) { j = 23; }
                        now[i, 0] = staticPoints[(((j) % 24) + place) % 24].R;
                        now[i, 1] = staticPoints[(((j) % 24) + place) % 24].G;
                        now[i, 2] = staticPoints[(((j) % 24) + place) % 24].B;
                    }
                    place = place + 1 * rI;
                    if(place < 0) { place = 23; }
                    place = place % 24;
                    count = 0;
                }

                if (count == 0)
                {
                    for (int i = 0; i < 24; i++)
                    {
                        int j = i + 1 * rI;
                        if(j < 0) { j = 23; }
                        int range = (int)(now[(j) % 24, 0] - now[i, 0]);
                        step[i, 0] = range / 25.0f;
                        range = (int)(now[(j) % 24, 1] - now[i, 1]);
                        step[i, 1] = range / 25.0f;
                        range = (int)(now[(j) % 24, 2] - now[i, 2]);
                        step[i, 2] = range / 25.0f;
                        current[i, 0] = now[i, 0];
                        current[i, 1] = now[i, 1];
                        current[i, 2] = now[i, 2];
                    }
                }
                
                for(int i = 0; i < 24; i++)
                {
                    current[i, 0] += step[i, 0];
                    current[i, 1] += step[i, 1];
                    current[i, 2] += step[i, 2];
                    points[i].R = Convert.ToByte(current[i, 0]);
                    points[i].G = Convert.ToByte(current[i, 1]);
                    points[i].B = Convert.ToByte(current[i, 2]);
                }

                changeBrushes();
                colorRects();
                count++;
            }
        }

        private void changeBrushes()
        {
            stopTL2.Color = points[0];
            stopT1.Color = points[0];
            stopT2.Color = points[1];
            stopT3.Color = points[2];
            stopT4.Color = points[3];
            stopT5.Color = points[4];
            stopT6.Color = points[5];
            stopTR1.Color = points[5];
            stopTR2.Color = points[6];
            stopR1.Color = points[6];
            stopR2.Color = points[7];
            stopR3.Color = points[8];
            stopR4.Color = points[9];
            stopR5.Color = points[10];
            stopR6.Color = points[11];
            stopBR1.Color = points[11];
            stopBR2.Color = points[12];
            stopB1.Color = points[12];
            stopB2.Color = points[13];
            stopB3.Color = points[14];
            stopB4.Color = points[15];
            stopB5.Color = points[16];
            stopB6.Color = points[17];
            stopBL1.Color = points[17];
            stopBL2.Color = points[18];
            stopL1.Color = points[18];
            stopL2.Color = points[19];
            stopL3.Color = points[20];
            stopL4.Color = points[21];
            stopL5.Color = points[22];
            stopL6.Color = points[23];
            stopTL1.Color = points[23];
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
            rectRed.Visibility = Visibility.Hidden;
            rectGreen.Visibility = Visibility.Hidden;
            rectBlue.Visibility = Visibility.Hidden;
            textValRed.Visibility = Visibility.Hidden;
            textValGreen.Visibility = Visibility.Hidden;
            textValBlue.Visibility = Visibility.Hidden;
            sliderRed.Visibility = Visibility.Hidden;
            sliderGreen.Visibility = Visibility.Hidden;
            sliderBlue.Visibility = Visibility.Hidden;
            buttonUpdate.Visibility = Visibility.Hidden;
        }

        private void OpenColor()
        {
            rectOuter.Visibility = Visibility.Visible;
            rectInner.Visibility = Visibility.Visible;
            rectRed.Visibility = Visibility.Visible;
            rectGreen.Visibility = Visibility.Visible;
            rectBlue.Visibility = Visibility.Visible;
            textValRed.Visibility = Visibility.Visible;
            textValGreen.Visibility = Visibility.Visible;
            textValBlue.Visibility = Visibility.Visible;
            sliderRed.Visibility = Visibility.Visible;
            sliderGreen.Visibility = Visibility.Visible;
            sliderBlue.Visibility = Visibility.Visible;
            buttonUpdate.Visibility = Visibility.Visible;
        }

        private void UpdateColor(byte red, byte green, byte blue)
        {
            updateBrush.Color = Color.FromRgb(red, green, blue);
        }

        private void FillRectangle()
        {
            rectTL.Fill = updateBrush;
            rectT.Fill = updateBrush;
            rectTR.Fill = updateBrush;
            rectR.Fill = updateBrush;
            rectBR.Fill = updateBrush;
            rectB.Fill = updateBrush;
            rectBL.Fill = updateBrush;
            rectL.Fill = updateBrush;
        }
        
        //TESTING AREA FOR ARDUINO INTEGRATION
        private void ON_Click(object sender, RoutedEventArgs e)
        {
            sp.Open();
            sp.Write("1~");
            sp.Close();
        }

        private void OFF_Click(object sender, RoutedEventArgs e)
        {
            sp.Open();
            sp.Write("0~");
            sp.Close();
        }
        // END TESTING AREA FOR ARDUINO


        private void dataConnect(object sender, SerialDataReceivedEventArgs e)
        {
            buffer += sp.ReadExisting();
            if (buffer.Contains("84652345shae"))
            {
                connected = true;
            }
        }

        private void ConnectSerial()
        {
            buffer = "";
            if (!consecutiveAttempt)
            {
                sp.PortName = "COM3";
                sp.ReadTimeout = 500;
                sp.BaudRate = 9600;
                sp.DataReceived += new SerialDataReceivedEventHandler(dataConnect);
                sp.Open();
            }
            consecutiveAttempt = true;
            sp.Write("HEY~");
        }

        private string CreateMousePadColorString()
        {
            string color;
            switch(textValRed.Text.Length)
            {
                case 1:
                    color = "00" + textValRed.Text;
                    break;
                case 2:
                    color = "0" + textValRed.Text;
                    break;
                default:
                    color = textValRed.Text;
                    break;
            }
            switch (textValGreen.Text.Length)
            {
                case 1:
                    color += "00" + textValGreen.Text;
                    break;
                case 2:
                    color += "0" + textValGreen.Text;
                    break;
                default:
                    color += textValGreen.Text;
                    break;
            }
            switch (textValBlue.Text.Length)
            {
                case 1:
                    color += "00" + textValBlue.Text;
                    break;
                case 2:
                    color += "0" + textValBlue.Text;
                    break;
                default:
                    color += textValBlue.Text;
                    break;
            }
            if(color.Length == 9)
            {
                return color;
            }
            else
            {
                return "FFFFFFFFF";
            }
        }

        private string CreateMousePadSliderSpeed()
        {
            string speed = Convert.ToString(Convert.ToInt32(slider.Value));
            switch (speed.Length)
            {
                case 1:
                    speed = "00" + speed;
                    break;
                case 2:
                    speed = "0" + speed;
                    break;
                default:
                    break;
            }
            if(speed.Length == 3)
            {
                return speed;
            }
            else
            {
                return "001";
            }
        }
    }
}
