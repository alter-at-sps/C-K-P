namespace sib
{
    internal class Program
    {
        static string current_word;

        static List<bool> revealed;
        static HashSet<char> used_chars;

        static int tries_remaining;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, sibeNIC! (press esc to exit)\n");

            SelectNewWord();

            while (true)
            {
                // print word

                DrawWorld();

                // check for win

                if (!revealed.Contains(false))
                {
                    Console.WriteLine("\n\n You did it, Yippe!\n");
                    SelectNewWord();
                    continue;
                }

                // get input

                Console.Write("\n\n Your guess: ");
                ConsoleKeyInfo s = Console.ReadKey();

                if (s.Key == ConsoleKey.Escape) return;

                // check for letter

                char c = s.KeyChar;

                if (used_chars.Contains(c))
                {
                    Console.WriteLine("\n Already used!\n");
                    continue;
                }
                used_chars.Add(c);

                if (current_word.Contains(c))
                {
                    for (int i = 0; i < current_word.Length; i++)
                    {
                        if (current_word[i] == c) revealed[i] = true;
                    }

                    Console.WriteLine("\n Yep!\n");
                } else
                {
                    tries_remaining--;
                    Console.WriteLine($"\n Nope! {tries_remaining} tries remaining...\n");
                }

                if (tries_remaining == 0)
                {
                    for (int i = 0; i < revealed.Count; i++)
                    {
                        revealed[i] = true;
                    }

                    Console.WriteLine("You failed! Here's the full word(s)\n");
                    DrawWorld(); Console.WriteLine("\n\n");

                    SelectNewWord();
                    continue;
                }
            }
        }

        static void SelectNewWord()
        {
            // select work from data file

            List<string> data = File.ReadLines("slova.txt").ToList();
            current_word = data[new Random().Next(data.Count)];

            revealed = new List<bool>(current_word.Length);
            used_chars = new HashSet<char>(current_word.Length);

            for (int i = 0; i < current_word.Length; i++)
            {
                revealed.Add(current_word[i] == ' '); // hide all but auto-reveal whitespaces
            }

            tries_remaining = 15;
        }

        static void DrawWorld()
        {
            for (int i = 0; i < current_word.Length; i++)
            {
                Console.Write(revealed[i] ? current_word[i] : '_'); Console.Write(' ');
            }
        }
    }
}
