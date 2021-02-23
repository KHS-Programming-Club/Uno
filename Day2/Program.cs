using System;

namespace UnoGame {
    class Program {
        private static bool GameGoing = false;

        static void Main(string[] args) {
            WriteUtils.WriteCenter("Uno!\n\nPress any key to start\nOr [esc] to quit");

            if (Console.ReadKey().Key == ConsoleKey.Escape)
                return;

            SelectionScreen();
        }

        private static void SelectionScreen() {
            ConsoleKey Key;
            bool PlayerFirst = true;

            do {
                Console.Clear();
                WriteUtils.WriteCenter("Who goes first?");

                string space = "          ";
                WriteUtils.WriteCenter(PlayerFirst ? $"*Player{space} Bot" : $" Player{space}*Bot");

                Key = Console.ReadKey().Key;

                if (Key == ConsoleKey.LeftArrow || Key == ConsoleKey.RightArrow)
                    PlayerFirst = !PlayerFirst;
            } while (Key != ConsoleKey.Enter);

            StartGame(PlayerFirst);
        }

        private static void StartGame(bool playerFirst) {
            GameGoing = true;

            Player player = new Player();
            Bot bot = new Bot();

            Pile Queued = ItitializeDeck();
            Pile Played = new Pile();

            Queued.Shuffle();

            do {
                Played.Add(Queued.Remove(0));
            } while ((int) Played.GetTop().Type > 9);

            Console.Write("Starting card is ");
            Played.GetTop().Print();
            Console.WriteLine("Press [Énter] to proceed");

            WriteUtils.AwaitEnter();

            player.InitializeHand(ref Queued);
            bot.InitializeHand(ref Queued);

            Card? LastPlayed;

            for(int i = Convert.ToInt32(playerFirst); GameGoing; i++) {
                if (i % 2 == 0)
                    LastPlayed = StartTurn(bot, ref Queued, ref Played);
                else
                    LastPlayed = StartTurn(player, ref Queued, ref Played);

                if (!LastPlayed.HasValue)
                    continue;

                int Type = (int) LastPlayed.Value.Type;

                if (Type > 9 && Type != 14)
                    i++;
            }
        }

        private static Card? StartTurn(Agent agent, ref Pile Queued, ref Pile Played) {
            return agent.StartTurn(ref Queued, ref Played);
        }

        private static Pile ItitializeDeck() {
            Pile Deck = new Pile();

            for (int i = 0; i <= 12; i++)
                for (int j = 0; j < (i == 0 ? 1 : 2); j++)
                    for (int k = 0; k < 4; k++)
                        Deck.Add(new Card((Colors) k, (Types) i));

            for(int i = 0; i < 4; i++) {
                Deck.Add(new Card(Colors.CHANGER, Types.DRAW_4));
                Deck.Add(new Card(Colors.CHANGER, Types.WILD));
            }

            return Deck;
        }
    }
}
