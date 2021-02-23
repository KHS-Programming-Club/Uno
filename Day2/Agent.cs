namespace UnoGame {
    interface Agent {
        Pile Deck { get; set; }
        Card? StartTurn(ref Pile Queued, ref Pile Played);
        Card? PlayCard(int Card, ref Pile Queued, ref Pile Played);
        void ChangeColor(ref Card Changer);
    }
}
