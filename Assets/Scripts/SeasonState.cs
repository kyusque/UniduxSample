using System;
using Unidux;

namespace App
{
    [Serializable]
    public class SeasonState : StateElement
    {
        public Season season;

        public enum Season
        {
            Summer,
            Winter,
        }
    }
}