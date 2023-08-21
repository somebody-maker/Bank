class Program
{
    static List<Customer> customers = new List<Customer>();

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Was möchtest du tun?");
            Console.WriteLine("1 - Liste aller Kunden");
            Console.WriteLine("2 - Hinzufügen eines Kunden");
            Console.WriteLine("Q - Beenden");

            string choice = Console.ReadLine().ToUpper();

            switch (choice)
            {
                case "1":
                    ListCustomers();
                    break;
                case "2":
                    
                    AddCustomer();
                    break;
                case "Q":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ungültige Auswahl.");
                    break;
            }
        }
    }

    static void ListCustomers()
    {   
    //Anzeigen der Kundendaten (Listenobjekt) 
        Console.WriteLine("Kundenliste:");
        foreach (Customer customer in customers)
        {
            Console.WriteLine(customer.Customerdata);
        }
    }

    static void AddCustomer() 
    {
        Customer customer = new Customer();
    //Baustelle: Funktionalität - Hinzufügen der Klasseneigenschaften
        Console.WriteLine("Vorname des Kunden: ");
        customer.Firstname = Console.ReadLine();

        Console.WriteLine("Nachname des Kunden: ");
        customer.Lastname = Console.ReadLine();

        Console.WriteLine("Alter des Kunden: ");
        customer.Age = int.Parse(Console.ReadLine());

        Console.WriteLine("Straße des Kunden: ");
        customer.Adress = Console.ReadLine();

        Console.WriteLine("Hausnummer des Kunden: ");
        customer.Adressnumber = int.Parse(Console.ReadLine());

        //Baustelle: Hinzufügen zur Liste
        customers.Add(customer);
    }

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
                return String.Format("{0} {1} {2} {3} {4}", this.Firstname, this.Lastname, this.Age, this.Adress, this.Adressnumber);
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