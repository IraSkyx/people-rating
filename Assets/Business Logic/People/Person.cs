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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this))
                return true;
            if (ReferenceEquals(obj, null))
                return false;
            if (ReferenceEquals(obj.GetType(), GetType()))
                return false;

            return Equals(obj as Person);
        }

        public bool Equals(Person person)
        {
            return person.completeName.Equals(completeName)
                && person.age.Equals(age)
                && person.occupation.Equals(occupation)
                && person.comment.Equals(comment);
        }

        public override string ToString()
        {
            return $"{completeName}\n{age}\n{occupation}\n{comment}\n";
        }
    }
}
