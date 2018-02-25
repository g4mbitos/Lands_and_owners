using System;

namespace Lands_and_owners
{
    class Program
    {
        static void Main(string[] args)
        {

// nice
            Money[] allOwners = new Money[]
            {
                new Money("Vasia",100_000,00),
                new Money("Dima",100_000,00),
                new Money("Serega",100_000,00),
                new Money("Jenia",100_000,00),
                new Money("Sasha",100_000,00),
            };
            Square[] all_Lands = new Square[]
            {
                new Square(15,20,"Vasia"),
                new Square(50,30,"Dima"),
                new Square(15,35,"Serega"),
                new Square(60,15,"Jenia"),
                new Square(10,10,"Sasha","Dima"),
                // new Square(10,10,"Boris"), // Not valid situation
            };

            foreach (var land in all_Lands)
            {
                foreach (var owner_name in land.Owners)
                {
                    if (Menu.CheckName(owner_name, allOwners, -1))
                    {
                        continue;
                    }
                    Console.WriteLine("Not valid situation");
                    Console.ReadKey();
                    return;
                }
            }
            while (Menu.StartPage(ref allOwners, ref all_Lands)) ;


        }
    }
}
