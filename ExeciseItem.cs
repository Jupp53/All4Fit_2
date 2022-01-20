
namespace All4Fit
{
    // obiekt opisujący ćwiczenie
    class ExeciseItem
    {
        private int _ID;
        private string _exeName;
        private string _musculesName;
        private int _repetitions;
        private int _series;
        private int _caloriesBurned;

        public ExeciseItem()
        {
            _ID = 0;
            _exeName = "";
            _musculesName = "";
            _repetitions = 0;
            _series = 0;
            _caloriesBurned = 0;
        }

        public ExeciseItem(int ID, string exeName, string musculesName, int repetitions, int series, int caloriesBurned)
        {
            _ID = ID;
            _exeName = exeName;
            _musculesName = musculesName;
            _repetitions = repetitions;
            _series = series;
            _caloriesBurned = caloriesBurned;
        }

        public ExeciseItem(string[] exesiceArray)
        {
            _ID = int.Parse(exesiceArray[0]);
            _exeName = exesiceArray[1].ToString();
            _musculesName = exesiceArray[2].ToString();
            _repetitions = int.Parse(exesiceArray[3]);
            _series = int.Parse(exesiceArray[4]);
            _caloriesBurned = int.Parse(exesiceArray[5]);
        }

        public int ID
        {
            get { return _ID; }

            set { _ID = value; }
        }

        public string ExeName
        {
            get { return _exeName; }

            set { _exeName = value; }
        }

        public string MusculesName
        {
            get { return _musculesName; }

            set { _musculesName = value; }
        }

        public int Repetitions
        {
            get { return _repetitions; }

            set { _repetitions = value; }
        }

        public int Series
        {
            get { return _series; }

            set { _series = value; }
        }

        public int CaloriesBurned
        {
            get { return _caloriesBurned; }

            set { _caloriesBurned = value; }
        }
    }
}
