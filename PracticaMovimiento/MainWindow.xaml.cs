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

using System.Threading;
using System.Diagnostics;

namespace PracticaMovimiento
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch;
        TimeSpan tiempoAnterior;
        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();


            //1. Establecer instrucciones
            ThreadStart threadStart = new ThreadStart(moverEnemigos);
            //2. Inicializar el thread
            Thread threadMoverEnemigos = new Thread(threadStart);
            //3. Ejecutar el thread
            threadMoverEnemigos.Start();
        }

        void moverEnemigos()
        {
            while (true)
            {
                Dispatcher.Invoke(
                    () =>
                    {
                        var tiempoActual = stopwatch.Elapsed;
                        var deltaTime = tiempoActual - tiempoAnterior;
                        double leftViejoActual = Canvas.GetLeft(imgViejo);
                        Canvas.SetLeft(imgViejo, leftViejoActual - (110 * deltaTime.TotalSeconds));
                        if(Canvas.GetLeft(imgViejo) <= -100)
                        {
                            Canvas.SetLeft(imgViejo, 800);
                        }
                        tiempoAnterior = tiempoActual;
                    }
                    );
            }
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
