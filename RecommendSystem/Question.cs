using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Client
{
    [DataContract]
    class Question
    {
        [DataMember]
        string question;
        [DataMember]
        List<string> answers;

        public Question()
        {
            question = "";
            answers = new List<string>();
            answers.Add("");
        }

        public Question(string questionText, List<string> answersText)
        {
            question = questionText;
            answers = new List<string>();
            foreach (string answerText in answersText)
            {
                answers.Add(answerText);
            }
        }
    }
}
