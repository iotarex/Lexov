﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lexov
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Pages.NDEFRead());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}