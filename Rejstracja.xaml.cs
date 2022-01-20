using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace All4Fit
{
    // Panel rejstracji
    public partial class Rejstracja : Window
    {
        public Rejstracja()
        {
            InitializeComponent();
            // dodanie do ComboBox nazw diet i systemów ćwiczeń odczytanych z plików txt
            dietComboBox.ItemsSource = File.ReadAllLines("diets/list_diets.txt");
            trainingComboBox.ItemsSource = File.ReadAllLines("trainingSystems/list_trainings.txt");
        }

        private void createUser(object sender, RoutedEventArgs e) // proces rejstracji po kliknięciu przycisku
        {
            // sprawdzenie czy wszystkie pola są uzupełnione
            if (txtLogin.Text != "" && txtPassword.Password != "" && txtPassword2.Password != "" && txtEmail.Text != "" && txtName.Text != "" && txtSurname.Text != "" && txtAge.Text != "" && txtHeight.Text != "" && txtWeight.Text != "" && dietComboBox.SelectedIndex > -1 && trainingComboBox.SelectedIndex > -1) // sprawdzenie czy wszystkie pola zostały wypełnione
            {
                // sprawdzenie czy watość email jest adresem emailem
                if (isValidEmail(txtEmail.Text) != false)
                {
                    // sprawdzenie czy watość wieku jest liczbą
                    if (isNumeric(txtAge.Text) == true)
                    {
                        // sprawdzenie czy watość wzrostu jest liczbą
                        if (isNumeric(txtHeight.Text) == true)
                        {
                            // sprawdzenie czy watość wagi jest liczbą
                            if (isNumeric(txtWeight.Text) == true)
                            {
                                // sprawdzenie czy hasła się zgadzają
                                if (txtPassword.Password == txtPassword2.Password)
                                {
                                    // rozpoczęcie próby utworzenia konta
                                    string userDetalits;
                                    string[] splitedUserDetalits;
                                    int counter = 0;

                                    try
                                    {
                                        StreamReader openFile;
                                        openFile = File.OpenText("users.txt");
                                        while (!openFile.EndOfStream)
                                        {
                                            userDetalits = openFile.ReadLine();
                                            splitedUserDetalits = userDetalits.Split(',');
                                            // sprawdzenie czy w pliku znajduje się wiersz z taką samą nazwą użytkownika
                                            if (splitedUserDetalits[0] == txtLogin.Text)
                                            {
                                                counter++;
                                            }
                                        }
                                        openFile.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Przepraszamy, nastąpił problem z rejstracją. \n\nBłąd: " + ex.Message);
                                    }
                                    if (counter == 0) // jeżeli w pliku nie znajduje się nazwa użytkownika następuje dodanie danych użytkownika do pliku, loginu do pliku z loginami, utworzenie pliku z kalendarzem dla diety i systemu ćwiczeń oraz tworzenie konta

                                    {
                                        File.AppendAllText("users.txt", txtLogin.Text + "," + txtPassword.Password + "," + txtName.Text + "," + txtSurname.Text + "," + txtEmail.Text + ",User," + txtAge.Text + "," + txtHeight.Text + "," + txtWeight.Text + "," + dietComboBox.SelectedItem + "," + trainingComboBox.SelectedItem + Environment.NewLine);
                                        using (StreamWriter writer = new StreamWriter("usersLogins.txt", true))
                                        {
                                            writer.WriteLine(txtLogin.Text);
                                        }
                                        File.Copy(@"" + Environment.CurrentDirectory + "\\DietCalendars\\calendar.txt", @"" + Environment.CurrentDirectory + "\\DietCalendars\\" + txtLogin.Text + "_calendar.txt");
                                        File.Copy(@"" + Environment.CurrentDirectory + "\\TrainingCalendars\\calendar.txt", @"" + Environment.CurrentDirectory + "\\TrainingCalendars\\" + txtLogin.Text + "_calendar.txt");
                                        createUserProfile();
                                    }
                                    else // jeżeli w pliku znajduje się nazwa użytkownika następuje powiadomienie o tym użytkownika
                                    {
                                        MessageBox.Show("Użytkownik o takim loginie już istnieje!");
                                        txtLogin.Text = "";
                                        txtPassword.Password = "";
                                        txtPassword2.Password = "";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Podane przez hasła nie są identyczne!");
                                    txtPassword.Password = "";
                                    txtPassword2.Password = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Waga musi być liczbą!");
                                txtWeight.Text = "";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wzrost musi być liczbą!");
                            txtHeight.Text = "";
                        }
                    }
                    else
                    {
                        MessageBox.Show("Wiek musi być liczbą!");
                        txtAge.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("Podany przez email ma nieprawidłową formę!");
                    txtEmail.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Wszystkie dane muszą zostać uzupełnione!");
            }
        }

        bool isNumeric(string sth)
        {
            bool isNumeric = int.TryParse(sth, out int n);
            return isNumeric;
        }

        bool isValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        private void createUserProfile()    // stworzenie obiektu użytkownik i przesłanie go do innych klas
        {
            User u1 = new User(txtLogin.Text, txtName.Text, txtSurname.Text, txtEmail.Text, "User", Convert.ToInt32(txtAge.Text), Convert.ToInt32(txtHeight.Text), Convert.ToInt32(txtWeight.Text), dietComboBox.SelectedItem.ToString(), trainingComboBox.SelectedItem.ToString());

            MessageBox.Show("Gratulacje, udało ci się poprawnie zarejestrować!");

            UserHome uH1 = new UserHome();  // stworzenie panelu głównego użytkownika

            uH1.SetObject(u1);              // przesłanie obiektu użytkownik do panelu głównego
            uH1.Show();                     // przejście do tego panelu
            this.Close();                   // zamknięcie panelu rejstracji
        }

        private void loginOpen(object sender, RoutedEventArgs e) // przejście do ekranu logowania
        {
            Logowanie l1 = new Logowanie();
            l1.Show();
            this.Close();
        }

        private void exitApp(object sender, RoutedEventArgs e) // wyjście z aplikacji
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e) //poruszanie oknem aplikacji
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
