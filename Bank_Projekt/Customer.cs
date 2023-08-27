using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

class Customer
{
    public int Id { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Age { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
    public string Adressnumber { get; set; } = string.Empty;
    public List<Account> Accounts { get; set; } = new List<Account>();
    public string Customerdata
    {
        get
        {
            string accountsInfo = string.Join(", ", Accounts.Select(account => $"Konto: {account.AccountNumber} - Kontostand: {account.Balance}"));
            return String.Format("ID: {0} - Vorname: {1} - Nachname: {2} - Alter: {3} - Adresse: {4} - Hausnummer: {5} - Konten: {6}", this.Id, this.Firstname, this.Lastname, this.Age, this.Adress, this.Adressnumber, accountsInfo);
        }
    }


    //----------------------------------------------------------ADD ACCOUNT METHODE-----------------------------------------------------------------------------
    public void AddAccount()
    {
        Account account = new Account();
        account.AccountNumber = this.Id + RandomNumberGenerator.GetInt32(100, 10000);
        account.Balance = RandomNumberGenerator.GetInt32(10000, 100000);
        Accounts.Add(account);
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
    public static void AddCustomer(List<Customer> customers)
    {
        Customer customer = new Customer();
        if (customer.Id > 0) 
        {
            var lastCustomerId = customers.Max(customer => customer.Id);
            customer.Id = lastCustomerId + 1;
        }
        else 
        {
            customer.Id = 1;
        }

        Console.WriteLine("Vorname des Kunden: ");
        customer.Firstname = Console.ReadLine();

        Console.WriteLine("Nachname des Kunden: ");
        customer.Lastname = Console.ReadLine();

        Console.WriteLine("Alter des Kunden: ");
        customer.Age = Console.ReadLine();

        Console.WriteLine("Straße des Kunden: ");
        customer.Adress = Console.ReadLine();

        Console.WriteLine("Hausnummer des Kunden: ");
        customer.Adressnumber = Console.ReadLine();

        Console.WriteLine("Soll ein Konto hinzugefügt werden? y oder n");
        var option = Console.ReadLine()!;

        if (option.ToLower() == "y")
        {
            customer.AddAccount();
            Console.WriteLine("Es wurde ein Konto hinzugefügt.");
        }
        else 
        {
            Console.WriteLine("Es wurde kein Konto hinzugefügt.");
        }

        customers.Add(customer);
    }


    //----------------------------------------------------------ADD ACCOUNT METHODE---------------------------------------------------------------------------------
    //public static void AddAccountForCustomer(List<Customer> customers)
    //{
    //    Console.WriteLine("ID des Kunden: ");
    //    if (int.TryParse(Console.ReadLine(), out int customerId))
    //    {
    //        Customer? customer = customers.Find(c => c.Id == customerId);
    //        if (customer != null)
    //        {
    //            Account account = new Account();
    //            account.AccountNumber = RandomNumberGenerator.GetInt32(1, 200);
    //            customer.Accounts.Add(account);
    //            Console.WriteLine("Konto erfolgreich hinzugefügt.");
    //        }
    //        else
    //        {
    //            Console.WriteLine("Es gibt keinen Kunden mit der ID.");
    //        }
    //    }
    //}
}

