using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Survey
    {
        public Question[] Questions { get; set; }

        public static Survey Default => new Survey
        {
            Questions = new[]
                {
                    new Question("What do you prefer?",
                        new List<string> {"City", "CountrySide"})
                }
        };
    }
}
