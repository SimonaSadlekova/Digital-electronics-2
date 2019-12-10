using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace Receive_Show_Save_Data
{
    public partial class Form1 : Form
    {                 
            public delegate void DataReceivedEvent(string in_data);
            
            public Form1()
        {
            InitializeComponent();
        }

        private void cpb_com_port_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();
            cpb_com_port.Items.AddRange(ports);
            cpb_com_port.SelectedIndex = 0;
            btnClose.Enabled = false;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = false;
            btnClose.Enabled = true;
            try

            {
                serialPort1.PortName = cpb_com_port.Text;
                serialPort1.Open();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            serialPort1.BaudRate = 9600;
            serialPort1.Parity = Parity.None;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = StopBits.One;
            serialPort1.DataReceived += SerialPort1_DataReceived;
            try 
            {
                tbValue.Text = "";
            }
            catch (Exception ex1)
            {
                MessageBox.Show(ex1.Message,"Error");
            }



        }
       
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string in_data = serialPort1.ReadLine();
                tbValue.Text = in_data;
            }
            catch (Exception ex2)
            {

                MessageBox.Show(ex2.Message, "Error");
                serialPort1.Close();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try {

                serialPort1.Close();
            }
            catch (Exception ex3)
            {
                MessageBox.Show(ex3.Message, "Error");
            }


        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                string pathFile = @"C:\Users\NTB\Desktop\DATA";
                string fileName = "measure_data.txt";
                System.IO.File.WriteAllText(pathFile + fileName, tbValue.Text);
                MessageBox.Show("Your data has been saved to" + pathFile, "Save file.");
            }

            catch (Exception ex4)
            {
                MessageBox.Show(ex4.Message, "Error");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            btnOpen.Enabled = true;
            btnClose.Enabled = false;
            try

            {
                serialPort1.Close();
            }
            catch (Exception ex5)
            {
                MessageBox.Show(ex5.Message, "Error");
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (serialPort1.IsOpen)
                serialPort1.Close();
        }
    }
}
