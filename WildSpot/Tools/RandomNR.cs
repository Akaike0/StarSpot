using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSpot
{
    class RandomNR
    {
        public int create(int min, int max)
        {
            Random random = new Random();
            int random_nr = random.Next(min, max);

            return random_nr;
        }

        public float create(double minimum, double maximum)
        {
            Random random = new Random();
            return Convert.ToSingle(random.NextDouble() * (maximum - minimum) + minimum);
        }

        public string ST(int Länge)
        {
            string ret = string.Empty;
            System.Text.StringBuilder SB = new System.Text.StringBuilder();
            string Content = "01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvw!öäüÖÄÜß\"§$%&/()=?*#-";
            Random rnd = new Random();
            for (int i = 0; i < Länge; i++)
                SB.Append(Content[rnd.Next(Content.Length)]);
            return SB.ToString();
        }
    }
}
