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
                    File.Create(filename);
                }
                catch
                {
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

            Console.Clear();
            Console.WriteLine("##################################################");
            Console.WriteLine("#####                  Menu                  #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##### [1] Update Inventory Quantity          #####");
            Console.WriteLine("##### [2] Add a New Item                     #####");
            Console.WriteLine("##### [3] Format Inventory Data Sheet        #####");
            Console.WriteLine("##### [4] Lookup Item via Item Number        #####");
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
                    else return;
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

            while (selection != 5)
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
                        break;
                    case 4:
                        break;
                    case 5:
                        foreach (Item item in inventory)
                        {
                            File.WriteAllText(filename, $"{item.PartNum} {item.Description} {item.Qty}");
                        }
                        Console.WriteLine("Program aborted.");
                        break;
                }
            }
        }
    }
}
