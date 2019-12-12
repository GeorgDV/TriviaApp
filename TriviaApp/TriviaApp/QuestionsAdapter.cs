using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using TriviaApp.Core.Models;

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

            //TODO - MAKE IT SO THAT ALL TEXT VALUE ON QUESTION CARD WILL SHOW AS STYLED OR NOT STYLED AT ALL (DECODE JSON TO UTF8)

            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.Questions_Row_Layout, null);

            view.FindViewById<TextView>(Resource.Id.textViewQuestion).Text = item.Question;

            List<string> Answers = new List<string>();
            //ADD ALL QUESTION ANSWERS TO A LIST
            Answers.Add(item.Correct_Answer);
            for (int i = 0; i < item.Incorrect_Answers.Length; i++)
            {
                Answers.Add(item.Incorrect_Answers[i]);
            }

            //CHECK IF QUESTION IS MULTIPLE CHOICE OR YES/NO
            if (item.Type == "multiple")
            {
                view.FindViewById<Button>(Resource.Id.answer1Btn).Text = Answers[0];
                view.FindViewById<Button>(Resource.Id.answer2Btn).Text = Answers[1];
                view.FindViewById<Button>(Resource.Id.answer3Btn).Text = Answers[2];
                view.FindViewById<Button>(Resource.Id.answer4Btn).Text = Answers[3];
            }
            else
            {
                view.FindViewById<Button>(Resource.Id.answer1Btn).Text = Answers[0];
                view.FindViewById<Button>(Resource.Id.answer2Btn).Text = Answers[1];
                view.FindViewById<Button>(Resource.Id.answer3Btn).Visibility = ViewStates.Gone;
                view.FindViewById<Button>(Resource.Id.answer4Btn).Visibility = ViewStates.Gone;
            }
            return view;
        }
    }
}