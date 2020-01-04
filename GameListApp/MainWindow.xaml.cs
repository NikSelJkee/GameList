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
            if (ListOfGames.SelectedItem != null)
            {
                int? id = ListOfGames.SelectedItem.GetType().GetProperty("Id").GetValue(ListOfGames.SelectedItem, null) as int?;

                Game game = db.Games.SingleOrDefault(g => g.Id == id);
                db.Games.Remove(game);
                db.SaveChanges();

                UpdateListOfGames();
            }
        }

        private void ListOfCompanies_Click(object sender, RoutedEventArgs e)
        {
            CompanyWindow companyWindow = new CompanyWindow();

            companyWindow.ShowDialog();
        }

        private void ListOfGames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            string title = ListOfGames.SelectedItem.GetType().GetProperty("Title").GetValue(ListOfGames.SelectedItem, null) as string;
            int? price = ListOfGames.SelectedItem.GetType().GetProperty("Price").GetValue(ListOfGames.SelectedItem, null) as int?;
            string company = ListOfGames.SelectedItem.GetType().GetProperty("Company").GetValue(ListOfGames.SelectedItem, null) as string;

            gameWindow.Title.Text = title;
            gameWindow.Price.Text = price.ToString();
            gameWindow.Company.SelectedItem = company;

            if (gameWindow.ShowDialog() == true)
            {
                UpdateListOfGames();
            }
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
