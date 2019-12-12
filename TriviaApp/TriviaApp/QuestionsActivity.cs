using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TriviaApp.Core;

namespace TriviaApp
{
    [Activity(Label = "QuestionsActivity")]
    public class QuestionsActivity : Activity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Questions_Layout);
            // Create your application here

            var questionsListView = FindViewById<ListView>(Resource.Id.questionsListView);

            string queryString = Intent.GetStringExtra("querystring").ToString();

            var data = await DataServiceQuestions.GetQuestions(queryString);
            questionsListView.Adapter = new QuestionsAdapter(this, data.Details);
        }
    }
}