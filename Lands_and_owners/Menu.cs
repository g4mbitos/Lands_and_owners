using System;
using System.IO;

namespace Lands_and_owners
{
    class Menu
    {
        static public bool StartPage(ref Money[] owners, ref Square[] lands) // Main menu of program
        {
            Console.Clear();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("| Make choise:     |");
            Console.WriteLine("|                  |");
            Console.WriteLine("| 1: Man           |");
            Console.WriteLine("| 2: Land          |");
            Console.WriteLine("| 0: Exit          |");
            Console.WriteLine("|                  |");
            Console.Write("| Choise:          |\n");
            Console.WriteLine(new string('-', 20));
            Console.SetCursorPosition(10, 7);

            switch (Console.ReadLine())
            {
                case "0":
                    Console.Clear();
                    Console.WriteLine(new string('-', 15));
                    Console.WriteLine("|             |");
                    Console.WriteLine("| GOOD BYE!!! |");
                    Console.WriteLine("|             |");
                    Console.WriteLine(new string('-', 15));

                    return false;
                case "1":
                    while (Man(ref owners, ref lands)) ;
                    return true;
                case "2":
                    while (Land(ref owners, ref lands)) ;
                    return true;
                default:
                    return true;
            }
        }

        //**********************************************************************************************************************************

        static bool Man(ref Money[] owners, ref Square[] lands) // Method for working with man
        {
            Console.Clear();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("| Make choise:     |");
            Console.WriteLine("|                  |");
            Console.WriteLine("| 1: New Man       |");
            Console.WriteLine("| 2: Show all men  |");
            Console.WriteLine("| 3: Change man    |");
            Console.WriteLine("| 0: Back          |");
            Console.WriteLine("|                  |");
            Console.Write("| Choise:          |\n");
            Console.WriteLine(new string('-', 20));
            Console.SetCursorPosition(10, 8);

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    NewMan(ref owners, ref lands);
                    return true;
                case "2":
                    Console.Clear();
                    Show<Money>(owners, -1);
                    Console.ReadKey();
                    return true;
                case "3":
                    while (ChoiseMan(ref owners, ref lands)) ;
                    return true;
                default:
                    return true;
            }
        }

        //**********************************************************************************************************************************

        static void NewMan(ref Money[] owners, ref Square[] lands) // Method of creating new man
        {
            Console.Clear();
            string name;
            int hryvna = 0;
            int kopyika = 0;
            Console.WriteLine("Adding new owner");
            Console.WriteLine("\nEnter name");
            name = Console.ReadLine();

            if (!CheckName(name, owners, -1))
            {
                Console.WriteLine("\nEnter hryvna");
                int.TryParse(Console.ReadLine(), out hryvna);
                Console.WriteLine("\nEnter kopyika");
                int.TryParse(Console.ReadLine(), out kopyika);

                Array.Resize(ref owners, owners.Length + 1);
                owners[owners.Length - 1] = new Money(name, hryvna, kopyika);
                Console.WriteLine("\nSuccessfully added");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("This name allready exists");
                Console.ReadKey();
            }
        }

        //**********************************************************************************************************************************

        static void Show<T>(T[] array, int index) // Method for show all men or lands
        {
            int count = 1;
            foreach (var el in array)
            {
                if (count != index)
                    Console.WriteLine($"{count}. {el}");
                count++;
            }
        }

        //**********************************************************************************************************************************

