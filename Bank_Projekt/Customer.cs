﻿using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Security.Cryptography;
using Serilog;
using Serilog.Context;

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
        if (customers.Count > 0)
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
        string? option = Console.ReadLine();

        if (option != null && option.ToLower() == "y")
        {
            customer.AddAccount();
            Console.WriteLine("Es wurde ein Konto hinzugefügt.");
        }
        else
        {
            Console.WriteLine("Es wurde kein Konto hinzugefügt.");
        }

        Log.Information("Kunde hinzugefügt: {0}",customer.Customerdata);
    
        customers.Add(customer);
    }


    //----------------------------------------------------------DELEATE CUSTOMER METHODE---------------------------------------------------------------------------
    public static void DeleteCustomer(List<Customer> customers)
    {
        ShowCustomerNamesAndIds(customers);
        Console.WriteLine("ID zum Löschen eines Kunden: ");
        int id;
        if (!Int32.TryParse(Console.ReadLine(), out id))
        {
            throw new Exception($"Ungültige ID: {id}");
        }
        Customer? delCustomer = customers.FirstOrDefault(x => x.Id == id);
        if (delCustomer != null)
        {
            customers.Remove(delCustomer);
            Log.Information("Kunde wurde gelöscht: {0}", delCustomer.Customerdata);
            Console.WriteLine("Kunde erfolgreich gelöscht");
        }
        else
        {
            Console.WriteLine($"Kunde mit ID {id} existiert nicht");
        }
    }

    //----------------------------------------------------------SHOW ID`S + NAMES METHODE---------------------------------------------------------------------------
    public static void ShowCustomerNamesAndIds(List<Customer> customers)
    {
        Console.WriteLine("Customer Names and IDs:");
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.Firstname} {customer.Lastname}");
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
            throw new Exception($"Ungültige ID: {fromId}");
        }

        Console.WriteLine("Auf welches Konto möchten Sie überweisen? (ID eingeben): ");
        int toId;
        if (!Int32.TryParse(Console.ReadLine(), out toId))
        {
            throw new Exception($"Ungültige ID: {toId}");
        }

        Console.WriteLine("Bitte den Betrag eingeben: ");
        float amount;
        while (!float.TryParse(Console.ReadLine(), out amount))
        {
            Console.WriteLine("Ungültiger Betrag, bitte erneut eingeben");
        }

        Customer? fromCustomer = customers.FirstOrDefault(c => c.Id == fromId);
        Customer? toCustomer = customers.FirstOrDefault(c => c.Id == toId);

        if (fromCustomer == null || toCustomer == null)
        {
            Console.WriteLine("Kunde konnte nicht gefunden werden.");
            return;
        }

        var accountFrom = fromCustomer.Accounts.FirstOrDefault(account => account.Balance >= amount);
        if (accountFrom == null)
        {
            Console.WriteLine("Nicht ausreichend Guthaben auf dem Senderkonto.");
            return;
        }
        {
            accountFrom.Balance -= amount;
            var accountTo = toCustomer.Accounts.FirstOrDefault();
            if (accountTo == null)
            {
                Console.WriteLine("Empfängerkonto konnte nicht gefunden werden.");
                return;
            }
            accountTo.Balance += amount;

            Console.WriteLine("Überweisung erfolgreich durchgeführt.");
        }
        Log.Information("ID: {0}, Name: {1} {2}, Auszahlung: - {3}", fromCustomer.Id, fromCustomer.Firstname, fromCustomer.Lastname, amount);
        Log.Information("ID: {0}, Name: {1} {2}, Einzahlung: {3}", toCustomer.Id, toCustomer.Firstname, toCustomer.Lastname, amount);
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
        Log.Information("Konto wurde {0} hinzugefügt", fromCustomer.Customerdata);
    }
}
