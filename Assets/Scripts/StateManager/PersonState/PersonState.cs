using System;
using Unidux;

namespace App
{
    [Serializable]
    public class PersonState : StateElement
    {
        public Person person;
        public enum Person
        {
            Kohaku,
            Misaki,
            Yuko
        }
    }
}