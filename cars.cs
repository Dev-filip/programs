Console.WriteLine("Zadejte počet aut:");
        int pocetAut = Convert.ToInt32(Console.ReadLine());

        double[,] hodnotyAut = new double[pocetAut, 3];
        string[] jmenaAut = new string[pocetAut];

        for (int i = 0; i < pocetAut; i++)
        {
            Console.WriteLine($"Zadejte jméno auta {i + 1}:");
            jmenaAut[i] = Console.ReadLine()!;

            Console.WriteLine($"Zadejte počáteční hodnotu auta {i + 1}:");
            hodnotyAut[i, 0] = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine($"Zadejte sazbu ztráty (%) pro auto {i + 1}:");
            hodnotyAut[i, 1] = Convert.ToDouble(Console.ReadLine());
        }

        Console.WriteLine("Zadejte počet parametrů:");
        int parametry = Convert.ToInt32(Console.ReadLine());
        string[] parametryNazvy = new string[parametry];

        for (int i = 0; i < parametry; i++)
        {
            Console.WriteLine($"Zadejte název parametru {i + 1}:");
            parametryNazvy[i] = Console.ReadLine()!;
        }

        double[,] parametryHodnoty = new double[pocetAut, parametry];

        for (int i = 0; i < pocetAut; i++)
        {
            for (int j = 0; j < parametry; j++)
            {
                Console.WriteLine($"Zadejte {parametryNazvy[j]} (částka se bude odečítat každý rok - ) pro {jmenaAut[i]}:");
                parametryHodnoty[i, j] = Convert.ToDouble(Console.ReadLine());
            }
        }

        int pocatecniRok = 2024;
        Console.WriteLine("Zadejte koncový rok:");
        int koncovyRok = Convert.ToInt32(Console.ReadLine());

        int pocetLet = koncovyRok - pocatecniRok;

        Console.WriteLine("Výsledky:");

        Console.Write("Rok      │");
        for (int j = 0; j < pocetAut; j++)
        {
            Console.Write($"{jmenaAut[j],10} │");
        }
        Console.WriteLine();

        double[,] odpisyAut = new double[pocetLet + 1, pocetAut];
        double[,] kumulativniOdpisy = new double[pocetLet + 1, pocetAut];

        for (int i = 0; i <= pocetLet; i++)
        {
            Console.Write($"{pocatecniRok + i,8} │");

            for (int j = 0; j < pocetAut; j++)
            {
                double pocatecniHodnota = hodnotyAut[j, 0];
                double sazbaZtraty = hodnotyAut[j, 1];
                double novaHodnota = pocatecniHodnota;

                if (i == 0)  
                {
                    odpisyAut[i, j] = pocatecniHodnota;
                    kumulativniOdpisy[i, j] = pocatecniHodnota;
                }
                else  
                {
                    for (int k = 0; k < i; k++)
                    {
                        novaHodnota -= novaHodnota * (sazbaZtraty / 100);
                        for (int p = 0; p < parametry; p++)
                        {
                            novaHodnota -= parametryHodnoty[j, p];
                        }
                    }
                    odpisyAut[i, j] = Math.Round(pocatecniHodnota - novaHodnota);
                    kumulativniOdpisy[i, j] = kumulativniOdpisy[i - 1, j] + odpisyAut[i, j];
                }

                novaHodnota = Math.Round(novaHodnota);
                if (novaHodnota < 0) novaHodnota = 0;
                Console.Write($"{novaHodnota,10} │");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Odpisy:");

        Console.Write("Rok      │");
        for (int j = 0; j < pocetAut; j++)
        {
            Console.Write($"{jmenaAut[j],10} │");
        }
        Console.WriteLine();

        for (int i = 0; i <= pocetLet; i++)
        {
            Console.Write($"{pocatecniRok + i,8} │");

            for (int j = 0; j < pocetAut; j++)
            {
                double odpis = kumulativniOdpisy[i, j];
                if (odpis < 0) odpis = 0;
                Console.Write($"{odpis,10} │");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Parametry aut:");
for (int i = 0; i < pocetAut; i++)
{
    Console.Write($"{jmenaAut[i]}: ");
    for (int j = 0; j < parametry; j++)
    {
        Console.Write($"{parametryNazvy[j]} - {parametryHodnoty[i, j]}; ");
    }
    Console.WriteLine();
}
