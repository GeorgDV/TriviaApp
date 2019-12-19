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
            var inputAmount = FindViewById<Spinner>(Resource.Id.spinnerInputAmount);
            var inputDifficulty = FindViewById<Spinner>(Resource.Id.spinnerInputDifficulty); 
            var inputCategory = FindViewById<Spinner>(Resource.Id.spinnerInputCategory);
            var inputType = FindViewById<Spinner>(Resource.Id.spinnerInputType);


            inputAmount.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var amountAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.amount_array, Android.Resource.Layout.SimpleSpinnerItem);
            amountAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputAmount.Adapter = amountAdapter;


            inputDifficulty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var difficultyAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.difficulty_array, Android.Resource.Layout.SimpleSpinnerItem);
            difficultyAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputDifficulty.Adapter = difficultyAdapter;


            inputCategory.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var categoryAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.category_array, Android.Resource.Layout.SimpleSpinnerItem);
            categoryAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputCategory.Adapter = categoryAdapter;


            inputType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);
            var typeAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);
            typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputType.Adapter = typeAdapter;




            beginButton.Click +=  delegate
            {
                string amount = "";
                string difficulty = "";
                string category = "";
                string type = "";
                if (inputAmount.SelectedItem.ToString() != "")
                    amount = "amount=" + inputAmount.SelectedItem.ToString();
                else
                    amount = "amount=10";
                if (inputDifficulty.SelectedItem.ToString() != "")
                    difficulty = "&difficulty=" + inputDifficulty.SelectedItem.ToString();
                if (inputCategory.SelectedItem.ToString() != "")
                    category = "&category=" + inputCategory.SelectedItem.ToString();
                if (inputType.SelectedItem.ToString() != "")
                    type = "&type=" + inputType.SelectedItem.ToString();
                string queryString = "https://opentdb.com/api.php?" + amount + difficulty + category + type;
                var intent = new Intent(this, typeof(QuestionsActivity));
                intent.PutExtra("querystring", queryString);
                StartActivity(intent);
            };
        }

        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string selectedAmount = spinner.GetItemAtPosition(e.Position).ToString();
        }
    }
}