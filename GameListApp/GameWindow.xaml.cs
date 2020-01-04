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
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        GameContext db;

        public GameWindow()
        {
            InitializeComponent();

            db = new GameContext();

            var companies = db.Companies.Select(p => p.Name).ToList();
            Company.ItemsSource = companies;
        }

        private void AcceptGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Game game = new Game();
                game.Title = Title.Text;
                game.Price = int.Parse(Price.Text);
                game.Company = db.Companies.SingleOrDefault(c => c.Name == Company.SelectedItem.ToString());

                db.Games.Add(game);
                db.SaveChanges();
                db.Dispose();

                DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Проверьте корректность ввода данных");
            }
        }

        private void CancelGameButton_Click(object sender, RoutedEventArgs e)
        {
            db.Dispose();

            DialogResult = false;
        }
    }
}
