namespace pis
{
    internal class Program
    {
        static int[,] map;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, piskvork!");
            Random rand = new Random();

            ResetMap();

            while (true)
            {
                DrawMap();

                Console.Write("\n Place x: ");
                string? p_x = Console.ReadLine();
                Console.Write(" Place y: ");
                string? p_y = Console.ReadLine();

                if (p_x == null || p_y == null) return;
                if (p_x == "" || p_y == "") return;

                int player_x = Int32.Parse(p_x);
                int player_y = Int32.Parse(p_y);

                if (!CheckValid(player_x, player_y))
                {
                    Console.WriteLine("Invalid pos");
                    continue;
                }

                map[player_y, player_x] = 1;

                int cpu_x = -1, cpu_y = -1;

                while (!CheckValid(cpu_x, cpu_y))
                {
                    cpu_x = rand.Next(0, map.GetLength(1));
                    cpu_y = rand.Next(0, map.GetLength(0));
                }

                Console.WriteLine($" Cpu x: {cpu_x}");
                Console.WriteLine($" Cpu y: {cpu_y}");

                map[cpu_y, cpu_x] = 2;

                int finish_status = CheckIfDone();

                switch (finish_status)
                {
                    case 0:
                        break;

                    case 1:
                        Console.WriteLine("Player wins!");
                        ResetMap();
                        break;

                    case 2:
                        Console.WriteLine("CPU wins!");
                        ResetMap();
                        break;
                }
            }
        }

        static void ResetMap()
        {
            Console.Write("\n Map size: ");
            string? s = Console.ReadLine();

            if (s == null) return;
            if (s == "") return;

            int map_size = Int32.Parse(s);

            map = new int[map_size, map_size]; // reset
        }

        static void DrawMap()
        {
            Console.Write($"\n");
            for (int j = 0; j < map.GetLength(1); j++)
                Console.Write($"|---");
            Console.WriteLine($"|");

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++) 
                    Console.Write($"| {map[i, j]} ");
                Console.WriteLine($"|");

                for (int j = 0; j < map.GetLength(1); j++)
                    Console.Write($"|---");
                Console.WriteLine($"|");
            }
        }

        static bool CheckValid(int x, int y)
        {
            if (x < 0 || x >= map.GetLength(1)) return false;
            if (y < 0 || y >= map.GetLength(0)) return false;

            return map[y, x] == 0;
        }

        static int CheckIfDone()
        {
            // rows

            for (int i = 0; i < map.GetLength(1); i++)
            {
                int match = map[i,0];

                for (int j = 0; j < map.GetLength(0); j++)
                {
                    if (map[i,j] != match)
                    {
                        match = -1;
                        break;
                    }
                }

                if (match != -1 && match != 0) return match;
            }

            // collums

            for (int i = 0; i < map.GetLength(1); i++)
            {
                int match = map[0, i];

                for (int j = 0; j < map.GetLength(0); j++)
                {
                    if (map[i, j] != match)
                    {
                        match = -1;
                        break;
                    }
                }

                if (match != -1 && match != 0) return match;
            }

            // diags

            for (int d = 0; d < 2; d++)
            {
                int match = map[0, d * (map.GetLength(1) - 1)];

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    if (map[i, ((map.GetLength(1) - 1) * d) + (i * -d)] != match)
                    {
                        match = -1;
                        break;
                    };
                }

                if (match != -1 && match != 0) return match;
            }

            return 0;
        }
    }
}
