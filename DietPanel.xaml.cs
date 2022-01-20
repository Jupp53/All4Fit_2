using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace All4Fit
{
    // Panel Dieta
    public partial class DietPanel : Window
    {
        private User _user;             // obiekt przechowujący dane użytkownika
        private FoodItem[] _meals;      // tablica obiektów posiłków
        private FoodItem[] _dayDiet;    // tablica obiektów posiłków na pory dnia
        private DietDay[] _weekDiet;    // tablica obiektów dni tygodnia 
        private string _userDietCalendarFile; // zmienna przechowująca ścieżkę do pliku kalenadarza użytkownika

        public DietPanel()
        {
            InitializeComponent();
        }

        public void SetObject(User obj) // funkcja uzupełniająca dane użytkownika dostarczone z innych paneli 
        {
            string[] linesList = File.ReadAllLines("Diets/list_diets.txt");

            _user = obj;
            _meals = new FoodItem[File.ReadLines("Diets/fooditems.txt").Count()]; // ilość posiłków w pliku
            _dayDiet = new FoodItem[3]; // 3 pory dnia
            _weekDiet = new DietDay[7]; // 7 dni tygodnia
            _userDietCalendarFile = "DietCalendars/" + _user.Login + "_calendar.txt";
            dietComboBox.ItemsSource = linesList; // załadowanie listy diet do comboboxa
            changeDietBtn.IsEnabled = false; // wyłączenie przycisku do zmiany diety

            int counter = 0;
            if (linesList.Length != 0)
            {
                for (int i = 0; i < linesList.Length; i++)
                {
                    if (linesList[i] == _user.Diet)
                    {
                        dietComboBox.SelectedItem = _user.Diet; // wyświetlenie aktualnej diety użytkownika w comboboxie
                        LoadFoods();
                    }
                    else
                    {
                        counter++;
                    }
                }
                if (counter == linesList.Length)
                {
                    dietContentControl.IsEnabled = false;
                    MessageBox.Show("Twojej diety nie ma w naszej bazie danych, prawdopodobnie została usunięta przez administrator. Proszę wybrać nową dietę!");
                }
            }
            else
            {
                dietContentControl.IsEnabled = false;
                MessageBox.Show("Nasza baza danych z dietami jest pusta, poczekaj aż administrator doda nową dietę!");
            }
        }

        private void LoadFoods() // funkcja do wczytania z pliku posiłków oraz stworzenie tablicy ich obiektów
        {
            int index = 0;
            string foodItem;
            string[] splitFoodArray;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText("Diets/fooditems.txt");
                while (!openFile.EndOfStream)
                {
                    foodItem = openFile.ReadLine();
                    splitFoodArray = foodItem.Split(',');
                    _meals[index++] = new FoodItem(splitFoodArray);
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji LoadFoods(). \n\nBłąd: " + ex.Message);
            }

            LoadDiet();
        }

        private void LoadDiet() // funkcja do wczytania z pliku poszczególnych posiłków na dany dzień i zapisanie ich to tablicy obietków 
        {
            int index = 0;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText("diets/" + _user.Diet + ".txt");
                while (!openFile.EndOfStream)
                {
                    string dayItem = openFile.ReadLine();
                    string[] splitDietArray = dayItem.Split(',');
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < _meals.Length; j++)
                        {
                            // jeżeli nazwa posiłku w pliku zgadza się z nazwą posiłku obiektu z tablicy _meals
                            if (splitDietArray[i] == _meals[j].FoodName)
                            {
                                // przypisanie do pory dnia posiłku
                                _dayDiet[i] = _meals[j];
                            }
                        }
                    }
                    // dodanie dnia do tablicy obiektów dni
                    _weekDiet[index++] = new DietDay(_dayDiet);
                }
                openFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji LoadDiet(). \n\nBłąd: " + ex.Message);
            }

            DietCalender.SelectedDate = DateTime.Today; // zaznaczenia w kalendarzu dzisiejszej daty
            int day = (int)DietCalender.SelectedDate.Value.DayOfWeek; // zmienna z dzisiejszym dniem tygodnia
            updatePage(day);
        }
        private void updatePage(int day) // funkcja aktualizująca panel Dieta
        {
            // zamknięcie expanderów
            expanderBreakfast.IsExpanded = false;
            expanderLunch.IsExpanded = false;
            expanderDinner.IsExpanded = false;

            // wypisanie nazw posiłków
            labelBreakfast.Content = "Śniadanie: " + _weekDiet[day].Breakfast.FoodName;
            labelLunch.Content = "Obiad: " + _weekDiet[day].Lunch.FoodName;
            labelDinner.Content = "Kolacja: " + _weekDiet[day].Dinner.FoodName;

            // wypisanie opisów posiłków
            tbBreakfast.Text = "Kalorie: " + _weekDiet[day].Breakfast.Calories + "  Porcje: " + _weekDiet[day].Breakfast.Servings + "\nZawartość:\nTłuszcz: " + _weekDiet[day].Breakfast.TotalFat + "  Białko: " + _weekDiet[day].Breakfast.Protein + "\nCukier: " + _weekDiet[day].Breakfast.Sugars + "  Błonnik: " + _weekDiet[day].Breakfast.Fiber;
            tbLunch.Text = "Kalorie: " + _weekDiet[day].Lunch.Calories + "  Porcje: " + _weekDiet[day].Lunch.Servings + "\nZawartość:\nTłuszcz: " + _weekDiet[day].Lunch.TotalFat + "  Białko: " + _weekDiet[day].Lunch.Protein + "\nCukier: " + _weekDiet[day].Lunch.Sugars + "  Błonnik: " + _weekDiet[day].Lunch.Fiber;
            tbDinner.Text = "Kalorie: " + _weekDiet[day].Dinner.Calories + "  Porcje: " + _weekDiet[day].Dinner.Servings + "\nZawartość:\nTłuszcz: " + _weekDiet[day].Dinner.TotalFat + "  Białko: " + _weekDiet[day].Dinner.Protein + "\nCukier: " + _weekDiet[day].Dinner.Sugars + "  Błonnik: " + _weekDiet[day].Dinner.Fiber;

            // ustawienie maksimum dla progressbaru
            dayProgressBar.Maximum = _weekDiet[day].Breakfast.Calories + _weekDiet[day].Lunch.Calories + _weekDiet[day].Dinner.Calories;

            CheckMealCalendarStatus();
        }

        private void CheckMealCalendarStatus() // funkcja sprawdzająca czy dane posiłki były zaznaczone czy też nie 
        {
            int day = (int)DietCalender.SelectedDate.Value.DayOfWeek; // zmienna z zaznaczonym dniem tygodnia
            string date = DietCalender.SelectedDate.ToString().Split()[0]; // zmienna z zaznaczoną datą w formie [yyyy.mm.dd]

            string dayDetalits = "";
            string[] splitedDayDetalits = new String[4]; // plik posiada kolumny: data, status śniadania, status obiadu, status kolacji
            try
            {
                StreamReader openFile;
                openFile = File.OpenText(_userDietCalendarFile);
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
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem plikiem w funkcji CheckMealCalendarStatus(). \n\nBłąd: " + ex.Message);
            }


            dayProgressBar.Value = 0; // wyzerowanie wartości progressbaru

            var bc = new BrushConverter(); // zmienna potrzebna do zmiany koloru tła expanderów

            // zresetowanie tła
            expanderBreakfast.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderLunch.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");
            expanderDinner.Background = (Brush)bc.ConvertFrom("#FFD6FFD6");

            // jeżeli wartość statusu śniadania, obiadu lub kolacji w pliku równa się 1 to następuje
            // zaznaczenie checkboxa, dodanie wartości kalorii oraz zmiana tła expandera
            if (Convert.ToInt32(splitedDayDetalits[1]) == 1)
            {
                checkboxBreakfast.IsChecked = true;
                dayProgressBar.Value += _weekDiet[day].Breakfast.Calories;
                expanderBreakfast.Background = (Brush)bc.ConvertFrom("#4caf50"); ;
            }
            else
            {
                checkboxBreakfast.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[2]) == 1)
            {
                checkboxLunch.IsChecked = true;
                dayProgressBar.Value += _weekDiet[day].Lunch.Calories;
                expanderLunch.Background = (Brush)bc.ConvertFrom("#4caf50"); ;
            }
            else
            {
                checkboxLunch.IsChecked = false;

            }
            if (Convert.ToInt32(splitedDayDetalits[3]) == 1)
            {
                checkboxDinner.IsChecked = true;
                dayProgressBar.Value += _weekDiet[day].Dinner.Calories;
                expanderDinner.Background = (Brush)bc.ConvertFrom("#4caf50"); ;
            }
            else
            {
                checkboxDinner.IsChecked = false;

            }
            //wyświetlenie wartości spożytych kalorii na progressbarze
            labelProgressBar.Content = dayProgressBar.Value + " z " + dayProgressBar.Maximum + " kcal";
        }

        private void updateMealStatus(object sender, RoutedEventArgs e) // funkcja działająca po zaznaczeniu checkboxa, aktaulizuje status posiłku w pliku kalendarza użytkownika
        {
            string date = DietCalender.SelectedDate.ToString().Split()[0];
            string dayDetalits = "";
            string[] splitedDayDetalits;

            int breakfastStatus = 0;
            int lunchStatus = 0;
            int dinnerStatus = 0;

            try
            {
                StreamReader openFile;
                openFile = File.OpenText(_userDietCalendarFile);
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
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem pliku w funkcji updateMealStatus(). \n\nBłąd: " + ex.Message);
            }

            if (checkboxBreakfast.IsChecked == true)
                breakfastStatus = 1;
            if (checkboxLunch.IsChecked == true)
                lunchStatus = 1;
            if (checkboxDinner.IsChecked == true)
                dinnerStatus = 1;

            // odczyt całego pliku kalendarza, zamiana wiersza dla danego dnia, zaaktualizowanie pilku kalenadrza
            string calendarFile = File.ReadAllText(_userDietCalendarFile);
            calendarFile = calendarFile.Replace(dayDetalits, date + "," + breakfastStatus + "," + lunchStatus + "," + dinnerStatus);
            File.WriteAllText(_userDietCalendarFile, calendarFile);

            CheckMealCalendarStatus();
        }

        private void DietCalender_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) //funkcja działająca po zmianie dnia na kalendarzu
        {
            if (DayTextBox != null)
            {
                // jeżeli zaznaczona data jest późniejsza niż dzisiejsza zablokowanie możliwości zaznaczenia posiłków
                if (DateTime.Today >= DietCalender.SelectedDate)
                {
                    checkboxBreakfast.IsEnabled = true;
                    checkboxLunch.IsEnabled = true;
                    checkboxDinner.IsEnabled = true;
                }
                else
                {
                    checkboxBreakfast.IsEnabled = false;
                    checkboxLunch.IsEnabled = false;
                    checkboxDinner.IsEnabled = false;
                }

                int day = (int)DietCalender.SelectedDate.Value.DayOfWeek; // zmienna z zaznaczonym dniem tygodnia
                updatePage(day);
            }
        }

        private void enableChangeDietBtn(object sender, SelectionChangedEventArgs e) // funkcja działająca po zmianie wyboru diety
        {
            // jeżeli została wybrana inna dieta niż dotychczasowa następuje umożliwienie kliknięcia przycisku zmiana diety
            if ((string)dietComboBox.SelectedItem == _user.Diet)
            {
                changeDietBtn.IsEnabled = false;
                changeDietBtn.Content = "Wybierz nową dietę powyżej";
            }
            else
            {
                changeDietBtn.IsEnabled = true;
                changeDietBtn.Content = "Zmień dietę";
            }
        }

        private void changeDiet(object sender, RoutedEventArgs e) // funkcja działąjąca po kliknięciu przycisku zmiana diety
        {
            int day = (int)DietCalender.SelectedDate.Value.DayOfWeek;
            string date = DietCalender.SelectedDate.ToString().Split()[0];

            if (MessageBox.Show("Czy na pewno chcesz zmienić dietę? Jeśli tak twoje dotychczasowe postępy zostaną usunięte.", "Pytanie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                // jeżeli wybrano NIE
                dietComboBox.SelectedItem = _user.Diet;
                changeDietBtn.Content = "Wybierz nową dietę powyżej";
                changeDietBtn.IsEnabled = false;
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
                MessageBox.Show("Przepraszamy, nastąpił problem z odczytem plikiem w funkcji changeDiet(). \n\nBłąd: " + ex.Message);
            }

            // odczyt całego pliku users.txt, zamiana wiersza dla danego użytkownika, zaaktualizowanie pilku users.txt
            string usersFile = File.ReadAllText("users.txt");
            usersFile = usersFile.Replace(userDetalits, _user.Login + "," + splitedUserDetalits[1] + "," + _user.Name + "," + _user.Surname + "," + _user.Email + "," + _user.Role + "," + _user.Age + "," + _user.Height + "," + _user.Weight + "," + (string)dietComboBox.SelectedItem + "," + _user.SystemTraining);
            File.WriteAllText("users.txt", usersFile);
            dietContentControl.IsEnabled = true;
            // zmiana nazwy diety w obiekcie użytkownik
            _user.Diet = (string)dietComboBox.SelectedItem;

            // zresetowanie kalendarza
            string newCalendarFile = File.ReadAllText("DietCalendars/calendar.txt");
            File.WriteAllText(_userDietCalendarFile, newCalendarFile);

            // załadowanie nowej diety
            LoadFoods();

            changeDietBtn.Content = "Wybierz nową dietę powyżej";
            changeDietBtn.IsEnabled = false;

            MessageBox.Show("Udało ci się zmienić dietę, dania na każdą porę dnia zostały zaktualizowane.");

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

        private void deleteUser(object sender, RoutedEventArgs e)
        {

        }
    }
}

