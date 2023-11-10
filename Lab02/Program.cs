using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using AttributeData;
using MainData;

delegate void MilkInOut();

namespace AttributeData
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MilkMoreInfo : System.Attribute
    {
        public string Manufacturer { get; set; }
        public string CompanyName { get; set; }

        public MilkMoreInfo(string Manufacturer = "", string CompanyName = "")
        {
            this.Manufacturer = Manufacturer;
            this.CompanyName = CompanyName;
        }
    }
}

namespace MainData
{
    interface IMilkIO
    {
        void InputInfo();
        void OutputInfo();
    }

    [MilkMoreInfo("Toyota", "Suzuki")]
    class Milk : IMilkIO
    {
        [DllImport("User32.dll")]
        public static extern int MessageBox(int hParent, string Message, string Caption, int Type);

        private string MilkID = "MILK01032016";
        private string MilkName;
        private DateTime ProductionDate;
        private DateTime ExpiredDate;
        private int Quantity;

        public Milk(string MilkName = "N/A", string ProductionDate = "01/01/2000",
                    string ExpiredDate = "01/01/2000", int Quantity = 0)
        {
            this.MilkName = MilkName;
            this.ProductionDate = DateTime.ParseExact(ProductionDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.ExpiredDate = DateTime.ParseExact(ExpiredDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Quantity = Quantity;
            MilkID = String.Format("MILK{0}", this.ProductionDate.ToString("ddMMyyyy"));
        }
        public Milk() { }

        public class NegativeNumException : Exception
        {
            public NegativeNumException() { }
            public NegativeNumException(string message) : base(message) { }
        }

        public class NullInputationException : Exception
        {
            public NullInputationException() { }
            public NullInputationException(string message) : base(message) { }
        }

        public void ProDuctionDateInputFunction()
        {
            bool isCompleted = false;
            Console.Write("Production Date: ");

            while (!isCompleted)
            {
                string strProductionDate = Console.ReadLine();
                
                try
                {
                    if (strProductionDate == null) throw new NullInputationException();

                    else
                    {
                        if (strProductionDate.Length == 10)
                        {
                            int.TryParse(strProductionDate.Substring(0, 2), out int day);
                            int.TryParse(strProductionDate.Substring(3, 2), out int month);
                            if (day >= 32 || day <= 0)
                            {
                                Console.WriteLine("Invalid: Day must be greater than 0 and less than 32");
                                throw new NegativeNumException();
                            }
                            else if(day >= 28 && month == 2)
                            {
                                Console.WriteLine("Invalid: Feb doeas not have {0} days", day);
                            }

                            if (month <= 0 || month >= 13)
                            {
                                Console.WriteLine("Invalid: Month must be greater than 0 and less than 13");
                                throw new NegativeNumException();
                            }

                        }
                    }
                        ValProductionDate = strProductionDate;

                    if (ProductionDate.GetType() != typeof(DateTime))
                    {
                        throw new FormatException();
                    }

                    isCompleted = true;
                }
                catch (NegativeNumException)
                {
                    Console.Write("Re-enter valid date: ");
                }
                catch (NullInputationException)
                {
                    Console.WriteLine("Look like you entered empty date. Please re-enter seriously: ");
                }
                catch (FormatException) 
                {
                    Console.Write("Input Production Date with form 'dd/MM/yyyy': ");
                }
                catch (Exception)
                {
                    // This Exception will not be catched but still here for the unknown case.
                    Console.Write("Something went wrong. Please retype Production Date: ");
                }
            }
        }

        public void ExpiredDateInputFunction()
        {
            bool isCompleted = false;
            Console.Write("Expired Date: ");

            while (!isCompleted)
            {
                string strExpiredDate = Console.ReadLine();

                try
                {
                    if (strExpiredDate == null) throw new NullInputationException();

                    else
                    {
                        if (strExpiredDate.Length == 10)
                        {
                            int.TryParse(strExpiredDate.Substring(0, 2), out int day);
                            int.TryParse(strExpiredDate.Substring(3, 2), out int month);
                            if (day >= 32 || day <= 0)
                            {
                                Console.WriteLine("Invalid: Day must be greater than 0 and less than 32");
                                throw new NegativeNumException();
                            }
                            else if (day >= 28 && month == 2)
                            {
                                Console.WriteLine("Invalid: Feb doeas not have {0} days", day);
                            }

                            if (month <= 0 || month >= 13)
                            {
                                Console.WriteLine("Invalid: Month must be greater than 0 and less than 13");
                                throw new NegativeNumException();
                            }

                        }
                    }

                    ValExpiredDate = strExpiredDate;

                    if (ExpiredDate.GetType() != typeof(DateTime))
                    {
                        throw new FormatException();
                    }

                    if (ExpiredDate < ProductionDate)
                    {
                        throw new Exception();
                    }

                    isCompleted = true;
                }
                catch (NegativeNumException)
                {
                    Console.Write("Re-enter valid date: ");
                }
                catch (NullInputationException)
                {
                    Console.WriteLine("Look like you entered empty date. Please re-enter seriously: ");
                }
                catch (FormatException)
                {
                    Console.Write("Input Production Date with form 'dd/MM/yyyy': ");
                }
                catch (Exception)
                {
                    Console.Write("Expired Date must be greater than Production Date !!\nPlease re-enter Expired Date: ");
                }
            }
        }

        public void QuantityInputFunction()
        {
            bool isCompleted = false;
            Console.Write("Quantity: ");

            while (!isCompleted)
            {
                string strQuantity = Console.ReadLine();

                try
                {

                    int num = int.Parse(strQuantity);

                    if (strQuantity == null)
                    {
                        throw new NullInputationException();
                    }

                    if (num < 0)
                    {
                        throw new NegativeNumException();
                    }

                    ValQuantity = num;
                    isCompleted = true;
                }
                catch (NegativeNumException)
                {
                    Console.Write("Quantity must be geater than 0. Re-enter Quantity: ");
                }
                catch (NullInputationException)
                {
                    Console.Write("Look like you entered empty quantity.Please re-enter seriously: ");
                }
                catch (FormatException)
                {
                    Console.Write("Quantity must be an interger. Re-enter Quantity: ");
                }
                catch (Exception)
                {
                    // This Exception will not be catched but still here for the unknown case.
                    Console.Write("Something went wrong. Please retype Production Date: ");
                }
            }
        }

        public void InputInfo()
        {
            Console.WriteLine("Input Milk Infomation: ");
            Console.Write("Name: "); ValMilkName = Console.ReadLine();

            if (ValMilkName == null)
            {
                ValMilkName = "N/A";
            }

            ProDuctionDateInputFunction();
            ExpiredDateInputFunction();
            QuantityInputFunction();
        }

        public void OutputInfo()
        {
            string OutputMessage = String.Format("\n Name: {0}", ValMilkName);
            OutputMessage += String.Format("\n Production Date: {0}", ValProductionDate);
            OutputMessage += String.Format("\n Expired Date: {0}", ValExpiredDate);
            OutputMessage += String.Format("\n Quantity: {0}", ValQuantity);
            MessageBox(0, OutputMessage, "Milk Infomation", 1);
        }

        public string ValMilkID
        {
            get { return MilkID; }

        }
        
        public string ValMilkName
        {
            get { return MilkName; }
            set { MilkName = value; }
        }

        public string ValProductionDate
        {
            get { return ProductionDate.ToString("dd/MM/yyyy"); }
            set
            {
                ProductionDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                MilkID = String.Format("MILK{0}", ProductionDate.ToString("ddMMyyyy"));
            }
        }

        public string ValExpiredDate
        {
            get { return ExpiredDate.ToString("dd/MM/yyyy"); }
            set { ExpiredDate = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture); }
        }

        public int ValQuantity
        {
            get { return Quantity; }
            set { Quantity = value; }
        }
    }
}

namespace MilkManagement
{
    internal class Program
    {
       [DllImport("User32.dll")]
        public static extern int MessageBox(int hParent, string Message, string Caption, int Type);
        
        static void Main(string[] args)
        {
            Milk milk = new Milk();

            MilkInOut milkin = new MilkInOut(milk.InputInfo);
            MilkInOut milkout = new MilkInOut(milk.OutputInfo);

            milkin.Invoke();
            milkout.Invoke();

            Type type = typeof(Milk);
            string OutputMessage = "Milk Infomation: ";
            foreach (var attribute in Attribute.GetCustomAttributes(type))
            {
                if (attribute is MilkMoreInfo moreinfo)
                {
                    if (moreinfo != null)
                    {
                        OutputMessage = String.Format("\n Manufacturer: {0}", moreinfo.Manufacturer);
                        OutputMessage += String.Format("\n Company: {0}", moreinfo.CompanyName);
                        Console.WriteLine(OutputMessage);
                    }
                }

            }


            Console.ReadKey();
        }
    }
}