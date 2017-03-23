using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Worker
{
    private int id;
    protected decimal midSelary=0;
    private string lastName;
    private string fistName;
    protected string type;

    public void set(int id, string lastName, string fistName)
        { 
            this.id = id;
            this.lastName = lastName;
            this.fistName = fistName;
        }

    public void get()//get information
    {
        Console.WriteLine("ID: {0}\nSelary: {1}\nLast name: {2}\nFist name: {3}\nType: {4}", id,midSelary,lastName,fistName,type);
    }

}

class PermanentWorker : Worker
{
    //public PermanentWorker();
   public void CulcSelary(decimal selary)
    {
        this.midSelary = selary;
        this.type = "permanent";
    }
}


class TimeWoker : Worker
{
    public void CulcSelary(decimal selary)
    {

        this.midSelary = Convert.ToDecimal(20.8 * 8) * selary;
        this.type = "time";
    }
}

namespace Task_one
{
    class Program
    {

        static void Main(string[] args)
        {
            int id = 1;
            decimal selary = 1000;
            string lastName = "Cherkasyn";
            string fistName = "Conatantine";

            TimeWoker perwork1 = new TimeWoker();
            perwork1.set(id, lastName, fistName);
            perwork1.CulcSelary(selary);
            perwork1.get();
            Console.ReadKey();
        }
    }
}
