/*
 * Name: James A. Chase
 * File: Program.cs
 * Date: 18 March 2024
*/

namespace inventory_manager
{
    internal class Program
    {
        // Runs basic setup for the program, checks if storage file exists
        // Parameters: None
        // Returns: void
        private static void Setup()
        {
            string data_file = "inventory.txt";

            if (!File.Exists(data_file))
            {
                try
                {
                    File.Create(data_file);
                }
                catch
                {
                    Console.WriteLine();
                    throw new Exception("Something went wrong. Aborting program...");
                }
            }
        }

        // Displays menu and handles user selected option
        // Parameters: None
        // Returns: int (contains user choice)
        private static int Menu()
        {
            string userInp;

            Console.WriteLine("##################################################");
            Console.WriteLine("#####                  Menu                  #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##### [1] Update Inventory Quantity          #####");
            Console.WriteLine("##### [2] Add a new part                     #####");
            Console.WriteLine("##### [3] Format Inventory Data Sheet        #####");
            Console.WriteLine("##### [4] Lookup part via part number        #####");
            Console.WriteLine("##### [5] End Program                        #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##################################################");

            while (true)
            {
                Console.Write("> ");
                userInp = Console.ReadLine();

                if (int.TryParse(userInp, out int menu_val))
                {
                    if (menu_val > 0 && menu_val < 6) return menu_val;
                    else {
                        Console.WriteLine(userInp + " is not a valid menu option.\n");
                    }
                }
                else
                {
                    Console.WriteLine("Error! Input must be an integer!\n");
                }
            }
        }

        // Main Function
        static void Main()
        {
            try
            {
                Setup();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return;
            }

            int selection = 0;

            Console.WriteLine("##################################################");
            Console.WriteLine("##### Welcome to Inventory Management System #####");
            Console.WriteLine("##################################################");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            Console.Clear();

            while (selection != 5)
            {
                selection = Menu();
                switch(selection)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                }
            }
        }
    }
}
