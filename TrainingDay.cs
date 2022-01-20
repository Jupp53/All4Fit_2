
namespace All4Fit
{
    // obiekt opisujący dzień treningowy, zawiera on obiekty 5 ćwiczeń
    class TrainingDay
    {
        private ExeciseItem _exe1;
        private ExeciseItem _exe2;
        private ExeciseItem _exe3;
        private ExeciseItem _exe4;
        private ExeciseItem _exe5;

        public TrainingDay()
        { }

        public TrainingDay(ExeciseItem[] training4day)
        {
            _exe1 = training4day[0];
            _exe2 = training4day[1];
            _exe3 = training4day[2];
            _exe4 = training4day[3];
            _exe5 = training4day[4];
        }

        public ExeciseItem Exe1
        {
            get { return _exe1; }

            set { _exe1 = value; }
        }

        public ExeciseItem Exe2
        {
            get { return _exe2; }

            set { _exe2 = value; }
        }

        public ExeciseItem Exe3
        {
            get { return _exe3; }

            set { _exe3 = value; }
        }
        public ExeciseItem Exe4
        {
            get { return _exe4; }

            set { _exe4 = value; }
        }
        public ExeciseItem Exe5
        {
            get { return _exe5; }

            set { _exe5 = value; }
        }
    }
}
