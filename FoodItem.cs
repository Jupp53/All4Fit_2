
namespace All4Fit
{
    // obiekt opisujący posiłek
    public class FoodItem
    {
        private string _foodName;
        private int _calories;
        private double _servings;
        private int _totalFat;
        private int _protein;
        private int _sugars;
        private int _fiber;

        public FoodItem()
        {
            _foodName = "";
            _calories = 0;
            _servings = 0.0;
            _totalFat = 0;
            _protein = 0;
            _sugars = 0;
            _fiber = 0;
        }

        public FoodItem(string foodName, int calories, double servings, int totalFat, int protein, int sugars, int fiber)
        {
            _foodName = foodName;
            _calories = calories;
            _servings = servings;
            _totalFat = totalFat;
            _protein = protein;
            _sugars = sugars;
            _fiber = fiber;
        }

        public FoodItem(string[] foodArray)
        {
            _foodName = foodArray[0].ToString();
            _calories = int.Parse(foodArray[1]);
            _servings = double.Parse(foodArray[2]);
            _totalFat = int.Parse(foodArray[3]);
            _protein = int.Parse(foodArray[4]);
            _sugars = int.Parse(foodArray[5]);
            _fiber = int.Parse(foodArray[6]);
        }

        public string FoodName
        {
            get { return _foodName; }

            set { _foodName = value; }
        }

        public int Calories
        {
            get { return _calories; }

            set { _calories = value; }
        }

        public double Servings
        {
            get { return _servings; }

            set { _servings = value; }
        }

        public int TotalFat
        {
            get { return _totalFat; }

            set { _totalFat = value; }
        }

        public int Protein
        {
            get { return _protein; }

            set { _protein = value; }
        }

        public int Sugars
        {
            get { return _sugars; }

            set { _sugars = value; }
        }

        public int Fiber
        {
            get { return _fiber; }

            set { _fiber = value; }
        }
    }
}
