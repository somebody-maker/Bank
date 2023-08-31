using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Text.Json;


class Program
{
    public static List<Customer> customers = new List<Customer>();
    public static string dataPath = "customers.json";
    public static int maxCustomerId;
    static void Main(string[] args)
    {
        CustomerData.LoadCustomers(dataPath, ref customers);
        bool running = true;
        while (running)
        {
            if (args.Length > 0)
            {
                string option = args[0];

                switch (option.ToLower())
                {
                    case "list":
                        Customer.ListCustomers(customers);
                        _ = args.Length - 1;
                        break;
                    case "add":
                        Customer.AddCustomer(customers, maxCustomerId);
                        break;
                    case "quit":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Ungültige Option. Wähle aus zwischen list, add oder quit");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Was möchtest du tun?");
                Console.WriteLine("L - Liste aller Kunden");
                Console.WriteLine("N - Hinzufügen eines Kunden");
                Console.WriteLine("A - Hinzufügen eines Kundenkontos");
                Console.WriteLine("T - Transfer von Guthaben");
                Console.WriteLine("Q - Beenden");

                string choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "L":
                        Customer.ListCustomers(customers);
                        break;
                    case "N":
                        Customer.AddCustomer(customers, maxCustomerId);
                        break;
                    case "A":
                        Customer.AddAccounts(customers);
                        break;
                    case "Q":
                        CustomerData.SaveCustomers(dataPath, customers);
                        running = false;
                        break;
                    case "T":
                        Customer.TransferFunds(customers);
                        break;
                    default:
                        Console.WriteLine("Ungültige Auswahl.");
                        break;
                }
            }
        }
    }
}