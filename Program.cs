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
            // If inventory.txt doesn't exist, add inventory.txt to bin
            if (!File.Exists(filename))
            {
                // Create new file and close
                try
                {
                    FileStream fs = File.Create(filename);
                    fs.Close();
                    return;
                }
                // Something went wrong during creation... (shouldn't happen)
                catch
                {
                    throw new Exception("Something went wrong. Aborting program...");
                }
            }

            // File already exists, then read the data
            string data = File.ReadAllText(filename);
            
            // get individual lines
            string[] lines = data.Split('\n');
            
            // iterate through each line and grab the data values
            foreach (string line in lines)
            {
                // ignore blank lines
                if (line == "") continue;

                // allows users to include spaces in their descriptions (a '@' character in the
                // description will still break it...)
                string[] values = line.Split('@');
                inventory.Add(new Item(values[0], values[1], int.Parse(values[2])));
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

        // Updates the quantity of a user provided part, provides an error if part is not in
        // inventory.
        // Parameters: None
        // Returns: bool, indicates if operation was successful or not
        private static bool UpdateQuantity()
        {
            // Holds user inputs
            string number, new_qty_inp;

            // Indicates if the item was found
            bool found = false;

            // holds list index of where the part was at
            int index = 0;

            // get user input for item number
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            // search for number
            foreach (Item item in inventory)
            {
                if (item.PartNum == number)
                {
                    found = true;
                    break;
                }
                index++;
            }

            // If not found, indicate unsuccessful search
            if (!found)
            {
                Console.WriteLine("Item not in inventory!");
                return false;
            }

            // get quantity input
            Console.Write("Enter new item quantity: ");
            new_qty_inp = Console.ReadLine();

            // update quantity if input was a valid integer
            if (int.TryParse(new_qty_inp, out int new_qty))
            {
                inventory[index].Qty = new_qty;
                return true;
            }

            // Print error if invalid typing was given and abort
            Console.WriteLine("Error! Quantity must be an integer! Aborting operation...");
            return false;
        }

        // Adds an item to the inventory
        // Parameters: None
        // Returns: bool, indicating if the operation was successful or not
        private static bool AddItem()
        {
            // hold user inputs
            string number, desc, q;

            // get id number
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            // ensure duplicates don't exist
            foreach (Item item in inventory)
            {
                if (item.PartNum == number)
                {
                    Console.WriteLine("Error! " + number + " already exists! Aborting " +
                        "operation...");
                    return false;
                }
            }

            // get description
            Console.Write("Enter Item Description: ");
            desc = Console.ReadLine();

            // get quantity
            Console.Write("Enter Item Quantity: ");
            q = Console.ReadLine();

            // ensure quantity was an integer
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

        // Look up an item in the inventory
        // Parameters: None
        // Returns: bool, indicating if the operation was successful or not
        private static bool LookupItem()
        {
            // hold user input
            string number;

            // get item id number
            Console.Write("Enter Item ID Number: ");
            number = Console.ReadLine();

            // search for item in inventory
            foreach (Item item in inventory)
            {
                // if found, print information and indicate success
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

            // if not found, print message
            Console.WriteLine("Item not found!");
            return false;
        }

        // Helper function to check for successful execution of menu functions
        // Parameters: Func<bool> func, a function that returns a boolean value
        // Returns: void
        private static void CheckSuccess(Func<bool> func)
        {
            // start with no success
            bool success = false;
            while (!success)
            {
                // run passed in function
                success = func();

                // if it was not successful, prompt user if they would like to try again or abort
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

            // run main loop (menu loop) while exit selection is not given
            while (selection != 4)
            {
                selection = Menu();
                Console.Clear();

                // determine which function to run based on user input
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
                        // truncate inventory file
                        using (FileStream fs = File.Open(filename, FileMode.Truncate))
                        {
                            fs.Close();
                        }

                        // write inventory list to file
                        foreach (Item item in inventory)
                        {
                            File.AppendAllText(filename,
                                $"{item.PartNum}@{item.Description}@{item.Qty}\n");
                        }

                        // write confirming message to console
                        Console.WriteLine("Program aborted.");
                        break;
                }
            }
        }
    }
}
