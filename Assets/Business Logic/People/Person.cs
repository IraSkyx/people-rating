using System;

namespace Business_Logic.People
{
    [Serializable]
    public class Person
    {
        public string completeName;

        public string age;

        public string occupation;

        public string comment;

        public Person(string completeName, string age, string occupation, string comment)
        {
            this.completeName = completeName;
            this.age = age;
            this.occupation = occupation;
            this.comment = comment;
        }

        public override string ToString()
        {
            return $"{completeName}\n{age}\n{occupation}\n{comment}\n";
        }
    }
}
