using System.Globalization;
using System.Linq;

namespace SaferPay
{
    public struct CreditCardExpiration
    {
        private int year;
        private int month;

        private static int FixYear(int y) => y < 2000 ? 2000 + y : y;

        public CreditCardExpiration(int year, int month)
        {
            this.year = year % 100;
            this.month = month;
        }

        public int Year
        {
            get { return year; }
            set { year = FixYear(value); }
        }

        public int Month
        {
            get { return month; }
            set { month = value; }
        }

        public override string ToString()
        {
            return
                month.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0') +
                year.ToString(CultureInfo.InvariantCulture).PadLeft(2, '0');
        }

        public static CreditCardExpiration Parse(string text)
        {
            text = new string(text.Where(char.IsNumber).ToArray());
            var m = int.Parse(text.Substring(0, 2));
            var y = int.Parse(text.Substring(2));

            return new CreditCardExpiration
            {
                month = m,
                year = FixYear(y)
            };
        }
    }
}