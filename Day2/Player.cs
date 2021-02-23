using System;

namespace UnoGame {
    class Player : Agent {
        public Pile Deck { get; set; }

        public Card? StartTurn(ref Pile Queued, ref Pile Played) {
            Console.Clear();
            
            if(this.HasPlayable(Played.GetTop())) {
                ConsoleKey? Key = null;
                Index Selected = new Index(0, Deck.Size);

                for(;;) {
                    while(Key != ConsoleKey.Enter) {
                        Console.Clear();
                        Console.Write("Last Played: ");
                        Played.GetTop().Print();
                        Console.WriteLine();

                        for(int i = 0; i < Deck.Size; i++) {
                            if (i == Selected.Get())
                                Console.Write('*');
                            Console.Write("{0}.", i + 1);
                            Deck.Get(i).Print();
                        }

                        Key = Console.ReadKey().Key;

                        switch(Key) {
                            case ConsoleKey.UpArrow:
                                Selected.Back();
                                break;
                            case ConsoleKey.DownArrow:
                                Selected.Add();
                                break;
                        }

                        if (Deck.Get(Selected.Get()).FitsOver(Played.GetTop()))
                            break;

                        Console.Write("\a");
                        Key = null;
                    }

                    return PlayCard(Selected.Get(), ref Queued, ref Played);
                }
            } else {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;

                WriteUtils.WriteCenter("Draw [Enter]");

                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;

                WriteUtils.AwaitEnter();

                Card NewCard = Queued.Remove(0);
                bool IsPlayable = NewCard.FitsOver(Played.GetTop());
                string SubText = "Press enter to " + (IsPlayable ? "play" : "pass");

                Deck.Add(NewCard);

                NewCard.Print();
                Console.WriteLine(SubText);

                WriteUtils.AwaitEnter();

                if (IsPlayable)
                    return PlayCard(Deck.Size - 1, ref Queued, ref Played);
                else
                    return null;
            }
        }

        public Card? PlayCard(int Card, ref Pile Queued, ref Pile Played) {
            Card ToPlay = Deck.Get(Card);

            if (ToPlay.Color == Colors.CHANGER)
                ChangeColor(ref ToPlay);

            Played.Add(Deck.Remove(Card));
            return ToPlay;
        }

        public void ChangeColor(ref Card Changer) {
            string[] ColorDisp = { " Red ", "Yellow", "Green", " Blue " };
            ConsoleColor[] DispColors = {
                ConsoleColor.DarkRed,
                ConsoleColor.DarkYellow,
                ConsoleColor.DarkGreen,
                ConsoleColor.DarkBlue
            };

            Index ColorIndex = new Index(0, 4);
            ConsoleKey Key;

            do {
                Console.Clear();
                WriteUtils.WriteCenter("Change Color\n");
                Console.WriteLine();

                for(int i = 0; i < ColorDisp.Length; i++) {
                    Console.BackgroundColor = DispColors[i];
                    if (i == ColorIndex.Get())
                        Console.Write('*');
                    Console.Write(ColorDisp[i]);

                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(i % 2 == 0 ? "\t" : "\n");
                }

                Key = Console.ReadKey().Key;

                switch(Key) {
                    case ConsoleKey.UpArrow:
                        ColorIndex.Back(2);
                        break;
                    case ConsoleKey.RightArrow:
                        ColorIndex.Add();
                        break;
                    case ConsoleKey.DownArrow:
                        ColorIndex.Add(2);
                        break;
                    case ConsoleKey.LeftArrow:
                        ColorIndex.Back();
                        break;
                }
            } while (Key != ConsoleKey.Enter);

            Changer.Color = (Colors) ColorIndex.Get();
        }
    }
}
