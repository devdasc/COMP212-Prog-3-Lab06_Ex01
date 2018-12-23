using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace Devdas_Chatterjee_Lab06_Ex01
{

    public enum PreviousType { Integer, Double, Char }

    public partial class Form1 : Form
    {
        // private field properties
        private readonly static int arrayLength = 10;
        private readonly static int[] intArray = new int[arrayLength];
        private readonly static double[] doubleArray = new double[arrayLength];
        private readonly static char[] charArray = new char[arrayLength];
        private PreviousType Previous;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            rbtn_integer.Checked = true;
            Previous = PreviousType.Integer;
        }
        // event handler to exit the app
        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        // method to calculate factorial
        #region Factorial
        private long FactorialCalculator(long i)
        {
            Thread.Sleep(200);
            if (i == 0)
            {
                return 0;
            }
            else if (i == 1)
            {
                return 1;
            }
            else
            {
                return i * FactorialCalculator(i - 1);
            }
        }
        // event handler for Calculate factorial
        private async void btn_CalcFactorial_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_msg.ForeColor = Color.Red;
                long number = long.Parse(textBox_facInput.Text);
                if (textBox_facInput.Text == null)
                {

                    lbl_msg.Text = "Input box is empty";
                }
                if (number < 0)
                {
                    textBox_facResult.Text = null;
                    lbl_msg.Text = "Only positive number accepted";
                }
                else
                {
                    Task<long> task = Task.Run(() => FactorialCalculator(number));
                    lbl_msg.ForeColor = Color.Black;
                    lbl_msg.Text = "Calculating...Please wait";
                    await task;
                    lbl_msg.Text = null;
                    textBox_facResult.Text = task.Result.ToString();
                }
            }
            catch (FormatException ee)
            {
                textBox_facResult.Text = null;
                lbl_msg.Text = "Invalid type";
            }
        }
        //event handler for odd event check
        #endregion
        #region oddEven
        private void btn_oddEven_Click(object sender, EventArgs e)
        {
            try
            {
                var number = int.Parse(textBox_oddEvenInput.Text);
                if (isOdd.Invoke(number) == true)
                {
                    lbl_oddEvenError.Text = null;
                    textBox_oddEvenResult.Text = "Odd";
                }
                if (isEven.Invoke(number) == true)
                {
                    lbl_oddEvenError.Text = null;
                    textBox_oddEvenResult.Text = "Even";
                }
            }
            catch (FormatException)
            {
                textBox_oddEvenResult.Text = null;
                lbl_oddEvenError.ForeColor = Color.Red;
                lbl_oddEvenError.Text = "Invalid Type";
            }
        }
        // methods for odd event check
        Func<int, bool> isOdd = (int x) => { return (x % 2) != 0; };
        Func<int, bool> isEven = (int x) => { return (x % 2) == 0; };
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        // event handler for generating integer/double or char array values
        #region display array
        private void btn_genValues_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_errorMsg.Text = null;
                if (rbtn_integer.Checked)
                {
                    //textBox_numGenerator.Clear();
                    for (int i = 0; i < intArray.Length; i++)
                    {
                        intArray[i] = random.Next(10, 100);
                        //textBox_numGenerator.Text += intArray[i].ToString();
                        //textBox_numGenerator.AppendText(Environment.NewLine);
                        
                    }
                    listBox1.DataSource = intArray;
                    Previous = PreviousType.Integer;
                }
                else if (rbtn_double.Checked)
                {
                    //textBox_numGenerator.Clear();
                    for (int i = 0; i < doubleArray.Length; i++)
                    {
                        doubleArray[i] = Math.Round(random.NextDouble(10.0, 100.0), 2, MidpointRounding.AwayFromZero);
                        //textBox_numGenerator.Text += doubleArray[i].ToString();
                        //textBox_numGenerator.AppendText(Environment.NewLine);
                    }
                    listBox1.DataSource = doubleArray;
                    Previous = PreviousType.Double;
                }
                else if (rbtn_char.Checked)
                {
                    //textBox_numGenerator.Clear();

                    for (int i = 0; i < intArray.Length; i++)
                    {
                        int num = random.Next(65, 122);
                        charArray[i] = (char)(num);
                        //textBox_numGenerator.Text += charArray[i].ToString();
                        //textBox_numGenerator.AppendText(Environment.NewLine);
                    }
                    listBox1.DataSource=charArray;
                    Previous = PreviousType.Char;
                }
                else
                {
                    lbl_errorMsg.ForeColor = Color.Red;
                    lbl_errorMsg.Text = "You haven't selected any type of values";
                }
            }
            catch (ArgumentNullException ee)
            {
                lbl_errorMsg.Text = ee.Message;
            }
        }
        // generic method for printing array between low and high index
        private void PrintData<T>(T[] inputArray, int lowIndex, int highIndex)
        {
            try
            {
                lbl_errorMsg.ForeColor = Color.Red;
                lbl_errorMsg.Text = null;
                textBox_LowHighOutput.Text = null;
                if (lowIndex < 0 || highIndex < 0)
                {
                    lbl_errorMsg.Text = "An index cannot be negative number";
                }
                else if (lowIndex >= highIndex)
                {
                    lbl_errorMsg.Text = "lowIndex cannot be greater than or equal to highIndex";
                }
                else if (lowIndex > inputArray.Length)
                {
                    lbl_errorMsg.Text = "Lowindex is out of Range";
                }
                else if (highIndex >= inputArray.Length)
                {
                    lbl_errorMsg.Text = "High index is out of Range";
                }
                else
                {
                    for (int i = lowIndex; i <= highIndex; i++)
                    {
                        textBox_LowHighOutput.Text += inputArray[i].ToString() + "  ";
                    }
                }
            }
            catch (ArgumentException e)
            {
                lbl_errorMsg.Text = null;

                lbl_errorMsg.Text = e.Message;
            }
        }
        // event handler for low and high index display button
        private void btn_lowHighResult_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_lowIndexInput.Text == "" || textBox_highIndexInput.Text == "")
                {
                    lbl_errorMsg.ForeColor = Color.Red;
                    lbl_errorMsg.Text = "Either of the text box is empty";
                }
                else
                {
                    int lowInt = int.Parse(textBox_lowIndexInput.Text);
                    int highInt = int.Parse(textBox_highIndexInput.Text);
                    if (rbtn_char.Checked)
                    {
                        PrintData(charArray, lowInt, highInt);
                    }
                    if (rbtn_integer.Checked)
                    {
                        PrintData(intArray, lowInt, highInt);
                    }
                    if (rbtn_double.Checked)
                    {
                        PrintData(doubleArray, lowInt, highInt);
                    }
                }
            }
            catch (FormatException)
            {
                textBox_LowHighOutput.Text = null;
                lbl_errorMsg.ForeColor = Color.Red;
                lbl_errorMsg.Text = "Invalid Type";
            }
        }
        // method to print search result
        #endregion
        private void SearchResult(bool result)
        {
            if (result == true)
            {
                MessageBox.Show("Search data found");
            }
            else
            {
                MessageBox.Show("Search data not found");
            }
        }
        #region search
        //event handler for search button
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_errorMsg.Text = null;

                switch (Previous)
                {
                    case PreviousType.Char:
                        {
                            char searchData = Convert.ToChar(textBox_searchdata.Text);
                            var result = SearchData(charArray, searchData);
                            SearchResult(result);
                            break;
                        }
                    case PreviousType.Integer:
                        {
                            int searchData = Convert.ToInt32(textBox_searchdata.Text);
                            var result = SearchData(intArray, searchData);
                            SearchResult(result);
                            break;
                        }
                    case PreviousType.Double:
                        {
                            double searchData = Convert.ToDouble(textBox_searchdata.Text);
                            var result = SearchData(doubleArray, searchData);
                            SearchResult(result);
                            break;
                        }
                }

            }
            catch (FormatException)
            {
                lbl_errorMsg.ForeColor = Color.Red;
                lbl_errorMsg.Text = "Invalid type";
            }
        }
        private static bool SearchData<T>(T[] ArryData, T searchNumber) where T : IComparable
        {
            bool isFound = false;
            var searchData = searchNumber;
            for (int counter = 0; counter < ArryData.Length; counter++)
            {
                if (ArryData[counter].CompareTo(searchNumber) == 0)
                {
                    isFound = true;
                }
            }
            return isFound;
        }
        #endregion
    }
    #region Random Class
    public static class RandomGenerator
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue / 2;
        }
    }
    #endregion
}
