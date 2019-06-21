using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonFinder.src
{
    public class Document
    {
        public string fileName { get; set; }
        public string path { get; private set; }
        public string uniqRegex { get; set; }
        public Person.StudyForm documentType { get; set; }
        public Year documentYear { get; set; }

        public enum Year
        {
            y2016,
            y2017, 
            y2018
        }

        public void InitPath()
        {
            switch(documentYear)
            {
                case Year.y2016:
                    path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "2016", fileName);
                    break;

                case Year.y2017:
                    path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "2017", fileName);
                    break;

                case Year.y2018:
                    path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Data", "2018", fileName);
                    break;
            }
            
        }
    }
    public class DatabaseCell
    {
        public List<string> stringData { get; set; }
        public Document.Year year { get; set; }
        public Person.StudyForm studyForm { get; set; }
        public DatabaseCell(List<string> stringData, Document.Year year, Person.StudyForm studyForm)
        {
            this.stringData = stringData;
            this.studyForm = studyForm;
            this.year = year;
        }
        public DatabaseCell()
        {
            stringData = new List<string>();
        }
    }
    public static class Database
    {
        public static List<DatabaseCell> mainDatabase = new List<DatabaseCell>();

        public static void InitMainDatabase()
        {
            
        }
        public static List<Document> GetDocuments()
        {
            if (documents != null)
            {
                foreach (Document doc in documents) doc.InitPath();
                return documents;
            }
            return null;
        }

        private static List<Document> documents = new List<Document>()
        {
            // 2018
            new Document()
            {
                fileName = "2018-08-03 бюджет.pdf",
                documentYear = Document.Year.y2018,
                documentType = Person.StudyForm.budget,
                uniqRegex = @"[^\§(\w+)](\D+)[^\;]"
            },
            new Document()
            {
                fileName = "2018-08-08 бюджет.pdf",
                documentYear = Document.Year.y2018,
                documentType = Person.StudyForm.budget,
                uniqRegex = @"[^\§(\w+)](\D+)[^\;]"
            },
            new Document()
            {
                fileName = "2018-08-08-p-rus платное.pdf",
                documentYear = Document.Year.y2018,
                documentType = Person.StudyForm.paid,
                uniqRegex = @"[^\§(\w+)](\D+)[^\;]"
            },
            new Document()
            {
                fileName = "2018-08-31-p-rus платное.pdf",
                documentYear = Document.Year.y2018,
                documentType = Person.StudyForm.paid,
                uniqRegex = @"[^\§(\w+)](\D+)[^\;]"
            },
            new Document()
            {
                fileName = "2018-07-29 бви.pdf",
                documentYear = Document.Year.y2018,
                documentType = Person.StudyForm.targeting,
                uniqRegex = @"[^\§(\w+)](\D+)[^\;]"
            },

            // 2017
            new Document()
            {
                fileName = "20170803 бюджет.pdf",
                documentYear = Document.Year.y2017,
                documentType = Person.StudyForm.budget
            },
            new Document()
            {
                fileName = "20170808 бюджет.pdf",
                documentYear = Document.Year.y2017,
                documentType = Person.StudyForm.budget
            },
            new Document()
            {
                fileName = "20170808_p_ платное.pdf",
                documentYear = Document.Year.y2017,
                documentType = Person.StudyForm.paid
            },
            new Document()
            {
                fileName = "20170729 бви.pdf",
                documentYear = Document.Year.y2017,
                documentType = Person.StudyForm.targeting
            },

            // 2016
            new Document()
            {
                fileName = "03082016.pdf",
                documentYear = Document.Year.y2016,
                documentType = Person.StudyForm.budget
            },
            new Document()
            {
                fileName = "08082016 бюджет.pdf",
                documentYear = Document.Year.y2016,
                documentType = Person.StudyForm.budget
            },
            new Document()
            {
                fileName = "08082016p платное.pdf",
                documentYear = Document.Year.y2016,
                documentType = Person.StudyForm.paid
            },
            new Document()
            {
                fileName = "31082016p платное.pdf",
                documentYear = Document.Year.y2016,
                documentType = Person.StudyForm.paid
            },
            new Document()
            {
                fileName = "29072016 бви.pdf",
                documentYear = Document.Year.y2016,
                documentType = Person.StudyForm.targeting
            }
        };

        
    }
}

