using System;

namespace UnoGame {
    struct Card {
        public Colors Color;
        public Types Type;

        public bool IsInstantion;

        internal bool FitsOver(Card Under) {
            return Type == Types.DRAW_4 ||
                   Type == Types.WILD ||
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

            if (Color != Colors.CHANGER) {
                ConsoleColor[] Colors = {
                    ConsoleColor.DarkRed,
                    ConsoleColor.DarkYellow,
                    ConsoleColor.DarkGreen,
                    ConsoleColor.DarkBlue
                };

                Console.BackgroundColor = Colors[(int) Color];
                Console.WriteLine(ToPrint);

                Console.BackgroundColor = ConsoleColor.Black;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ToPrint);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public override string ToString() {
            return $"{Color} {Type}";
        }
    }

}
