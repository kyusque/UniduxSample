using System;
using Unidux;

namespace App
{
    [Serializable]
    public class PositionState : StateElement
    {
        public Position position;

        public enum Position
        {
            East,
            West,
            South,
            North,
        }
    }
}