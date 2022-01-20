using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace All4Fit
{
    // Panel główny użytkownika
    public partial class UserHome : Window
    {
        private User _user; // obiekt przechowujący dane użytkownika

        public UserHome()
        {
            InitializeComponent();
        }

        public void SetObject(User obj) // funkcja uzupełniająca dane użytkownika dostarczone z innych paneli 
        {
            _user = obj;

            tbMenu.Text = "Witaj " + _user.Name + "!";

            showBMI(); // po uzupełnieniu danych
        }

        private void showBMI() // wyświetlenie wskaźnika BMI w panelu głównych
        {
            bmiBtn.Content = "BMI: " + _user.BMI; // obliczenie BMI następuje już przy tworzeniu obiektu klasy User

            var bc = new BrushConverter();  // zmienna potrzebna do zmiany koloru paska BMI

            // zmiana koloru paska BMI w zależności od wartości
            if (_user.BMI < 18.5)
                bmiBtn.Background = (Brush)bc.ConvertFrom("#06d3b4");
            else if (_user.BMI >= 18.5 && _user.BMI < 25)
                bmiBtn.Background = (Brush)bc.ConvertFrom("#27b707");
            else if (_user.BMI >= 25 && _user.BMI < 30)
                bmiBtn.Background = (Brush)bc.ConvertFrom("#fcb004");
            else if (_user.BMI >= 30 && _user.BMI < 35)
                bmiBtn.Background = (Brush)bc.ConvertFrom("#fa6708");
            else
                bmiBtn.Background = (Brush)bc.ConvertFrom("#ff1700");
        }

        private void openTraining(object sender, RoutedEventArgs e) //przejście do panelu Treningu
        {
            TrainingPanel t1 = new TrainingPanel(); // stworzenie panelu treningu 
            t1.SetObject(_user);                     // przesłanie obiektu użytkownik do panelu 
            t1.Show();                              // przejście do tego panelu
            this.Close();                           // zamknięcie panelu głównego
        }

        private void openDiet(object sender, RoutedEventArgs e) //przejście do panelu Dieta
        {
            DietPanel d1 = new DietPanel(); // stworzenie panelu diety
            d1.SetObject(_user);             // przesłanie obiektu użytkownik do panelu 
            d1.Show();                      // przejście do tego panelu
            this.Close();                   // zamknięcie panelu głównego
        }

        private void logoutUser(object sender, RoutedEventArgs e) // wylogowanie użytkownika, przejście do ekranu logowania
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
