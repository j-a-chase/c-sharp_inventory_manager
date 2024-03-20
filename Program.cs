/*
 * Name: James A. Chase
 * File: Program.cs
 * Date: 18 March 2024
*/

namespace inventory_manager
{
    internal class Program
    {
        // Holds Current Inventory
        private static List<Item> inventory = [];

        // Holds file name for inventory storage to be read and written to
        private readonly static string filename = "inventory.txt";

        // Runs basic setup for the program, checks if storage file exists
        // Parameters: None
        // Returns: void
        private static void Setup()
        {
            if (!File.Exists(filename))
            {
                try
                {
                    FileStream fs = File.Create(filename);
                    fs.Close();
                    return;
                }
                catch
                {
                    throw new Exception("Something went wrong. Aborting program...");
                }
            }
            else
            {
                string data = File.ReadAllText(filename);
                string[] lines = data.Split('\n');
                foreach (string line in lines)
                {
                    if (line == "") continue;
                    string[] values = line.Split('@');
                    inventory.Add(new Item(values[0], values[1], int.Parse(values[2])));
                }
            }
        }

        // Displays menu and handles user selected option
        // Parameters: None
        // Returns: int (contains user choice)
        private static int Menu()
        {
            string userInp;

            Console.Clear();
            Console.WriteLine("##################################################");
            Console.WriteLine("#####                  Menu                  #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##### [1] Update Inventory Quantity          #####");
            Console.WriteLine("##### [2] Add a New Item                     #####");
            Console.WriteLine("##### [3] Lookup Item via Item Number        #####");
            Console.WriteLine("##### [4] End Program                        #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##################################################");

            while (true)
            {
                Console.Write("> ");
                userInp = Console.ReadLine();

                if (int.TryParse(userInp, out int menu_val))
                {
                    if (menu_val > 0 && menu_val < 5) return menu_val;
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

        private static bool UpdateQuantity()
        {
            string number, new_qty_inp;
            bool found = false;
            int index = 0;
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            foreach (Item item in inventory)
            {
                if (item.PartNum == number)
                {
                    found = true;
                    break;
                }
                index++;
            }

            if (!found)
            {
                Console.WriteLine("Item not in inventory!");
                return false;
            }

            Console.Write("Enter new item quantity: ");
            new_qty_inp = Console.ReadLine();
            if (int.TryParse(new_qty_inp, out int new_qty))
            {
                inventory[index].Qty = new_qty;
                return true;
            }

            Console.WriteLine("Error! Quantity must be an integer! Aborting operation...");
            return false;
        }

        private static bool AddItem()
        {
            string number, desc, q;
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            foreach (Item item in inventory)
            {
                if (item.PartNum == number)
                {
                    Console.WriteLine("Error! " + number + " already exists! Aborting operation...");
                    return false;
                }
            }

            Console.Write("Enter Item Description: ");
            desc = Console.ReadLine();

            Console.Write("Enter Item Quantity: ");
            q = Console.ReadLine();

            if (int.TryParse(q, out int q_val))
            {
                inventory.Add(new Item(number, desc, q_val));
            }
            else
            {
                Console.WriteLine("Error! Quantity must be an integer! Aborting operation...");
                return false;
            }

            return true;
        }

        private static bool LookupItem()
        {
            string number;
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            foreach (Item item in inventory)
            {
                if (item.PartNum == number)
                {
                    Console.WriteLine("Item ID: " + item.PartNum);
                    Console.WriteLine("Description: " + item.Description);
                    Console.WriteLine("Quantity: " + item.Qty);
                    Console.WriteLine("\nPress Enter to continue...");
                    Console.ReadLine();
                    return true;
                }
            }

            Console.WriteLine("Item not found!");
            return false;
        }

        private static void CheckSuccess(Func<bool> func)
        {
            bool success = false;
            while (!success)
            {
                success = func();
                if (!success)
                {
                    Console.Write("\nDo you wish to try again? [y/n]: ");
                    string response = Console.ReadLine();

                    if (response == "n") success = true;
                    else Console.WriteLine();
                }
            }
        }

        // Main Function
        static void Main()
        {
            // run setup
            try
            {
                Setup();
            }
            catch (Exception e) {
                Console.WriteLine(e);
                return;
            }

            // holds user menu selection
            int selection = 0;

            // print intro display
            Console.WriteLine("##################################################");
            Console.WriteLine("##### Welcome to Inventory Management System #####");
            Console.WriteLine("##################################################");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();

            // clear console for menu operations
            Console.Clear();

            while (selection != 4)
            {
                selection = Menu();
                Console.Clear();
                switch(selection)
                {
                    case 1:
                        CheckSuccess(UpdateQuantity);
                        break;
                    case 2:
                        CheckSuccess(AddItem);
                        break;
                    case 3:
                        CheckSuccess(LookupItem);
                        break;
                    case 4:
                        using (FileStream fs = File.Open(filename, FileMode.Truncate))
                        {
                            fs.Close();
                        }
                        foreach (Item item in inventory)
                        {
                            File.AppendAllText(filename, $"{item.PartNum}@{item.Description}@{item.Qty}\n");
                        }
                        Console.WriteLine("Program aborted.");
                        break;
                }
            }
        }
    }
}
