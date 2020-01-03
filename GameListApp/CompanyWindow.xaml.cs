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
        public CompanyWindow()
        {
            InitializeComponent();

            using (GameContext db = new GameContext())
            {
                UpdateListOfCompanies(db);
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            using (GameContext db = new GameContext())
            {
                db.Companies.Add(new Company { Name = "Новая компания" });
                db.SaveChanges();

                UpdateListOfCompanies(db);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var company = (string)ListOfCompanies.SelectedItem;

            if (company != null)
            {
                using (GameContext db = new GameContext())
                {
                    db.Companies.Remove(db.Companies.FirstOrDefault(c => c.Name == company));
                    db.SaveChanges();

                    UpdateListOfCompanies(db);
                }
            }
            else
            {
                MessageBox.Show("Выберите компанию из списка");
            }
        }

        private void UpdateListOfCompanies(GameContext db)
        {
            var companies = db.Companies.Select(c => c.Name).ToList();

            ListOfCompanies.ItemsSource = companies;
        }
    }
}
