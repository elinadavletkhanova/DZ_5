namespace latypova_5.Structs
{
    public struct Student
    {
        public string Surname;
        public string Name;
        public int BirthYear;
        public string Exam;
        public int Score;


        public Student(string surname, string name, int birthYear, string exam, int score)
        {
            Surname = surname;
            Name = name;
            BirthYear = birthYear;
            Exam = exam;
            Score = score;
        }

        public string PrintStudent()
        {
            return $"{Surname} {Name}, {BirthYear}г., экзамен - {Exam}, баллы - {Score}";
        }
    }    
}