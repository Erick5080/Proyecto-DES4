using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proyecto1
{
    public partial class Form1 : Form
    {
        // 1. Instancia de la clase de conexión a la base de datos
        private readonly ConexionDB db = new ConexionDB();

        // Variables para el cálculo
        private decimal FirstNumber = 0;
        private string Operation = "";

        // Variable para controlar si se acaba de presionar un operador o el igual
        private bool isNewNumber = true;

        public Form1()
        {
            InitializeComponent();
        }

        // --- Funciones Auxiliares ---
        private void concatText(string valor)
        {
            if (isNewNumber || textBox1.Text == "0" || textBox1.Text == "Cannot divide by zero")
            {
                textBox1.Text = valor;
                isNewNumber = false;
            }
            else
            {
                textBox1.Text += valor;
            }
        }

        // --- Manejadores de Eventos Numéricos y Decimales (Mismo Código) ---

        private void button1_Click(object sender, EventArgs e) => concatText("1");
        private void button2_Click(object sender, EventArgs e) => concatText("2");
        private void button3_Click(object sender, EventArgs e) => concatText("3");
        private void button4_Click(object sender, EventArgs e) => concatText("4");
        private void button5_Click(object sender, EventArgs e) => concatText("5");
        private void button6_Click(object sender, EventArgs e) => concatText("6");
        private void button7_Click(object sender, EventArgs e) => concatText("7");
        private void button8_Click(object sender, EventArgs e) => concatText("8");
        private void button9_Click(object sender, EventArgs e) => concatText("9");
        private void button10_Click(object sender, EventArgs e) => concatText("0");

        private void btn_decimal_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains("."))
            {
                textBox1.Text += ".";
            }
        }

        private void btn_neg_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal number))
            {
                textBox1.Text = (-number).ToString();
            }
        }

        // --- Manejadores de Eventos de Operadores Aritméticos (Mismo Código) ---

        private void button11_Click(object sender, EventArgs e) // División ( / )
        {
            FirstNumber = Convert.ToDecimal(textBox1.Text);
            Operation = "/";
            isNewNumber = true;
        }

        private void btn_por_Click(object sender, EventArgs e) // Multiplicación ( * )
        {
            FirstNumber = Convert.ToDecimal(textBox1.Text);
            Operation = "*";
            isNewNumber = true;
        }

        private void btn_min_Click(object sender, EventArgs e) // Resta ( - )
        {
            FirstNumber = Convert.ToDecimal(textBox1.Text);
            Operation = "-";
            isNewNumber = true;
        }

        private void btn_plus_Click(object sender, EventArgs e) // Suma ( + )
        {
            FirstNumber = Convert.ToDecimal(textBox1.Text);
            Operation = "+";
            isNewNumber = true;
        }

        // --- Botones de Control y Funciones Especiales (Lógica de Guardado Agregada) ---

        private void button11_Click_1(object sender, EventArgs e) // Botón CE (Clear Entry)
        {
            // Solo limpia la entrada, no se guarda en la DB

            textBox1.Text = "0";
            isNewNumber = true;
        }

        private void button12_Click(object sender, EventArgs e) // Botón C (Clear All)
        {
            // Limpia todo, no se guarda en la DB
            textBox1.Text = "0";
            FirstNumber = 0;
            Operation = "";
            isNewNumber = true;
        }

        private void button13_Click(object sender, EventArgs e) // Raíz Cuadrada ( SQRT )
        {

            if (decimal.TryParse(textBox1.Text, out decimal number) && number >= 0)
            {
                decimal result_raiz = (decimal)Math.Sqrt((double)number);
                textBox1.Text = result_raiz.ToString();

                // GUARDAR EN DB: Convertir a STRING para la DB
                try
                {
                    db.InsertarRegistro(
                        number.ToString(),             // El número original (como string)
                        "0",                           // Segundo número es 0 (como string)
                        result_raiz.ToString(),        // Resultado de Raíz Cuadrada (como string)
                        "0", "0", null                 // Resto de resultados como string
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar Raíz Cuadrada en DB: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FirstNumber = result_raiz;
            }
            else
            {
                MessageBox.Show("Operación no válida para raíz cuadrada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
            }
            isNewNumber = true;
        }

        private void button14_Click(object sender, EventArgs e) // Cuadrado ( X^2 )
        {

            if (decimal.TryParse(textBox1.Text, out decimal number))
            {
                decimal result_cuadrado = number * number;
                textBox1.Text = result_cuadrado.ToString();

                try
                {
                    db.InsertarRegistro(
                        number.ToString(),            
                        "0",                           
                        result_cuadrado.ToString(),    
                        "0", "0", null                 
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar Cuadrado en DB: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FirstNumber = result_cuadrado;
            }
            else
            {
                MessageBox.Show("Operación no válida para cuadrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "0";
            }
            isNewNumber = true;
        }

        // 2. Abre la ventana de historial al hacer clic en el PictureBox
        private void pictureBoxHistorial_Click(object sender, EventArgs e)
        {
            HistorialForm historial = new HistorialForm(db);
            historial.ShowDialog();
        }


        //  Lógica de Cálculo y Guardado

        private void btn_equal_Click(object sender, EventArgs e) 
        {
            if (Operation == "") return;

            if (!decimal.TryParse(textBox1.Text, out decimal secondNumber))
            {
                MessageBox.Show("Segundo número inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            decimal suma = 0, resta = 0, multiplicacion = 0;
            decimal? division = null;
            decimal result = 0;

            // 1. Realiza el cálculo y asigna el resultado
            switch (Operation)
            {
                case "+":
                    result = suma = FirstNumber + secondNumber;
                    break;
                case "-":
                    result = resta = FirstNumber - secondNumber;
                    break;
                case "*":
                    result = multiplicacion = FirstNumber * secondNumber;
                    break;
                case "/":
                    if (secondNumber == 0)
                    {
                        textBox1.Text = "Cannot divide by zero";
                        return;
                    }
                    division = FirstNumber / secondNumber;
                    result = division.Value;
                    break;
            }

            // 2. Muestra el resultado y actualiza el valor para la siguiente operación
            textBox1.Text = result.ToString();

            // 3. Guarda en la base de datos usando la instancia 'db'
            try
            {
                db.InsertarRegistro(
                    FirstNumber.ToString(),
                    secondNumber.ToString(),
                    suma.ToString(),
                    resta.ToString(),
                    multiplicacion.ToString(),
                    division.HasValue ? division.Value.ToString() : null // Maneja decimal? a string?
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar en la Base de Datos: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 4. Reinicia las variables
            FirstNumber = result;
            Operation = "";
            isNewNumber = true;
        }


        private void Form1_Load(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Operation == "")
            {
                // Si no hay operación, simplemente calcula el 1% del número actual
                if (decimal.TryParse(textBox1.Text, out decimal number))
                {
                    decimal result_solo = number / 100;
                    textBox1.Text = result_solo.ToString();
                    FirstNumber = result_solo;
                }
                isNewNumber = true;
                return;
            }

            if (decimal.TryParse(textBox1.Text, out decimal secondNumber))
            {
                decimal result = 0;
                decimal result_suma = 0, result_resta = 0, result_multiplicacion = 0;
                string division_guardar = null;

                // 1. Calcula el valor absoluto del porcentaje
                decimal porcentaje_valor = FirstNumber * (secondNumber / 100);

                // 2. Ejecuta la operación final dependiendo del operador
                switch (Operation)
                {
                    case "+":
                        
                        result = result_suma = FirstNumber + porcentaje_valor;
                        break;

                    case "-":
                        
                        result = result_resta = FirstNumber - porcentaje_valor;
                        break;

                    case "*":
                        // Multiplica FirstNumber por el valor del porcentaje
                        result = result_multiplicacion = FirstNumber * (secondNumber / 100);
                        break;

                    case "/":
                        // Divide FirstNumber por el valor del porcentaje
                        if (porcentaje_valor == 0)
                        {
                            textBox1.Text = "Cannot divide by zero";
                            return;
                        }
                        result = FirstNumber / porcentaje_valor;
                        division_guardar = result.ToString();
                        break;
                }

                textBox1.Text = result.ToString();

                //  3. GUARDADO EN DB 
                try
                {
                    db.InsertarRegistro(
                        FirstNumber.ToString(),            
                        secondNumber.ToString() + " %",     
                        result_suma.ToString(),             
                        result_resta.ToString(),            
                        result_multiplicacion.ToString(),   
                        division_guardar                    
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar Porcentaje en DB: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // 4. Reinicia las variables para la siguiente operación
                FirstNumber = result;
                Operation = "";
                isNewNumber = true;
            }
        }

        private void btnReciproco_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(textBox1.Text, out decimal number))
            {
                if (number == 0)
                {
                    textBox1.Text = "Cannot divide by zero";
                    return;
                }

                decimal result_reciprocal = 1 / number;
                textBox1.Text = result_reciprocal.ToString();

                try
                {
                    db.InsertarRegistro(
                        "1",
                        number.ToString(),
                        "0", "0", "0",
                        result_reciprocal.ToString() // Resultado de 1/X
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al guardar Recíproco en DB: {ex.Message}", "Error DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                FirstNumber = result_reciprocal;
            }
            isNewNumber = true;
        }
    }
}