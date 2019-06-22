using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFinder.src
{
    public class Person
    {
        public string fullName { get; set; }
        public string points{ get; set; }
        public string year { get; set; }
        public string ldNumber { get; set; }
        public string group { get; set; }

        public StudyForm formOfStudy { get; set; }
        public enum StudyForm
        {
            budget,
            targeting,
            paid
        }
    }
}
