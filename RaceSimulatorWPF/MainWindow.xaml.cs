using System;
using System.Collections.Generic;
using System.Drawing;
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
using System.Windows.Threading;
using Controller;
using Model;

namespace RaceSimulatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Data.Initialise(isWpf: true);
            Visuals.Initialise(Data.CurrentRace);
            Data.NewVisuals += OnNewVisuals;
            Data.CurrentRace.DriversChanged += OnDriversChanged;
            Data.CurrentRace.Start();
            Data.CurrentRace.CollectGarbage += OnCollectWpfGarbage;
        }
        public void OnDriversChanged(object sender, EventArgs e)
        {
            this.Image.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Image.Source = null;
                    this.Image.Source = Visuals.DrawTrack(Data.CurrentRace.Track);
                }));
        }
        public void OnNewVisuals(object sender, EventArgs e)
        {
            if (sender == null) 
            {
                CleanUpWpf();
                return;
            }
            ImageHandler.ClearCache();
            Data.CurrentRace.DriversChanged += OnDriversChanged; // <-- Unsubscribe this 
            Visuals.Initialise(Data.CurrentRace);
            Data.CurrentRace.Start();
        }
        public void OnCollectWpfGarbage(object sender, EventArgs e)
        {
            Data.CurrentRace.DriversChanged -= OnDriversChanged;
        }

        public void CleanUpWpf()
        {
            this.Image.Dispatcher.BeginInvoke(
                DispatcherPriority.Render,
                new Action(() =>
                {
                    this.Image.Source = null;
                }));
        }
    }
}
