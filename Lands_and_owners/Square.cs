using System;

namespace Lands_and_owners
{
    class Square
    {
        static int _priceForAcr = 10;

        int _length;
        int _width;
        string[] _owners;

        static public int PriceForAcr
        {
            get => _priceForAcr;
            set => _priceForAcr = (value > 10) ? value : 10;
        }

        public int Price
        {
            get => PriceForAcr * this.Length * this.Width;
        }

        public int Length
        {
            get => _length;
            set => _length = value > 0 ? value : 1;
        }

        public int Width
        {
            get => _width;
            set => _width = value > 0 ? value : 1;

        }

        public bool IsForSale
        {
            get => OwnerCount == 0 ? true : false;
        }

        public int OwnerCount
        {
            get => (_owners != null)? _owners.Length : 0;
        }

        public string[] Owners
        {
            get => _owners;
            set => _owners = value;
        }

        public Square(int length, int width, params string[] owners)
        {
            Length = length;
            Width = width;
            _owners = owners;
        }

        public Square(int length, int width)
        {
            Length = length;
            Width = width;
        }

        static public Square Sum(Square sq1, Square sq2) // Method for addition 2 exemplar of Square
        {
            if (sq1.Length == sq2.Length)
            {
                string[] newOwners = sq1._owners;

                foreach (var own in sq2._owners)
                {
                    if (!CheckOne(own, sq1._owners))
                    {
                        Array.Resize(ref newOwners, newOwners.Length + 1);
                        newOwners[newOwners.Length - 1] = own;
                    }
                }

                return new Square(sq1.Length, sq1.Width + sq2.Width, newOwners);
            }
            return null;
        }

        static bool CheckOne(string owner, string[] another) // Check owner name in array of names
        {
            foreach (var an in another)
            {
                if (owner == an)
                    return true;
            }
            return false;
        }

        static public bool CheckOwners(Square sq1, Square sqr2) // Check existing owners in each of exemplar of Square
        {
            bool IsIn = false;
            for (int i = 0; i < sq1.OwnerCount; i++)
            {
                IsIn = false;
                for (int j = 0; j < sqr2.OwnerCount; j++)
                {
                    if (sq1._owners[i] == sqr2._owners[j])
                    {
                        IsIn = true;
                    }
                }
                if (!IsIn)
                {
                    return false;
                }
            }
            return true;
        }

        static public Square Diff(Square sq1, Square sq2) // Method for subtraction 2 exemplar of Square 
        {
            if (CheckOwners(sq2, sq1) && sq1.Length == sq2.Length && sq1.Width >= sq2.Width)
            {
                string[] newOwners = new string[1] { "Null" };
                int count = 0;
                bool IsIn;

                foreach (var own1 in sq1._owners)
                {
                    IsIn = false;
                    foreach (var own2 in sq2._owners)
                    {
                        if (own1 == own2)
                        {
                            IsIn = true;
                        }
                    }
                    if (!IsIn)
                    {
                        newOwners[count++] = own1;
                        Array.Resize(ref newOwners, newOwners.Length + 1);
                    }
                }
                if (newOwners[0] != "Null")
                {
                    Array.Resize(ref newOwners, newOwners.Length - 1);
                    return new Square(sq1.Length, sq1.Width - sq2.Width, newOwners);
                }
                return new Square(sq1.Length, sq1.Width - sq2.Width, null);

            }
            return null;
        }

        string stringOwners(params string[] owners) // Convert array of owners in one string
        {
            string newOwners = "";
            if (!IsForSale)
            {
                if (owners.Length == 1)
                {
                    newOwners = owners[0];
                }
                else
                {
                    foreach (var owner in owners)
                    {
                        newOwners += owner + ',';
                    }
                    newOwners = newOwners.Substring(0, newOwners.Length - 1);
                }
                return newOwners;
            }
            return null;
        }

        static public Square operator +(Square sq1, Square sq2) => Sum(sq1, sq2);

        static public Square operator -(Square sq1, Square sq2) => Diff(sq1, sq2);

        public override string ToString() // Show info about current exemplar Square
        {
            string owners = stringOwners(_owners);
            if (!IsForSale)
                return $"{owners}  L:{Length}, W:{Width}  {Price}hrn";
            else
                return $"For sale  L:{Length}, W:{Width}  {Price}hrn";
        }
    }
}
