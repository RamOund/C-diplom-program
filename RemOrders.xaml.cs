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
    /// Логика взаимодействия для RemOrders.xaml
    /// </summary>
    public partial class RemOrders : Window
    {
        public RemOrders()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();

        private void dataGrid_RemOrders_Initialized(object sender, EventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT remote_orders.id_remote_order, client.surname, employees.surname_employees, remote_orders.address_client, product.name_product, remote_orders.rem_date, remote_orders.re_reason FROM remote_orders INNER JOIN client ON client.id_client = remote_orders.id_client INNER JOIN employees ON employees.id_employee = remote_orders.id_employee INNER JOIN product ON product.id_product = remote_orders.id_product";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            dataGrid_RemOrders.ItemsSource = DGT.AsDataView();
        }
    }
}
