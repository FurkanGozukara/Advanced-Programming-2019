using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace week_7_forms
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Form2 : Window
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnStartCounterSingle_Click(object sender, RoutedEventArgs e)
        {
            int irCounter = 0;
            while (true)
            {
                irCounter++;
                lblCounter.Content = irCounter.ToString("N0");
                System.Threading.Thread.Sleep(1);
            }
        }

        private void btnStartMultiThread_Click(object sender, RoutedEventArgs e)
        {
            Task t = Task.Factory.StartNew(() =>
            {
                CounterFunction();
            });

            //Task t2 = Task.Factory.StartNew(() =>
            //{
            //    CounterFunction();
            //});

            Task t3 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Debug.WriteLine(DateTime.Now + " thread 1: " + t.Status);
                    //Debug.WriteLine(DateTime.Now + " thread 2: " + t2.Status);

                    System.Threading.Thread.Sleep(100);
                }
            });
        }

        private void CounterFunction()
        {
            int irCounter = 0;
            while (true)
            {
                irCounter++;

                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
                {
                    lblCounter.Content = irCounter.ToString("N0");
                }));

                Debug.WriteLine(irCounter);

                System.Threading.Thread.Sleep(1);
            }
        }
    }
}
