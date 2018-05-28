using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Question
    {
        public string Title { get; set; }
        public List<string> Answers { get; set; }

        public string Type { get; set; }
        public Question(string title, List<string> answers, string type)
        {
            Title = title;
            Answers = answers;
            Type = type;
        }
    }
}
