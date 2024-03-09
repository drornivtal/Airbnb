using HW4_EX3_Server.DAL;

namespace HW4_EX3_Server.BL
{

    public class Flat
    {
        int id;
        string city;
        string address;
        int numberOfRooms;
        double price;  // Dollars


        public Flat( int id,string city, string address, int numberOfRooms, double price)
        {
            Id = id;
            City = city;
            Address = address;
            NumberOfRooms = numberOfRooms;
            Price = price;
        }
        public Flat() { }
        public int Id
        {
            get { return id; }
            set
            {
                if (value < 0)
                {
                    Console.Write("Enter Valid id!");
                    return;
                }
                id = value;
            }
        }
        public string City { get => city; set => city = value; }
        public string Address { get => address; set => address = value; }
        public int NumberOfRooms
        {
            get { return numberOfRooms; }

            set
            {
                if (value < 1)
                {
                    Console.Write("Enter Valid Price!");
                    return;
                }
                numberOfRooms = value;
            }
        }
        public double Price
        {
            get { return price; }

            set
            {
                if (value < 0)
                {
                    Console.Write("Enter Valid Price!");
                    return;
                }
                price = Discount(value, this.NumberOfRooms);
            }
        }
        public double Discount(double p, int numOfRooms)
        {
            if (p > 100 && numOfRooms > 1)
            {
                return p * 0.9;
            }
            return p;
        }
        public int Insert()
        {
            DBservices db= new DBservices();
            return db.InsertNewFlat(this);
        }
        public static List<Flat> Read()
        {
            DBservices db = new DBservices();
            return db.ReadAllflats();
        }



        //public static List<Flat> ReadCityPrice(string city, double price)
        //{
        //    List<Flat> selectedList = new List<Flat>();

        //    foreach (Flat tempFlat in FlatsList)
        //    {
        //        if (tempFlat.City == city && tempFlat.Price < price)
        //        {
        //            selectedList.Add(tempFlat);
        //        }
        //    }
        //    return selectedList;
        //}

    }
}

