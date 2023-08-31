using System.IO;
using System.Text.Json;

class CustomerData
{

    //----------------------------------------------------------Save CUSTOMERS METHODE---------------------------------------------------------------------------
    public static void SaveCustomers(string dataPath, List<Customer> customers)
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataPath, json);
    }



    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
    public static List<Customer> LoadCustomers(string dataPath, ref List<Customer> customers)
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            customers = JsonSerializer.Deserialize<List<Customer>>(json);
            if (customers != null) 
            {
                return customers;
            }

        }
        else
        {
            Console.WriteLine("Der angegebene Pfad ist falsch oder es existiert keine Json Datei. Wir haben nun eine neue erstellt.");
            customers = new List<Customer>();
            return customers;
        }
        return customers ?? new List<Customer>();
    }
}