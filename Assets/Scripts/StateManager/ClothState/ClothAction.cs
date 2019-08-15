using System;
using Unidux;

namespace App
{
    public static class ClothAction
    {
        public enum ActionType
        {
            ToSummer,
            ToWinter,
        }
        
        public class Action
        {
            public ActionType type;
        }

        public static class ActionCreator
        {
            public static Action ChangeCloth(State state)
            {
                switch (state.clothState.cloth)
                {
                    case ClothState.Cloth.Summer:
                        return new Action(){type = ActionType.ToWinter};
                    case ClothState.Cloth.Winter:
                        return new Action(){type = ActionType.ToSummer};
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public class Reducer : ReducerBase<State, Action>
        {
            public override State Reduce(State state, Action action)
            {
                switch (action.type)
                {
                    case ActionType.ToSummer:
                        state.clothState.cloth = ClothState.Cloth.Summer;
                        break;
                    case ActionType.ToWinter:
                        state.clothState.cloth = ClothState.Cloth.Winter;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return state;
            }
        }
    }
}
