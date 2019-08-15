using System;
using Unidux;

namespace App
{
    [Serializable]
    public class ClothState : StateElement
    {
        public Cloth cloth;
        
        public enum Cloth
        {
            Summer,
            Winter,
        }
    }
}