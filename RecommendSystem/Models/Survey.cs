using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Survey
    {
        public const string WhatDoYouPreferQuestion = "What do you prefer?";
        public const string HowManyPeopleAreInYourFamilyQuestion = "How many people are in your family?";
        public const string SpecifyPlaceOfWorkQuestion = "Specify place of work";
        public const string SpecifyPlaceOfStudyQuestion = "Specify place of study";
        public const string ClarifyCityToLiveQuestion = "Clarify the city to live";
        public const string DoYouHavePetsToWalkQuestion = "Do you have pets to walk?";

        public Question[] Questions { get; set; }

        public static Survey Default => new Survey
        {
            Questions = new[]
                {
                    new Question(WhatDoYouPreferQuestion,
                        new List<string> {"City", "CountrySide"}, "checkbox"),
                    new Question(HowManyPeopleAreInYourFamilyQuestion , null, "input"),
                    new Question(SpecifyPlaceOfWorkQuestion, null, "input"),
                    new Question(SpecifyPlaceOfStudyQuestion, null, "input"),
                    new Question(ClarifyCityToLiveQuestion, 
                        new List<string> {"San-Diego", "San-Francisko"}, "select"),
                     new Question(DoYouHavePetsToWalkQuestion,
                        new List<string> {"Yes", "No"}, "select"),
                }
        };
    }
}
