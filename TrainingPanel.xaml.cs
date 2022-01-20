using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace All4Fit
{
    // Panel Trening
    public partial class TrainingPanel : Window
    {
        private User _user;                         // obiekt przechowujący dane użytkownika
        private ExeciseItem[] _execises;            // tablica obiektów ćwiczeń
        private ExeciseItem[] _dayTraining;         // tablica obiektów ćwiczeń w dniu
        private TrainingDay[] _weekSystemTraining;  // tablica obiektów dni tygodnia 
        private string _userTrainingCalendarFile;   // zmienna przechowująca ścieżkę do pliku kalenadarza użytkownika

        public TrainingPanel()
        {
            InitializeComponent();
        }

        public void SetObject(User obj) // funkcja uzupełniająca dane użytkownika dostarczone z innych paneli 
        {
            string[] linesList = File.ReadAllLines("TrainingSystems/list_trainings.txt");

            _user = obj;
            _execises = new ExeciseItem[25]; // ilość ćwiczeń w pliku
            _dayTraining = new ExeciseItem[5]; // 5 ćwiczeń na dzień
            _weekSystemTraining = new TrainingDay[7]; // 7 dni tygodnia
            _userTrainingCalendarFile = "TrainingCalendars/" + _user.Login + "_calendar.txt";

            systemTrainingComboBox.ItemsSource = linesList; //załadowanie listy systemów treningowych do comboboxa
            changeTrainingBtn.IsEnabled = false; //wyłączenie przycisku do zmiany systemu treningowego

            int counter = 0;
            if (linesList.Length != 0)
            {
                for (int i = 0; i < linesList.Length; i++)
                {
                    if (linesList[i] == _user.SystemTraining)
                    {
                        systemTrainingComboBox.SelectedItem = _user.SystemTraining; //wyświetlenie aktualnego systemu treningowego użytkownika w comboboxie
                        LoadExesices();
                    }
                    else
                    {
                        counter++;
                    }
                }
                if (counter == linesList.Length)
                {
                    trainingContentControl.IsEnabled = false;
                    MessageBox.Show("Twojego systemu treningowego nie ma w naszej bazie danych, prawdopodobnie został usunięta przez administrator. Proszę wybrać nowy system!");
                }
            }
            else
            {
                trainingContentControl.IsEnabled = false;
                MessageBox.Show("Nasza baza danych z systemami treningowymi jest pusta, poczekaj aż administrator doda nowy system treningowy!");
            }
        }

        private void LoadExesices() // funkcja do wczytania z pliku ćwiczeń oraz stworzenie tablicy ich obiektów
        {
            int index = 0;
            string exeItem;
            string[] splitExeArray;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText("TrainingSystems/exeitems.txt");
                while (!openFile.EndOfStream)
                {
                    exeItem = openFile.ReadLine();
                    splitExeArray = exeItem.Split(',');
                    _execises[index++] = new ExeciseItem(splitExeArray);
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z otwarciem pliku w funkcji LoadExesices().\n\nBłąd: " + ex.Message);
            }

            LoadSystemTraining();
        }

        private void LoadSystemTraining() // funkcja do wczytania z pliku poszczególnych ćwiczeń na dany dzień i zapisanie ich to tablicy obietków 
        {
            int index = 0;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText("TrainingSystems/" + _user.SystemTraining + ".txt");
                while (!openFile.EndOfStream)
                {
                    string dayItem = openFile.ReadLine();
                    string[] splitDietArray = dayItem.Split(',');

                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < _execises.Length; j++)
                        {
                            // jeżeli nazwa ćwiczenia w pliku zgadza się z nazwą ćwiczenia obiektu z tablicy _execises
                            if (splitDietArray[i] == _execises[j].ExeName)
                            {
                                // przypisanie do dnia ćwiczenia
                                _dayTraining[i] = _execises[j];
                            }
                        }
                    }
                    // dodanie dnia do tablicy obiektów dni
                    _weekSystemTraining[index++] = new TrainingDay(_dayTraining);
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z otwarciem pliku w funkcji LoadSystemTraining(). \n\nBłąd: " + ex.Message);
            }

            TrainingCalender.SelectedDate = DateTime.Today; //zaznaczenia w kalendarzu dzisiejszej daty
            int day = (int)TrainingCalender.SelectedDate.Value.DayOfWeek; //zmienna z dzisiejszym dniem tygodnia
            updatePage(1);
        }

        private void updatePage(int day) //zaktualiozwanie panelu treningu
        {
            // zamknięcie expanderó
            expanderExe1.IsExpanded = false;
            expanderExe2.IsExpanded = false;
            expanderExe3.IsExpanded = false;
            expanderExe4.IsExpanded = false;
            expanderExe5.IsExpanded = false;

            // wypisanie nazw ćwiczeń
            labelExe1.Content = "Ćwiczenie 1: " + _weekSystemTraining[day].Exe1.ExeName;
            labelExe2.Content = "Ćwiczenie 2: " + _weekSystemTraining[day].Exe2.ExeName;
            labelExe3.Content = "Ćwiczenie 3: " + _weekSystemTraining[day].Exe3.ExeName;
            labelExe4.Content = "Ćwiczenie 4: " + _weekSystemTraining[day].Exe4.ExeName;
            labelExe5.Content = "Ćwiczenie 5: " + _weekSystemTraining[day].Exe5.ExeName;

            // wypisanie opisów ćwiczeń
            tbExe1.Text = _weekSystemTraining[day].Exe1.ExeName + "\nZaangażowane mięśnie: " + _weekSystemTraining[day].Exe1.MusculesName + "\nLiczba powtórzeń: " + _weekSystemTraining[day].Exe1.Repetitions + "  Ilość serii: " + _weekSystemTraining[day].Exe1.Series;
            tbExe2.Text = _weekSystemTraining[day].Exe2.ExeName + "\nZaangażowane mięśnie: " + _weekSystemTraining[day].Exe2.MusculesName + "\nLiczba powtórzeń: " + _weekSystemTraining[day].Exe2.Repetitions + "  Ilość serii: " + _weekSystemTraining[day].Exe2.Series;
            tbExe3.Text = _weekSystemTraining[day].Exe3.ExeName + "\nZaangażowane mięśnie: " + _weekSystemTraining[day].Exe3.MusculesName + "\nLiczba powtórzeń: " + _weekSystemTraining[day].Exe3.Repetitions + "  Ilość serii: " + _weekSystemTraining[day].Exe3.Series;
            tbExe4.Text = _weekSystemTraining[day].Exe4.ExeName + "\nZaangażowane mięśnie: " + _weekSystemTraining[day].Exe4.MusculesName + "\nLiczba powtórzeń: " + _weekSystemTraining[day].Exe4.Repetitions + "  Ilość serii: " + _weekSystemTraining[day].Exe4.Series;
            tbExe5.Text = _weekSystemTraining[day].Exe5.ExeName + "\nZaangażowane mięśnie: " + _weekSystemTraining[day].Exe5.MusculesName + "\nLiczba powtórzeń: " + _weekSystemTraining[day].Exe5.Repetitions + "  Ilość serii: " + _weekSystemTraining[day].Exe5.Series;

            // ustawienie maksimum dla progressbaru
            dayProgressBar.Maximum = _weekSystemTraining[day].Exe1.CaloriesBurned + _weekSystemTraining[day].Exe2.CaloriesBurned + _weekSystemTraining[day].Exe3.CaloriesBurned + _weekSystemTraining[day].Exe4.CaloriesBurned + _weekSystemTraining[day].Exe5.CaloriesBurned;

            CheckTrainingCalendarStatus();
        }

        private void CheckTrainingCalendarStatus() // funkcja sprawdzająca czy dane ćwiczenie były zaznaczone czy też nie 
        {
            int day = (int)TrainingCalender.SelectedDate.Value.DayOfWeek; // zmienna z zaznaczonym dniem tygodnia
            string date = TrainingCalender.SelectedDate.ToString().Split()[0]; // zmienna z zaznaczoną datą w formie [yyyy.mm.dd]

            string dayDetalits = "";
            string[] splitedDayDetalits = new String[6]; // plik posiada kolumny: data, cw1, ... , cw5

            try
            {
                StreamReader openFile;
                openFile = File.OpenText(_userTrainingCalendarFile);
                while (!openFile.EndOfStream)
                {
                    dayDetalits = openFile.ReadLine();
                    splitedDayDetalits = dayDetalits.Split(',');
                    // jeżeli data z pliku zgadza się z zaznaczoną datą na kalendarzu następuje przerwanie while'a
                    if (splitedDayDetalits[0] == date)
                    {
                        break;
                    }
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji CheckTrainingCalendarStatus(). \n\nBłąd: " + ex.Message);
            }

            dayProgressBar.Value = 0; // wyzerowanie wartości progressbaru

            var bc = new BrushConverter(); // zmienna potrzebna do zmiany koloru tła expanderów

            // zresetowanie tła
            expanderExe1.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderExe2.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderExe3.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderExe4.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderExe5.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            // jeżeli wartość statusu ćw1, ..., ćw5 w pliku równa się 1 to następuje
            // zaznaczenie checkboxa, dodanie wartości spalonych kalorii oraz zmiana tła expandera

            if (Convert.ToInt32(splitedDayDetalits[1]) == 1)
            {
                checkboxExe1.IsChecked = true;
                dayProgressBar.Value += _weekSystemTraining[day].Exe1.CaloriesBurned;
                expanderExe1.Background = (Brush)bc.ConvertFrom("#4caf50");
            }
            else
            {
                checkboxExe1.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[2]) == 1)
            {
                checkboxExe2.IsChecked = true;
                dayProgressBar.Value += _weekSystemTraining[day].Exe2.CaloriesBurned;
                expanderExe2.Background = (Brush)bc.ConvertFrom("#4caf50");
            }
            else
            {
                checkboxExe2.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[3]) == 1)
            {
                checkboxExe3.IsChecked = true;
                dayProgressBar.Value += _weekSystemTraining[day].Exe3.CaloriesBurned;
                expanderExe3.Background = (Brush)bc.ConvertFrom("#4caf50");
            }
            else
            {
                checkboxExe3.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[4]) == 1)
            {
                checkboxExe4.IsChecked = true;
                dayProgressBar.Value += _weekSystemTraining[day].Exe4.CaloriesBurned;
                expanderExe4.Background = (Brush)bc.ConvertFrom("#4caf50");
            }
            else
            {
                checkboxExe4.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[5]) == 1)
            {
                checkboxExe5.IsChecked = true;
                dayProgressBar.Value += _weekSystemTraining[day].Exe5.CaloriesBurned;
                expanderExe5.Background = (Brush)bc.ConvertFrom("#4caf50");
            }
            else
            {
                checkboxExe5.IsChecked = false;

            }
            //wyświetlenie wartości spalonych kalorii na progressbarze
            labelProgressBar.Content = dayProgressBar.Value + " z " + dayProgressBar.Maximum + " kcal";
        }

        private void updateExeStatus(object sender, RoutedEventArgs e) // funkcja działająca po zaznaczeniu checkboxa, aktaulizuje status ćwiczenia w pliku kalendarza użytkownika
        {
            string date = TrainingCalender.SelectedDate.ToString().Split()[0];
            string dayDetalits = "";
            string[] splitedDayDetalits;

            int exe1tStatus = 0;
            int exe2tStatus = 0;
            int exe3tStatus = 0;
            int exe4tStatus = 0;
            int exe5tStatus = 0;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText(_userTrainingCalendarFile);
                while (!openFile.EndOfStream)
                {
                    dayDetalits = openFile.ReadLine();
                    splitedDayDetalits = dayDetalits.Split(',');
                    if (splitedDayDetalits[0] == date)
                    {
                        break;
                    }
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji updateExeStatus(). \n\nBłąd: " + ex.Message);
            }

            if (checkboxExe1.IsChecked == true)
                exe1tStatus = 1;
            if (checkboxExe2.IsChecked == true)
                exe2tStatus = 1;
            if (checkboxExe3.IsChecked == true)
                exe3tStatus = 1;
            if (checkboxExe4.IsChecked == true)
                exe4tStatus = 1;
            if (checkboxExe5.IsChecked == true)
                exe5tStatus = 1;

            // odczyt całego pliku kalendarza, zamiana wiersza dla danego dnia, zaaktualizowanie pilku kalenadrza
            string calendarFile = File.ReadAllText(_userTrainingCalendarFile);
            calendarFile = calendarFile.Replace(dayDetalits, date + "," + exe1tStatus + "," + exe2tStatus + "," + exe3tStatus + "," + exe4tStatus + "," + exe5tStatus);
            File.WriteAllText(_userTrainingCalendarFile, calendarFile);

            CheckTrainingCalendarStatus();
        }

        private void TrainingCalender_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //funkcja działająca po zmianie dnia na kalendarzu
        {
            if (DayTextBox != null)
            {
                // jeżeli zaznaczona data jest późniejsza niż dzisiejsza zablokowanie możliwości zaznaczenia posiłków
                if (DateTime.Today >= TrainingCalender.SelectedDate)
                {
                    checkboxExe1.IsEnabled = true;
                    checkboxExe2.IsEnabled = true;
                    checkboxExe3.IsEnabled = true;
                    checkboxExe4.IsEnabled = true;
                    checkboxExe5.IsEnabled = true;
                }
                else
                {
                    checkboxExe1.IsEnabled = false;
                    checkboxExe2.IsEnabled = false;
                    checkboxExe3.IsEnabled = false;
                    checkboxExe4.IsEnabled = false;
                    checkboxExe5.IsEnabled = false;
                }

                int day = (int)TrainingCalender.SelectedDate.Value.DayOfWeek; // zmienna z zaznaczonym dniem tygodnia
                updatePage(day);
            }
        }

        private void enableChangeTrainingBtn(object sender, SelectionChangedEventArgs e)// funkcja działająca po zmianie wyboru systemu treningowego
        {
            // jeżeli został wybrany  inny system niż dotychczasowy następuje umożliwienie kliknięcia przycisku zmiana systemu ćwiczeń
            if ((string)systemTrainingComboBox.SelectedItem == _user.SystemTraining)
            {
                changeTrainingBtn.IsEnabled = false;
                changeTrainingBtn.Content = "Wybierz nowy system powyżej";
            }
            else
            {
                changeTrainingBtn.IsEnabled = true;
                changeTrainingBtn.Content = "Zmień system treningowy";
            }
        }


        private void changeSystemTraining(object sender, RoutedEventArgs e) // funkcja działąjąca po kliknięciu przycisku zmiana systemu treningowego
        {
            int day = (int)TrainingCalender.SelectedDate.Value.DayOfWeek;
            string date = TrainingCalender.SelectedDate.ToString().Split()[0];

            if (MessageBox.Show("Czy na pewno chcesz zmienić system ćwiczeń? Jeśli tak twoje dotychczasowe postępy zostaną usunięte.", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // jeżeli wybrano NIE
                systemTrainingComboBox.SelectedItem = _user.SystemTraining;
                changeTrainingBtn.Content = "Wybierz nową system powyżej";
                changeTrainingBtn.IsEnabled = false;
                return;
            }

            // jeżeli wybrano TAK

            string userDetalits = "";
            string[] splitedUserDetalits = new String[11]; // 11 zmiennych z danymi użytkownika

            try
            {
                StreamReader openFile;
                openFile = File.OpenText("users.txt");
                while (!openFile.EndOfStream)
                {
                    userDetalits = openFile.ReadLine();
                    splitedUserDetalits = userDetalits.Split(',');
                    // jeżeli login z pliku zgadza się z loginiem użytkownika
                    if (splitedUserDetalits[0] == _user.Login)
                    {
                        break;
                    }
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z otwarciem plik users.txt w funkcji changeSystemTraining(). \n\nBłąd: " + ex.Message);
            }

            // odczyt całego pliku users.txt, zamiana wiersza dla danego użytkownika, zaaktualizowanie pilku users.txt
            string usersFile = File.ReadAllText("users.txt");
            usersFile = usersFile.Replace(userDetalits, _user.Login + "," + splitedUserDetalits[1] + "," + _user.Name + "," + _user.Surname + "," + _user.Email + "," + _user.Role + "," + _user.Age + "," + _user.Height + "," + _user.Weight + "," + _user.Diet + "," + (string)systemTrainingComboBox.SelectedItem);
            File.WriteAllText("users.txt", usersFile);

            // zmiana nazwy systemu treningowego w obiekcie użytkownik
            _user.SystemTraining = (string)systemTrainingComboBox.SelectedItem;

            // zresetowanie kalendarza
            string newCalendarFile = File.ReadAllText("TrainingCalendars/calendar.txt");
            File.WriteAllText(_userTrainingCalendarFile, newCalendarFile);

            // załadowanie nowej diety
            LoadSystemTraining();

            changeTrainingBtn.Content = "Wybierz nową dietę powyżej";
            changeTrainingBtn.IsEnabled = false;

            MessageBox.Show("Udało ci się zmienić system treningowy, ćwiczenia zostały zaktualizowane.");
        }

        private void backToMainPanel(object sender, RoutedEventArgs e) // przejście do panelu głównego 
        {
            UserHome fr2 = new UserHome();
            fr2.SetObject(_user);
            fr2.Show();
            this.Close();
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
