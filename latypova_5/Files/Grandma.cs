using System.Collections.Generic;

namespace latypova_5.Files
{
    public class Grandma //классы, а не структуры для того, чтобы изменять значения
    {
        public string Name;
        public byte Age;
        public List<Disease> Diseases;
        public string NameOfHospital;

        public Grandma(string name, byte age, List<Disease> diseases)
        {
            Name = name;
            Age = age;
            Diseases = diseases;
            NameOfHospital = "Не в больнице (не лечится)";
        }
    }

    public class Disease
    {
        public string Name;
        public string MedicationName;

        public Disease(string name, string medicationName)
        {
            Name = name;
            MedicationName = medicationName;
        }
    }

    public class Hospital
    {
        public string Name;
        public List<string> ListOfDiseases;
        public int Capacity;
        public int CountNow;

        public Hospital(string name, List<string> listOfDiseases, int capacity)
        {
            Name = name;
            ListOfDiseases = listOfDiseases;
            Capacity = capacity;
            CountNow = 0;
        }
    }
}