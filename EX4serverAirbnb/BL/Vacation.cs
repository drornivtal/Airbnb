
using HW4_EX3_Server.DAL;
using Microsoft.AspNetCore.Mvc;

namespace HW4_EX3_Server.BL
{
    public class Vacation
    {
        int id;
        string userEmail;
        int flatId;
        DateTime startDate;
        DateTime endDate;
        static List<Vacation> vacationsList = new List<Vacation>();

        public Vacation() { }
        public Vacation(int id, string userEmail, int flatId, DateTime startDate, DateTime endDate)
        {
            Id = id;
            UserEmail = userEmail;
            FlatId = flatId;
            StartDate = startDate;
            EndDate = endDate;
        }
        public int Id
        {
            get { return id; }

            set
            {
                if (value < 0)
                {
                    return;
                }
                id = value;
            }
        }
        public string UserEmail { get => userEmail; set => userEmail = value; }
        public int FlatId { get => flatId; set => flatId = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public DateTime EndDate { get => endDate; set => endDate = value; }

        //public bool Insert()
        //{
        //    if (this.EndDate < this.StartDate)
        //        return false;

        //    for (int i = 0; i < vacationsList.Count; i++)
        //    {
        //        if (vacationsList[i].Id == this.Id || (vacationsList[i].FlatId == this.FlatId &&
        //            this.StartDate >= vacationsList[i].StartDate &&
        //            this.EndDate <= vacationsList[i].EndDate))
        //        {
        //            return false;
        //        }
        //    }
        //    vacationsList.Add(this);
        //    return true;
        //}

        public int Insert()
        {
            DBservices db= new DBservices();

            List <Vacation>vacationsList = Read();

            foreach (var v in vacationsList)
            {
                if (this.FlatId == v.FlatId)
                {
                    if (this.StartDate > v.EndDate || this.EndDate < v.StartDate)
                    {
                        continue;
                    }
                    return 0;
                }
            }

            return db.InsertNewVacation(this);
        }

        public static List<Vacation> Read()
        {

            DBservices db = new DBservices();
            return db.ReadAllVacations();
        }
        public static List<Vacation> ReadByDates(DateTime startDate, DateTime endDate)
        {
            List<Vacation> datesVacation = new List<Vacation>();

            foreach (var vacationItem in vacationsList)
            {
                if (vacationItem.StartDate >= startDate && vacationItem.EndDate <= endDate)
                {
                    datesVacation.Add(vacationItem);
                }
            }
            return datesVacation;
        }

    }
}