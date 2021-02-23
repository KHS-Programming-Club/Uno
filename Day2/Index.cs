namespace UnoGame {
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
}
