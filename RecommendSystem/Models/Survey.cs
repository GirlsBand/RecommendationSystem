using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Survey
    {
        public const string InCityPreferenceQuestion = "What do you prefer?";

        public List<Question> Questions { get; set; }    
        public Survey(List<Question> questions)
        {
            Questions = questions;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public Question FindQuestion(Question question) => FindQuestion(question.Title);
        

        public Question FindQuestion(string title) => Questions.Find(question => question.Title == title);
    
        public void ReplaceQuestion(Question question)
        {
            Questions.Remove(FindQuestion(question));

            AddQuestion(question);
        }

        public void AddAnswer(string title, string answer)
        {
            var questionToUpdate = FindQuestion(title);

            if (questionToUpdate == null)
                throw new ArgumentException("No question with such title was found");
            if (questionToUpdate.Answers.Contains(answer))
                throw new ArgumentException("Such answer is already present");

            questionToUpdate.AddAnswer(answer);

            ReplaceQuestion(questionToUpdate);
        }

        public static Survey Default
        {
            get
            {
                var firstQuestion = new Question(InCityPreferenceQuestion, new List<string> { "City", "CountrySide" });

                return new Survey(new List<Question> { firstQuestion });
            }
        }
    }
}
