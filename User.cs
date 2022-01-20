using System;

namespace All4Fit
{
    // obiekt opisujący użytkownika
    public class User
    {
        private string _login;
        private string _name;
        private string _surname;
        private string _email;
        private string _role;
        private int _age;
        private int _height;
        private int _weight;
        private double _bmi;
        private string _diet;
        private string _systemTraining;

        public User()
        {
            _login = "";
            _name = "";
            _surname = "";
            _email = "";
            _role = "";
            _age = 0;
            _height = 0;
            _weight = 0;
            _bmi = 0.0;
            _diet = "";
            _systemTraining = "";
        }

        public User(string login, string name, string surname, string email, string role, int age, int height, int weight, string diet, string systemTraining)
        {
            _login = login;
            _name = name;
            _surname = surname;
            _email = email;
            _role = role;
            _age = age;
            _height = height;
            _weight = weight;
            _bmi = Math.Round((double)_weight / Math.Pow((double)_height / 100, 2), 2); // obliczenie BMI
            _diet = diet;
            _systemTraining = systemTraining;
        }

        public string Login
        {
            get { return _login; }

            set { _login = value; }
        }

        public string Name
        {
            get { return _name; }

            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }

            set { _surname = value; }
        }

        public string Email
        {
            get { return _email; }

            set { _email = value; }
        }

        public string Role
        {
            get { return _role; }

            set { _role = value; }
        }

        public int Age
        {
            get { return _age; }

            set { _age = value; }
        }

        public int Height
        {
            get { return _height; }

            set { _height = value; }
        }

        public int Weight
        {
            get { return _weight; }

            set { _weight = value; }
        }

        public double BMI
        {
            get { return _bmi; }

            set { _bmi = value; }
        }

        public string Diet
        {
            get { return _diet; }

            set { _diet = value; }
        }

        public string SystemTraining
        {
            get { return _systemTraining; }

            set { _systemTraining = value; }
        }
    }
}
