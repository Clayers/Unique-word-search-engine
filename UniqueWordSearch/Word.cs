using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UniqueWordSearch
{
    internal class Word
    {
        public Word(int Quantity, string Words)
        {
            this.Words = Words;
            this.Quantity = Quantity;
        }
        public string Words { get; set; }

        public int Quantity { get; set; }
    }
}
