﻿using System;

using CustomerApplication.GUI.ViewModels;

using Windows.UI.Xaml.Controls;

namespace CustomerApplication.GUI.Views
{
  
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
        }
    }
}
