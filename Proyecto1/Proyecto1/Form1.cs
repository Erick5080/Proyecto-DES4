using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// proyecto calculadora basica
// autores: Anthony Hernandez y Erick Ozpina
/* 
 * - Permitir realizar las siguientes operaciones: sumar, restar, multiplicar, dividir.
 * - Permitir borrar mediante CE (clear entry) (botón CE).
 * - Permitir realizar las operaciones: número elevado al cuadrado y raíz cuadrada de un número.
 * - Permitir utilizar números negativos (botón -).
 * - Permitir utilizar números decimales.
 * - Guardar cada cálculo realizado en una tabla de una base de datos de MS SQL Server (bajo el nombre que desee colocarle tanto a la base de datos como la tabla, y sus campos).

 */

namespace Proyecto1
{
    public partial class Form1 : Form
    {

        double FirstNumber;
        string Operation;

        public Form1()
        {
            InitializeComponent();
        }

        //private void label1_Click(object sender, EventArgs e)           // label calculadora
        //{

        //}
        private void button1_Click(object sender, EventArgs e)          // btn 1
        {
            String valor = "1";
            concatText(valor);
        }

        private void button2_Click(object sender, EventArgs e)          // btn 2
        {
            String valor = "2";
            concatText(valor);
        }
        private void button3_Click(object sender, EventArgs e)          // btn 3
        {
            String valor = "3";
            concatText(valor);
        }
        private void button4_Click(object sender, EventArgs e)          // btn 4
        {
            String valor = "4";
            concatText(valor);
        }
        private void button5_Click(object sender, EventArgs e)          // btn 5
        {
            String valor = "5";
            concatText(valor);
        }
        private void button6_Click(object sender, EventArgs e)          // btn 6
        {
            String valor = "6";
            concatText(valor);
        }

        private void button7_Click(object sender, EventArgs e)          // btn 7
        {
            String valor = "7";
            concatText(valor);
        }

        private void button8_Click(object sender, EventArgs e)          // btn 8
        {
            String valor = "8";
            concatText(valor);
        }

        private void button9_Click(object sender, EventArgs e)          // btn 9
        {
            String valor = "9";
            concatText(valor);
        }

        private void button10_Click(object sender, EventArgs e)         // btn 0
        {
            String valor = "0";
            concatText(valor);
        }

        private void button11_Click(object sender, EventArgs e)         // button division
        {
            FirstNumber = Convert.ToDouble(textBox1.Text);
            textBox1.Text = "0";
            Operation = "/";
        }

        private void btn_equal_Click(object sender, EventArgs e)        // button equals
        {

            double secondNumber = Convert.ToDouble(textBox1.Text);
            double result = CalcularResultado(FirstNumber, secondNumber, Operation);

            // Actualizar el TextBox y el valor de FirstNumber
            if (textBox1.Text != "Cannot divide by zero" && textBox1.Text != "Invalid operation")
            {
                textBox1.Text = result.ToString();
                FirstNumber = result;
            }
        }

        private void btn_por_Click(object sender, EventArgs e)          // button multiplication
        {
            FirstNumber = Convert.ToDouble(textBox1.Text);
            textBox1.Text = "0";
            Operation = "*";
        }

        private void btn_min_Click(object sender, EventArgs e)          // button minus
        {
            FirstNumber = Convert.ToDouble(textBox1.Text);
            textBox1.Text = "0";
            Operation = "-";
        }

        private void btn_plus_Click(object sender, EventArgs e)         // button plus
        {
            FirstNumber = Convert.ToDouble(textBox1.Text);
            textBox1.Text = "0";
            Operation = "+";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)         // C button
        {
            textBox1.Text = "0";
        }

        private void button11_Click_1(object sender, EventArgs e)       // CE button
        {

        }

        private void button13_Click(object sender, EventArgs e)         // square root button
        {

        }

        private void button14_Click(object sender, EventArgs e)         // square button
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)   // result textbox
        {
            
        }

        // funciones adicionales

        //concatenar en textbox
        private void concatText(string valor)
        {
            if (textBox1.Text == "0" && textBox1.Text != null)
            {
                textBox1.Text = valor;
            }
            else
            {
                textBox1.Text = textBox1.Text + valor;
            }
        }


        private double CalcularResultado(double firstNumber, double secondNumber, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    break;

                case "-":
                    result = firstNumber - secondNumber;
                    break;

                case "*":
                    result = firstNumber * secondNumber;
                    break;

                case "/":
                    if (secondNumber == 0)
                    {
                        textBox1.Text = "Cannot divide by zero";
                        return firstNumber; // Mantener el valor anterior
                    }
                    result = firstNumber / secondNumber;
                    break;

                default:
                    textBox1.Text = "Invalid operation";
                    return firstNumber;
            }

            return result;
        }


    }
}
