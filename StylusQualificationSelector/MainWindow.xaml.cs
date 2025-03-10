﻿using System;
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

namespace StylusQualificationSelector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path;

        public MainWindow(string[] args)
        {
            InitializeComponent();
            DataContext = new MainViewModel(args);
            path = args[0];

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            new PcmWriter(path).Abort();
        }
    }
}
