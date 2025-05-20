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
    /// Логика взаимодействия для Clients.xaml
    /// </summary>
    public partial class Clients : Window
    {
        public Clients()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();

        private void dataGridClient_Initialized(object sender, EventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT * FROM `client`";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            dataGridClient.ItemsSource = DGT.AsDataView();
        }

        private void Button_Click_Find(object sender, RoutedEventArgs e)
        {
            if(find_surname.Text == "")
            {
                MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
                string datagridZapr = $"SELECT * FROM `client`";
                MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
                datagridAdpt.SelectCommand = DGC;
                DataTable DGT = new DataTable();
                datagridAdpt.Fill(DGT);
                dataGridClient.ItemsSource = DGT.AsDataView();
            }
            else
            {
                string datagridzapr = $"select * from `client` where surname='{find_surname.Text}'";
                MySqlCommand cmd = new MySqlCommand(datagridzapr, database.getConnection());
                DataTable dt = new DataTable();
                database.openConnection();
                MySqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                database.closeConnection();
                dataGridClient.ItemsSource = dt.DefaultView;
            }
        }

        private void Button_EditClient(object sender, RoutedEventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            int selindex = Convert.ToInt32(dataGridClient.SelectedIndex);
            Options.indexclient = Convert.ToInt32((dataGridClient.Columns[0].GetCellContent(dataGridClient.Items[selindex]) as TextBlock).Text.ToString());
            EditClient editcl = new EditClient();
            editcl.ShowDialog();
            string datagridZapr1 = $"SELECT * FROM `client`";
            MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
            datagridAdpt.SelectCommand = SDG;
            DataTable DGT1 = new DataTable();
            datagridAdpt.Fill(DGT1);
            dataGridClient.ItemsSource = DGT1.AsDataView();
        }

        private void Button_ClickDelete(object sender, RoutedEventArgs e)
        {
            try
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
                    indexrow = Convert.ToInt32(dataGridClient.SelectedIndex);
                    int vibrindex = Convert.ToInt32((dataGridClient.Columns[0].GetCellContent(dataGridClient.Items[indexrow]) as TextBlock).Text.ToString());
                    MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
                    string datagridZapr = $"DELETE FROM `client` WHERE `client`.`id_client`='{vibrindex}'";
                    MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
                    DGC.ExecuteNonQuery();
                    datagridAdpt.SelectCommand = DGC;
                    DataTable DGT = new DataTable();
                    string datagridZapr1 = $"select * from `client`";
                    MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
                    datagridAdpt.SelectCommand = SDG;
                    DataTable DGT1 = new DataTable();
                    datagridAdpt.Fill(DGT1);
                    dataGridClient.ItemsSource = DGT1.AsDataView();
                    database.closeConnection();
                }
            }
            catch
            {
                MessageBox.Show("Невозможно удалить клиента");
            }
        }
    }
}
