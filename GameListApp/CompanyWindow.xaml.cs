using System;
using System.Collections.Generic;
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

namespace GameListApp
{
    /// <summary>
    /// Логика взаимодействия для CompanyWindow.xaml
    /// </summary>
    public partial class CompanyWindow : Window
    {
        GameContext db;
        Company selectedCompany;

        public CompanyWindow()
        {
            InitializeComponent();

            db = new GameContext();

            UpdateListOfCompanies(db);

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;

            db.SaveChanges();
            db.Dispose();
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            db.Companies.Add(new Company { Name = "Новая компания" });
            db.SaveChanges();

            UpdateListOfCompanies(db);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var company = (string)ListOfCompanies.SelectedItem;

            if (company != null)
            {
                db.Companies.Remove(db.Companies.FirstOrDefault(c => c.Name == company));
                db.SaveChanges();

                UpdateListOfCompanies(db);
            }
            else
            {
                MessageBox.Show("Выберите компанию из списка");
            }
        }

        private void ListOfCompanies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Name.IsEnabled = true;
            DateOfFoundation.IsEnabled = true;
            Information.IsEnabled = true;

            var item = (string)ListOfCompanies.SelectedItem;
            selectedCompany = db.Companies.FirstOrDefault(c => c.Name == item);

            Name.Text = selectedCompany.Name;
            DateOfFoundation.Text = selectedCompany.DateOfFoundation;
            Information.Text = selectedCompany.Information;
        }

        private void UpdateListOfCompanies(GameContext db)
        {
            var companies = db.Companies.Select(c => c.Name).ToList();

            ListOfCompanies.ItemsSource = companies;
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedCompany.Name = Name.Text;
        }

        private void DateOfFoundation_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedCompany.DateOfFoundation = DateOfFoundation.Text;
        }

        private void Information_TextChanged(object sender, TextChangedEventArgs e)
        {
            selectedCompany.Information = Information.Text;
        }
    }
}
