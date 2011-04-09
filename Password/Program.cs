using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Password
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Password Storage, by Ethryx");
                Console.WriteLine("For help, use pw -h");
            }
            else if (args[0].ToLower() == "-h" || args[0].ToLower() == "-help")
            {
                Console.WriteLine("Usage pw [-amxdhol] [alias] [password]");
                Console.WriteLine("-h = shows this help text");
                Console.WriteLine("-o = opens database in default text viewer");
                Console.WriteLine("-a = add an entry to the database");
                Console.WriteLine("-m = modify an existing entry");
                Console.WriteLine("-d = delete an entry from the database");
                Console.WriteLine("-x = place entry on the clipboard");
                Console.WriteLine("-l = lists all passwords saved");
            }
            else if (args[0].ToLower() == "-o")
            {
                if (File.Exists("passwds.txt"))
                    System.Diagnostics.Process.Start("passwds.txt");
                else
                    Console.WriteLine("Database doesn't exist yet");
            }
            else if (args[0].ToLower() == "-a")
            {
                if (entryExists(args[1].ToLower()))
                    Console.WriteLine("Already exists");
                else
                {
                    if (args.Length == 3)
                    {
                        addEntry(args[1].ToLower(), args[2]);
                        Console.WriteLine("Added");
                    }
                    else
                        Console.WriteLine("You need to specify an actual password");
                }
            }
            else if (args[0].ToLower() == "-m")
            {
                if (!entryExists(args[1].ToLower()))
                    Console.WriteLine("Doesn't exist");
                else
                {
                    if (args.Length == 3)
                    {
                        modifyEntry(args[1].ToLower(), args[2]);
                        Console.WriteLine("Saved");
                    }
                    else
                        Console.WriteLine("You need to specify an actual password");
                }
            }
            else if (args[0].ToLower() == "-d")
            {
                if (!entryExists(args[1].ToLower()))
                    Console.WriteLine("Doesn't exist");
                else
                {
                    deleteEntry(args[1].ToLower());
                    Console.WriteLine("Deleted");
                }
            }
            else if (args[0].ToLower() == "-x")
            {
                getEntryPassword(args[1].ToLower());
            }
            else if (args[0].ToLower() == "-l")
            {
                displayEntries();
            }
            else
            {
                Console.WriteLine("Invalid parameter");
            }
        }

        static bool entryExists(string check)
        {
            bool exists = false;
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                string line = "";
                while ((line = tr.ReadLine()) != null)
                {
                    string[] entry = line.Split(' ');
                    if (entry[0] == check)
                    {
                        exists = true;
                        break;
                    }
                }
                tr.Close();
            }
            catch { }
            return exists;
        }

        static void getEntryPassword(string key)
        {
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                string line = "";
                while ((line = tr.ReadLine()) != null)
                {
                    string[] entry = line.Split(' ');
                    if (entry[0] == key)
                    {
                        Console.WriteLine("Set to clipboard");
                        System.Windows.Forms.Clipboard.SetText(entry[1]);
                        break;
                    }
                }
                tr.Close();
            }
            catch { }
        }

        static void displayEntries()
        {
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                string line = "";
                while ((line = tr.ReadLine()) != null)
                {
                    string[] entry = line.Split(' ');
                    Console.WriteLine(entry[0]);
                }
                tr.Close();
            }
            catch
            {
                Console.WriteLine("Database doesn't exist yet");
            }
        }

        static void addEntry(string key, string value)
        {
            string currentContents = "";
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                currentContents = tr.ReadToEnd() + Environment.NewLine;
                tr.Close();
            }
            catch { }
            TextWriter tw = new StreamWriter("passwds.txt");
            tw.Write(currentContents + key + " " + value);
            tw.Close();
        }

        static void modifyEntry(string key, string value)
        {
            string contents = "";
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                string line = "";
                while ((line = tr.ReadLine()) != null)
                {
                    string[] entry = line.Split(' ');
                    if (entry[0] == key)
                        contents += key + " " + value + Environment.NewLine;
                    else
                        contents += line + Environment.NewLine;
                }
                tr.Close();
                contents = contents.Substring(0, contents.Length - Environment.NewLine.Length);
            }
            catch { }
            TextWriter tw = new StreamWriter("passwds.txt");
            tw.Write(contents);
            tw.Close();
        }

        static void deleteEntry(string key)
        {
            string contents = "";
            try
            {
                TextReader tr = new StreamReader("passwds.txt");
                string line = "";
                while ((line = tr.ReadLine()) != null)
                {
                    string[] entry = line.Split(' ');
                    if (entry[0] != key)
                        contents += line + Environment.NewLine;
                }
                tr.Close();
                if(contents != "")
                    contents = contents.Substring(0, contents.Length - Environment.NewLine.Length);
            }
            catch { }
            TextWriter tw = new StreamWriter("passwds.txt");
            tw.Write(contents);
            tw.Close();
        }
    }
}
