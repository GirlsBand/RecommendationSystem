using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Question
    {
        public string Title { get; set; }
        public List<string> Answers { get; set; }


        public Question(string title, List<string> answers)
        {
            Title = title;
            Answers = answers;
        }
    }
}
