using System.Collections.Generic;

namespace RecommendationSystem.Models
{
    public class Form
    {
        public Dictionary<string, string[]> Questions { get; }

        public Form(Dictionary<string, string[]> formItems)
        {
            Questions = formItems;
        }

        public static Form Default => 
            new Form(new Dictionary<string, string[]>
            {
                { "What do you prefer?", new []{ "City", "CountrySide"} },
                { "Do you want to live near a subway station?", new []{ "Yes", "No"}}
            });

    }
}
