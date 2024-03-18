/*
 * Name: James A. Chase
 * File: Program.cs
 * Date: 18 March 2024
*/

namespace inventory_manager
{
    internal class Program
    {
        static void menu()
        {
            bool valid_inp = false;
            int menu_val;
            string userInp;

            Console.WriteLine("##################################################");
            Console.WriteLine("#####                  Menu                  #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##### [1] Update Inventory Quantity          #####");
            Console.WriteLine("##### [2] Add a new part                     #####");
            Console.WriteLine("##### [3] Format Inventory Data Sheet        #####");
            Console.WriteLine("##### [4] Lookup part via part number        #####");
            Console.WriteLine("#####----------------------------------------#####");
            Console.WriteLine("##################################################");

            while (!valid_inp)
            {
                Console.Write("> ");
                userInp = Console.ReadLine();

                if (int.TryParse(userInp, out menu_val))
                {
                    valid_inp = true;
                    switch (menu_val)
                    {
                        case 1:
                            Console.WriteLine(menu_val);
                            break;
                        case 2:
                            Console.WriteLine(menu_val);
                            break;
                        case 3:
                            Console.WriteLine(menu_val);
                            break;
                        case 4:
                            Console.WriteLine(menu_val);
                            break;
                        default:
                            Console.WriteLine(userInp + " is not a valid menu option.\n");
                            valid_inp = false;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Error! Input must be an integer!\n");
                }
            }
        }

        static void Main()
        {
            Console.WriteLine("##################################################");
            Console.WriteLine("##### Welcome to Inventory Management System #####");
            Console.WriteLine("##################################################");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();

            menu();
        }
    }
}
