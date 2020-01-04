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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameListApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameContext db;

        public MainWindow()
        {
            InitializeComponent();

            db = new GameContext();

            UpdateListOfGames();
        }

        private void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();

            if (gameWindow.ShowDialog() == true)
            {
                UpdateListOfGames();
            }
        }

        private void DeleteGameButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListOfCompanies_Click(object sender, RoutedEventArgs e)
        {
            CompanyWindow companyWindow = new CompanyWindow();

            companyWindow.ShowDialog();
        }

        private void UpdateListOfGames()
        {
            ListOfGames.ItemsSource = db.Games.Select(g => new
            { Id = g.Id,
                Title = g.Title,
                Price = g.Price,
                Company = g.Company.Name }).ToList();
        }
    }
}
