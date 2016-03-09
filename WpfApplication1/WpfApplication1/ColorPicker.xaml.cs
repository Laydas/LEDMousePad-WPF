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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ColorPicker : Window
    {
        LinearGradientBrush gradient = new LinearGradientBrush();
        GradientStop stopTop = new GradientStop();
        GradientStop stopMed = new GradientStop();
        GradientStop stopBot = new GradientStop();
        private byte[] pixels;
        public Color returnColor = new Color();

        public ColorPicker(Color currentColor)
        {
            InitializeComponent();
            stopTop.Offset = 0;
            stopMed.Offset = 0.5;
            stopBot.Offset = 1;
            stopTop.Color = Colors.White;
            stopMed.Color = currentColor;
            stopBot.Color = Colors.Black;
            gradient.StartPoint = new Point(1, 0);
            gradient.EndPoint = new Point(1, 1);
            gradient.GradientStops.Add(stopTop);
            gradient.GradientStops.Add(stopMed);
            gradient.GradientStops.Add(stopBot);
            rectCPCurrent.Fill = new SolidColorBrush(currentColor);
            rectCPGradient.Fill = gradient;
        }

        private void image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int x = Convert.ToInt32(Mouse.GetPosition(btnColor).X);
                int y = Convert.ToInt32(Mouse.GetPosition(btnColor).Y);
                CroppedBitmap cb = new CroppedBitmap(btnPick.Source as BitmapSource, new Int32Rect(x, y, 1, 1));
                pixels = new byte[4];
                try
                {
                    cb.CopyPixels(pixels, 4, 0);
                    returnColor = Color.FromRgb(pixels[2], pixels[1], pixels[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                rectCPCurrent.Fill = new SolidColorBrush(returnColor);
                stopMed.Color = returnColor;
            }
            catch (Exception exc) { }
        }

        private void cpAccept_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void cpCancel_Click(object sender, RoutedEventArgs e)
        {

        }

        public Color NewColor
        {
            get { return returnColor;  }
        }

    }
}
