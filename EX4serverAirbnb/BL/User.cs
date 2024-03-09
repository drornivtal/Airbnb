using HW4_EX3_Server.DAL;

namespace HW4_EX3_Server.BL
{
    public class User
    {
        string firstName;
        string familyName;
        string email;
        string password;
        bool isActive;
        bool isAdmin;
        public User() {}      

        public User(string firstName, string familyName, string email, string password, bool isActive, bool isAdmin)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Email = email;
            Password = password;
            IsActive = isActive;
            IsAdmin = isAdmin;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string FamilyName { get => familyName; set => familyName = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }

        public int Insert() {

            DBservices db =new DBservices();
            return db.Insert(this);
        }

        public List<User> Read()
        {
            DBservices dbServices = new DBservices();
            return dbServices.ReadUsers();
        }
        public User LogIn()
        {
            DBservices db = new DBservices();
            return db.LogIn(this);
        }

         public int UpdateUser()
        {
            DBservices db= new DBservices();
            return db.UpdateUserDetails(this);

        }

        //toggle function to change user activity
        public int UpdateUserActivity(string id)
        {
            DBservices db=new DBservices();
            return db.UpdateUserActivityId(id);
        }

        public Object GetAvregePerNightForCity(int monthNum)
        {
            DBservices db = new DBservices();
            return db.GetAvgPerNightForCity(monthNum);
        }

    }
}
