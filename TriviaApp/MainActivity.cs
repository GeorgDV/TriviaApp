﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using Android.Content;

namespace TriviaApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            //OPENTRIVIA DB LINK - https://opentdb.com/api_config.php

            var quitButton = FindViewById<Button>(Resource.Id.quitButton);
            var startButton = FindViewById<Button>(Resource.Id.startButton);

            quitButton.Click += QuitButton_click;
            startButton.Click += StartButton_click;


        }

        private void QuitButton_click(object sender, EventArgs e)
        {
            Process.KillProcess(Process.MyPid());
        }

        private void StartButton_click(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(ChooseTriviaActivity));
            this.StartActivity(intent);
        }
    }
}