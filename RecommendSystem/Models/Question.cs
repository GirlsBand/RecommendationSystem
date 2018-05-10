using System;
using System.Collections.Generic;
using System.Text;

namespace RecommendationSystem.Models
{
    public class Question
    {
        public string Title = string.Empty;
        public List<string> Answers = new List<string>();

        public Question() {  }

        public Question(string title, List<string> answers)
        {
            Title = title;
            Answers = answers;
        }

        public void AddAnswer(string answer)
        {
            Answers.Add(answer);
        }
    }
}
