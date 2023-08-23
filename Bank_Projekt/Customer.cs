using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

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

    //----------------------------------------------------------LIST CUSTOMER METHODE---------------------------------------------------------------------------
    public static void ListCustomers(List<Customer> customers)
    {
        Console.WriteLine("Kundenliste: ");
        foreach (Customer customer in customers)
        {
            Console.WriteLine(customer.Customerdata);
        }
    }

    //----------------------------------------------------------ADD CUSTOMER METHODE---------------------------------------------------------------------------
    public static void AddCustomer(List<Customer> customers, ref int lastCustomerId)
    {
        Customer customer = new Customer();
        lastCustomerId++;
        customer.Id = lastCustomerId;

        Console.WriteLine("Vorname des Kunden: ");
        customer.Firstname = Console.ReadLine();

        Console.WriteLine("Nachname des Kunden: ");
        customer.Lastname = Console.ReadLine();

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
        customers.Add(customer);
    }


    //----------------------------------------------------------ADD ACCOUNT METHODE---------------------------------------------------------------------------------
    public static void AddAccountForCustomer(List<Customer> customers)
    {
        Console.WriteLine("ID des Kunden: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            Customer? customer = customers.Find(c => c.Id == customerId);
            if (customer != null)
            {
                Account account = new Account();
                account.AccountNumber = RandomNumberGenerator.GetInt32(1, 200);
                customer.Accounts.Add(account);
                Console.WriteLine("Konto erfolgreich hinzugefügt.");
            }
            else
            {
                Console.WriteLine("Es gibt keinen Kunden mit der ID.");
            }
        }
    }
}

