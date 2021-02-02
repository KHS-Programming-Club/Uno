using System;
using System.Linq;

namespace UnoGame {
    public static class Settings {
        public static readonly ushort DECK_SIZE = 14 * 8 * 4;
        public static readonly byte STARTING_HAND = 7;
    }

    enum Colors { RED, YELLOW, GREEN, BLUE, CHANGER };
    enum Types { 
        ZERO, ONE, TWO, THREE, FOUR, FIVE, SIX, SEVEN, EIGHT, NINE, TEN,
        SKIP, REVERSE, DRAW_2, DRAW_4, WILD
    };

    interface Agent {
        Pile Deck { get; set; }
        void StartTurn(ref Pile Queued, ref Pile Played);
        void PlayCard(ref Pile Queued, ref Pile Played);
    }

    static class AgentExtensions {

    }

    struct Card {
        public Colors Color;
        public Types Type;

        public bool IsInstantion;
        
        internal bool FitsOver(Card Under) {
            return Type == Types.DRAW_4 ||
                   Type == Types.WILD   ||
                   Color == Under.Color ||
                   Type == Under.Type;
        }

        public Card(Colors Color, Types Type) {
            this.Color = Color;
            this.Type = Type;
            IsInstantion = true;
        }

        public void Print() {
            string ToPrint = Type.ToString()[0] + Type.ToString().Substring(1).ToLower().Replace('_', ' ');

            if(Color != Colors.CHANGER) {
                ConsoleColor[] Colors = {
                    ConsoleColor.DarkRed,
                    ConsoleColor.DarkYellow,
                    ConsoleColor.DarkGreen,
                    ConsoleColor.DarkBlue
                };

                Console.BackgroundColor = Colors[(int) Color];
                Console.WriteLine(ToPrint);

                Console.BackgroundColor = ConsoleColor.Black;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ToPrint);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override string ToString() {
            return $"{Color} {Type}";
        }
    }

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

            for(int i = 0; i < Size; i++) {
                int rand = Rand.Next(Size);
                Index index = new Index(rand, Size);

                int val = index.Get();

                while(Shuffled[val].IsInstantion) {
                    index.Add();
                    val = index.Get();
                }

                Shuffled[index.Get()] = Cards[i];
            }

            Cards = Shuffled;
        }
    }

    class Index {
        private int MaxValue;
        private int Value;

        public Index(int Value, int MaxValue) {
            this.Value = Value;
            this.MaxValue = MaxValue;
        }

        public Index Add(int increment = 1) {
            Value += increment;
            Value %= MaxValue;

            return this;
        }

        public Index Back(int increment = 1) {
            return Add(MaxValue - increment);
        }

        public int Get() => Value;
    }

    class Player : Agent {
        Pile Agent.Deck { get; set; }

        public void StartTurn(ref Pile Queued, ref Pile Player) { }
        public void PlayCard(ref Pile Queued, ref Pile Player) { }
    }

    class Bot : Agent {
        Pile Agent.Deck { get; set; }

        public void StartTurn(ref Pile Queued, ref Pile Player) { }
        public void PlayCard(ref Pile Queued, ref Pile Player) { }
    }

    static class WriteUtils {
        internal static void WriteCenter(string text) {
            if (text.Contains('\n'))
                foreach (string Line in text.Split('\n'))
                    WriteCenter(Line);
            else {
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop + 1);
                Console.Write(text);
            }
        }

        internal static void AwaitEnter() {
            while (Console.ReadKey().Key != ConsoleKey.Enter);
        }
    }

    class Program {
        private static bool GameGaming = false;

        static void Main(string[] args) {
            WriteUtils.WriteCenter("Uno!\n\nPress any key to start\nOr [esc] to quit");

            if (Console.ReadKey().Key == ConsoleKey.Escape)
                return;

            SelectionScreen();
        }

        private static void SelectionScreen() { }
    }
}
