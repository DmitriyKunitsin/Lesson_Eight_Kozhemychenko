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
    public partial class CityForm : Form
    {
        public CityForm()
        {
            InitializeComponent();
        }

        private void CityForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK) try
                {
                    if (nameTB.Text.Trim() == "")
                    {
                        nameTB.Focus();
                        throw new Exception("Не введено наименование города!");
                    }
                    if (regionTB.Text.Trim() == "")
                    {
                        regionTB.Focus();
                        throw new Exception("Не введено название области!");
                    }
                    try
                    {
                        if (populationTB.Text.Trim() != "")
                        {
                            int population = Convert.ToInt32(populationTB.Text);
                            if (population < 0)
                            {
                                populationTB.Focus();
                                throw new Exception("Численность населения не может быть меньше нуля!");
                            }
                        }
                        else
                        {
                            populationTB.Focus();
                            throw new Exception("Не указана численность населения!");
                        }
                    }
                    catch (FormatException)
                    {
                        populationTB.Focus();
                        throw new FormatException("Некорректная численность населения (численность может быть задана только целым неотрицательным числом)");
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
