using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace All4Fit
{
    // Panel logowania
    public partial class Logowanie : Window
    {
        public Logowanie()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logInUser(object sender, RoutedEventArgs e) // proces logowania po kliknięciu przycisku 
        {
            // sprawdzenie czy wszystkie pola są uzupełnione
            if (txtLogin.Text != "" && txtPassword.Password != "")
            {
                // rozpoczęcie próby logowania konta
                string userDetalits;
                string[] splitedUserDetalits;
                bool userMatched = false;

                try
                {
                    StreamReader openFile;
                    openFile = File.OpenText("users.txt");
                    while (!openFile.EndOfStream)
                    {
                        userDetalits = openFile.ReadLine();
                        splitedUserDetalits = userDetalits.Split(',');
                        //sprawdzenie czy dane z pliku pokrywają się z danymi wprowadzonymi przez użytkownika
                        if (splitedUserDetalits[0] == txtLogin.Text && splitedUserDetalits[1] == txtPassword.Password)
                        {
                            userMatched = true;
                            // stworzenie obiektu użytkownik
                            User u1 = new User(splitedUserDetalits[0], splitedUserDetalits[2], splitedUserDetalits[3], splitedUserDetalits[4], splitedUserDetalits[5], Convert.ToInt32(splitedUserDetalits[6]), Convert.ToInt32(splitedUserDetalits[7]), Convert.ToInt32(splitedUserDetalits[8]), splitedUserDetalits[9], splitedUserDetalits[10]);
                            if (u1.Role == "Admin") // jeżeli użytkownik jest administratorem
                            {
                                AdminHome ah = new AdminHome(); // stworzenie panelu głównego administratora
                                ah.SetObject(u1);   // przesłanie obiektu użytkownik do panelu głównego
                                ah.Show(); // przejście do tego panelu
                            }
                            else
                            {
                                UserHome uh = new UserHome(); // stworzenie panelu głównego użytkownika
                                uh.SetObject(u1);   // przesłanie obiektu użytkownik do panelu głównego
                                uh.Show();          // przejście do tego panelu
                                this.Close();       // zamknięcie panelu logowania
                            }
                            break; //zatrzymanie while kiedy dane się zgadzają
                        }
                    }
                    openFile.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Przepraszamy, nastąpił problem z logowaniem. \n\nBłąd: " + ex.Message);
                }

                if (userMatched == false) //jeżeli dane z pliku nie pokrywają się z danymi wprowadzonymi przez użytkownika
                {
                    MessageBox.Show("Twój login lub hasło jest niepoprawne!");
                    txtPassword.Password = "";
                }
            }
            else
            {
                MessageBox.Show("Wszystkie dane muszą być uzupełnione");
            }
        }

        private void registerOpen(object sender, RoutedEventArgs e) // przejście do ekranu rejstracji
        {
            Rejstracja r1 = new Rejstracja();
            r1.Show();
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
