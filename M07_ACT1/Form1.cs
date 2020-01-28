using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using calc;

namespace M07_ACT1
{
    public partial class Form1 : Form
    {
        Calculadora c = new Calculadora();
        //Declaración de variables asignadas al operador, a los dos números introducidos y a la memoria almacenada
        String op, n1, n2, result, mem;
        //Declaración de variable booleana para comprobar si se ha pulsado el botón '='
        Boolean calculado = false;
        public Form1()
        {
            InitializeComponent();
        }
        //Pulsación de botones de números
        private void number_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (String.IsNullOrEmpty(et_muestraResult.Text))
            {   //Si el visualizador de resultados está vacío almacenamos el valor introducido como la primera variable de la operación (n1)
                if (et_pantalla.Text == "0")
                {
                    et_pantalla.Text = button.Text;
                    n1 = et_pantalla.Text;
                } else
                {
                    et_pantalla.Text = et_pantalla.Text + button.Text;
                    n1 = et_pantalla.Text;
                }
            } else
            {
                if (calculado)
                {   //Si ya se ha pulsado el botón igual, al pulsar un número se reinician todas las variables y operaciones
                    et_pantalla.Text = "";
                    et_muestraResult.Text = "";
                    n1 = et_pantalla.Text + button.Text;
                    n2 = "";
                    op = "";
                    calculado = false;
                }
                //Si ya hay valores en la pantalla que muestra la operación almacenamos la segunda variable (n2)
                et_pantalla.Text = et_pantalla.Text + button.Text;
                n2 = et_pantalla.Text;
            }
            
        }
        //Condiciones de pulsación de la coma
        private void coma_click(object sender, EventArgs e)
        {
            if (!et_pantalla.Text.Contains(","))
            {   //Si ya hay una coma en la pantalla no se puede introducir otra
                if (String.IsNullOrEmpty(et_pantalla.Text))
                {   //Si la pantalla está< vacía se detecta como un 0,xxx
                    et_pantalla.Text = "0" + bt_coma.Text;
                } else
                {
                    if (calculado)
                    {
                        et_muestraResult.Text = "";
                        et_pantalla.Text = "0" + bt_coma.Text;
                        calculado = false;
                    } else et_pantalla.Text = et_pantalla.Text + bt_coma.Text;
                }
            }
            
        }
        //Pulsaciones de los diferentes operadores
        private void operator_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (!String.IsNullOrEmpty(et_pantalla.Text) || String.IsNullOrEmpty(et_pantalla.Text) && !String.IsNullOrEmpty(et_muestraResult.Text))
            {   //Si la pantalla no esta vacía o en su defecto, no está vacía la visualización de operaciones...
                if (!String.IsNullOrEmpty(n1) && !String.IsNullOrEmpty(n2) && !String.IsNullOrEmpty(op))
                {   //Si ya se ha introducido una operación, al volver a pulsar sobre un operador este la calcula y sigue con el proceso
                    //Cumple la función de '='
                    c.insert_num(float.Parse(n1));
                    c.insert_op(op[0]);
                    c.insert_num(float.Parse(n2));
                    result = c.operar().ToString();
                    n1 = result;
                    n2 = "";
                }
                if (calculado)
                {   //Si ya se ha calculado un valor a través del botón '=', este resultado será la primera entrada de una nueva operación
                    n1 = result;
                    calculado = false;
                }
                op = button.Text;
                et_muestraResult.Text = n1 + op;
                et_pantalla.Text = "";

            }
        }
        //Cambio de signo del valor introducido
        private void bt_signo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(et_pantalla.Text))
            {
                et_pantalla.Text = (float.Parse(et_pantalla.Text) * (-1)).ToString();
                if (String.IsNullOrEmpty(et_muestraResult.Text))
                {
                    n1 = et_pantalla.Text;
                } else
                {
                    if (calculado)
                    {
                        n1 = et_pantalla.Text;
                        calculado = false;
                    }
                    n2 = et_pantalla.Text;
                }
                    
                
            }
        }
        //Botón de retroceso
        private void bt_borra_Click(object sender, EventArgs e)
        {
            if (!(String.IsNullOrEmpty(et_pantalla.Text) || et_pantalla.Text.Equals("0")))
            {   //Muestra el valor en pantalla eliminando el último dígito
                et_pantalla.Text = et_pantalla.Text.Substring(0, et_pantalla.Text.Length - 1);
                if (String.IsNullOrEmpty(et_muestraResult.Text))
                {
                    n1 = et_pantalla.Text;
                }
                else
                {
                    if (calculado)
                    {
                        n1 = et_pantalla.Text;
                        et_muestraResult.Text = "";
                        calculado = false;
                    }
                    n2 = et_pantalla.Text;
                }
            }
        }
        //Botón de borrar todo
        private void bt_clear_Click(object sender, EventArgs e)
        {
            et_pantalla.Text = "0";
            et_muestraResult.Text = "";
            n1 = "";
            n2 = "";
            op = "";
            calculado = false;
        }
        //Botones de memoria
        private void bt_guardaMem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(et_pantalla.Text))
            {
                mem = et_pantalla.Text;
            }
        }

        private void bt_llamaMem_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(mem))
            {
                if (String.IsNullOrEmpty(et_muestraResult.Text))
                {
                    et_pantalla.Text = mem;
                    n1 = et_pantalla.Text;
                }
                else
                {
                    if (calculado)
                    {
                        et_pantalla.Text = "";
                        et_muestraResult.Text = "";
                        n1 = et_pantalla.Text + mem;
                        n2 = "";
                        op = "";
                        calculado = false;
                    }
                    et_pantalla.Text = mem;
                    n2 = et_pantalla.Text;
                }
            }
        }

        private void bt_borraMem_Click(object sender, EventArgs e)
        {
            mem = "";
        }
        //Botón igual
        private void bt_calcula_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(et_muestraResult.Text) && !String.IsNullOrEmpty(n1) && !String.IsNullOrEmpty(n2) && !String.IsNullOrEmpty(op))
            {   //Se ejecuta solo si todas las variables existen
                c.insert_num(float.Parse(n1));
                c.insert_op(op[0]);
                c.insert_num(float.Parse(n2));
                et_muestraResult.Text = n1 + op + n2 + bt_calcula.Text;
                result = c.operar().ToString();
                et_pantalla.Text = result;
                calculado = true;
                n1 = "";
                n2 = "";
                op = "";
            }
        }
        //Evento de pulsación de teclas asignadas a los botones
        private void btNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            Button button = sender as Button;
            switch (e.KeyChar.ToString())
            {
                case "0":
                    num0.PerformClick();
                    break;
                case "1":
                    num1.PerformClick();
                    break;
                case "2":
                    num2.PerformClick();
                    break;
                case "3":
                    num3.PerformClick();
                    break;
                case "4":
                    num4.PerformClick();
                    break;
                case "5":
                    num5.PerformClick();
                    break;
                case "6":
                    num6.PerformClick();
                    break;
                case "7":
                    num7.PerformClick();
                    break;
                case "8":
                    num8.PerformClick();
                    break;
                case "9":
                    num9.PerformClick();
                    break;
                case "+":
                    bt_suma.PerformClick();
                    break;
                case "-":
                    bt_resta.PerformClick();
                    break;
                case "*":
                    bt_multiplica.PerformClick();
                    break;
                case "/":
                    bt_divide.PerformClick();
                    break;
                case "m":
                case "M":
                    bt_llamaMem.PerformClick();
                    break;
                case "g":
                case "G":
                    bt_guardaMem.PerformClick();
                    break;
                case "x":
                case "X":
                    bt_borraMem.PerformClick();
                    break;
                case "c":
                case "C":
                    bt_clear.PerformClick();
                    break;
                case ",":
                    bt_coma.PerformClick();
                    break;
                case "=":
                    bt_calcula.PerformClick();
                    break;
                case "r":
                case "R":
                    bt_borra.PerformClick();
                    break;
                case "s":
                case "S":
                    bt_signo.PerformClick();
                    break;
            }
        }
    }
}
