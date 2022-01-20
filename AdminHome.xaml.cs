using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace All4Fit
{
    public partial class AdminHome : Window
    {
        private User _user; // obiekt przechowujący dane użytkownika

        public AdminHome()
        {
            InitializeComponent();
        }

        public void SetObject(User obj) // funkcja uzupełniająca dane użytkownika dostarczone z innych paneli 
        {
            _user = obj;
            updatePage();
        }

        private void updatePage()
        {
            deleteUserComboBox.SelectedIndex = -1;
            deleteUserComboBox.ItemsSource = File.ReadAllLines("usersLogins.txt"); // załadowanie listy użytkowników do comboboxa
            deleteUsertBtn.Content = "Wybierz użytkownika powyżej";
            deleteUsertBtn.IsEnabled = false;

            deleteTrainingComboBox.SelectedIndex = -1;
            deleteTrainingComboBox.ItemsSource = File.ReadAllLines("TrainingSystems/list_trainings.txt");  // załadowanie listy systemów trenigowych do comboboxa
            deleteTrainingBtn.Content = "Wybierz system powyżej";
            deleteTrainingBtn.IsEnabled = false;

            deleteDietComboBox.SelectedIndex = -1;
            deleteDietComboBox.ItemsSource = File.ReadAllLines("Diets/list_diets.txt"); // załadowanie listy diet do comboboxa
            deleteDietBtn.Content = "Wybierz dietę powyżej";
            deleteDietBtn.IsEnabled = false;
        }

        private void logoutUser(object sender, RoutedEventArgs e)
        {
            Logowanie l1 = new Logowanie();
            this.Close();
            l1.Show();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void enableChangeDeleteUsertBtn(object sender, SelectionChangedEventArgs e)
        {
            deleteUsertBtn.IsEnabled = true;
            deleteUsertBtn.Content = "Usuń użytkownika";
        }

        private void deleteUser(object sender, RoutedEventArgs e)
        {
            string userDetalits = "";
            string[] splitedUserDetalits;
            try
            {
                StreamReader openFile;
                openFile = File.OpenText("users.txt");
                while (!openFile.EndOfStream)
                {
                    userDetalits = openFile.ReadLine();
                    splitedUserDetalits = userDetalits.Split(',');
                    if (splitedUserDetalits[0] == (string)deleteUserComboBox.SelectedItem)
                    {
                        break;
                    }
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji deleteUser(). \n\nBłąd: " + ex.Message);
            }
            string[] linesList = File.ReadAllLines("users.txt");

            for (int i = 0; i < linesList.Length; i++)
            {
                if (linesList[i] == userDetalits)
                {
                    linesList = linesList.Where(w => w != linesList[i]).ToArray();
                }
            }
            File.WriteAllLines("users.txt", linesList);

            linesList = File.ReadAllLines("usersLogins.txt");

            for (int i = 0; i < linesList.Length; i++)
            {
                if (linesList[i] == (string)deleteUserComboBox.SelectedItem)
                {
                    linesList = linesList.Where(w => w != linesList[i]).ToArray();
                }
            }
            File.WriteAllLines("usersLogins.txt", linesList);

            MessageBox.Show("Udało ci się usunąć użytkownika!");
            updatePage();

        }

        private void deleteTraining(object sender, RoutedEventArgs e)
        {

            string[] linesList = File.ReadAllLines("TrainingSystems/list_trainings.txt");

            for (int i = 0; i < linesList.Length; i++)
            {
                if (linesList[i] == (string)deleteTrainingComboBox.SelectedItem)
                {
                    linesList = linesList.Where(w => w != linesList[i]).ToArray();
                }
            }
            File.WriteAllLines("TrainingSystems/list_trainings.txt", linesList);


            File.Delete("TrainingSystems/" + (string)deleteTrainingComboBox.SelectedItem + ".txt");

            MessageBox.Show("Udało ci się usunąć system treningowy!");
            updatePage();

        }

        private void deleteDiet(object sender, RoutedEventArgs e)
        {
            string[] linesList = File.ReadAllLines("Diets/list_diets.txt");

            for (int i = 0; i < linesList.Length; i++)
            {
                if (linesList[i] == (string)deleteDietComboBox.SelectedItem)
                {
                    linesList = linesList.Where(w => w != linesList[i]).ToArray();
                }
            }
            File.WriteAllLines("Diets/list_diets.txt", linesList);


            File.Delete("Diets/" + (string)deleteDietComboBox.SelectedItem + ".txt");

            MessageBox.Show("Udało ci się usunąć dietę!");
            updatePage();
        }

        private void enableChangeDeleteTrainingBtn(object sender, SelectionChangedEventArgs e)
        {
            deleteTrainingBtn.IsEnabled = true;
            deleteTrainingBtn.Content = "Usuń system treningowy";
        }

        private void enableChangeDeleteDietBtn(object sender, SelectionChangedEventArgs e)
        {
            deleteDietBtn.IsEnabled = true;
            deleteDietBtn.Content = "Usuń dietę";
        }

        private void AddTraining(object sender, RoutedEventArgs e)
        {

        }

        private void AddDiet(object sender, RoutedEventArgs e)
        {

        }
    }
}
