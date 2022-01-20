
namespace All4Fit
{
    // obiekt opisujący dzień diety, zawiera on obiekty 3 posiłków
    public class DietDay
    {

        private FoodItem _breakfast;
        private FoodItem _lunch;
        private FoodItem _dinner;

        public DietDay()
        { }

        public DietDay(FoodItem[] diet4day)
        {
            _breakfast = diet4day[0];
            _lunch = diet4day[1];
            _dinner = diet4day[2];
        }

        public FoodItem Breakfast
        {
            get { return _breakfast; }

            set { _breakfast = value; }
        }

        public FoodItem Lunch
        {
            get { return _lunch; }

            set { _lunch = value; }
        }

        public FoodItem Dinner
        {
            get { return _dinner; }

            set { _dinner = value; }
        }
    }
}
