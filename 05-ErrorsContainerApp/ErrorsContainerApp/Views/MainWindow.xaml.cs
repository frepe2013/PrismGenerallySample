﻿using System.Windows;
using Prism.Regions;

namespace ErrorsContainerApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            regionManager.RegisterViewWithRegion("LeftRegion", typeof(List));
        }
    }
}
