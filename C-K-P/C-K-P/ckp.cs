using System.ComponentModel;

namespace C_K_P
{
    internal class ckp
    {
        static Dictionary<string, int> name_ids = new Dictionary<string, int>()
        {
            { "camen", 0 },
            { "knuzky", 1 },
            { "papir", 2 },
        };

        // lookup if an id wins using `win_matrix[input][cpu]`
        // -1 == lose
        // 0 == draw
        // 1 == win

        static int[,] win_matrix = new int[3,3]
        {
            { 0, 1, -1 },
            { -1, 0, 1 },
            { 1, -1, 0 }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, C-K-P!");

            Random rand = new Random();

            while (true)
            {
                Console.WriteLine("\nEnter your next play:\n - camen \n - knuzky\n - papir");
                string? input = Console.ReadLine();

                if (input == null) return;
                if (input == "") return;

                int input_id = name_ids[input];
                int cpu = rand.Next(0, 3);

                if (win_matrix[input_id, cpu] == -1)
                {
                    Console.WriteLine("Lose!");
                }
                else if (win_matrix[input_id, cpu] == 0)
                {
                    Console.WriteLine("Draw!");
                }
                else if (win_matrix[input_id, cpu] == 1)
                {
                    Console.WriteLine("Win!");
                }
            }
        }
    }
}
