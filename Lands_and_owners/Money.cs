using System;

namespace Lands_and_owners
{
    class Money // class Money have proprty Name and 2 values: hryvna and kopyika
    {
        int _hryvna;
        int _kopyika;

        public Money(string name,int hryvna, int kopyika)
        {
            Name = name;
            Hryvna = hryvna;
            Kopyika = kopyika;
        }

        public int Hryvna
        {
            set => _hryvna = value > 0 ? value : 0;
            get => _hryvna;
        }

        public int Kopyika
        {
            set
            {
                if (value > 0)
                {
                    _hryvna += value / 100;
                    _kopyika = value % 100;
                }
            }

            get => _kopyika;
        }

        public string Name { get; set; }

        static public Money Sum(Money mon1, Money mon2) // Method for addition 2 exemplar of Money
        {
            int coin = mon1.Kopyika + mon2.Kopyika;
            int currency = mon1.Hryvna + mon2.Hryvna;

            if (coin >= 100)
            {
                coin -= 100;
                currency++;
            }

            return new Money(mon1.Name,currency, coin);

        }

        static public Money Diff(Money mon1, Money mon2) // Method for subtraction 2 exemplar of Money 
        {
            int currency = mon1.Hryvna;
            int coin;

            if (mon1.Kopyika - mon2.Kopyika < 0)
            {
                coin = mon1.Kopyika - mon2.Kopyika + 100;
                currency--;
            }
            else
            {
                coin = mon1.Kopyika - mon2.Kopyika;
            }
            currency -= mon2.Hryvna;

            return new Money(mon1.Name,currency, coin);
        }

        static public Money operator +(Money mon1, Money mon2) => Sum(mon1, mon2); // overloading operator +

        static public Money operator -(Money mon1, Money mon2) => Diff(mon1, mon2); // overloading operator -

        public override string ToString() => $"{Name}: {Hryvna} hrn, {Kopyika} kop";  // overriding ToString()
    }
}
