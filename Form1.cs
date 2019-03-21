using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
namespace Proyecto3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [SQLiteFunction(Name = "Ping", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Program : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                Ping ping = new Ping();
                String ip = args[0].ToString();
                try
                {
                    PingReply reply = ping.Send(ip);
                    if (reply.Status == IPStatus.Success)
                    {
                        return 1;
                    }

                }
                catch (PingException e)
                {
                    System.Console.WriteLine(e.StackTrace);
                }


                return 0;
            }
        }

        [SQLiteFunction(Name = "PMT", Arguments = 3, FuncType = FunctionType.Scalar)]
        class pmt : SQLiteFunction
        {
            public static double PMT(double yearlyInterestRate, int totalNumberOfMonths, double loanAmount)
            {
                var rate = (double)yearlyInterestRate / 100 / 12;
                var denominator = Math.Pow((1 + rate), totalNumberOfMonths) - 1;
                return (rate + (rate / denominator)) * loanAmount;
            }

        }

        [SQLiteFunction(Name = "Bin2Dec", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Bin2Dec : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                String bin = args[0].ToString();
                return Convert.ToInt32(bin, 2).ToString();

            }
            


        }
        [SQLiteFunction(Name = "Dec2Bin", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Dec2Bin : SQLiteFunction
        {

            public override object Invoke(object[] args)
            {
                int dec = Convert.ToInt32(args[0].ToString());
                return  Convert.ToString(dec, 2);

            }



        }
        
        

        [SQLiteFunction(Name = "F2C", Arguments = 1, FuncType = FunctionType.Scalar)]
        class F2C : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int fahrenheit = Convert.ToInt32(args[0].ToString());
                int celsius = (fahrenheit - 32) * 5 / 9;

                return celsius;
            }
        }

        [SQLiteFunction(Name = "C2F", Arguments = 1, FuncType = FunctionType.Scalar)]
        class C2F : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int celsius = Convert.ToInt32(args[0].ToString());
                int fahrenheit = (celsius * 9) / 5 + 32;

                return fahrenheit;
            }
        }
        [SQLiteFunction(Name = "Hex2Bin", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Hex2Bin : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                string binarystring = String.Join(String.Empty,
                    args.ToString().Select( c => Convert.ToString(Convert.ToInt32(args.ToString(), 16), 2).PadLeft(4, '0')));

                return binarystring;
            }
        }

        [SQLiteFunction(Name = "Factorial", Arguments = 1, FuncType = FunctionType.Scalar)]
        class Factorial : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int numero, resultado = 1;

                numero = Convert.ToInt32(args[0].ToString());

                for (int i = 1; i <= numero; i++)
                {
                    resultado = resultado * i;
                }

                return resultado;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=testFile.txt"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    cmd.CommandText = textBox1.Text;
                    cmd.ExecuteNonQuery();

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                            textBox2.Text = (reader[0].ToString());
                    }
                }
            }
        }
    }
}
