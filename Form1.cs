using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lombard_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void comboBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOperation = comboBox1.SelectedItem.ToString();
            //MessageBox.Show(selectedOperation, "Selected Operation", MessageBoxButtons.OK, MessageBoxIcon.Information);

            label2.Visible = false;
            label2.Text = "Введіть ПІБ клієнта";
            label3.Visible = false;
            label3.Text = "Введіть номер телефону клієнта";
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            textBox1.Visible= false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            button4.Visible = false;
            button5.Visible = false;





            if (selectedOperation == "Прийом товару")

            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                //label5.Visible = true;
                //label6.Visible = true;
                //label7.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                button4.Visible = true;

            }

            else if (selectedOperation == "Повернення товару")
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                button4.Visible = true;
            }

            else if (selectedOperation == "Переглянути стан товару")
            {
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = true;
                textBox4.Visible = true;
                textBox5.Visible = true;
                button4.Visible = true;
            }

        }
        Client client = new Client();
        private void button4_Click(object sender, EventArgs e)
        {
            string filePath = "file.xml";

            






            label8.Visible = false;
            label5.Visible = false;
            label7.Visible = false;
            string selectedOperation = comboBox1.SelectedItem.ToString();
            if (selectedOperation == "Прийом товару")

            {
                string lastName = textBox1.Text;
                string firstName = textBox2.Text;
                string middleName = textBox3.Text;
                string phoneNumber = textBox4.Text;
                int age = int.Parse(textBox5.Text);

                
                
                client = new Client(lastName+" "+firstName+ " " + middleName, age, phoneNumber);
                
                //string filePath = "file.xml"; 
                client.AddClientToFile(filePath);
                label2.Visible = true;
                label2.Text = "Найменування";
                label3.Visible = true;
                label3.Text = "Термін зберігання (в днях)";
                label4.Visible = false;
                //label5.Visible = true;
                label6.Visible = true;
                //label7.Visible = true;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox3.Visible = false;
                textBox4.Visible = true;
                textBox5.Visible = false;
                button5.Visible = true;
                button4.Visible = false;
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
            }
            else if (selectedOperation == "Повернення товару")
            {

                string lastName = textBox1.Text;
                string firstName = textBox2.Text;
                string middleName = textBox3.Text;
                string phoneNumber = textBox4.Text;
                int age = int.Parse(textBox5.Text);


                
                client = new Client(lastName + " " + firstName + " " + middleName, age, phoneNumber);
                



                
                label2.Visible = true;
                label2.Text = "Найменування";
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();
                button5.Visible = true;
                button4.Visible = false;

            }

            else if (selectedOperation == "Переглянути стан товару")
            {

                string lastName = textBox1.Text;
                string firstName = textBox2.Text;
                string middleName = textBox3.Text;
                string phoneNumber = textBox4.Text;
                int age = int.Parse(textBox5.Text);


                
                client = new Client(lastName + " " + firstName + " " + middleName, age, phoneNumber);
                



                

                label2.Visible = true;
                label2.Text = "Найменування";
                textBox2.Visible = false;
                textBox3.Visible = false;
                textBox4.Visible = false;
                textBox5.Visible = false;
                label3.Visible = false;
                label4.Visible = false;
                textBox1.Clear();
                textBox2.Clear();
                textBox4.Clear();
                button5.Visible = true;
                button4.Visible = false;
            }


            StorageManager.UpdateStorageDurations(filePath);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string selectedOperation = comboBox1.SelectedItem.ToString();
            if (selectedOperation == "Прийом товару")

            {
                string Item_name = textBox1.Text;
                int valuation = int.Parse(textBox2.Text);
                int duration = int.Parse(textBox4.Text);

                double loanAmount = Math.Round(Math.Max(valuation * 0.5, valuation * 0.9 * Math.Pow(0.95, duration / 30)), 3);
                DateTime date = DateTime.Now;

                
                Item item = new Item(Item_name, valuation, loanAmount, date, duration);
                
                
                string filePath = "file.xml"; 
                client.AddItem(item,filePath);


                int ind = comboBox1.SelectedIndex;
                comboBox1.SelectedIndex = (comboBox1.SelectedIndex + 1) % 2;
                comboBox1.SelectedIndex = ind;
                label8.Visible = true;
                label8.Text = "Ваш товар прийнято";
                label5.Visible = true;
                label5.Text = "Сума видана під заставу  " + loanAmount.ToString();
                label7.Visible = true;
                label7.Text = "Дата здачі товару  " + date.ToString("dd/MM/yyyy");
            }
            else if (selectedOperation == "Повернення товару")
            {
                string Item_name = textBox1.Text;
                string filePath = "file.xml"; 
                double returnAmount = client.RemoveItem(Item_name, filePath);

                
                int ind = comboBox1.SelectedIndex;
                comboBox1.SelectedIndex = (comboBox1.SelectedIndex + 1) % 2;
                comboBox1.SelectedIndex = ind;
                if (returnAmount == 0)
                {
                    label8.Visible = true;
                    label8.Text = "Товар не повернуто";
                }
                else
                {
                    label8.Visible = true;
                    label8.Text = "Товар повернуто";
                    label5.Visible = true;
                    label5.Text = "Сума за повернення " + returnAmount.ToString();
                }
                

            }

            else if (selectedOperation == "Переглянути стан товару")
            {
                string Item_name = textBox1.Text;
                string filePath = "file.xml"; 
                string status = client.CheckItemStatus(Item_name, filePath);



                

                int ind = comboBox1.SelectedIndex;
                comboBox1.SelectedIndex = (comboBox1.SelectedIndex + 1) % 2;
                comboBox1.SelectedIndex = ind;

                if (status == "")
                {
                    label8.Visible = true;
                    label8.Text = "Невідповідні дані";
                }
                else
                {
                    label8.Visible = true;
                    label8.Text = status;
                    //label5.Visible = true;
                    //label5.Text = "Сума за повернення " + returnAmount.ToString();
                }
            }
        }
    }
}
