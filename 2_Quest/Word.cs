namespace _2_Quest
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