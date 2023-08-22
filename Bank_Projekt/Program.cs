using System.Text.Json;

class Program
{
    static List<Customer> customers = new List<Customer>();
    static string dataPath = "customers.json"; //TO DO: Json File beim Neustart der Anwendung in die Liste laden.

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("L - Liste aller Kunden");
            Console.WriteLine("N - Hinzufügen eines Kunden");
            //Console.WriteLine("A - Hinzufügen eines Accounts"); //TO DO: Zuordnung eines Accounts zu existierenden Kunden
            Console.WriteLine("Q - Beenden");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "L":
                    LoadCustomers();
                    ListCustomers();
                    break;
                case "N":
                    LoadCustomers();
                    AddCustomer();
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
        Console.WriteLine("Kundenliste:");
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
    }



    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
    static void LoadCustomers()
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            customers = JsonSerializer.Deserialize<List<Customer>>(json);
        }
    else
        {
            Console.WriteLine("Der angegebene Pfad ist falsch oder es existiert keine Json Datei.");
        }
    }



    //----------------------------------------------------------CLASS CUSTOMER---------------------------------------------------------------------------------
    class Customer
    {
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Adress { get; set; } = string.Empty;
        public int Adressnumber { get; set; }
        public string Customerdata
        {
            get
            {
                return String.Format("Vorname: {0} - Nachname: {1} - Alter: {2} - Adresse: {3} - Hausnummer: {4}", this.Firstname, this.Lastname, this.Age, this.Adress, this.Adressnumber);
            }
        }

    }
}
//class Account
//{
//    public Account(, int balance)
//    {

//    }
//}