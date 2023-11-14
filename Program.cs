using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeworkPlanner
{
    internal class Program // the comments are here you just have to look carefully
    {
        public class homework
        {
            public string title { get; set; }
            public string description { get; set; }
            public DateTime dueDate { get; set; }
            public bool isDone { get; set; }   
            public int iD {  get; set; }

            public homework(string Title, string Description, DateTime DueDate, bool IsDone, int ID)
            {
                title = Title;
                description = Description;
                dueDate = DueDate;
                isDone = IsDone;
                iD = ID;
            }
        }

        public static List<homework> homeworkList = new List<homework>();
        static void Main()
        {
            initiate();
            mainMenu();
        }
        
        static void initiate()
        {
            StreamReader getLog = new StreamReader("homework.txt");

            string[] lines = getLog.ReadToEnd().Split(new char[] { '\n' });

            foreach (string line in lines)
            {
               string[] data = line.Split(new char[] {','});
               
               homeworkList.Add( new homework(data[0], data[1], DateTime.Parse(data[2]), bool.Parse(data[3]), int.Parse(data[4])));         
            }               
            
            homeworkList = homeworkList.OrderBy(x => x.dueDate).ToList();

            getLog.Close();
            
        }
        static void mainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("########################################");
                Console.WriteLine("");
                Console.WriteLine("           Homework Planner");
                Console.WriteLine("");
                Console.WriteLine("############ - Main Menu - #############");
                Console.WriteLine("");
                Console.WriteLine("[1] - View Upcoming Homework");
                Console.WriteLine("[2] - Add A New Entry");
                Console.WriteLine("[3] - Edit And Change An Existing Entry");
                Console.WriteLine("[4] - Exit Planner");
                Console.WriteLine("");
                Console.WriteLine("########################################");

                char input = Console.ReadKey().KeyChar;

                switch (input)
                {
                    case '1':
                        displayHomework();
                        break;
                    case '2':
                        addHomework();
                        break;
                    case '3':
                        editHomework();
                        break;
                    case '4':
                        exit();
                        break;
                }
            }    
        }

        static void displayHomework()
        {

                Console.Clear();
                Console.WriteLine("########################################");
                Console.WriteLine("");
                Console.WriteLine("           Homework List");
                Console.WriteLine("    Sorted By Ascending Due Date");
                Console.WriteLine("");
                Console.WriteLine("########################################");
                Console.WriteLine("");

                if (homeworkList.Count == 0)
                {
                    Console.WriteLine("No Homework Is Upcoming!");
                }
                else
                {
                    foreach (homework homework in homeworkList)
                    {
                        Console.WriteLine("Homework: " + homework.title);
                        Console.WriteLine("          " + homework.description);
                        Console.WriteLine("     Due: " + homework.dueDate.ToShortDateString());
                        if (homework.isDone )
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("           Completed!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("           Incomplete.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.WriteLine("");
                    
                }
                }
            Console.WriteLine("Press any key to continue:");
            Console.ReadKey();


        }

        static void addHomework()
        {
            Console.Clear();

            DateTime due;
            Console.WriteLine("########################################");
            Console.WriteLine("");
            Console.WriteLine("             Add Homework");
            Console.WriteLine("");
            Console.WriteLine("########################################");
            Console.WriteLine("");
            Console.WriteLine("Enter Name Of New Homework:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Description Of New Homework:");
            string desc = Console.ReadLine();
            Console.WriteLine("Enter Due Date Of Homework In The Form (DD/MM/YYYY)");
            while (true)
            {
                try
                {
                    due = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("en-UK"));
                    if (due > DateTime.Today)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Date must be in future.");
                    }
                }
                catch
                {
                    Console.WriteLine("Incorrect format");
                }
            }
             
            Console.WriteLine("Name: " + name);
            Console.WriteLine("Description: " + desc);
            Console.WriteLine("Due Date: " + due.ToString());
            Console.WriteLine("");
            Console.WriteLine("Press Y to confirm, or any other key to cancel.");

            List<homework> idSort = homeworkList.OrderBy(x => x.iD).ToList();
            int lastID = idSort[0].iD;
            char input = Console.ReadKey().KeyChar;
            if (input == 'y')
            {
                homework newHW = new homework(name, desc, due, false, lastID);
                homeworkList.Add(newHW);
                return;
            }
            else
            {
                return;
            }
        }

        static void editHomework()
        {
            Console.Clear();
            Console.WriteLine("########################################");
            Console.WriteLine("");
            Console.WriteLine("             Edit Homework");
            Console.WriteLine("");
            Console.WriteLine("########################################");
            Console.WriteLine("");
            Console.WriteLine("Enter ID Of Homework To Edit.");
            foreach (homework homework in homeworkList)
            {
                Console.WriteLine ("[" + homework.iD + "]  - " + homework.title );
            }

            homework found = null;

            while (true)
            {
                try
                {
                    int input = int.Parse(Console.ReadKey().KeyChar.ToString());
                    homework foundElement = homeworkList.FirstOrDefault(x => x.iD == input);
                    if (foundElement != null)
                    {
                        found = foundElement;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Input error. Try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Input error. Try again.");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
            Console.WriteLine("Select attribute to edit for " + found.title + ".");
            Console.WriteLine("");
            Console.WriteLine("[1] Title");
            Console.WriteLine("[2] Description");
            Console.WriteLine("[3] Due Date");
            Console.WriteLine("[4] Mark As Done");
            Console.WriteLine("[5] Delete");
            Console.WriteLine("");

            char input2;

            while (true)
            {
                input2 = Console.ReadKey().KeyChar;

                Console.WriteLine("");

                if (input2 == '1' || input2 == '2' || input2 == '3' || input2 == '4' || input2 == '5')
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Input error. Please try again.");
                }
            }

            switch (input2)
            {
                case '1':
                    Console.WriteLine("Enter new title");
                    string newTitle = Console.ReadLine();
                    found.title = newTitle;
                    break;

                case '2':
                    Console.WriteLine("Enter new description");
                    string newDesc = Console.ReadLine();
                    found.description = newDesc;
                    break;

                case '3':
                    Console.WriteLine("Enter new due date");
                    DateTime due;
                    while (true)
                    {
                        try
                        {
                            due = DateTime.ParseExact(Console.ReadLine(), "dd/MM/yyyy", new CultureInfo("en-UK"));
                            if (due > DateTime.Today)
                            {
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Date must be in future.");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Incorrect format");
                        }
                    }
                    found.dueDate = due;
                    Console.WriteLine("Due date changed!");
                    break;

                case '4':
                    if (found.isDone)
                    {
                        Console.WriteLine("Homework is already completed. Press Y to mark as incomplete.");
                        char input = Console.ReadKey().KeyChar;

                        if (input == 'y')
                        {
                            found.isDone = false;
                        }
                    }
                    else
                    {
                        found.isDone = true;
                        Console.WriteLine("Homework marked as completed!");
                    }
                    break;

                case '5':

                    homeworkList.RemoveAll(x => x.iD == found.iD);
                    Console.WriteLine("Homework Removed.");
                    break;

            }
            Console.ReadKey();
        }

        static void exit()
        {
            StreamWriter sw = new StreamWriter("homework.txt");
            int count = 1;

            foreach(homework hw in homeworkList)
            {
                sw.Write(hw.title);
                sw.Write(",");
                sw.Write(hw.description);
                sw.Write(",");
                sw.Write(hw.dueDate.ToShortDateString());
                sw.Write(",");
                sw.Write(hw.isDone.ToString()); 
                sw.Write(",");
                sw.Write(count.ToString());
                sw.Write('\n');
                count++;
                
            }
            sw.Close();
            Environment.Exit(0);
        }
    }
}
