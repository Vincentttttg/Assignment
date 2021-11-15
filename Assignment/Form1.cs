using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] split = inputtextbox.Text.Split(',');
            int[] number = new int[split.Length];
            Stopwatch a = new Stopwatch();
            Stopwatch b = new Stopwatch();
            Stopwatch c = new Stopwatch();
            Stopwatch d = new Stopwatch();

            try
            {
                for (int i = 0; i < number.Length; i++)
                {
                    number[i] = int.Parse(split[i]);
                }

                int[] newarr = new int[number.Length];
                Array.Copy(number, newarr, number.Length);

                int[] newarr1 = new int[number.Length];
                Array.Copy(number, newarr1, number.Length);

                int[] newarr2 = new int[number.Length];
                Array.Copy(number, newarr2, number.Length);

                int[] newarr3 = new int[number.Length];
                Array.Copy(number, newarr3, number.Length);

                a.Start();
                int[] quickSort = QuickSort(newarr, 0, newarr.Length);
                a.Stop();
                dataGridView1.Columns.Add("SortedArray", "");
                dataGridView1.Columns["SortedArray"].DataPropertyName = "Array";
                dataGridView1.DataSource = quickSort.Select(x => new { Array = x }).ToList();
                quicksortlbl.Text = a.Elapsed.TotalMilliseconds.ToString() + " ms";

                b.Start();
                int[] bubbleSort = BubbleSort(newarr1);
                b.Stop();
                dataGridView2.Columns.Add("SortedArray", "");
                dataGridView2.Columns["SortedArray"].DataPropertyName = "Array";
                dataGridView2.DataSource = bubbleSort.Select(x => new { Array = x }).ToList();
                bubblesortlbl.Text = b.Elapsed.TotalMilliseconds.ToString() + " ms";

                c.Start();
                int[] mergeSort = MergeSort(newarr2, 0 , newarr2.Length-1);
                c.Stop();
                dataGridView3.Columns.Add("SortedArray", "");
                dataGridView3.Columns["SortedArray"].DataPropertyName = "Array";
                dataGridView3.DataSource = mergeSort.Select(x => new { Array = x }).ToList();
                mergesortlbl.Text = c.Elapsed.TotalMilliseconds.ToString() + " ms";

                d.Start();
                int[] gcfSort = GCFSort(newarr3,20);
                d.Stop();
                dataGridView4.Columns.Add("SortedArray", "");
                dataGridView4.Columns["SortedArray"].DataPropertyName = "Array";
                dataGridView4.DataSource = gcfSort.Select(x => new { Array = x }).ToList();
                gcfsortlbl.Text = d.Elapsed.TotalMilliseconds.ToString() + " ms";

                errorlabel.Text = "";
            }
            catch(Exception ex)
            {
                errorlabel.Text = ex.Message;
            }
            
            
        }

        public int[] QuickSort(int[] input,int left, int right)
        {
            int pivot;
            if (left < right)
            {
                pivot = Partitioning(input, left, right);
                QuickSort(input, left, pivot);
                QuickSort(input, pivot + 1, right);
            }

            return input;
            
        }

        public int Partitioning(int[] input, int left, int right)
        {
            int pivot = input[left];
            int swap = left;
            int temp;

            for(int j = left + 1; j < right; j++)
            {
                if (input[j] < pivot)
                {
                    swap++;
                    temp = input[j];
                    input[j] = input[swap];
                    input[swap] = temp;
                }
            }
            temp = input[left];
            input[left] = input[swap];
            input[swap] = temp;
            return swap;
        }

        public int[] BubbleSort(int[] input)
        {
            int temp;

            for(int i = 0; i < input.Length -1; i++)
            {
                for(int j =0; j < input.Length - 1; j++)
                {
                    if (input[j] > input[j+1])
                    {
                        temp = input[j];
                        input[j] = input[j+1];
                        input[j+1] = temp;
                    }
                }
            }

            return input;

        }

        public int[] MergeSort(int[] input, int left, int right)
        {
            int mid;
            if (left < right)
            {
                mid = (left + right) / 2;
                MergeSort(input, left, mid);
                MergeSort(input, mid + 1, right);
                input = Merging(input, left, mid, right);
            }

            return input;
        }

        public int[] Merging(int[] input, int left, int mid, int right)
        {
            int[] leftarr = new int[mid -left+1];
            int[] rightarr = new int[right - mid];

            for(int i = 0; i < leftarr.Length; i++)
            {
                leftarr[i] = input[left+i];
            }

            for(int i =0; i< rightarr.Length; i++)
            {
                rightarr[i] = input[i + mid + 1];
            }

            int b = 0; int c = 0; int d = left;

            while (b < leftarr.Length && c < rightarr.Length)
            {
                if (leftarr[b] < rightarr[c])
                {
                    input[d] = leftarr[b];
                    b++;
                }
                else
                {
                    input[d] = rightarr[c];
                    c++;
                }
                d++;
            }

            while (b < leftarr.Length)
            {
                input[d] = leftarr[b];
                b++;
                d++;
            }

            while (c < rightarr.Length)
            {
                input[d] = rightarr[c];
                c++;
                d++;
            }

            return input;
        }

        public int[] GCFSort(int[] input,int comp)
        {
            int gcf1;
            int gcf2;
            int temp;

            for (int i = 0; i < input.Length - 1; i++)
            {
                for (int j = 0; j < input.Length - 1; j++)
                {
                    gcf1 = GCFactor(input[j],comp);
                    gcf2 = GCFactor(input[j+1], comp);
                    if (gcf1> gcf2)
                    {
                        temp = input[j];
                        input[j] = input[j + 1];
                        input[j + 1] = temp;
                    }
                }
            }

            return input;

        }

        public int GCFactor(int a, int b)
        {
            if( a > b)
            {
                return Factorize(a, b);
            }
            else
            {
                return Factorize(b, a);
            }
        }

        public int Factorize(int a , int b)
        {
            int modulus = a % b;

            if(modulus == 0)
            {
                return b;
            }
            else
            {
                return Factorize(b, modulus);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
