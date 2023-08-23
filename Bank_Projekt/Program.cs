using System.Security.Cryptography;
using System.Security.Principal;
using System.Text.Json;

class Program
{
    static int lastCustomerId = 0;
    static List<Customer> customers = new List<Customer>();
    static string dataPath = "customers.json"; //TO DO: Json File beim Neustart der Anwendung in die Liste laden.
    static string idPath = "id.json";
    static void Main(string[] args)
    {
        LoadCustomers();
        bool running = true;
        while (running)
        {
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("L - Liste aller Kunden");
            Console.WriteLine("N - Hinzufügen eines Kunden");
            Console.WriteLine("A - Hinzufügen eines Kundenkontos"); //TO DO: Zuordnung eines Accounts zu existierenden Kunden
            Console.WriteLine("Q - Beenden");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "L":
                    ListCustomers();
                    break;
                case "N":
                    AddCustomer();
                    break;
                case "A":
                    AddAccountForCustomer();
                    break;
                case "Q":
                    SaveCustomers();
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ungültige Auswahl.");
                    break;
            }
        }
    }




    //----------------------------------------------------------LIST CUSTOMER METHODE---------------------------------------------------------------------------
    static void ListCustomers()
    {   
    //Anzeigen der Kundendaten (Listenobjekt) 
        Console.WriteLine("Kundenliste: ");
        foreach (Customer customer in customers)
        {
            Console.WriteLine(customer.Customerdata);
        }
    }



    //----------------------------------------------------------ADD CUSTOMER METHODE---------------------------------------------------------------------------
    static void AddCustomer() 
    {
        Customer customer = new Customer();
        //Baustelle: Funktionalität - Hinzufügen der Klasseneigenschaften --Implementiert
        lastCustomerId++;
        customer.Id = lastCustomerId;

        Console.WriteLine("Vorname des Kunden: ");
        customer.Firstname = Console.ReadLine();

        Console.WriteLine("Nachname des Kunden: ");
        customer.Lastname = Console.ReadLine();

    //Ersetzen durch TryParse - Errorhandling. Program crasht bei Rückgabe von str (Kann nur Zahlen in int wandeln) --Implementiert
        Console.WriteLine("Alter des Kunden: ");
        int age;
        bool validAge = false;
        while (!validAge)
        {
            if (int.TryParse(Console.ReadLine(), out age))
            {
                customer.Age = age;
                validAge = true;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe, ich brauche eine Zahl.");
            }
        }

        Console.WriteLine("Straße des Kunden: ");
        customer.Adress = Console.ReadLine();

    //Ersetzen durch TryParse - Errorhandling. Program crasht bei Rückgabe von str (Kann nur Zahlen in int wandeln) --Implementiert
        Console.WriteLine("Hausnummer des Kunden: ");
        int houseNumber;
        bool validHouse = false;
        while (!validHouse)
        {
            if (int.TryParse(Console.ReadLine(), out houseNumber))
            {
                customer.Adressnumber = houseNumber;
                validHouse = true;
            }
            else
            {
                Console.WriteLine("Ungültige Eingabe, ich brauche eine Zahl.");
            }
        }

    //TO DO: Hinzufügen zur Liste --Implementiert
        customers.Add(customer);
    }



    //----------------------------------------------------------SAVE CUSTOMERS METHODE---------------------------------------------------------------------------
    static void SaveCustomers() 
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataPath, json);

        string idJson = JsonSerializer.Serialize(lastCustomerId);
        File.WriteAllText(idPath, idJson);
    }



    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
    static void LoadCustomers()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            customers = JsonSerializer.Deserialize<List<Customer>>(json);

            if (File.Exists(idPath))
            {
                string idJson = File.ReadAllText(idPath);
                lastCustomerId = JsonSerializer.Deserialize<int>(idJson);
            }
        }
    else
        {
            Console.WriteLine("Der angegebene Pfad ist falsch oder es existiert keine Json Datei.");
        }
    }



    //----------------------------------------------------------ADD ACCOUNT METHODE---------------------------------------------------------------------------------
    static void AddAccountForCustomer()
    {
        Console.WriteLine("ID des Kunden: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Customer customer = customers.Find(c => c.Id == customerId)!;
            if (customer != null)
            {
                Account account = new Account();
                account.AccountNumber = RandomNumberGenerator.GetInt32(1, 200);
                customer.Accounts.Add(account);
                Console.WriteLine("Konto erfolgreich hinzugefügt.");
                SaveCustomers();
            }
            else
            {
                Console.WriteLine("Es gibt keinen Kunden mit der ID.");
            }
        }
    }




    //----------------------------------------------------------CLASS CUSTOMER---------------------------------------------------------------------------------
    class Customer
    {
        public int Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Adress { get; set; } = string.Empty;
        public int Adressnumber { get; set; }
        public List<Account> Accounts { get; set; } = new List<Account>();
        public string Customerdata
        {
            get
            {
                string accountsInfo = string.Join(", ", Accounts.Select(account => $"Konto: {account.AccountNumber} - Kontostand: {account.Balance}"));
                return String.Format("ID: {0} - Vorname: {1} - Nachname: {2} - Alter: {3} - Adresse: {4} - Hausnummer: {5} - Konten: {6}", this.Id, this.Firstname, this.Lastname, this.Age, this.Adress, this.Adressnumber, accountsInfo);
            }
        }

    }

    class Account
    {
        public int AccountNumber { get; set; }

        public float Balance { get; set; }
    }
}
