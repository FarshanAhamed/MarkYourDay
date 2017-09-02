﻿using MarkYourDay.Helpers;
using MarkYourDay.Interfaces;
using MarkYourDay.Services;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarkYourDay.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Punch : ContentPage
    {
        DateTime start, stop;
        User people;
        WorkPlace fc;
        public static Double LATITUDE = 11.284108;
        public static Double LONGITUDE = 75.792672;
        bool AtFantaCode = false;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //DependencyService.Get<IStatusBar>().ShowStatusBar();
            if (!string.IsNullOrWhiteSpace(Settings.Username))
            {
                if (!string.IsNullOrWhiteSpace(Settings.StartTime))
                {
                    Start.IsEnabled = false;
                    where.Text = Settings.StartTime;
                }

                else
                    Start.IsEnabled = true;
            }
            
        }
        public Punch(string Username)
        {
            InitializeComponent();
            fc = new WorkPlace(LATITUDE, LONGITUDE);

            //for startbutton tap
            var StartTapRecognizer = new TapGestureRecognizer();
            StartTapRecognizer.NumberOfTapsRequired = 1;
            StartTapRecognizer.Tapped += OnStartTapped;
            Start.GestureRecognizers.Add(StartTapRecognizer);

            //for stop button tap
            var StopTapRecognizer = new TapGestureRecognizer();
            StopTapRecognizer.NumberOfTapsRequired = 1;
            StopTapRecognizer.Tapped += OnStopTapped;
            Stop.GestureRecognizers.Add(StopTapRecognizer);
            


        }

        async void OnStartTapped(object sender,EventArgs args)
        {
            Start.IsVisible = false;
            Stop.IsVisible = true;
            Settings.StartTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt");
            var result = WhereAmI.GetLocation();
            if (result)
            {
                if (Settings.OK == "Yes")
                {
                    AtFantaCode = true;
                    await DisplayAlert("Yes!", "You are at FantaCode", "OK");
                    start = DateTime.ParseExact(Settings.StartTime, "dd-MM-yyyy HH:mm:ss tt", null);
                    User user = new User
                    {
                        name = Settings.Username,
                        present = true,
                        start = start
                    };
                    string body = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(user));
                    var res = await UserServices.UpdateDetails(body);
                    LoginHelper.SaveUser(user);
                   // Start.IsEnabled = false;
                    //Start.IsVisible = false;
                    //Stop.IsVisible = true;
                    where.Text = "Entry : " + start;
                }
                else if (Settings.OK == "No")
                {
                    AtFantaCode = false;
                    await DisplayAlert("Sorry", "You are not at FantaCode ", "OK");
                }
                else
                {
                    await DisplayAlert("Sorry", "Some Unknown Error Occured!", "OK");
                    return;
                }
            }
        }

        async void OnStopTapped(object sender,EventArgs args)
        {
            Start.IsVisible = true;
            Stop.IsVisible = false;
            Settings.StopTime = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss tt");
            var result = WhereAmI.GetLocation();
            if (result)
            {
                if (Settings.OK == "Yes")
                {
                    AtFantaCode = true;
                    var res = await DisplayAlert("Are you Sure?", "Are you sure you want to leave Fantacode", "OK", "Cancel");
                    if (res)
                    {
                        await DisplayAlert("Buhbye!", "Have a Nice Day", "OK");
                        stop = DateTime.ParseExact(Settings.StopTime, "dd-MM-yyyy HH:mm:ss tt", null);
                        User user = new User
                        {
                            name = Settings.Username,
                            present = true,
                            start = start,
                            stop = stop
                        };

                        string body = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(user));
                        var ok = await UserServices.UpdateDetails(body);
                        LoginHelper.SaveUser(user);
                        Start.IsEnabled = true;
                        where.Text = "Exit at " + stop;
                        Settings.StartTime = "";
                    }
                }
                else if (Settings.OK == "No")
                {
                    AtFantaCode = false;
                    await DisplayAlert("Sorry", "You are not at FantaCode ", "OK");
                }
                else
                {
                    await DisplayAlert("Sorry", "Some Unknown Error Occured!", "OK");
                    return;
                }
            }

        }
    }
}