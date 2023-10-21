using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;



namespace Manually_Throwing
{
    public partial class Inventory : Form
    {
        private string _ProductName;
        private string _Category;
        private string _MfgDate;
        private string _ExpDate;
        private string _Description;
        private int _Quantity;
        private BindingSource showProductList = new BindingSource();
        public double _SellPrice { get; private set; }

        public Inventory()
        {
            InitializeComponent();
        }

        public string Product_Name(string name)
        {
            if (!Regex.IsMatch(name, @"^[a-zA-Z]+$"))
                throw new NumberFormatException("Number Format Exception");
            return name;
        }
        public int Quantity(string qty)
        {
            if (!Regex.IsMatch(qty, @"^[0-9]"))
                throw new StringFormatException("String Format Exception");
            return Convert.ToInt32(qty);
        }

        public double SellingPrice(string price)
        {
            if (!Regex.IsMatch(price.ToString(), @"^(\d*\.)?\d+$"))
                throw new CurrencyFormatException("Currency Format Exception");
            return Convert.ToDouble(price);
        }

        class NumberFormatException : Exception
        {
            public NumberFormatException(string numberException) : base(numberException) { }
        }

        class StringFormatException : Exception
        {
            public StringFormatException(string stringException) : base(stringException) { }
        }

        class CurrencyFormatException : Exception
        {
            public CurrencyFormatException(string currencyException) : base(currencyException) { }
        }

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                _ProductName = Product_Name(txtProductName.Text);
                _Category = cbCategory.Text;
                _MfgDate = dtPickerMfgDate.Value.ToString("yyyy-MM-dd");
                _ExpDate = dtPickerExpDate.Value.ToString("yyyy-MM-dd");
                _Description = richTxtDescription.Text;
                _Quantity = Quantity(txtQuantity.Text);
                _SellPrice = SellingPrice(txtSellPrice.Text);
                showProductList.Add(new ProductClass(_ProductName, _Category, _MfgDate, _ExpDate, _SellPrice, _Quantity, _Description));
                gridViewProductList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                gridViewProductList.DataSource = showProductList;
            }
            catch (NumberFormatException ex)
            {
                MessageBox.Show(ex.Message, "Number Format Error");
            }
            catch (StringFormatException ex)
            {
                MessageBox.Show(ex.Message, "String Format Error");
            }
            catch (CurrencyFormatException ex)
            {
                MessageBox.Show(ex.Message, "Currency Format Error");
            }
           
        }

        private void Inventory_Load(object sender, EventArgs e)
        {
            string[] ListofCategory = new String[]
            {
                "Berevages",
                "Bread/Bakery",
                "Canned/Jared Goods",
                "Dairy",
                "Frozen Goods",
                "Meat",
                "Personal Care",
                "Other",
            };

            for (int i = 0; i<8; i++)
            {
                cbCategory.Items.Add(ListofCategory[i].ToString());
            }
        }
    }
}
