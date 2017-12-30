using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Black;
            while (true)
            {
                string temp;
                byte[] file;
                byte[] salt = null;
                string password;
                int max =0;
                Console.WriteLine(@"> /$$$$$$$$ /$$                        /$$$$$$                                  /$$                     /$$$  ");
                Console.WriteLine(@">|__  $$__/| $$                       /$$__  $$                                | $$                    |_  $$ ");
                Console.WriteLine(@">   | $$   | $$$$$$$   /$$$$$$       | $$  \__/  /$$$$$$  /$$   /$$  /$$$$$$  /$$$$$$         /$$        \  $$");
                Console.WriteLine(@">   | $$   | $$__  $$ /$$__  $$      | $$       /$$__  $$| $$  | $$ /$$__  $$|_  $$_/        |__/         | $$");
                Console.WriteLine(@">   | $$   | $$  \ $$| $$$$$$$$      | $$      | $$  \__/| $$  | $$| $$  \ $$  | $$                       | $$");
                Console.WriteLine(@">   | $$   | $$  | $$| $$_____/      | $$    $$| $$      | $$  | $$| $$  | $$  | $$ /$$       /$$         /$$/");
                Console.WriteLine(@">   | $$   | $$  | $$|  $$$$$$$      |  $$$$$$/| $$      |  $$$$$$$| $$$$$$$/  |  $$$$/      |__/       /$$$/ ");
                Console.WriteLine(@">   |__/   |__/  |__/ \_______/       \______/ |__/       \____  $$| $$____/    \___/                  |___/  ");
                Console.WriteLine(@">                                                         /$$  | $$| $$                                       ");
                Console.WriteLine(@">                                                        |  $$$$$$/| $$                                       ");
                Console.WriteLine(@">                                                         \______/ |__/                                       ");
                Console.WriteLine(">\n>\n>\n>\n>Would you like to\n>(1) Encrypt Data\n>(2) Decrypt Data");
                do
                {
                    Console.Write(">");
                    temp = Console.ReadLine();
                } while (temp != "1" && temp != "2");
                switch (Convert.ToInt32(temp))
                {
                    case 1:
                        string fileLocation;
                        do
                        {
                            Console.Write(">Please select the file you would like to encrypt:");
                            fileLocation = Console.ReadLine();
                        } while (!System.IO.File.Exists(fileLocation));
                        Console.WriteLine(">File is loading");

                        file = System.IO.File.ReadAllBytes(fileLocation);
                        Console.WriteLine(">File is loaded");
                        do
                        {
                            Console.Write(">File has loaded would you like to delete the main copy (Y/N):");
                            temp = Console.ReadLine().ToLower();
                        } while (temp != "y" && temp != "n");
                        if (temp == "y")
                            System.IO.File.Delete(fileLocation);
                        Console.Write(">Please select if you would like to\n>(1) Enter a Code to use\n>(2) Select a file to use\n>Please not what ever you chose you need to use the same to get your files back");
                        do
                        {
                            Console.Write(">");
                            temp = Console.ReadLine();
                        } while (temp != "1" && temp != "2");
                        switch (Convert.ToInt32(temp))
                        {
                            case 1:
                                Console.Write("Please enter code:");
                                salt = Encoding.ASCII.GetBytes(Console.ReadLine());
                                break;
                            case 2:
                                do
                                {
                                    Console.Write(">Please select the file you would like to use:");
                                    fileLocation = Console.ReadLine();
                                } while (!System.IO.File.Exists(fileLocation));
                                Console.WriteLine(">File is loading");
                                salt = System.IO.File.ReadAllBytes(fileLocation);
                                Console.WriteLine(">File is loaded");
                                break;
                        }
                        do
                        {
                            Console.Write(">Please enter rounds: ");
                            temp = Console.ReadLine();
                        } while (!int.TryParse(temp,out max));
                        Console.Write(">Please enter a Password:");
                        password = Console.ReadLine();
                        for (int i = 0; i < max; i++)
                        {
                            file = Cryptography.Cryptography.EncryptBytes(file, password, max, salt);
                            Console.WriteLine($"{(decimal)i / (decimal)max * (decimal)100}%");
                        }
                        System.IO.File.WriteAllBytes("out.ecp", file);
                        Console.WriteLine($">THe file has been put in {Environment.CurrentDirectory}\\out.ecp");
                        break;
                    case 2:
                        string fileLocation2;
                        do
                        {
                            Console.Write(">Please select the file you would like to decrypt:");
                            fileLocation = Console.ReadLine();
                        } while (!System.IO.File.Exists(fileLocation));
                        Console.WriteLine(">File is loading");

                        file = System.IO.File.ReadAllBytes(fileLocation);
                        Console.WriteLine(">File is loaded");
                        Console.Write(">Please select if you would like to\n>(1) Enter a Code to use\n>(2) Select a file to use\n>Please not what ever you chose you need to use the same to get your files back");
                        do
                        {
                            Console.Write(">");
                            temp = Console.ReadLine();
                        } while (temp != "1" && temp != "2");
                        switch (Convert.ToInt32(temp))
                        {
                            case 1:
                                Console.Write(">Please enter code:");
                                salt = Encoding.ASCII.GetBytes(Console.ReadLine());
                                break;
                            case 2:
                                do
                                {
                                    Console.Write(">Please select the file you would like to use:");
                                    fileLocation2 = Console.ReadLine();
                                } while (!System.IO.File.Exists(fileLocation2));
                                Console.WriteLine(">File is loading");
                                salt = System.IO.File.ReadAllBytes(fileLocation2);
                                Console.WriteLine(">File is loaded");
                                break;
                        }
                        do
                        {
                            Console.Write(">Please enter rounds: ");
                            temp = Console.ReadLine();
                        } while (!int.TryParse(temp, out max));
                        Console.Write(">Please enter a Password:");
                        password = Console.ReadLine();
                        for (int i = 0; i < max; i++)
                        {
                            file = Cryptography.Cryptography.DecryptBytes(file, password, max, salt);
                            Console.WriteLine($"{(decimal)i / (decimal)max * (decimal)100}%");
                        }
                        System.IO.File.WriteAllBytes("out.dcp", file);
                        Console.WriteLine($">THe file has been put in {Environment.CurrentDirectory}\\out.dcp\n>You will have to change the file name and type to match");
                        break;
                }
                Console.ReadLine();
            }
        }
    }

}
