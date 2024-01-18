namespace Programming
{
    public partial class MainForm : Form
    {
        List<TCity> cities = new List<TCity>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (catalogTree.SelectedNode != null)
            {
                if (catalogTree.SelectedNode.Level == 0)
                {
                    CityForm cf = new CityForm();

                    if (cf.ShowDialog() == DialogResult.OK)
                    {
                        TCity city = new TCity();
                        city.Name = cf.nameTB.Text;
                        city.RegionName = cf.regionTB.Text;
                        city.Population = Convert.ToInt32(cf.populationTB.Text);
                        cities.Add(city);

                        TreeNode tempNode = catalogTree.SelectedNode.Nodes.Add(city.Name);
                        tempNode.Nodes.Add($"Область: {city.RegionName}");
                        tempNode.Nodes.Add($"Число жителей: {city.Population}");
                        tempNode.Nodes.Add("Список библиотек");
                    }
                }
                else if (catalogTree.SelectedNode.Level == 2 && catalogTree.SelectedNode.Index == 2)
                {
                    LibraryForm lf = new LibraryForm();

                    if (lf.ShowDialog() == DialogResult.OK)
                    {
                        TLibrary library = new TLibrary();
                        library.Name = lf.nameTB.Text;
                        library.Adress = lf.adressTB.Text;
                        library.BooksQuantity = Convert.ToInt32(lf.bookQuantityTB.Text);
                        TCity city = cities.ElementAt(catalogTree.SelectedNode.Parent.Index);
                        city.Libraries.Add(library);

                        TreeNode tempNode = catalogTree.SelectedNode.Nodes.Add(library.Name);
                        tempNode.Nodes.Add($"Адрес: {library.Adress}");
                        tempNode.Nodes.Add($"Всего книг: {library.BooksQuantity}");
                    }
                }
            }
        }

        private void catalogTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TCity city;
            TLibrary library;

            switch (catalogTree.SelectedNode.Level)
            {
                case 0:
                    infoTB.Text = "Это корень дерева";
                    buttonAdd.Enabled = true;
                    buttonModify.Enabled = false;
                    buttonRemove.Enabled = false;
                    break;
                case 1:
                    city = cities.ElementAt(catalogTree.SelectedNode.Index);
                    infoTB.Text = $"Всего библиотек: {city.Libraries.Count}";
                    buttonAdd.Enabled = false;
                    buttonModify.Enabled = true;
                    buttonRemove.Enabled = true;
                    break;
                case 2:
                    city = cities.ElementAt(catalogTree.SelectedNode.Parent.Index);
                    infoTB.Text = $"Город: {city.Name}";
                    if (catalogTree.SelectedNode.Index == 2)
                    {
                        buttonAdd.Enabled = true;
                    }
                    else
                    {
                        buttonAdd.Enabled = false;
                    }
                    buttonModify.Enabled = false;
                    buttonRemove.Enabled = false;
                    break;
                case 3:
                    city = cities.ElementAt(catalogTree.SelectedNode.Parent.Parent.Index);
                    library = city.Libraries.ElementAt(catalogTree.SelectedNode.Index);
                    infoTB.Text = $"Адрес: {library.Adress}";
                    buttonAdd.Enabled = false;
                    buttonModify.Enabled = true;
                    buttonRemove.Enabled = true;
                    break;
                case 4:
                    city = cities.ElementAt(catalogTree.SelectedNode.Parent.Parent.Parent.Index);
                    library = city.Libraries.ElementAt(catalogTree.SelectedNode.Parent.Index);
                    infoTB.Text = $"Библиотека: {library.Name}";
                    buttonAdd.Enabled = false;
                    buttonModify.Enabled = false;
                    buttonRemove.Enabled = false;
                    break;
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            TCity city;
            TLibrary library;

            switch (catalogTree.SelectedNode.Level)
            {
                case 1:
                    city = cities.ElementAt(catalogTree.SelectedNode.Index);

                    CityForm cf = new CityForm();
                    cf.nameTB.Text = city.Name;
                    cf.regionTB.Text = city.RegionName;
                    cf.populationTB.Text = city.Population.ToString();

                    if (cf.ShowDialog() == DialogResult.OK)
                    {
                        cities[catalogTree.SelectedNode.Index].Name = cf.nameTB.Text;
                        cities[catalogTree.SelectedNode.Index].RegionName = cf.regionTB.Text;
                        cities[catalogTree.SelectedNode.Index].Population = Convert.ToInt32(cf.populationTB.Text);

                        catalogTree.SelectedNode.Text = cf.nameTB.Text;
                        catalogTree.SelectedNode.Nodes[0].Text = $"Область: {cf.regionTB.Text}";
                        catalogTree.SelectedNode.Nodes[1].Text = $"Число жителей: {cf.populationTB.Text}";
                    }
                    break;
                case 3:
                    int cityIndex = catalogTree.SelectedNode.Parent.Parent.Index;
                    int libraryIndex = catalogTree.SelectedNode.Index;

                    city = cities.ElementAt(cityIndex);
                    library = city.Libraries.ElementAt(libraryIndex);

                    LibraryForm lf = new LibraryForm();
                    lf.nameTB.Text = library.Name;
                    lf.adressTB.Text = library.Adress;
                    lf.bookQuantityTB.Text = library.BooksQuantity.ToString();

                    if (lf.ShowDialog() == DialogResult.OK)
                    {
                        cities[cityIndex].Libraries[libraryIndex].Name = lf.nameTB.Text;
                        cities[cityIndex].Libraries[libraryIndex].Adress = lf.adressTB.Text;
                        cities[cityIndex].Libraries[libraryIndex].BooksQuantity = Convert.ToInt32(lf.bookQuantityTB.Text);

                        catalogTree.SelectedNode.Text = lf.nameTB.Text;
                        catalogTree.SelectedNode.Nodes[0].Text = $"Адрес: {lf.adressTB.Text}";
                        catalogTree.SelectedNode.Nodes[1].Text = $"Всего книг: {lf.bookQuantityTB.Text}";
                    }
                    break;
                default:
                    MessageBox.Show("Выберите город или библиотеку для изменения!", "Информация");
                    break;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            switch (catalogTree.SelectedNode.Level)
            {
                case 1:
                    cities.RemoveAt(catalogTree.SelectedNode.Index);
                    catalogTree.SelectedNode.Remove();
                    break;
                case 3:
                    TCity city = cities.ElementAt(catalogTree.SelectedNode.Parent.Parent.Index);
                    city.Libraries.RemoveAt(catalogTree.SelectedNode.Index);
                    catalogTree.SelectedNode.Remove();
                    break;
                default:
                    MessageBox.Show("Выберите город или библиотеку для удаления!", "Информация");
                    break;
            }
        }
    }
}