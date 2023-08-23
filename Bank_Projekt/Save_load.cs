using System.IO;
using System.Text.Json;

class CustomerData
{

    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
public static void SaveCustomers(string dataPath, string idPath, List<Customer> customers, int lastCustomerId)
    {
        var json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(dataPath, json);

        string idJson = JsonSerializer.Serialize(lastCustomerId);
        File.WriteAllText(idPath, idJson);
    }



    //----------------------------------------------------------LOAD CUSTOMERS METHODE---------------------------------------------------------------------------
    public static void LoadCustomers(string dataPath, string idPath, ref List<Customer> customers, ref int lastCustomerId)
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
}