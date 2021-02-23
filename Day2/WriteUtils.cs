using System;

namespace UnoGame {
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
            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }
    }
}
