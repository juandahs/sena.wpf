using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf.ListadoEstudiantes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> Nombres { get; set; }

        public MainWindow()
        {
            Nombres = new ObservableCollection<string>();
            InitializeComponent();
            lbEstudiantes.ItemsSource = Nombres;
            txtNombre.Text = string.Empty;
            txtNombre.Focus();

        }

        static bool EsNombreValido(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
                return false;

            string patron = @"^[a-zA-ZáéíóúüÁÉÍÓÚÜñÑ\s]+$";
            return Regex.IsMatch(nombre, patron);
        }

        private void bntAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (EsNombreValido(txtNombre.Text))
                Nombres.Add(txtNombre.Text);
            else
                MessageBox.Show("El nombre ingresado no es valido", "Lista de nombres", MessageBoxButton.OK, MessageBoxImage.Error);

            txtNombre.Focus();
        }

        private void bntEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lbEstudiantes.SelectedItem != null)
                Nombres.Remove(lbEstudiantes.SelectedItem.ToString());
            else
                MessageBox.Show("No hay nombre seleccionado", "Lista nombres", MessageBoxButton.OK, MessageBoxImage.Information);            
        }
        
    }
}
