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
using System.IO;
using System.Threading;

namespace Hilos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Aquí va el código personalizado
        //Ruta al archivo
        String path = "C:\\Users\\Katarino\\Documents\\test_file.txt";
        String line2 = "";

        //Método para iniciar todo
        public MainWindow()
        {
            InitializeComponent();
            textBox.Text = "";
            label.Content = "";
            label1.Content = "";
        }

        
        //Método para cargar archivo
        public void readFile()
        {
            String line = "";
            StreamReader fileReader = new StreamReader(path);
            while((line = fileReader.ReadLine()) != null)
            {
                //Llamada de hilo para actualizar el objeto en 
                //la interfaz de usuario,
                //con esto se previene un error de acceso de memoria
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                this.Dispatcher.BeginInvoke(new Action(() =>
                 {
                     line2 = line;
                     textBox.Text += line2;
                 }));
            }
            fileReader.Close();
        }

        public void count()
        {
            for(int x = 0; x < 20; x++)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(900));
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    label.Content = Convert.ToString(x);
                }));
            }
        }

        public void count2()
        {
            for (int x = 0; x < 10; x++)
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(1200));
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    label1.Content = Convert.ToString(x);
                }));
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            //Llamada al método en un hilo
            Thread thread = new Thread(readFile);
            thread.Start();

            Thread thread2 = new Thread(count);
            thread2.Start();

            Thread thread3 = new Thread(count2);
            thread3.Start();


        }


    }
}
