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
    /// Логика взаимодействия для AddProd.xaml
    /// </summary>
    public partial class AddProd : Window
    {
        public AddProd()
        {
            InitializeComponent();
        }

        DataBase database = new DataBase();
        private void Button_AddProd(object sender, RoutedEventArgs e)
        {
            try
            {
                database.openConnection();
                MySqlCommand addproduct = new MySqlCommand($"insert into `product` (type_product, name_product, description_product) values (@type_product, @name_product, @description_product)", database.getConnection());
                addproduct.Parameters.AddWithValue("type_product", type_product.Text);
                addproduct.Parameters.AddWithValue("name_product", name_product.Text);
                addproduct.Parameters.AddWithValue("description_product", description_product.Text);
                addproduct.ExecuteNonQuery();
                this.Close();
            }   
            catch
            {
                MessageBox.Show("Неверный тип данных");
                database.closeConnection();
            }
        }

        private void Button_CancelProd(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
