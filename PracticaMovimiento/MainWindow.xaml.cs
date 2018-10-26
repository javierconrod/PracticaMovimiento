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

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key== Key.W)
            {
                double topDinoActual = Canvas.GetTop(imgDinosaurio);
                Canvas.SetTop(imgDinosaurio, topDinoActual - 15);
            }
            if (e.Key == Key.S)
            {
                double topDinoActual = Canvas.GetTop(imgDinosaurio);
                Canvas.SetTop(imgDinosaurio, topDinoActual + 15);
            }
            if (e.Key == Key.A)
            {
                double rightDinoActual = Canvas.GetLeft(imgDinosaurio);
                Canvas.SetLeft(imgDinosaurio, rightDinoActual - 15);
            }
            if (e.Key == Key.D)
            {
                double rightDinoActual = Canvas.GetLeft(imgDinosaurio);
                Canvas.SetLeft(imgDinosaurio, rightDinoActual + 15);
            }
        }
    }
}
