﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TriviaApp.Core;
using static Android.Widget.AdapterView;

namespace TriviaApp
{
    [Activity(Label = "QuestionsActivity")]
    public class QuestionsActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Questions_Layout);

            var questionsListView = FindViewById<ListView>(Resource.Id.questionsListView);

            string queryString = Intent.GetStringExtra("querystring").ToString();

            var data = await DataServiceQuestions.GetQuestions(queryString);
            questionsListView.Adapter = new QuestionsAdapter(this, data.Results);

            if (data.Response_Code.ToString() != "0")
            {
                Context context = Application.Context;
                string text = "Serverside Error. Please check your query.";
                ToastLength duration = ToastLength.Short;

                var toast = Toast.MakeText(context, text, duration);
                toast.Show();
                var intent = new Intent(this, typeof(ChooseTriviaActivity));
                this.StartActivity(intent);
            }

            questionsListView.ItemClick += (object sender, ItemClickEventArgs e) =>
            {
                //var clickPostitionText = moviesListView.GetItemAtPosition(e.Position); // Show text
                //var clickPostitionID = Convert.ToString(e.Position); // Show index
                var button = questionsListView.GetItemAtPosition(e.Position);
                var questionDetails = data.Results[e.Position];
                string CorrectAnswer = data.Results[e.Position].Correct_Answer;

                //DOES NOT WORK, WORK TODO


            };
        }
    }
}