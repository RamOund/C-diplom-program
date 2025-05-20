using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace modulnik
{
    /// <summary>
    /// Логика взаимодействия для orders.xaml
    /// </summary>
    public partial class orders : Window
    {
        public orders()
        {
            InitializeComponent();
        }
        DataBase database = new DataBase();

        private void dataGrid1_Initialized(object sender, EventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT orders.id_order, client.surname, employees.surname_employees, orders.address_client, product.name_product, order_list.amount, orders.date_ad, orders.state FROM orders INNER JOIN order_list ON orders.id_order = order_list.id_order INNER JOIN client ON client.id_client = orders.id_client INNER JOIN employees ON employees.id_employee = orders.id_employee INNER JOIN product ON product.id_product = order_list.id_product";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            dataGrid1.ItemsSource = DGT.AsDataView();
        }

        //private void Button_Add(object sender, RoutedEventArgs e)
        //{
        //    MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
        //    AddOrder windowadd = new AddOrder();
        //    windowadd.ShowDialog();
        //    string datagridZapr1 = $"SELECT orders.id_order, orders.id_client, orders.id_employee, orders.address_client, order_list.id_product, order_list.amount, orders.date_ad, orders.state FROM orders INNER JOIN order_list ON orders.id_order = order_list.id_order;";
        //    MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
        //    datagridAdpt.SelectCommand = SDG;
        //    DataTable DGT1 = new DataTable();
        //    datagridAdpt.Fill(DGT1);
        //    dataGrid1.ItemsSource = DGT1.AsDataView();
        //    database.closeConnection();
        //}

        private void Button_Click_Clients(object sender, RoutedEventArgs e)
        {
            Clients ClientWindow = new Clients();
            ClientWindow.ShowDialog();
        }

        private void Button_Click_Products(object sender, RoutedEventArgs e)
        {
            Products ProductWindow = new Products();
            ProductWindow.ShowDialog();
        }

        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            int selindex = Convert.ToInt32(dataGrid1.SelectedIndex);
            Options.indexord = Convert.ToInt32((dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[selindex]) as TextBlock).Text.ToString());
            EditOrders EditOrdersWindow = new EditOrders();
            EditOrdersWindow.ShowDialog();
            string datagridZapr1 = $"SELECT orders.id_order, client.surname, employees.surname_employees, orders.address_client, product.name_product, order_list.amount, orders.date_ad, orders.state FROM orders INNER JOIN order_list ON orders.id_order = order_list.id_order INNER JOIN client ON client.id_client = orders.id_client INNER JOIN employees ON employees.id_employee = orders.id_employee INNER JOIN product ON product.id_product = order_list.id_product;";
            MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
            datagridAdpt.SelectCommand = SDG;
            DataTable DGT1 = new DataTable();
            datagridAdpt.Fill(DGT1);
            dataGrid1.ItemsSource = DGT1.AsDataView();
        }

        private void Button_DeleteRow(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Вы действительно хотите удалить строку?";
            string caption = "Удаление";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Question;
            MessageBoxResult result;
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                ReasRem ReasonToRemote = new ReasRem();
                ReasonToRemote.ShowDialog();
                database.openConnection();
                int indexrow;
                indexrow = Convert.ToInt32(dataGrid1.SelectedIndex);
                int vibrindex = Convert.ToInt32((dataGrid1.Columns[0].GetCellContent(dataGrid1.Items[indexrow]) as TextBlock).Text.ToString());

                MySqlDataAdapter datagridAdptRem = new MySqlDataAdapter();
                string datagridZaprRem = $"SELECT * FROM `orders` where id_order ='{vibrindex}'";
                MySqlCommand DGC1 = new MySqlCommand(datagridZaprRem, database.getConnection());
                datagridAdptRem.SelectCommand = DGC1;
                DataTable DGTRem = new DataTable();
                datagridAdptRem.Fill(DGTRem);
                string datagridZapr2 = $"SELECT * FROM `order_list` where id_order ='{vibrindex}'";
                MySqlCommand DGC2 = new MySqlCommand(datagridZapr2, database.getConnection());
                datagridAdptRem.SelectCommand = DGC2;
                DataTable DGT2 = new DataTable();
                datagridAdptRem.Fill(DGT2);
                object clientnum = DGTRem.Rows[0]["id_client"];
                object employeenum = Options.idclient;
                object address = DGTRem.Rows[0]["address_client"];
                object numprod = DGT2.Rows[0]["id_product"];
                string insertremrable = $"insert into `remote_orders` (id_client, id_employee, address_client, id_product, rem_date, re_reason) values ('{clientnum}', '{employeenum}', '{address}', '{numprod}', NOW(), '{Options.resremote}')";
                MySqlCommand insertremtablecom = new MySqlCommand(insertremrable, database.getConnection());
                insertremtablecom.ExecuteNonQuery();

                MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
                string datagridZapr = $"DELETE FROM `order_list` WHERE `order_list`.`id_order_list`='{vibrindex}';" + $"DELETE FROM `orders` WHERE `orders`.`id_order` = {vibrindex}" ;
                MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
                DGC.ExecuteNonQuery();
                datagridAdpt.SelectCommand = DGC;
                string datagridZapr1 = $"SELECT orders.id_order, client.surname, employees.surname_employees, orders.address_client, product.name_product, order_list.amount, orders.date_ad, orders.state FROM orders INNER JOIN order_list ON orders.id_order = order_list.id_order INNER JOIN client ON client.id_client = orders.id_client INNER JOIN employees ON employees.id_employee = orders.id_employee INNER JOIN product ON product.id_product = order_list.id_product;";
                MySqlCommand SDG = new MySqlCommand(datagridZapr1, database.getConnection());
                datagridAdpt.SelectCommand = SDG;
                DataTable DGT1 = new DataTable();
                datagridAdpt.Fill(DGT1);
                dataGrid1.ItemsSource = DGT1.AsDataView();
                database.closeConnection();
            }
        }

        private void loginman_Initialized(object sender, EventArgs e)
        {
            object nedit = Options.idclient;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();
            string zapr = $"select surname_employees, name, pathonimic FROM `employees` where id_user = '{nedit}'";
            MySqlCommand getid = new MySqlCommand(zapr, database.getConnection());
            adapter.SelectCommand = getid;
            adapter.Fill(table);
            string surname = table.Rows[0]["surname_employees"].ToString();
            string name = table.Rows[0]["name"].ToString();
            string pathonimic = table.Rows[0]["pathonimic"].ToString();
            loginman.Content = $"Вы авторизировались как: {surname} {name} {pathonimic}";
        }

        private void Button_Click_Refresh(object sender, RoutedEventArgs e)
        {
            MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
            string datagridZapr = $"SELECT orders.id_order, client.surname, employees.surname_employees, orders.address_client, product.name_product, order_list.amount, orders.date_ad, orders.state FROM orders INNER JOIN order_list ON orders.id_order = order_list.id_order INNER JOIN client ON client.id_client = orders.id_client INNER JOIN employees ON employees.id_employee = orders.id_employee INNER JOIN product ON product.id_product = order_list.id_product;";
            MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
            datagridAdpt.SelectCommand = DGC;
            DataTable DGT = new DataTable();
            datagridAdpt.Fill(DGT);
            dataGrid1.ItemsSource = DGT.AsDataView();
        }

        private void Button_Rem_Click(object sender, RoutedEventArgs e)
        {
            RemOrders RemOrdersWin = new RemOrders();
            RemOrdersWin.ShowDialog();
        }

        private void GenerateReport()
        {
            Document doc = new Document(PageSize.A4.Rotate());


            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("Report.pdf", FileMode.Create));
            string fontPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ARIALUNI.TTF");
            BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font font = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
            doc.Open();


            Paragraph title = new Paragraph("Отчет о поступивших заказах", font);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);

            Paragraph title1 = new Paragraph("ЗАО НВП «Болид»", font);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title1);

            Paragraph title3 = new Paragraph("Адрес: Пионерская ул., 4, корп. 11, Королёв", font);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title3);
            DataTable dt = GetDataFromDatabase();

            Paragraph spacing = new Paragraph(" ");
            spacing.SpacingAfter = 20f;
            doc.Add(spacing);
            PdfPTable table = new PdfPTable(5);
            float[] columnWidths = new float[] { 10f, 10f, 10f, 10f, 10f };
            table.SetWidths(columnWidths);
            BaseFont tableBaseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
            Font tableFont = new Font(tableBaseFont, 12, Font.NORMAL, BaseColor.BLACK);

            PdfPCell idHeaderCell = new PdfPCell(new Phrase("Номер заказа", tableFont));
            table.AddCell(idHeaderCell);

            PdfPCell nameHeaderCell = new PdfPCell(new Phrase("Номер клиента", tableFont));
            table.AddCell(nameHeaderCell);

            PdfPCell testTimeHeaderCell = new PdfPCell(new Phrase("Выполнить по адресу", tableFont));
            table.AddCell(testTimeHeaderCell);

            PdfPCell testerHeaderCell = new PdfPCell(new Phrase("Дата поступления", tableFont));
            table.AddCell(testerHeaderCell);

            PdfPCell testResultHeaderCell = new PdfPCell(new Phrase("Статус", tableFont));
            table.AddCell(testResultHeaderCell);


            foreach (DataRow row in dt.Rows)
            {
                PdfPCell idCell = new PdfPCell(new Phrase(row["id_order"].ToString(), tableFont));
                table.AddCell(idCell);

                PdfPCell nameCell = new PdfPCell(new Phrase(row["id_client"].ToString(), tableFont));
                table.AddCell(nameCell);

                PdfPCell testTimeCell = new PdfPCell(new Phrase(row["address_client"].ToString(), tableFont));
                table.AddCell(testTimeCell);

                PdfPCell testerCell = new PdfPCell(new Phrase(row["date_ad"].ToString(), tableFont));
                table.AddCell(testerCell);

                PdfPCell testResultCell = new PdfPCell(new Phrase(row["state"].ToString(), tableFont));
                table.AddCell(testResultCell);
            }

            doc.Add(table);

            doc.Close();

            string pdfPath = "Report.pdf";
            System.Diagnostics.Process.Start(new ProcessStartInfo { FileName = pdfPath, UseShellExecute = true });

        }

        private DataTable GetDataFromDatabase()
        {
            DataTable dataTable = new DataTable();

            try
            {
                MySqlDataAdapter datagridAdpt = new MySqlDataAdapter();
                string datagridZapr = $"select * from `orders`";
                MySqlCommand DGC = new MySqlCommand(datagridZapr, database.getConnection());
                datagridAdpt.SelectCommand = DGC;
                datagridAdpt.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при получении данных из базы данных: " + ex.Message);
            }

            return dataTable;
        }

        private void Button_Click_Otchet(object sender, RoutedEventArgs e)
        {
            GenerateReport();
        }
    }
}
