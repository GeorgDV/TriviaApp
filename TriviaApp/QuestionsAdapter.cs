using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using TriviaApp.Core.Models;
using static Android.Widget.AdapterView;

namespace TriviaApp
{
    class QuestionsAdapter : BaseAdapter<QuestionDetails>
    {
        List<QuestionDetails> _items;
        Activity _context;

        public QuestionsAdapter(Activity context, List<QuestionDetails> items) : base()
        {
            this._context = context;
            this._items = items;
        }

        public override QuestionDetails this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];

            View view = convertView;

            //TODO - DECODE JSON RESPONSE SO QUESTION LOOKS MORE READABLE

            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.Questions_Row_Layout, null);

            view.FindViewById<TextView>(Resource.Id.textViewQuestion).Text = item.Question;

            var button1 = view.FindViewById<Button>(Resource.Id.answer1Btn);
            var button2 = view.FindViewById<Button>(Resource.Id.answer2Btn);
            var button3 = view.FindViewById<Button>(Resource.Id.answer3Btn);
            var button4 = view.FindViewById<Button>(Resource.Id.answer4Btn);
            //button1.Tag = position;
            //button2.Tag = position;
            //button3.Tag = position;
            //button4.Tag = position;

            var questionsListView = view.FindViewById<ListView>(Resource.Id.questionsListView);
            int startPosition = 0;

            List<string> Answers = new List<string>();
            //ADD ALL QUESTION ANSWERS TO A LIST
            Answers.Add(item.Correct_Answer);
            foreach (string answer in item.Incorrect_Answers)
            {
                Answers.Add(answer);
            }

            //SHUFFLE ANSWERS
            Random rng = new Random();
            int n = Answers.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                string value = Answers[k];
                Answers[k] = Answers[n];
                Answers[n] = value;
            }

            //MAKE SURE BUTTONS ARE VISIBLE
            button1.Visibility = ViewStates.Visible;
            button2.Visibility = ViewStates.Visible;
            button3.Visibility = ViewStates.Visible;
            button4.Visibility = ViewStates.Visible;

            //CHECK HOW MANY ANSWERS ARE
            if (item.Type == "multiple" && Answers.Count == 4)
            {
                button1.Text = Answers[0];
                button2.Text = Answers[1];
                button3.Text = Answers[2];
                button4.Text = Answers[3];
            }
            else if (item.Type == "boolean" && Answers.Count == 2)
            {
                button1.Text = "True";
                button2.Text = "False";
                button3.Visibility = ViewStates.Gone;
                button4.Visibility = ViewStates.Gone;
            }
            button1.Click += ListView_ItemClick;
            button2.Click += ListView_ItemClick;
            button3.Click += ListView_ItemClick;
            button4.Click += ListView_ItemClick;
            return view;

            void ListView_ItemClick(object sender, EventArgs e)
            {
                if (position == startPosition)
                {
                    //int pos = (int)((Button)sender).Tag;
                    var selectedButton = (Button)sender;
                    string selectedAnswer = selectedButton.Text;
                    string correctAnswer = item.Correct_Answer;
                    Context context = Application.Context;
                    ToastLength duration = ToastLength.Short;
                    string text = "";

                    if (selectedAnswer == correctAnswer)
                    {
                        text = correctAnswer + " -> Correct!";
                    }
                    else
                    {
                        text = "Incorrect! Correct -> " + correctAnswer;

                    }
                    var toast = Toast.MakeText(context, text, duration);
                    toast.Show();
                    _items.RemoveAt(position);
                    _context.RunOnUiThread(() => this.NotifyDataSetChanged());
                    startPosition++;
                }

                button1.Click -= ListView_ItemClick;
                button2.Click -= ListView_ItemClick;
                button3.Click -= ListView_ItemClick;
                button4.Click -= ListView_ItemClick;
            }
        }


    }
}