using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Client
{
    class Questionary
    {
        private List<Question> questions;

        public Questionary()
        {
            questions = new List<Question>();
            List<string> answers1 = new List<string>();
            answers1.Add("City");
            answers1.Add("Countryside");
            Question question1 = new Question("What do you prefer?", answers1);
            List<string> answers2 = new List<string>();
            answers2.Add("Yes");
            answers2.Add("No");
            Question question2 = new Question("Do you want to live near a subway station?", answers2);
            questions.Add(question1);
            questions.Add(question2);
        }

        public string GetQuestion(int no)
        {
            DataContractJsonSerializer JS = new DataContractJsonSerializer(typeof(Question));
            MemoryStream msObj = new MemoryStream();
            JS.WriteObject(msObj, questions[no]);
            msObj.Position = 0;
            StreamReader sr = new StreamReader(msObj);

            string json = sr.ReadToEnd();

            sr.Close();
            msObj.Close();

            return json;
        }

        public int GetNumberofQuestions()
        {
            return questions.Count;
        }

    }
}
