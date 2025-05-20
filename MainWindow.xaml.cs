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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace modulnik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();

        private void Button_Click_Auth(object sender, RoutedEventArgs e)
        {
            string login = TextBoxLogin.Text.Trim().ToLower();
            string password = TextBoxPassword.Password.Trim();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            string zapros = $"select login, password from `users` where login  ='{login}' and password='{password}'";
            MySqlCommand command = new MySqlCommand(zapros, database.getConnection());
            adapter.SelectCommand = command;
            adapter.Fill(table);
            if (table.Rows.Count == 1)
            {
                string idcl = $"select id_user from `users` where login = '{login}'";
                MySqlCommand getid = new MySqlCommand(idcl, database.getConnection());
                Options.idclient = table.Rows[0][0];
                orders priloja = new orders();
                this.Close();
                priloja.ShowDialog();
            }
            else if (table.Rows.Count != 1)
            {
                checkpass.Visibility = Visibility;
            }
        }
    }
}
