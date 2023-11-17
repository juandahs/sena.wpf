using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.Calculadora
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        decimal input1 = 0;
        decimal input2 = 0;
        string operations = string.Empty;

        public MainWindow()
        {


            InitializeComponent();
        }

        #region Numeros
        private void btn0_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn1_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn2_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn3_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn4_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn5_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn6_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn7_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn8_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);
        private void btn9_Click(object sender, RoutedEventArgs e) => btnActionNumbers(sender);

        #endregion

        #region Operaciones Basicas
        private void btnDivision_Click(object sender, RoutedEventArgs e) => btnActionOperations(sender);
        private void btnMultiplicatino_Click(object sender, RoutedEventArgs e) => btnActionOperations(sender);
        private void btnSubtraction_Click(object sender, RoutedEventArgs e) => btnActionOperations(sender);
        private void btnAddition_Click(object sender, RoutedEventArgs e) => btnActionOperations(sender); 
        #endregion

        #region Limpieza de información
        //Botón Clear
        private void btnC_Click(object sender, RoutedEventArgs e)
        {
            input1 = 0;
            input2 = 0;
            operations = string.Empty;
            txtResult.Text = "0";
        }

        //Bóton Clear Entry
        private void btnCe_Click(object sender, RoutedEventArgs e)
        {
            //Se valida que entrada debe ser limpiada
            if (string.IsNullOrEmpty(operations))
                input1 = 0;
            else
                input2 = 0;

            txtResult.Text = "0";
        }

        //Bóton elimina la última entrada
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //del sender se convierte en btn para obtener la propiedad content y así su valor.
            Button btn = (Button)sender;
            //Se obtiene el valor del btn enviado            
            if (!decimal.TryParse(txtResult.Text, out decimal value))
                txtResult.Text = "Error";

            if (string.IsNullOrEmpty(operations))
            {
                input1 = (int)Convert.ToDecimal(value / 10);
                txtResult.Text = input1.ToString();
            }
            else
            {
                input2 = (int)Convert.ToDecimal(value / 10);
                txtResult.Text = input2.ToString();
            }
        }
        #endregion


        private void btnSqrt_Click(object sender, RoutedEventArgs e) => txtResult.Text = (Math.Sqrt(double.Parse(txtResult.Text))).ToString();

        private void btnPow_Click(object sender, RoutedEventArgs e) => txtResult.Text = (Math.Pow(double.Parse(txtResult.Text), 2)).ToString();

        private void btnPositiveNegative_Click(object sender, RoutedEventArgs e) => txtResult.Text = (-1 * int.Parse(txtResult.Text)).ToString();

        private void btnPoint_Click(object sender, RoutedEventArgs e) => txtResult.Text += ",";


        private void btnEqual_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (operations)
                {
                    case "+":
                        txtResult.Text = (input1 + input2).ToString();
                        break;
                    case "-":
                        txtResult.Text = (input1 - input2).ToString();
                        break;
                    case "/":
                        txtResult.Text = (input1 / input2).ToString();
                        break;
                    case "*":
                        txtResult.Text = (input1 * input2).ToString();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                txtResult.Text = ex.Message;
                return;
            }

            input2 = decimal.Parse(txtResult.Text);
            operations = string.Empty;
        }

        private void btnActionNumbers(object sender)
        {
            // Del sender se convierte en btn para obtener la propiedad content y así su valor.
            Button btn = (Button)sender;

            // Se obtiene el valor del btn enviado            
            if (!decimal.TryParse(btn.Content.ToString(), out decimal value))
            {
                txtResult.Text = "Error";
                return;
            }

            // Si no hay operación se agrega el número clickeado a lo que tiene el txtResult
            if (string.IsNullOrEmpty(operations))
            {
                //Se valida si la entrada es un decimal o no
                if (!txtResult.Text.Contains(','))
                {
                    input1 = (input1 * 10) + value;
                    txtResult.Text = input1.ToString();
                }
                else
                {
                    // Si ya hay un punto decimal, se añade el valor como decimal
                    input1 += value * 0.1m; // Agregar un dígito decimal al final
                    txtResult.Text = input1.ToString();
                }
            }
            // Si hay una operación por hacer, se almacena el número 
            else
            {
                //Se valida si la entrada es un decimal o no
                if (!txtResult.Text.Contains(','))
                {
                    input2 = (input2 * 10) + value;
                    txtResult.Text = input2.ToString();
                }
                else
                {
                    // Si ya hay un punto decimal, se añade el valor como decimal
                    input2 += value * 0.1m; // Agregar un dígito decimal al final
                    txtResult.Text = input2.ToString();
                }
            }
        }

        private void btnActionOperations(object sender)
        {
            //Si hay una operación ya realizada se almacena en la entrada 1 para operar con la entrada siguiente
            input1 = txtResult.Text != "0" ? decimal.Parse(txtResult.Text) : 0;
            input2 = 0;
            Button button = (Button)sender;
            operations = (string)button.Content;
            txtResult.Text = "0";

        }

        private void btnPorcentage_Click(object sender, RoutedEventArgs e)
        {            
            decimal porcentaje = (input1 * input2) / 100;          
            txtResult.Text = porcentaje.ToString();
            input2 = porcentaje;
        }

        private void btnInverse_Click(object sender, RoutedEventArgs e)
        {
            // Obtiene el valor actual del txtResult
            if (!decimal.TryParse(txtResult.Text, out decimal currentValue))
            {
                txtResult.Text = "Error";
                return;
            }

            // Calcula el inverso (1/x) del valor actual
            decimal inverse = 1 / currentValue;

            // Muestra el resultado en el txtResult
            txtResult.Text = inverse.ToString();
        }
    }
}
