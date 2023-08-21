using System.Numerics;
using System.Text;

bool running = true;
while (running)
{
    Console.WriteLine("Was möchtest du tun?");
    Console.WriteLine("1 - Liste aller Kunden");
    Console.WriteLine("Q - Beenden");

    string choice = Console.ReadLine().ToUpper();

    switch (choice)
    {
        case "1":
            Console.WriteLine("In Bearbeitung.");
            break;
        case "Q":
            running = false;
            break;
        default:
            Console.WriteLine("Ungültige Auswahl.");
            break;
    }
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
//class Account
//{
//    public Account(, int balance)
//    {
    
//    }
//}