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
    [Activity(Label = "StartActivity")]
    public class ChooseTriviaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ChooseTrivia_Layout);

            // Create your application here

            var beginButton = FindViewById<Button>(Resource.Id.beginButton);
            var inputAmount = FindViewById<EditText>(Resource.Id.inputQuestionAmount);
            var inputDifficulty = FindViewById<EditText>(Resource.Id.inputQuestionDifficulty); 
            var inputCategory = FindViewById<EditText>(Resource.Id.inputQuestionCategory);
            var inputType = FindViewById<EditText>(Resource.Id.inputQuestionType);
            var questionsListView = FindViewById<ListView>(Resource.Id.questionsListView);

            beginButton.Click += async delegate
            {
                string amount = "";
                string difficulty = "";
                string category = "";
                string type = "";
                if (inputAmount.Text != "")
                    amount ="amount=" + inputAmount.Text + "&";
                if (inputDifficulty.Text != "")
                    difficulty = "difficulty=" + inputDifficulty.Text + "&";
                if (inputCategory.Text != "")
                    category = "category=" + inputCategory.Text + "&";
                if (inputType.Text != "")
                    type = "type=" + inputType.Text;
                string queryString = "https://opentdb.com/api_config.php?" + amount + difficulty + category + type;
                var data = await DataServiceQuestions.GetQuestions(queryString);
                questionsListView.Adapter = new QuestionsAdapter(this, data.Details);
            };
        }
    }
}