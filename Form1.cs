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

        [SQLiteFunction(Name = "Trim", Arguments = 2, FuncType = FunctionType.Scalar)]
        class trim : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                char string2 = Convert.ToChar(args[1].ToString());
                string string1 = args[0].ToString();
                string result = string1.Trim(string2);
                return result;
            }


        }

        [SQLiteFunction(Name = "PMT", Arguments = 3, FuncType = FunctionType.Scalar)]
        class pmt : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                var rate = Convert.ToDouble(args[0].ToString())/ 100 / 12;
                var denominator = Math.Pow((1 + rate), Convert.ToInt32(args[1].ToString())) - 1;
                return (rate + (rate / denominator)) * Convert.ToInt32(args[2].ToString());
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
                String valBinario = Convert.ToString(Convert.ToInt32(args[0].ToString(), 16), 2);

                return valBinario;
            }
        }

        [SQLiteFunction(Name = "CompareString", Arguments = 2, FuncType = FunctionType.Scalar)]
        class CompareString : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                int resultado = 0;
                if (args[0].ToString().Length == args[1].ToString().Length)
                    return 0;
                if (args[0].ToString().Length < args[1].ToString().Length)
                    return -1;
                if (args[0].ToString().Length > args[1].ToString().Length)
                    return 1;
                return resultado;
            }
        }
        [SQLiteFunction(Name = "Repeat", Arguments = 2, FuncType = FunctionType.Scalar)]
        class Repeat : SQLiteFunction
        {
            public override object Invoke(object[] args)
            {
                String val=args[0].ToString(),result="";
                int numero;

                numero = Convert.ToInt32(args[1].ToString());

                for (int i = 1; i <= numero; i++)
                {
                    result+=" "+val;
                }

                return result;
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
