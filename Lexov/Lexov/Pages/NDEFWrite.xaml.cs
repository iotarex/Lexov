﻿using Lexov.Utilities;
using NdefLibrary.Ndef;
using Poz1.NFCForms.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Lexov.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NDEFWrite : ContentPage
    {
        //instantiates a new NFC tag object
        private readonly INfcForms device;
        private NdefMessage ndefMessage;
        public NDEFWrite(NdefMessage NDEFPayload)
        {
            InitializeComponent();
            DependencyService.Get<IOrientationHandler>().ForcePortrait();

            device = DependencyService.Get<INfcForms>();
            device.NewTag += HandleNewTag;

            ndefMessage = NDEFPayload;
        }

        private async void HandleNewTag(object sender, NfcFormsTag e)
        {
            //ensures that this event logic is only applied on the ndefwrite page
            if(Navigation.NavigationStack.Count == 3)
            {
                try
                {
                    //writes the contents of the ndefread editor
                    device.WriteTag(ndefMessage);
                    await DisplayAlert("Success", "NDEF write operation successful", "OK");

                    //performs the equivilent of popping the navigation stack twice
                    INavigation navigation = Application.Current.MainPage.Navigation;
                    Page currentPage = navigation.NavigationStack.ElementAt(navigation.NavigationStack.Count - 1);

                    while(navigation.NavigationStack.ElementAt(0)!=currentPage)
                    {
                        navigation.RemovePage(navigation.NavigationStack.ElementAt(0));
                    }

                    Page scanPromptPage = new ScanPrompt();
                    navigation.InsertPageBefore(scanPromptPage, currentPage);
                    await Task.Delay(100);
                    await navigation.PopAsync();
                }

                catch
                {
                    await DisplayAlert("Error", "NDEF write operation unsuccessful", "OK");
                }
            }
        }
    }
}