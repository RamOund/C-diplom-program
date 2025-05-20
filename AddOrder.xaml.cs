using MySql.Data.MySqlClient;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace modulnik
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();

        private void Button_AddOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                database.openConnection();
                MySqlCommand addorder = new MySqlCommand("insert into `orders` (id_client, id_employee, address_client, date_ad, state) values (@id_client, @id_employee, @address_client, NOW(), 'Новый'); SELECT LAST_INSERT_ID();", database.getConnection());
                addorder.Parameters.AddWithValue("id_client", id_client.Text);
                addorder.Parameters.AddWithValue("id_employee", id_employee.Text);
                addorder.Parameters.AddWithValue("address_client", address_client.Text);
                int id_order = Convert.ToInt32(addorder.ExecuteScalar());
                MySqlCommand addorderlist = new MySqlCommand($"insert into `order_list` (id_order, id_product, amount) values ({id_order}, @id_product, @amount)", database.getConnection());
                addorderlist.Parameters.AddWithValue("id_product", id_product.Text);
                addorderlist.Parameters.AddWithValue("amount", amount.Text);
                addorderlist.ExecuteNonQuery();
                this.Close();
                //Исправить ошибку с дробными числами
            }
            catch
            {
                MessageBox.Show("Неверный тип данных и/или номер");
                database.closeConnection();
            }
        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
