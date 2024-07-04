using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }
        public int NightsCount
        {
            get
            {
                return nightsCount;
            }

            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                nightsCount = value;
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        public double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                if (value > 100)
                    throw new ArgumentException();
                discount = value;
                total = price * nightsCount * (1 - discount / 100);
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        public double Total
        {
            get
            {
                return total;
            }

            set
            {
                if (value < 0)
                    throw new ArgumentException();
                total = value;
                discount = (1 - total / (price * nightsCount)) * 100;
                if (discount > 100)
                    throw new ArgumentException();
                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }
    }
}
