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
        private List<KeyValuePair<string, string>> category;
        private List<KeyValuePair<string, string>> difficulty;
        private List<KeyValuePair<string, string>> type;
        string selectedCategory = "";
        string selectedDifficulty = "";
        string selectedType = "";

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


            category = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Any Category", ""),
                new KeyValuePair<string, string>("General Knowledge", "9"),
                new KeyValuePair<string, string>("Entertainment: Books", "10"),
                new KeyValuePair<string, string>("Entertainment: Films", "11"),
                new KeyValuePair<string, string>("Entertainment: Music", "12"),
                new KeyValuePair<string, string>("Entertainment: Musicals and Theatres" , "13"),
                new KeyValuePair<string, string>("Entertainment: Television", "14"),
                new KeyValuePair<string, string>("Entertainment: Video Games", "15"),
                new KeyValuePair<string, string>("Entertainment: Board Games", "16"),
                new KeyValuePair<string, string>("Science and Nature", "17"),
                new KeyValuePair<string, string>("Science: Computers", "18"),
                new KeyValuePair<string, string>("Science: Mathematics" , "19"),
                new KeyValuePair<string, string>("Mythology", "20"),
                new KeyValuePair<string, string>("Sports", "21"),
                new KeyValuePair<string, string>("Geography", "22"),
                new KeyValuePair<string, string>("History", "23"),
                new KeyValuePair<string, string>("Politics", "24"),
                new KeyValuePair<string, string>("Art", "25"),
                new KeyValuePair<string, string>("Celebrities", "26"),
                new KeyValuePair<string, string>("Animals", "27"),
                new KeyValuePair<string, string>("Vehicles", "28"),
                new KeyValuePair<string, string>("Entertainment: Comics", "29"),
                new KeyValuePair<string, string>("Science: Gadgets", "30"),
                new KeyValuePair<string, string>("Entertainment: Japanese Anime and Manga", "31"),
                new KeyValuePair<string, string>("Entertainement: Cartoon and Animations", "32")
            };

            difficulty = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Any Difficulty", ""),
                new KeyValuePair<string, string>("Easy", "easy"),
                new KeyValuePair<string, string>("Medium", "medium"),
                new KeyValuePair<string, string>("Hard", "hard")
            };

            type = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Any Type", ""),
                new KeyValuePair<string, string>("Multiple Choice", "multiple"),
                new KeyValuePair<string, string>("True / False", "boolean")
            };

            List<string> categoryNames = new List<string>();
            foreach (var item in category)
                categoryNames.Add(item.Key);

            List<string> difficultyNames = new List<string>();
            foreach (var item in difficulty)
                difficultyNames.Add(item.Key);

            List<string> typeNames = new List<string>();
            foreach (var item in type)
                typeNames.Add(item.Key);


            inputAmount.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerAmount_ItemSelected);
            var amountAdapter = ArrayAdapter.CreateFromResource(
                    this, Resource.Array.amount_array, Android.Resource.Layout.SimpleSpinnerItem);
            amountAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputAmount.Adapter = amountAdapter;


            inputDifficulty.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerDiff_ItemSelected);
            var difficultyAdapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, difficultyNames);
            difficultyAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputDifficulty.Adapter = difficultyAdapter;


            inputCategory.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerCat_ItemSelected);
            var categoryAdapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, categoryNames);
            categoryAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            inputCategory.Adapter = categoryAdapter;


            inputType.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinnerType_ItemSelected);
            var typeAdapter = new ArrayAdapter<string>(this,
                    Android.Resource.Layout.SimpleSpinnerItem, typeNames);
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
                if (selectedDifficulty != "")
                    difficulty = "&difficulty=" + selectedDifficulty;
                if (selectedCategory != "")
                    category = "&category=" + selectedCategory;
                if (selectedType != "")
                    type = "&type=" + selectedType;
                string queryString = "https://opentdb.com/api.php?" + amount + difficulty + category + type;
                var intent = new Intent(this, typeof(QuestionsActivity));
                intent.PutExtra("querystring", queryString);
                StartActivity(intent);
            };
        }


        public void spinnerAmount_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string selectedItem = spinner.GetItemAtPosition(e.Position).ToString();
        }

        public void spinnerDiff_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedDifficulty = difficulty[e.Position].Value;
        }

        public void spinnerCat_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedCategory = category[e.Position].Value;
        }

        public void spinnerType_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            selectedType = type[e.Position].Value;
        }
    }
}