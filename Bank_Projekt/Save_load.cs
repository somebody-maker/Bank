using System.IO;
using System.Text.Json;

class CustomerData
{

    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
public static void SaveCustomers(string dataPath, List<Customer> customers)
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataPath, json);
    }



    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
    public static void LoadCustomers(string dataPath, ref List<Customer> customers)
    {
        if (File.Exists(dataPath))
        {
            string json = File.ReadAllText(dataPath);
            customers = JsonSerializer.Deserialize<List<Customer>>(json)!;
        }
        else
        {
            Console.WriteLine("Der angegebene Pfad ist falsch oder es existiert keine Json Datei.");
        }
    }
}