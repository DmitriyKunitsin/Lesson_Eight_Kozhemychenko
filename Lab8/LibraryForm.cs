using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programming
{
    public partial class LibraryForm : Form
    {
        public LibraryForm()
        {
            InitializeComponent();
        }

        private void LibraryForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK) try
                {
                    if (nameTB.Text.Trim() == "")
                    {
                        nameTB.Focus();
                        throw new Exception("Не введен адрес библиотеки!");
                    }
                    try
                    {
                        if (bookQuantityTB.Text.Trim() != "")
                        {
                            int quantity = Convert.ToInt32(bookQuantityTB.Text);
                            if (quantity < 0)
                            {
                                bookQuantityTB.Focus();
                                throw new Exception("Количество книг не может быть меньше 0!");
                            }
                        }
                        else
                        {
                            bookQuantityTB.Focus();
                            throw new Exception("Не указано количество книг!");
                        }
                    }
                    catch (FormatException)
                    {
                        bookQuantityTB.Focus();
                        throw new FormatException("Некорректное количество книг (количество книг может быть задано только целым неотрицательным числом)");
                    }
                }
                catch (Exception ex)
                {
                    e.Cancel = true;
                    MessageBox.Show(ex.Message + "\nПовторите ввод или нажмите на кнопку \"Отмена\"", "Ошибка!");
                }
        }
    }
}
