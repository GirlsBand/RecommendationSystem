using System;
using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Survey
    {
        static List<Question> Questions;    
        public Survey(List<Question> questions)
        {
            Questions = questions;
        }

        public static void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public static Question FindQuestion(Question question) => FindQuestion(question.Title);
        

        public static Question FindQuestion(string title) => Questions.Find(question => question.Title == title);
    
        public static void ReplaceQuestion(Question question)
        {
            Questions.Remove(FindQuestion(question));

            AddQuestion(question);
        }

        public static void AddAnswer(string title, string answer)
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
                var firstQuestion = new Question("What do you prefer?", new List<string> { "City", "CountrySide" });
                var secondQuestion = new Question("Do you want to live near a subway station?", new List<string> { "Yes", "No" });

                return new Survey(new List<Question> { firstQuestion, secondQuestion });
            }
        }
    }
}
