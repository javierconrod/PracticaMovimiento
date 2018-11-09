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

        enum EstadoJuego { Gameplay, Gameover };
        EstadoJuego estadoActual = EstadoJuego.Gameplay;

        public MainWindow()
        {
            InitializeComponent();
            miCanvas.Focus();

            stopwatch = new Stopwatch();
            stopwatch.Start();


            //1. Establecer instrucciones
            ThreadStart threadStart = new ThreadStart(actualizar);
            //2. Inicializar el thread
            Thread threadMoverEnemigos = new Thread(threadStart);
            //3. Ejecutar el thread
            threadMoverEnemigos.Start();
        }

        void actualizar()
        {
            while (true)
            {
                Dispatcher.Invoke(
                    () =>
                    {
                        var tiempoActual = stopwatch.Elapsed;
                        var deltaTime = tiempoActual - tiempoAnterior;

                        if(estadoActual == EstadoJuego.Gameplay)
                        {
                            double leftViejoActual = Canvas.GetLeft(imgViejo);
                            Canvas.SetLeft(imgViejo, leftViejoActual - (110 * deltaTime.TotalSeconds));
                            if (Canvas.GetLeft(imgViejo) <= -100)
                            {
                                Canvas.SetLeft(imgViejo, 800);
                            }

                            //interseccion en X
                            double xViejo = Canvas.GetLeft(imgViejo);
                            double xDinosaurio = Canvas.GetLeft(imgDinosaurio);
                            if (xDinosaurio + imgDinosaurio.Width >= xViejo && xDinosaurio <= xViejo + imgViejo.Width)
                            {
                                lblInterseccionX.Text = "SI HAY COLISIÓN EN X!!!";
                            }
                            else
                            {
                                lblInterseccionX.Text = "No hay intersección en X";
                            }
                            //en y
                            double yViejo = Canvas.GetTop(imgViejo);
                            double yDinosaurio = Canvas.GetTop(imgDinosaurio);
                            if (yDinosaurio + imgDinosaurio.Height >= yViejo && yDinosaurio <= yViejo + imgViejo.Height)
                            {
                                lblInterseccionY.Text = "SI HAY INTERSECCION EN Y";
                            }
                            else
                            {
                                lblInterseccionY.Text = "No hay intersección en Y";
                            }

                            if (xDinosaurio + imgDinosaurio.Width >= xViejo && xDinosaurio <= xViejo + imgViejo.Width && yDinosaurio + imgDinosaurio.Height >= yViejo && yDinosaurio <= yViejo + imgViejo.Height)
                            {
                                lblColision.Text = "Eh we, ya te mataron";
                                estadoActual = EstadoJuego.Gameover;
                                miCanvas.Visibility = Visibility.Collapsed;
                                canvasGameOver.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                lblColision.Text = "No hay colisión";
                            }
                        }
                        else if(estadoActual == EstadoJuego.Gameover)
                        {

                        }

                        

                            tiempoAnterior = tiempoActual;
                    }
                    );
            }
        }

        private void miCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            if(estadoActual == EstadoJuego.Gameplay)
            { 
                if(e.Key== Key.Up)
                {
                    double topDinoActual = Canvas.GetTop(imgDinosaurio);
                    Canvas.SetTop(imgDinosaurio, topDinoActual - 15);
                }
                if (e.Key == Key.Down)
                {
                    double topDinoActual = Canvas.GetTop(imgDinosaurio);
                    Canvas.SetTop(imgDinosaurio, topDinoActual + 15);
                }
                if (e.Key == Key.Left)
                {
                    double rightDinoActual = Canvas.GetLeft(imgDinosaurio);
                    Canvas.SetLeft(imgDinosaurio, rightDinoActual - 15);
                }
                if (e.Key == Key.Right)
                {
                    double rightDinoActual = Canvas.GetLeft(imgDinosaurio);
                    Canvas.SetLeft(imgDinosaurio, rightDinoActual + 15);
                }
            }
        }
    }
}
