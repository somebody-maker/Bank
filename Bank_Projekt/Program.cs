internal class Program
{
    private static void Main(string[] args)
    {
        Customer customer1 = new Customer();
        customer1.Firstname = "Bernd";
        customer1.Lastname = "Dovic";
        customer1.Age = 30;
        customer1.Adress = "Bahnhofstraße";
        customer1.Adressnumber = 5;

        Console.WriteLine(customer1.Customerdata);
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