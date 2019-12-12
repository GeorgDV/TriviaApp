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

            if (view == null)
                view = _context.LayoutInflater.Inflate(Resource.Layout.Questions_Row_Layout, null);

            view.FindViewById<TextView>(Resource.Id.textViewQuestion).Text = item.Question;

            List<string> Answers = new List<string>();
            Answers.Add(item.CorrectAnswer);
            for (int i = 0; i < item.IncorrectAnswers.Length; i++)
            {
                Answers.Add(item.IncorrectAnswers[i]);
            }

            int counter = 1;
            if (Answers.Count >= 4)
            {
                view.FindViewById<Button>(Resource.Id.answer1Btn).Text = Answers[0];
                view.FindViewById<Button>(Resource.Id.answer2Btn).Text = Answers[1];
                view.FindViewById<Button>(Resource.Id.answer3Btn).Text = Answers[2];
                view.FindViewById<Button>(Resource.Id.answer4Btn).Text = Answers[3];
            }


            return view;
        }
    }
}