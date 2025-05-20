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
    /// Логика взаимодействия для EditOrders.xaml
    /// </summary>
    public partial class EditOrders : Window
    {
        DataBase database = new DataBase();
        public EditOrders()
        {
            InitializeComponent();
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr1 = $"SELECT * FROM `orders` where id_order ='{Options.indexord}'";
            MySqlCommand DGC1 = new MySqlCommand(datagridZapr1, database.getConnection());
            datagridAdpt.SelectCommand = DGC1;
            DataTable DGT1 = new DataTable();
            datagridAdpt.Fill(DGT1);
            string datagridZapr2 = $"SELECT * FROM `order_list` where id_order ='{Options.indexord}'";
            MySqlCommand DGC2 = new MySqlCommand(datagridZapr2, database.getConnection());
            datagridAdpt.SelectCommand = DGC2;
            DataTable DGT2 = new DataTable();
            datagridAdpt.Fill(DGT2);
            object address= DGT1.Rows[0][3];
            object status = DGT1.Rows[0][5];
            object numprod = DGT2.Rows[0][2];
            object am = DGT2.Rows[0][3];
            string datagridZapr3 = $"select name_product from `product` where id_product = '{numprod}'";
            MySqlCommand getnamepr = new MySqlCommand(datagridZapr3, database.getConnection());
            datagridAdpt.SelectCommand=getnamepr;
            DataTable DGT3 = new DataTable();
            datagridAdpt.Fill(DGT3);
            object nameprod = DGT3.Rows[0][0];
            address_client.Text = address.ToString();
            id_product.Text = nameprod.ToString();
            amount.Text = am.ToString();
            state.Text = status.ToString();
        }

        private void Button_EditOrder(object sender, RoutedEventArgs e)
        {
            string address = Convert.ToString(address_client.Text);
            string id_products = Convert.ToString(id_product.Text);
            string am = Convert.ToString(amount.Text);
            string status = Convert.ToString(state.Text);
            try
            {
                MySqlDataAdapter checkadapt = new MySqlDataAdapter();
                string checkprod = $"select id_product, name_product from `product` where name_product = '{id_products}'";
                MySqlCommand check = new MySqlCommand(checkprod, database.getConnection());
                DataTable checkT = new DataTable();
                checkadapt.SelectCommand = check;
                checkadapt.Fill(checkT);
                object getidprod = checkT.Rows[0][0];
                if (checkT.Rows.Count == 1)
                {
                    database.openConnection();
                    var RedactQuery1 = $"UPDATE `orders` SET address_client='{address}', state='{status}' WHERE id_order = '{Options.indexord}'";
                    var command1 = new MySqlCommand(RedactQuery1, database.getConnection());
                    command1.ExecuteNonQuery();
                    var RedactQuery2 = $"UPDATE `order_list` SET id_product ='{getidprod}', amount='{am}' WHERE id_order = '{Options.indexord}'";
                    var command2 = new MySqlCommand(RedactQuery2, database.getConnection());
                    command2.ExecuteNonQuery();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Такого товара в базе не найдено");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Неправильный тип данных");
            }
        }

        private void Button_CancelEditWindow2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
