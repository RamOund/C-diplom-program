using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace modulnik
{
    /// <summary>
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();
        private void dataGridProducts_Initialized(object sender, EventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT * FROM `product`";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            dataGridProducts.ItemsSource = DGT.AsDataView();
        }

        private void Button_AddProd(object sender, RoutedEventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            AddProd AddproductsWin = new AddProd();
            AddproductsWin.ShowDialog();
            string datagridZapr1 = $"SELECT * FROM `product`";
            MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
            datagridAdpt.SelectCommand = SDG;
            DataTable DGT1 = new DataTable();
            datagridAdpt.Fill(DGT1);
            dataGridProducts.ItemsSource = DGT1.AsDataView();
            database.closeConnection();
        }

        private void Button_EditProd(object sender, RoutedEventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            int selindex = Convert.ToInt32(dataGridProducts.SelectedIndex);
            Options.indexprod = Convert.ToInt32((dataGridProducts.Columns[0].GetCellContent(dataGridProducts.Items[selindex]) as TextBlock).Text.ToString());
            EditProd EditproductsWin = new EditProd();
            EditproductsWin.ShowDialog();
            string datagridZapr1 = $"SELECT * FROM `product`";
            MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
            datagridAdpt.SelectCommand = SDG;
            DataTable DGT1 = new DataTable();
            datagridAdpt.Fill(DGT1);
            dataGridProducts.ItemsSource = DGT1.AsDataView();
        }

        private void Button_DeleteProd(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Вы действительно хотите удалить строку?";
            string caption = "Удаление";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                database.openConnection();
                int indexrow;
                indexrow = Convert.ToInt32(dataGridProducts.SelectedIndex);
                int vibrindex = Convert.ToInt32((dataGridProducts.Columns[0].GetCellContent(dataGridProducts.Items[indexrow]) as TextBlock).Text.ToString());
                MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
                string datagridZapr = $"DELETE FROM `product` WHERE `product`.`id_product`='{vibrindex}'";
                MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
                DGC.ExecuteNonQuery();
                datagridAdpt.SelectCommand = DGC;
                DataTable DGT = new DataTable();
                string datagridZapr1 = $"SELECT * FROM `product`";
                MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
                datagridAdpt.SelectCommand = SDG;
                DataTable DGT1 = new DataTable();
                datagridAdpt.Fill(DGT1);
                dataGridProducts.ItemsSource = DGT1.AsDataView();
                database.closeConnection();
            }
        }
    }
}
