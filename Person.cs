using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFinder.src
{
    public class Person
    {
        public string fullName, points, year, ldNumber, group;
        public StudyForm formOfStudy;
        /*
        public Person(string fullName, string points, string year, string ldNumber, StudyForm studyForm)
        {

        }
        */
        public enum StudyForm
        {
            budget,
            targeting,
            paid
        }
    }
}
