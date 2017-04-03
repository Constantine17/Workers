using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml;





class Worker // parent class    daughter: PermanentWorker, TimeWorker
{
    private int id;
    protected decimal midSelary=0;
    private string lastName;
    private string fistName;
    protected string type;

    /// <summary>
    public virtual void CulcSelary(decimal selary) { Console.WriteLine("culcselary"); }
    /// </summary>

    public void set_user()  // keyboard input 
    {

        //this.id = id;
        Console.Write("Last name: ");
        lastName = Console.ReadLine();
        Console.Write("Fist name: ");
        this.fistName = Console.ReadLine();
        this.id = Counter_ID.get_id();

    }

    public void set_program(string lastName, string fistName) //programm input
        {
           
            //this.id = id;
            this.id = Counter_ID.get_id();
            this.lastName = lastName;
            this.fistName = fistName;
        }
    public void set_program(int id, decimal selary,string lastName, string fistName, string type)
    {
        this.id = id;
        this.midSelary = selary;
        //this.id = Counter_ID.get_id();
        this.lastName = lastName;
        this.fistName = fistName;
        this.type = type;
        //Console.WriteLine("ID: {0}\nSelary: {1}$\nLast name: {2}\nFist name: {3}\nType: {4}", id, midSelary, lastName, fistName, type);
    }

    public void get()//get information
    {
        Console.WriteLine("ID: {0}\nSelary: {1}$\nLast name: {2}\nFist name: {3}\nType: {4}", id,midSelary,lastName,fistName,type);
    }

    public int get_id_num() { return id; }
    public decimal get_selary() {  return midSelary; }
    public string get_last_name() { return lastName; }
    public string get_fist_name() { return fistName; }
    public string get_type() { return type; }

    public void File_add(string directory)
    {
        StreamWriter write_file = new StreamWriter(directory, true);
        write_file.WriteLine("id: "+ id);
        write_file.WriteLine("Selary: " + midSelary);
        write_file.WriteLine("Last name: " + lastName);
        write_file.WriteLine("Fist name: " + fistName);
        write_file.WriteLine("Type: " + type);
        write_file.WriteLine();
        write_file.Close();
    }
}
                           /// ////  ADD METOD FOR MID SELARY !!!! //// /// 
class PermanentWorker : Worker //doughter class    parnet: Worker
{
   public override void CulcSelary(decimal selary)// culculation salary of permanent worker 
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
    public override void CulcSelary(decimal selary) // culculation salary of time worker
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

class AllWorkers : Worker // dougher class
{
    public Worker[] allworker ;
    public int Length = 0;

    /// //// Command association two array in single array //// /// {
    public AllWorkers(Worker[] fistArraym, Worker[] secondArray)
    {
        int fistsize = fistArraym.Length;
        int secondsize = secondArray.Length;
        Length = fistArraym.Length + secondArray.Length;

        allworker = new Worker[Length];
 
        for(int i = 0; i< fistsize ; i++)
        {
            
              allworker[i] = fistArraym[i];
        }
        for (int i = 0; i < secondsize; i++)
        {

            allworker[fistsize + i] = secondArray[i];
        }
    }
    /// //// Command association two array in single array //// /// }


    public void add(Worker[] fistArraym, Worker[] secondArray)
    {
        allworker = new Worker[Length]; // not working
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
static class XML
{ 
    public static Worker[] array_workers;
    public static int Length = 0;

    /// //// Command write objects class in xml file //// /// {
    public static void Write(string directory, Worker[] worker)
    {
        XmlTextWriter write = new XmlTextWriter(directory, Encoding.UTF8);
        write.WriteStartElement("project"); write.WriteString("\n");//   <project>   /// open and start write file

        write.WriteStartElement("size");//  <size>
        int size = worker.Length;
        write.WriteAttributeString("counter", Convert.ToString(size));
        write.WriteEndElement(); write.WriteString("\n");//  </size>

        /// //// write array objects {
        for (int i = 0; i < size; i++)
        {
            write.WriteStartElement("worker_"+i);//   <worker>
            write.WriteAttributeString("id", Convert.ToString(worker[i].get_id_num()));
            write.WriteAttributeString("selary", Convert.ToString(worker[i].get_selary()));
            write.WriteAttributeString("last_name", worker[i].get_last_name());
            write.WriteAttributeString("fist_name", worker[i].get_fist_name());
            write.WriteAttributeString("type", worker[i].get_type());

            write.WriteString("\n");

            write.WriteEndElement(); // </worker>

            write.WriteString("\n"); write.WriteString("\n");
        }
        /// //// write array objects }

        write.WriteEndElement();//  </project>  /// close last an element
        write.Close(); // save and close file 
    }
    /// //// Command write objects class in xml file //// /// }


    /// //// comand read objects class in xml file //// /// {
    public static void Read(string directory)
    {
        XmlTextReader read_Length = new XmlTextReader(directory); //command reading array size

        /// //// size array in xml file {
        while (read_Length.Read()) 
        {
            if (read_Length.Name == "size") Length = Convert.ToInt32(read_Length.GetAttribute("counter"));
        }
        array_workers = new Worker[Length]; // memory for array
        /// //// size array in xml file }
        
       // XmlTextReader read_worker = new XmlTextReader(directory);
        for (int i = 0; i < Length; i++)
        {
            array_workers[i] = new Worker(); 
            XmlTextReader read_worker = new XmlTextReader(directory); // command reading workers
            int r_id = 5; decimal r_selary = 5; string r_lastname = "", r_fistname = "", r_type = ""; // intermediate variables
            while (read_worker.Read())
            {
                
                if (read_worker.Name == "worker_"+i && read_worker.GetAttribute("id")!=null) // test attribute worker
                {

                    /// //// reading attribute {
                    r_id = Convert.ToInt32(read_worker.GetAttribute("id"));
                    r_selary = Convert.ToDecimal(read_worker.GetAttribute("selary"));
                    r_lastname = read_worker.GetAttribute("last_name");
                    r_fistname = read_worker.GetAttribute("fist_name");
                    r_type = read_worker.GetAttribute("type");
                    /// //// reading attribute }
                    
                    array_workers[i].set_program(r_id, r_selary, r_lastname, r_fistname, r_type); // input array 
                }
            }
        }
    }
    /// //// comand read objects class in xml file //// /// }

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
            ///////////////
            string directory = @"d:\programs\GIT_C#\Task_one\";
            string file = @"file.txt";
            string xml_file = @"file.xml";
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


            if (yn == "n" || yn == "no" || yn == "No" || yn == "N" || yn == "NO")
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

            goto metka;
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


            StreamWriter cl = new StreamWriter(directory + file);
            cl.Close();
            /////////////
            Console.WriteLine(directory+file);
            for (int i = 0; i < size; i++)
            {
                work[i].get();
                Console.WriteLine();
                work[i].File_add(directory+file);
            }
            /////////////
        metka: Console.WriteLine("//////////////////////////////////////////////");
            //string str = @"d:\programs\GIT_C#\Task_one\file.txt";
          //  File.CreateText(str)
             //   File.
            
        AllWorkers allworkers = new AllWorkers(parmamentwoker, timewoker);
        XML.Read(directory + xml_file);
        for (int i = 0; i < allworkers.Length; i++) 
            XML.array_workers[i].get();
          //  allworkers.allworker[i].get();
            Console.WriteLine();
           // XML.Write(directory + xml_file, allworkers.allworker);
            
          //  XML.Write(directory+xml_file,work);
                Console.ReadKey();
        }
    }
}
