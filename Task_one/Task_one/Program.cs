using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;





class Worker // parent class    daughter: PermanentWorker, TimeWorker
{
    private int id;
    protected decimal midSelary=0;
    private string lastName;
    private string fistName;
    protected string type;


    public void set_user()
    {

        //this.id = id;
        Console.Write("Last name: ");
        lastName = Console.ReadLine();
        Console.Write("Fist name: ");
        this.fistName = Console.ReadLine();
        this.id = Counter_ID.get_id();

    }

    public void set_program(string lastName, string fistName)
        {
           
            //this.id = id;
            this.id = Counter_ID.get_id();
            this.lastName = lastName;
            this.fistName = fistName;
        }

    public void get()//get information
    {
        Console.WriteLine("ID: {0}\nSelary: {1}$\nLast name: {2}\nFist name: {3}\nType: {4}", id,midSelary,lastName,fistName,type);
    }

    public decimal get_selary()
    {
        return midSelary;
    }
}
                           /// ////  ADD METOD FOR MID SELARY !!!! //// /// 
class PermanentWorker : Worker //doughter class    parnet: Worker
{
   public void CulcSelary(decimal selary)// culculation salary of permanent worker 
    {
        this.midSelary = selary;
        this.type = "permanent";
    }
   public void CulcSelary()
   {
       Console.Write("Selary: ");

       this.midSelary = Convert.ToDecimal(Console.ReadLine());
       this.type = "permanent";
   }
}


class TimeWoker : Worker //doughter class    parnet: Worker
{
    public void CulcSelary(decimal selary) // culculation salary of time worker
    {

        this.midSelary = Convert.ToDecimal(20.8 * 8) * selary;
        this.type = "time";
    }
    public void CulcSelary()
    {
        Console.Write("Selary: ");
        
       this.midSelary =Convert.ToDecimal(20.8 * 8) * Convert.ToDecimal(Console.ReadLine());
       this.type = "time";
    }
}

static class Counter_ID // class counter
{
    private static int id;

   // private static Counter_ID() { }
    public static int get_id()// genaration id
    {
        id++;
        return id - 1; 
    }
    public static int show_id()
    {
     return id;
    }
}

static class OnlyNumber
{   private static string str;
    public static int ReturnInt()
    {
        int number=0;
        while (true)
        {
            str = Console.ReadLine();
            if (Regex.IsMatch(str, @"\D") == false) { number = Convert.ToInt32(str); return number; }
            else Console.WriteLine("Sorry, incorrect number! Please, try again:");
        }
    }
}





namespace Task_one
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int quantity_parament_worker=5; // array size of paramet worker
            int quantity_time_worker = 5;// array size of time worker
            int size = 0; // array size of all element

            ////////////// identification worker options
            int id = 1;
            decimal selary = 1000;
            string lastName = "Cherkasyn";
            string fistName = "Conatantine";
            //////////////
            PermanentWorker[] parmamentwoker;
            TimeWoker[] timewoker;


            string yn; // user change
            for (; ; )
            {
                Console.Write("Do you want use rendom? (Y/N): ");
                yn = Console.ReadLine();
                if (yn == "y" || yn == "yes" || yn == "Yes" || yn == "Y" || yn == "YES" || yn == "n" || yn == "no" || yn == "No" || yn == "N" || yn == "NO") { break; }
                else Console.WriteLine("Sorry, I didn't understand you! Please, try again"); ;
            }


            if (yn == "y" || yn == "yes" || yn == "Yes" || yn == "Y" || yn == "YES")
            {
                //////////////////////////////////////////  Input with Console {
                                           /// //// ADD TEST FOR NUMBER OF WRITELINE !!! //// ///
                Console.WriteLine("How many parmanent worker do you add?");
                quantity_parament_worker = OnlyNumber.ReturnInt();


                //Input with keybord PermanentWorker
                parmamentwoker = new PermanentWorker[quantity_parament_worker];
                for (int i = 0; i < quantity_parament_worker; i++)
                {
                    Console.WriteLine("{0}-th worker:",i+1);
                    parmamentwoker[i] = new PermanentWorker();
                    parmamentwoker[i].set_user();
                    parmamentwoker[i].CulcSelary();
                }

                Console.WriteLine("How many time worker do you add?");
                quantity_time_worker = OnlyNumber.ReturnInt();

                //Input with keybord TimeWorker 
                timewoker = new TimeWoker[quantity_parament_worker];
                for (int i = 0; i < quantity_time_worker; i++)
                {
                    Console.WriteLine("{0}-th worker:", i + 1);
                    timewoker[i] = new TimeWoker();
                    timewoker[i].set_user();
                    timewoker[i].CulcSelary();
                }
                //////////////////////////////////////////  Input with Console } 
            }
            else    /// rendom workers
            {

                //////////////////////////////////////////  random genaration all workers {
                Random rand = new Random();

                parmamentwoker = new PermanentWorker[quantity_parament_worker];//random Time Worker
                for (int i = 0; i < quantity_parament_worker; i++)
                {

                    selary = rand.Next(1000, 6000); // rendom selary
                    lastName = "OtherLastName" + i; // rendom last name
                    fistName = "OtherFistName" + i; // rendom last name

                    parmamentwoker[i] = new PermanentWorker();
                    parmamentwoker[i].set_program(lastName, fistName);
                    parmamentwoker[i].CulcSelary(selary);
                }

               timewoker = new TimeWoker[quantity_parament_worker];
                for (int i = 0; i < quantity_time_worker; i++)
                {

                    selary = rand.Next(8, 36); // rendom selary
                    lastName = "LastName" + i; // rendom last name
                    fistName = "FistName" + i; // rendom last name
                    //
                    timewoker[i] = new TimeWoker();
                    timewoker[i].set_program(lastName, fistName);
                    timewoker[i].CulcSelary(selary);
                    //

                }
                //////////////////////////////////////////  random genaration all workers }
            }


            /////////////////////////////// permanent workers and time workers in single array {
            size = Counter_ID.show_id();
            Worker [] work = new Worker[size];
            for (int i = 0; i < quantity_parament_worker; i++)
            {
                work[i] = parmamentwoker[i];
            }
            for (int i = quantity_time_worker,j = 0; i < size; i++,j++)
            {
                work[i] = timewoker[j];
            }
            /////////////////////////////// permanent workers and time workers in single array }



            /////////////////////////////// bubble (Sorting in decreasing order) {
            size = Counter_ID.show_id();
            Worker[] mid = new Worker[1];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size-1; j++)
                {
                    if (work[j].get_selary() < work[j + 1].get_selary()){
                        mid[0] = work[j];
                        work[j] = work[j + 1];
                        work[j + 1] = mid[0];
                    }
                }
            }

            /////////////////////////////// bubble (Sorting in decreasing order) }


            
            /////////////
            Console.WriteLine("Sorting:");
            for (int i = 0; i < size; i++)
            {
                work[i].get();
                Console.WriteLine();
            }
            /////////////


                Console.ReadKey();
        }
    }
}
