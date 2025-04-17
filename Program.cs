class Donation
{
    
    static Dictionary<string, (double collected, double target, List<double> donations)> collections = new Dictionary<string, (double, double, List<double>)>();
    
    public static void Main()
    {
        while (true)
        {

            Console.WriteLine("\n- DONAION COLLECTION -");
            Console.WriteLine("1.Collections");
            Console.WriteLine("2.Add Collection");
            Console.WriteLine("3.Make payment");
            Console.WriteLine("4.Logout");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ShowCollection();
                    break;
                case "2":
                    AddCollection();
                    break;
                case "3":
                    Donate();
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

    private static void ShowCollection()
    {
        if (collections.Count == 0)
        {
            Console.WriteLine("There are no collections:(");
            return;
        }
        
        Console.WriteLine("Collecion list: ");
        foreach (var collection in collections)
        {
            ProgressBar(collection.Key, collection.Value.collected, collection.Value.target);
        }
    }

    private static void AddCollection()
    {
        Console.WriteLine("Choose name for collection: ");
        string name = Console.ReadLine();

        if (collections.ContainsKey(name))
        {
            Console.WriteLine("Name already exists 0_0");
            return;
        }
        
        Console.WriteLine("How much you want to collect: ");
        double target;
        while (!double.TryParse(Console.ReadLine(), out target) || target <= 0)
        {
            Console.WriteLine("Invalid ammount :l");
        }
        collections.Add(name, (0, target, new List<double>()));
        Console.WriteLine($"Collecion {name} created :D");
    }

    private static void Donate()
    {
        if (collections.Count == 0)
        {
            Console.WriteLine("There are no collections *_* ");
            return;
        }
        
        Console.WriteLine($"You want donate to? ");
        string name = Console.ReadLine();

        if (collections.ContainsKey(name))
        {
            Console.WriteLine($"How much you want to donate?");
            double donateAmmount;
            while (!double.TryParse(Console.ReadLine(), out donateAmmount) || donateAmmount <= 0)
            {
                Console.WriteLine("Invalid ammount :l");
            }
            
            var(collected, target, donations) = collections[name];
            collected += donateAmmount;
            donations.Add(donateAmmount);
            if (collected >= target)
                collected = donateAmmount;
            
            collections[name] = (collected, target, donations);
            Console.WriteLine($"You donnate {donateAmmount}$ to {name} :3");
            ProgressBar(name, collected, target);
        }
        else
        {
            Console.WriteLine("There are no collections:#");
        }
    }

    static void ProgressBar(string name, double collected, double target)
    {
        int barWidth = 30; 
        double percentage = (collected / target) * 100;
        int progress = (int)(percentage * barWidth / 100);
        
        Console.Write($"{name}: [");
        for (int i = 0; i < barWidth; i++)
        {
            if (i < progress)
                Console.Write("█");
            else
                Console.Write(" ");
        }
        Console.WriteLine($"] {collected:F2}/{target:F2} ({percentage:F1}%)");
    }
}
