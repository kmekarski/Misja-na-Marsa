using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisjaNaMarsa
{
    public class Question
    {
        public string qCode;
        public string type;
        public int startTime, endTime;

        public Dictionary<string, Tuple<string, int>> answers = new Dictionary<string, Tuple<string, int>>();

        public Dictionary<int, Tuple<string, int>> type3Correct = new Dictionary<int, Tuple<string, int>>();
        public Tuple<string, int> type3Wrong;

        public Question(string type, string qCode, int startTime, int endTime, Dictionary<string, Tuple<string, int>> answers, Dictionary<int, Tuple<string, int>> type3Correct, Tuple<string, int> type3Wrong)
        {
            this.qCode = qCode;
            this.type = type;
            this.startTime = startTime;
            this.endTime = endTime;
            this.answers = answers;
            this.type3Correct = type3Correct;
            this.type3Wrong = type3Wrong;

        }
    }
}
