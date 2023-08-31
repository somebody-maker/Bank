using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public static void AddCustomer(List<Customer> customers, int maxCustomerId)
    {
        Customer customer = new Customer();
        if (customers.Count > 0)
        {
            var lastCustomerId = customers.Max(customer => customer.Id);
            customer.Id = lastCustomerId + 1;
            maxCustomerId = customer.Id;
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
        var option = Console.ReadLine();

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


    //----------------------------------------------------------SHOW ID`S + NAMES METHODE---------------------------------------------------------------------------
    public static void ShowCustomerNamesAndIds(List<Customer> customers)
    {
        Console.WriteLine("Customer Names and IDs:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"  ID: {customer.Id}, Name: {customer.Firstname} {customer.Lastname}");
        }
    }


    //----------------------------------------------------------TRANSFER FUNDS METHODE---------------------------------------------------------------------------

    public static void TransferFunds(List<Customer> customers)
    {
        ShowCustomerNamesAndIds(customers);

        Console.WriteLine("Von welchem Konto möchten Sie überweisen? (ID eingeben): ");
        int fromId;
        if (!Int32.TryParse(Console.ReadLine(), out fromId))
        {           
            throw new Exception($"Ungültige Kundennummer: {idInput}");
        }

            Console.WriteLine("Auf welches Konto möchten Sie überweisen? (ID eingeben): ");
        int toId = int.Parse(Console.ReadLine());

        Console.WriteLine("Bitte den Betrag eingeben: ");
        float amount;
        while (!float.TryParse(Console.ReadLine(), out amount))
        {
            Console.WriteLine("Ungültiger Betrag, bitte erneut eingeben");
        }

        Customer? fromCustomer = customers.FirstOrDefault(c => c.Id == fromId);
        Customer? toCustomer = customers.FirstOrDefault(c => c.Id == toId);

        if (fromCustomer != null && toCustomer != null)
        {
            var accountFrom = fromCustomer.Accounts.FirstOrDefault(account => account.Balance >= amount);
            if (accountFrom != null)
            {
                accountFrom.Balance -= amount;
                var accountTo = toCustomer.Accounts.FirstOrDefault();
                if (accountTo != null)
                {
                    accountTo.Balance += amount;
                    Console.WriteLine("Überweisung erfolgreich durchgeführt.");
                }
                else
                {
                    Console.WriteLine("Empfängerkonto konnte nicht gefunden werden.");
                }
            }
            else
            {
                Console.WriteLine("Nicht ausreichend Guthaben auf dem Senderkonto.");
            }
        }
        else
        {
            Console.WriteLine("Kunde konnte nicht gefunden werden.");
        }

    }


    //----------------------------------------------------------ADD MORE ACCOUNTS METHODE---------------------------------------------------------------------------
    public static void AddAccounts(List<Customer> customers)
    {
        ShowCustomerNamesAndIds(customers);

        Console.WriteLine("ID für ein weiteres Konto: ");
        var fromId = Console.ReadLine();
        int id;
        if (!Int32.TryParse(fromId, out id))
        {
            throw new Exception($"Ungültige ID: {fromId}");
        }
        Customer? fromCustomer = customers.FirstOrDefault(c => c.Id == id);

            if (fromCustomer != null)
            {
                fromCustomer.AddAccount();
                Console.WriteLine("Ein Konto wurde für den Kunden eröffnet.");
            }
            else
            {
                Console.WriteLine("ID existiert nicht.");
            }
    }  
}



    //public static class ConsoleInputOutput
    //{
    //    public static Customer SelectCustomer(List<Customer> customers)
    //    {
    //        ShowCustomerNamesAndIds(customers);
    //        Console.WriteLine("Bitte gib die Id des Kunden an: ");
    //        var idInput = Console.ReadLine()!;
    //        if (!int.TryParse(idInput, out var id))
    //        {
    //            throw new Exception($"Ungültige Kundennummer: {idInput}");
    //        }

    //        var customer = customers.Find(c => c.Id == id);
    //        if (customer == null)
    //        {
    //            throw new Exception($"Kunde nicht gefunden");
    //        }

    //        return customer;
    //    }
    //}

