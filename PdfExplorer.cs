using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PersonFinder.src
{
    public static class PdfExplorer
    {
        private static List<Document> docs = new List<Document>();

        public static List<DatabaseCell> Parse(List<Document> docs)
        {
            List<DatabaseCell> list = new List<DatabaseCell>();
            try
            {
                foreach(Document doc in docs)
                {
                    try
                    {
                        using (PdfReader pdfReader = new PdfReader(doc.path))
                        {
                            DatabaseCell cell = new DatabaseCell();
                            string text = "";
                            for (int i = 1; i <= pdfReader.NumberOfPages; i++) text += " " + PdfTextExtractor.GetTextFromPage(pdfReader, i);
                            text = Regex.Replace(text, @"\r\n?|\n", " ");
                            MatchCollection m = Regex.Matches(text, @"\§(.*?)\s\d\d\d;");
                            for (int i = 0; i < m.Count; i++)
                            {
                                cell.stringData.Add(m[i].Value);
                            }
                            cell.studyForm = doc.documentType;
                            cell.year = doc.documentYear;
                            if (cell != null) list.Add(cell);
                            else continue;
                        }
                    }
                    catch(Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Error: {ex.ToString()}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                return list;
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Ошибка: {ex.ToString()}");
                Console.ForegroundColor = ConsoleColor.White;
                return null;
            }
        }

        public static List<Person> Find(string request)
        {
            List<Person> foundedPersons = new List<Person>();
            request = request.ToUpper();
            MatchCollection collection = Regex.Matches(request, @"[^\s](\w+)");
            List<string> req = new List<string>();
            for(int i = 0; i < collection.Count; i++) req.Add(collection[i].Value);

            foreach(DatabaseCell cell in Database.mainDatabase)
            {
                foreach(string str in cell.stringData)
                {
                    string localStr = str;
                    localStr = localStr.ToUpper();

                    bool cont = false;
                    string n = Regex.Matches(str, @"[^\§(\w+)](\D+)[^\;]")[0].Value;
                    MatchCollection names = Regex.Matches(n, @"\w+");

                    foreach (string r in req)
                    {
                        bool wordFounded = false;
                        for (int j = 0; j < names.Count; j++)
                        {
                            if (names[j].Value.ToUpper() == r)
                            {
                                wordFounded = true;
                                break;
                            }
                        }
                        if (!wordFounded)
                        {
                            cont = false;
                            break;
                        }
                        else cont = true;
                    }
                    if(cont)
                    {
                        try
                        {
                            
                            string name = Regex.Matches(str, @"[^\§(\w+)](\w+\s+\w+\s+\w+)")[0].Value;
                            string groupf = Regex.Matches(str, @"(\w+-\w+)")[0].Value;
                            string pointsf = Regex.Matches(str, @"(\s+\d\d\d)")[0].Value;

                            Person per = new Person
                            {
                                fullName = name,
                                group = groupf,
                                points = pointsf.Replace(" ", ""),
                                formOfStudy = cell.studyForm,
                                year = cell.year.ToString().Replace("y", "")
                            };
                            foundedPersons.Add(per);
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"Исключение типа: {ex.ToString()}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
            return foundedPersons;
        }
    }
}
