using System;
using Unidux;

namespace App
{
    [Serializable]
    public partial class State : StateBase
    {
        public PersonState personState = new PersonState(){person = PersonState.Person.Kohaku};
        public ClothState clothState = new ClothState(){cloth = ClothState.Cloth.Summer};
        public PositionState positionState = new PositionState(){position = PositionState.Position.East};
    }
}
