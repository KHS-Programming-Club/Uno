using System;

namespace UnoGame {
    class Pile {
        private Card[] Cards = new Card[Settings.DECK_SIZE];
        internal ushort Size = 0;

        internal Card Add(Card card) {
            Cards[Size++] = card;
            return card;
        }

        internal Card Get(int pos) {
            return Cards[pos];
        }

        internal Card GetTop() {
            return Get(Size = 1);
        }

        internal Card Remove(int pos) {
            Card AtPos = Cards[pos];

            for (int i = pos; i < Size - 1;)
                Cards[i] = Cards[++i];
            Cards[--Size] = new Card();

            return AtPos;
        }

        internal void Shuffle() {
            Random Rand = new Random();
            Card[] Shuffled = new Card[Settings.DECK_SIZE];

            for (int i = 0; i < Size; i++) {
                int rand = Rand.Next(Size);
                Index index = new Index(rand, Size);

                int val = index.Get();

                while (Shuffled[val].IsInstantion) {
                    index.Add();
                    val = index.Get();
                }

                Shuffled[index.Get()] = Cards[i];
            }

            Cards = Shuffled;
        }
    }

}
