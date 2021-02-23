namespace UnoGame {
    class Bot : Agent {
        public Pile Deck { get; set; }

        public Card? StartTurn(ref Pile Queued, ref Pile Player) { return null; }
        public Card? PlayCard(int Card, ref Pile Queued, ref Pile Player) { return null; }
        public void ChangeColor(ref Card Changer) { }
    }
}
