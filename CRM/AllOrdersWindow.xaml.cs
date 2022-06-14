using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using Microsoft.Office.Interop.Word;
using System.Windows.Threading;
using System.Reflection;
using System.Windows.Input;

namespace CRM
{

    public partial class AllOrdersWindow : System.Windows.Window
    {
        string login, password;
        string pathToDocFile = "D:/projects/CRM/CRM/documents\\Отчёт.docx";
        public AllOrdersWindow()
        {
            InitializeComponent();
        }
        public AllOrdersWindow(string login, string password)
        {
            InitializeComponent();
            this.login = login;
            this.password = password;
        }
        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            PersonalOfficeWindow personalOffice = new PersonalOfficeWindow(login, password);
            personalOffice.Show();
            this.Close();
        }

        private void Authorization_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void Add_Order_Click(object sender, RoutedEventArgs e)
        {
            AddElement addElement = new AddElement();
            addElement.Show();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
             try
             {
                 RebindData();
                 SetTimer();
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.ToString());
             }
        }
        protected void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            RebindData();
        }
        private void RebindData()
        {
            OrdersdbEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(o => o.Reload());
            dgOrders.ItemsSource = OrdersdbEntities.GetContext().Orders.ToList();
        }
        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            ProductsWindow productsWindow = new ProductsWindow(login, password);
            productsWindow.Show();
            this.Close();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clients = new ClientsWindow(login, password);
            clients.Show();
            this.Close();
        }
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void Statistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsWindow statistics = new StatisticsWindow(login, password);
            statistics.Show();
            this.Close();
        }

        private void Delete_Order_Click(object sender, RoutedEventArgs e)
        {
            var ordersToRemove = dgOrders.SelectedItems.Cast<Orders>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следующие {ordersToRemove.Count()} элементов?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    OrdersdbEntities.GetContext().Orders.RemoveRange(ordersToRemove);
                    OrdersdbEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");

                    dgOrders.ItemsSource = OrdersdbEntities.GetContext().Orders.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Change_Order_Click(object sender, RoutedEventArgs e)
        {
            var orderToChange = dgOrders.SelectedItem;
            if (orderToChange != null)
            {
                EditElement changeOrder = new EditElement((Orders)orderToChange);
                changeOrder.Show();
            }
            else
            {
                MessageBox.Show("Выберите заказ, который хотите изменить");
            }
        }

        private void Create_File_Click(object sender, RoutedEventArgs e)
        {
            CreateDocument();
        }
        private string SubstringStr(string str)
        {
            int lenghtTo_T = str.IndexOf("\t");
            int lenghtTo_N = str.IndexOf("\n");
            string subStr = "";
            if (lenghtTo_T < lenghtTo_N)
            {
                subStr = str.Substring(0, lenghtTo_T);
            }
            else
            {
                subStr = str.Substring(0, lenghtTo_N);
            }
            return subStr;
        }
        private void CreateDocument()
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            DataGrid dg = dgOrders;
            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            string result = (string)Clipboard.GetData(DataFormats.Text);
            //Start Word and create a new document.
            Microsoft.Office.Interop.Word._Application oWord;
            Microsoft.Office.Interop.Word._Document oDoc;
            oWord = new Microsoft.Office.Interop.Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
            ref oMissing, ref oMissing);
            oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
            //Insert a paragraph at the beginning of the document.
            Microsoft.Office.Interop.Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Отчёт по заказам";
            oPara1.Range.Font.Bold = 1;
            oPara1.Range.Font.Size = 18;
            oPara1.Range.Font.Name = "Times New Roman";
            oPara1.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter; 
            oPara1.Format.SpaceAfter = 14;    //14 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert a paragraph at the end of the document.
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;

            //Insert another paragraph.
            Microsoft.Office.Interop.Word.Paragraph oPara3;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = "В данной таблице находятся сведения о заказах:";
            oPara3.Range.Font.Size = 14;
            oPara3.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            oPara3.Range.Font.Bold = 0;
            oPara3.Format.SpaceAfter = 24;
            oPara3.Range.InsertParagraphAfter();

            int strSize = dgOrders.Items.Count;
            int columnSize = 7;

            Microsoft.Office.Interop.Word.Table oTable;
            Microsoft.Office.Interop.Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, strSize, columnSize, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            oTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            oTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
            int r, c;
            string strText = result;
            oTable.Range.Font.Size = 13;
            for (r = 1; r <= strSize; r++)
                for (c = 1; c <= columnSize; c++)
                {
                    string subStringText = SubstringStr(strText);
                    strText = strText.Remove(0, subStringText.Length + 1);
                    oTable.Cell(r, c).Range.Text = subStringText;
                }
            oTable.Rows[1].Range.Font.Bold = 1;

            oDoc.SaveAs(pathToDocFile);
        }
    }
}
