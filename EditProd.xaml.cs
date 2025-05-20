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
    /// Логика взаимодействия для EditProd.xaml
    /// </summary>
    public partial class EditProd : Window
    {
        public EditProd()
        {
            InitializeComponent();
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT * FROM `product` where id_product ='{Options.indexprod}'";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            object typepr = DGT.Rows[0][1];
            object namepr = DGT.Rows[0][2];
            object descriptionpr = DGT.Rows[0][3];
            type_product.Text = typepr.ToString();
            name_product.Text = namepr.ToString();
            description_product.Text = descriptionpr.ToString();
        }
        DataBase database = new DataBase();

        private void Button_EditProd(object sender, RoutedEventArgs e)
        {
            string typepr = type_product.Text;
            string namepr = name_product.Text;
            string descriptionpr = description_product.Text;
            try
            {
                database.openConnection();
                var RedactQuery = $"UPDATE `product` SET type_product='{typepr}', name_product='{namepr}', description_product='{descriptionpr}' WHERE id_product = '{Options.indexprod}'";
                var command = new MySqlCommand(RedactQuery, database.getConnection());
                command.ExecuteNonQuery();
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Неверный тип данных");
            }
        }

        private void Button_CancelProd(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