        static bool ChoiseMan(ref Money[] owners, ref Square[] lands) // Method for choising man and then change him
        {
            Console.Clear();
            Console.WriteLine("Choise man for changing: ");
            Console.WriteLine();
            Show<Money>(owners, -1);
            Console.WriteLine("0: Back");
            Console.WriteLine();

            int index;

            Console.Write("Choise: ");

            if (int.TryParse(Console.ReadLine(), out index) && (index >= 0 && index <= owners.Length))
            {
                if (index > 0 && index <= owners.Length)
                {
                    for (int i = 1; i <= owners.Length; i++)
                    {
                        if (index == i)
                        {
                            while (ChangeMan(ref owners, ref lands, index)) ;
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                NotCorrectIndex();
                return true;
            }
        }

        //**********************************************************************************************************************************

        static bool ChangeMan(ref Money[] owners, ref Square[] lands, int index) // Method for changing man
        {
            Console.Clear();
            Console.WriteLine($"Changing {owners[index - 1]}\n");
            Console.WriteLine("1: Change name   ");
            Console.WriteLine("2: Change hryvna ");
            Console.WriteLine("3: Change kopyika");
            Console.WriteLine("0: Back          \n");
            Console.Write("Choise: ");

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    Console.Clear();
                    ChangeName(ref owners, ref lands, index);
                    return true;
                case "2":
                    Console.Clear();
                    ChangeHryvna(ref owners, ref lands, index);
                    return true;
                case "3":
                    Console.Clear();
                    ChangeKopyika(ref owners, ref lands, index);
                    return true;
                default:
                    return true;
            }
        }

        //**********************************************************************************************************************************

        static void ChangeName(ref Money[] owners, ref Square[] lands, int index) // Method for change name
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();

            if (!CheckName(name, owners, index) && name != "")
            {
                ChangeNameInLands(owners[index - 1].Name, name, ref lands);
                owners[index - 1] = new Money(name, owners[index - 1].Hryvna, owners[index - 1].Kopyika);
                Console.WriteLine("\nSuccessfully changed");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("This name allready exists");
                Console.ReadKey();
            }
        }

        //**********************************************************************************************************************************

        static void ChangeHryvna(ref Money[] owners, ref Square[] lands, int index) // Method for change hryvna
        {
            int hryvna = 0;
            Console.WriteLine("Enter hryvna");
            if (int.TryParse(Console.ReadLine(), out hryvna) && hryvna > 0)
            {
                owners[index - 1] = new Money(owners[index - 1].Name, hryvna, owners[index - 1].Kopyika);
                Console.WriteLine("\nSuccessfully changed");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Not correct hryvna");
                Console.ReadKey();
            }
        }

        //**********************************************************************************************************************************

        static void ChangeKopyika(ref Money[] owners, ref Square[] lands, int index) // Method for change kopyika
        {
            int kopyika = 0;
            Console.WriteLine("Enter kopyika");
            if (int.TryParse(Console.ReadLine(), out kopyika) && kopyika >= 0)
            {
                owners[index - 1] = new Money(owners[index - 1].Name, owners[index - 1].Hryvna, kopyika);
                Console.WriteLine("\nSuccessfully changed");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Not correct kopyika");
                Console.ReadKey();
            }
        }
        //**********************************************************************************************************************************

        static void ChangeNameInLands(string oldName, string newName, ref Square[] lands) // Method for changing man's name in all him land
        {
            foreach (var land in lands)
            {
                for (int i = 0; i < land.OwnerCount; i++)
                {
                    if (oldName == land.Owners[i])
                    {
                        land.Owners[i] = newName;
                    }
                }
            }
        }

        //**********************************************************************************************************************************

        static void NotCorrectIndex() // Outputs error about index
        {
            Console.WriteLine("Not corret index");
            Console.ReadKey();
        }

        //**********************************************************************************************************************************

        static public bool CheckName(string name, Money[] owners, int Ignorindex) // Check existing current name in array of Money but ignore one index
        {
            for (int i = 0; i < owners.Length; i++)
            {
                if (name == owners[i].Name && Ignorindex != i + 1)
                {
                    return true;
                }
            }
            return false;
        }

        //**********************************************************************************************************************************

        static bool Land(ref Money[] owners, ref Square[] lands) // Method for working with lands
        {
            Console.Clear();
            Console.WriteLine(new string('-', 20));
            Console.WriteLine("| Make choise:     |");
            Console.WriteLine("|                  |");
            Console.WriteLine("| 1: New land      |");
            Console.WriteLine("| 2: Sell land     |");
            Console.WriteLine("| 3: Join lands    |");
            Console.WriteLine("| 4: All lands     |");
            Console.WriteLine("| 5: Price for acr |");
            Console.WriteLine("| 0: Back          |");
            Console.WriteLine("|                  |");
            Console.Write("| Choise:          |\n");
            Console.WriteLine(new string('-', 20));
            Console.SetCursorPosition(10,10);

            switch (Console.ReadLine())
            {
                case "0":
                    return false;
                case "1":
                    NewLand(ref lands);
                    return true;
                case "2":
                    Sell_Land(ref owners,ref lands);
                    return true;
                case "3":
                    JoinLands(ref owners, ref lands);
                    return true;
                case "4":
                    Console.Clear();
                    Show<Square>(lands, -1);
                    Console.ReadKey();
                    return true;
                case "5":
                    PriceForAcr();
                    return true;
                default:
                    return true;
            }
        }

        //**********************************************************************************************************************************

        static void NewLand(ref Square[] lands) // Method of creating new land
        {
            int length;
            int width;

            Console.Clear();
            Console.WriteLine("New land creation");
            Console.WriteLine();

            Console.WriteLine("Enter length");
            if (!int.TryParse(Console.ReadLine(), out length))
            {
                Console.WriteLine("Not correct length");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Enter width");

                if (!int.TryParse(Console.ReadLine(), out width))
                {
                    Console.WriteLine("Not correct width");
                    Console.ReadKey();
                }
                else
                {
                    Array.Resize(ref lands, lands.Length + 1);
                    lands[lands.Length - 1] = new Square(length, width);
                    Console.WriteLine("New land successfully created");
                    Console.ReadKey();
                }
            }
        }

        //**********************************************************************************************************************************

        static void Sell_Land(ref Money[] owners, ref Square[] lands) //Method for selling lands with or without owners
        {
            Console.Clear();
            Console.WriteLine("Choise land for selling: ");
            Console.WriteLine();
            Show<Square>(lands, -1);
            Console.WriteLine("0: Back");
            Console.WriteLine();

            int index;

            Console.Write("Choise: ");

            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index > 0 && index <= lands.Length)
                {
                    for (int i = 1; i <= lands.Length; i++)
                    {
                        if (index == i)
                        {
                            if(lands[index-1].Owners != null)
                            {
                                SellFrom(ref owners,ref lands, index);
                            }
                            else
                            {
                                while(SellTo(ref owners,ref lands, index));
                            }
                        }
                    }
                }
                else if (index < 0 || index > lands.Length)
                {
                    NotCorrectIndex();
                }
            }
            else
            {
                NotCorrectIndex();
            }
        }

        //**********************************************************************************************************************************

        static bool SellTo(ref Money[] owners, ref Square[] lands,int indexSale) // Method for selling land for sale
        {
            Console.Clear();
            Console.WriteLine("Select the owner you want to sell\n");
            Show<Money>(owners, -1);
            Console.WriteLine("0: Back\n");

            int indexOwner;

            Console.Write("Choise: ");
            if(int.TryParse(Console.ReadLine(),out indexOwner) && indexOwner >=0 && indexOwner <= owners.Length)
            {
                if(indexOwner > 0 && indexOwner <= owners.Length)
                {
                    if(lands[indexSale-1].Price <= owners[indexOwner-1].Hryvna)
                    {

                        //owners[indexOwner - 1].Hryvna -= lands[indexSale - 1].Price; //- Work too
                        owners[indexOwner - 1] -= new Money("Sold", lands[indexSale - 1].Price, 0);
                        lands[indexSale - 1].Owners = new string[1] { owners[indexOwner - 1].Name };
                        Console.WriteLine($"Sold to {owners[indexOwner - 1].Name}");
                        Console.ReadKey();
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("Not enough money");
                        Console.ReadKey();
                    }
                        

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                NotCorrectIndex();
                return true;
            }
        }

        //**********************************************************************************************************************************

        static void SellFrom(ref Money[] owners, ref Square[] lands,int index) // Method for selling owner's land
        {
            double price = lands[index - 1].Price / lands[index - 1].OwnerCount;
            int currency = (int)price;
            int coin = (int)(price % 1 * 100);
            Money money = new Money("sold", currency, coin);

            foreach(var owner_name in lands[index -1].Owners)
            {
                for(int i =0;i< owners.Length;i++)
                {
                    if(owner_name == owners[i].Name)
                    {
                        owners[i] += money;
                    }
                }
            }


            lands[index - 1].Owners = null;

            Console.WriteLine("Successfully sold");
            Console.ReadKey();


        }

        //**********************************************************************************************************************************

        static void JoinLands(ref Money[] owners, ref Square[] lands) // Method for join two lands if is real
        {
            int index1;
            int index2;
            Console.Clear();
            Console.WriteLine("Select first land to join");

            Show<Square>(lands, -1);

            Console.Write("\nChoise: ");
            if (int.TryParse(Console.ReadLine(), out index1) && index1 > 0 && index1 <= lands.Length)
            {
                if (lands[index1 - 1].Owners != null)
                {
                    Console.Clear();
                    Console.WriteLine($"You choise {lands[index1 - 1]}");
                    Console.WriteLine("Select second land");
                    Show<Square>(lands, index1);
                    Console.Write("\nChoise: ");
                    if ((int.TryParse(Console.ReadLine(), out index2) && index2 > 0 && index2 <= lands.Length && index1 != index2))
                    {
                        if (lands[index2 - 1].Owners != null)
                        {
                            if (lands[index1 - 1] + lands[index2 - 1] != null)
                            {
                                Console.Clear();
                                Console.WriteLine($"Lands {lands[index1 - 1]} and {lands[index2 - 1]} joined");
                                lands[index1 - 1] += lands[index2 - 1];
                                lands[index2 - 1] = null;
                                Update<Square>(ref lands);
                                Console.WriteLine($"Now you have land {lands[index1 - 1]}");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine("You can't join this lands");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            Console.WriteLine("You can't join land for sale");
                            Console.ReadKey();
                        }

                    }
                    else
                    {
                        NotCorrectIndex();
                    }
                }
                else
                {
                    Console.WriteLine("You can't join land for sale");
                    Console.ReadKey();
                }
            }
            else
            {
                NotCorrectIndex();
            }

        }

        //**********************************************************************************************************************************

        static void Update<T>(ref T[] array) // Update array of Square (maybe in future array of Money), find null-exemplar and delete it
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    Swap<T>(ref array[i], ref array[array.Length - 1]);
                    Array.Resize(ref array, array.Length - 1);
                }
            }
        }
        //**********************************************************************************************************************************

        static void Swap<T>(ref T ex1, ref T ex2) // Swap two exemplar of certain type
        {
            T temp = ex1;
            ex1 = ex2;
            ex2 = temp;
        }

        //**********************************************************************************************************************************

        static void PriceForAcr() // Set price for acr
        {
            Console.Clear();
            Console.WriteLine($"Current price: {Square.PriceForAcr} (minimum - 10)");
            Console.Write($"New price: ");
            int price = 10;
            int.TryParse(Console.ReadLine(), out price);
            Square.PriceForAcr = price;
        }
    }
}
