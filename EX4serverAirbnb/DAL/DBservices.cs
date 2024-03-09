
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using HW4_EX3_Server.BL;
using System.Xml.Linq;


namespace HW4_EX3_Server.DAL
{
    public class DBservices
    {
        public DBservices() {}

        //--------------------------------------------------------------------------------------------------
        // This method creates a connection to the database according to the connectionString name in the web.config 
        //--------------------------------------------------------------------------------------------------
        public SqlConnection connect(String conString)
        {

            // read the connection string from the configuration file
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
            string cStr = configuration.GetConnectionString("myProjDB");
            SqlConnection con = new SqlConnection(cStr);
            con.Open();
            return con;
        }


        //----------------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (1-Registration & Details update)
        //----------------------------------------------------------------------------------------
        private SqlCommand CreateUserInsertCommandWithStoredProcedure(String spName, SqlConnection con, User user)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@firstName", user.FirstName);

            cmd.Parameters.AddWithValue("@familyName", user.FamilyName);

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@password", user.Password);

            return cmd;
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (2)
        //---------------------------------------------------------------------------------
        private SqlCommand SelectUserByEmailWithStoredProcedureWithParameters(String spName, SqlConnection con,  string email)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

  
            cmd.Parameters.AddWithValue("@email", email);

            return cmd;
        }

        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (3-LogIn)
        //---------------------------------------------------------------------------------
        private SqlCommand SelectUserToLogInWithStoredProcedureWithParameters(String spName, SqlConnection con, string email,string password)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);

            return cmd;
        }


        //---------------------------------------------------------------------------------
        // Create the SqlCommand using a stored procedure (4)
        //---------------------------------------------------------------------------------
        private SqlCommand CreateCommandWithStoredProcedureWithoutParameters(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }


        //----------------------------------------------------------------------------------------
        // Flat insert --> Create the SqlCommand using a stored procedure (5)
        //----------------------------------------------------------------------------------------
        private SqlCommand CreateFlatInsertCommandWithStoredProcedure(String spName, SqlConnection con, Flat flat)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@flatCity", flat.City);
            cmd.Parameters.AddWithValue("@flatAddress", flat.Address);
            cmd.Parameters.AddWithValue("@flatPrice", flat.Price);
            cmd.Parameters.AddWithValue("@numberOfRooms", flat.NumberOfRooms);

            return cmd;
        }



        //----------------------------------------------------------------------------------------
        // Vacation insert --> Create the SqlCommand using a stored procedure (6)
        //----------------------------------------------------------------------------------------
        private SqlCommand CreateVacationInsertCommandWithStoredProcedure(String spName, SqlConnection con, Vacation vacation)
        {
            
            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

           
            cmd.Parameters.AddWithValue("@userEmail", vacation.UserEmail);
            cmd.Parameters.AddWithValue("@flatId", vacation.FlatId);
            cmd.Parameters.AddWithValue("@startDate", vacation.StartDate);
            cmd.Parameters.AddWithValue("@endDate", vacation.EndDate);

            return cmd;
        }

        //------------------//
        //     User:        //
        //------------------//
        //--------------------------------------------------------------------------------------------------
        // read all users
        //--------------------------------------------------------------------------------------------------
        public List<User> ReadUsers()
        {

            SqlConnection con;
            SqlCommand cmd;
            List<User> usersList = new List<User>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = buildReadStoredProcedureCommand("spReadAllUsers", con);             // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dataReader.Read())
                {
                    User s = new User();

                    s.FirstName = dataReader["firstName"].ToString();
                    s.FamilyName = dataReader["familyName"].ToString();
                    s.Email = dataReader["email"].ToString();
                    s.Password = dataReader["password"].ToString();
                    s.IsActive = Convert.ToBoolean(dataReader["isActive"]);
                    s.IsAdmin = Convert.ToBoolean(dataReader["isAdmin"]);
                    usersList.Add(s);
                }
                return usersList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        private SqlCommand buildReadStoredProcedureCommand(String spName, SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // Registration --> This method Inserts a user to the users table 
        //--------------------------------------------------------------------------------------------------
        public int Insert(User user)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserInsertCommandWithStoredProcedure("spInsertNewUser", con, user); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // LogIn --> This method reads user by email and password from the database 
        //--------------------------------------------------------------------------------------------------
        public User LogIn(User user) { 

            SqlConnection con;
            SqlCommand cmd;
            

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = SelectUserToLogInWithStoredProcedureWithParameters("spLogInUser", con, user.Email,user.Password);   // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                User u = new User();
                while (dataReader.Read())
                {

                    u.FirstName = dataReader["firstName"].ToString();
                    u.FamilyName = dataReader["familyName"].ToString();
                    u.Email = dataReader["email"].ToString();
                    u.Password = dataReader["password"].ToString();
                    u.IsActive = dataReader.GetBoolean(dataReader.GetOrdinal("isActive"));
                    u.IsAdmin = dataReader.GetBoolean(dataReader.GetOrdinal("isAdmin"));
                }

                return u;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }

        }

        //--------------------------------------------------------------------------------------------------
        // Details update --> This method Inserts a user to the users table 
        //--------------------------------------------------------------------------------------------------
        public int UpdateUserDetails(User user) //(CCEC)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserInsertCommandWithStoredProcedure("spUpdateUserDetails", con, user); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


      


        //--------------------------------------------------------------------------------------------------
        // Actuvity update --> This method update a user activity in toggele
        //--------------------------------------------------------------------------------------------------
        public int UpdateUserActivityId(string id) //(CCEC)
        {
            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateUserUpdateActivityCommandWithStoredProcedure("spChangeUserStatus", con, id); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        private SqlCommand CreateUserUpdateActivityCommandWithStoredProcedure(String spName, SqlConnection con, string id)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@email", id);


            return cmd;
        }

        //--------------------------------------------------------------------------------------------------
        // give a list of all the cities with avereg per night for cpesific month
        //--------------------------------------------------------------------------------------------------
        public List<object> GetAvgPerNightForCity(int monthNum) 
        {
            SqlConnection con;
            SqlCommand cmd;
            List<object> ObjList = new List<object>();


            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateAvgPerNightCommandWithStoredProcedure("GetAveragePricesPerCityByMonth", con, monthNum); // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    string city;
                    double average;
                    ObjList.Add(
                        new
                    {
                        city = dataReader["City"].ToString(),
                        average = Convert.ToDouble(dataReader["average_price_per_night"])
                    });                    
                    
                }

                return ObjList;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }



        private SqlCommand CreateAvgPerNightCommandWithStoredProcedure(String spName, SqlConnection con, int month)
        {

            SqlCommand cmd = new SqlCommand(); // create the command object

            cmd.Connection = con;              // assign the connection to the command object

            cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

            cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

            cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

            cmd.Parameters.AddWithValue("@TargetMonth", month);


            return cmd;
        }
        
        //------------------//
        //     Flat:        //
        //------------------//


        //--------------------------------------------------------------------------------------------------
        // This method reads Allflats from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Flat> ReadAllflats()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Flat> FlatsDBlist = new List<Flat>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureWithoutParameters("spReadAllFlats", con);   // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                Flat f = new Flat();
                    f.Id = Convert.ToInt32(dataReader["flatId"]);
                    f.Address = dataReader["flatCity"].ToString();
                    f.City = dataReader["flatAddress"].ToString();
                    f.Price = Convert.ToSingle(dataReader["flatPrice"]);
                    f.NumberOfRooms = Convert.ToInt16(dataReader["numberOfRooms"]);

                    FlatsDBlist.Add(f);
                }

                return FlatsDBlist;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //--------------------------------------------------------------------------------------------------
        //This method Inserts a new flat to the flats table 
        //--------------------------------------------------------------------------------------------------
        public int InsertNewFlat(Flat flat)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateFlatInsertCommandWithStoredProcedure("spInsertNewFlat", con,flat); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        //------------------//
       //     Vacation:    //
      //------------------//


        //--------------------------------------------------------------------------------------------------
        //This method Inserts a new vacation to the vacations table 
        //--------------------------------------------------------------------------------------------------
        public int InsertNewVacation(Vacation vacation)
        {

            SqlConnection con;
            SqlCommand cmd;

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateVacationInsertCommandWithStoredProcedure("spInsertNewVacation", con, vacation); // create the command

            try
            {
                int numEffected = cmd.ExecuteNonQuery(); // execute the command
                return numEffected;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }

        //--------------------------------------------------------------------------------------------------
        // This method reads AllVacations from the database 
        //--------------------------------------------------------------------------------------------------
        public List<Vacation> ReadAllVacations()
        {
            SqlConnection con;
            SqlCommand cmd;
            List<Vacation> VacationsDBlist = new List<Vacation>();

            try
            {
                con = connect("myProjDB"); // create the connection
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            cmd = CreateCommandWithStoredProcedureWithoutParameters("spReadAllVacations", con);   // create the command

            try
            {
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dataReader.Read())
                {
                    Vacation v = new Vacation();
                    v.Id = Convert.ToInt32(dataReader["vacationId"]);
                    v.UserEmail = dataReader["userEmail"].ToString();
                    v.FlatId= Convert.ToInt32(dataReader["flatId"]);
                    v.StartDate = Convert.ToDateTime(dataReader["startDate"]);
                    v.EndDate = Convert.ToDateTime(dataReader["endDate"]);

                    VacationsDBlist.Add(v);
                }

                return VacationsDBlist;
            }
            catch (Exception ex)
            {
                // write to log
                throw (ex);
            }

            finally
            {
                if (con != null)
                {
                    // close the db connection
                    con.Close();
                }
            }
        }


        

    }
}
