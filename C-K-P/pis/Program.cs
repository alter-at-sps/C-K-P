namespace pis
{
    internal class Program
    {
        static int[,] map = new int[3,3];

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, piskvork!");
            Random rand = new Random();

            while (true)
            {
                DrawMap();

                Console.Write("\n Place x: ");
                string p_x = Console.ReadLine();
                Console.Write(" Place y: ");
                string p_y = Console.ReadLine();

                if (p_x == null || p_y == null) return;
                if (p_x == "" || p_y == "") return;

                int player_x = Int32.Parse(p_x);
                int player_y = Int32.Parse(p_y);

                if (!CheckValid(player_x, player_y))
                {
                    Console.WriteLine("Invalid pos");
                    continue;
                }

                map[player_x, player_y] = 1;

                int cpu_x = -1, cpu_y = -1;

                while (!CheckValid(cpu_x, cpu_y))
                {
                    cpu_x = rand.Next(0, 3);
                    cpu_y = rand.Next(0, 3);
                }

                Console.WriteLine($" Cpu x: {cpu_x}");
                Console.WriteLine($" Cpu x: {cpu_y}");

                map[cpu_x, cpu_y] = 2;
            }
        }

        static void DrawMap()
        {
            Console.WriteLine($"\n|---|---|---|");

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine($"| {map[i, 0]} | {map[i, 1]} | {map[i, 2]} |");
                Console.WriteLine($"|---|---|---|");
            }
        }

        static bool CheckValid(int x, int y)
        {
            if (x < 0 || x > 2) return false;
            if (y < 0 || y > 2) return false;

            return map[x, y] == 0;
        }

        static bool CheckIfDone()
        {
            // rows

            for (int i = 0; i < 3; i++)
            {
                bool all = true;

                for (int j = 0; j < 3; j++)
                {
                    if (map[i,j] == 0) all = false;
                }

                if (all) return true;
            }

            // collums

            for (int i = 0; i < 3; i++)
            {
                bool all = true;

                for (int j = 0; j < 3; j++)
                {
                    if (map[j, i] == 0) all = false;
                }

                if (all) return true;
            }

            // diags

            for (int d = 0; d < 2; d++)
            {
                bool all = true;

                for (int i = 0; i < 3; i++)
                {
                    if (map[i, (2 * d) + (i * -d)] == 0) all = false;
                }

                if (all) return true;
            }

            return false;
        }
    }
}
