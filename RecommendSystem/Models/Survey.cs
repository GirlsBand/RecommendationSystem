using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Survey
    {
        public const string Region = "region";
        public const string People = "people";
        public const string Work = "work";
        public const string Study = "study";
        public const string City = "city";
        public const string Pets = "pets";

        public Question[] Questions { get; set; }
    }

    public class Question
    {
        public string Name { get; set; }
        public string Answer { get; set; }

        public Question(string name, string answer)
        {
            Name = name;
            Answer = answer;
        }
    }
}
