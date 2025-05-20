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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Window
    {
        public EditClient()
        {
            InitializeComponent();
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT * FROM `client` where id_client ='{Options.indexclient}'";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            object surnamecl = DGT.Rows[0][2];
            object namecl = DGT.Rows[0][3];
            object panthonimiccl = DGT.Rows[0][4];
            object mob_num = DGT.Rows[0][5];
            object e_mail = DGT.Rows[0][6];
            surname.Text = surnamecl.ToString();
            name.Text = namecl.ToString();
            panthonimic.Text = panthonimiccl.ToString();
            tel_num.Text = mob_num.ToString();
            email.Text= e_mail.ToString();
        }
        DataBase database = new DataBase();

        private void Button_EditClient(object sender, RoutedEventArgs e)
        {
           string surnamecl = surname.Text;
           string namecl = name.Text;
           string panthonimiccl = panthonimic.Text;
           string mob_num = tel_num.Text;
           string e_mail = email.Text;
            try
            {
                database.openConnection();
            var RedactQuery = $"UPDATE `client` SET `surname` = '{surnamecl}', `name` = '{namecl}', `pathonimic` = '{panthonimiccl}', `tel_num` = '{mob_num}', `email` = '{e_mail}' WHERE `client`.`id_client` = {Options.indexclient};";
            var command = new MySqlCommand(RedactQuery, database.getConnection());
                command.ExecuteNonQuery();
                this.Close();
        }
            catch
            {
                MessageBox.Show("Неверный тип данных");
            }
}

        private void Button_CancelClient(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
