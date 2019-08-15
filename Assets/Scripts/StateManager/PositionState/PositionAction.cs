using System;
using Unidux;

namespace App
{
    public static class PositionAction
    {
        public enum ActionType
        {
            ToEast,
            ToWest,
            ToSouth,
            ToNorth,
        }
        public class Action
        {
            public ActionType type;
        }

        public static class ActionCreator
        {
            public static Action ToEast() => new Action(){type = ActionType.ToEast};
            public static Action ToWest() => new Action(){type = ActionType.ToWest};
            public static Action ToSouth() => new Action(){type = ActionType.ToSouth};
            public static Action ToNorth() => new Action(){type = ActionType.ToNorth};
        }

        public class Reducer : ReducerBase<State, Action>
        {
            public override State Reduce(State state, Action action)
            {
                switch (action.type)
                {
                    case ActionType.ToEast:
                        state.positionState.position = PositionState.Position.East;
                        break;
                    case ActionType.ToWest:
                        state.positionState.position = PositionState.Position.West;
                        break;
                    case ActionType.ToSouth:
                        state.positionState.position = PositionState.Position.South;
                        break;
                    case ActionType.ToNorth:
                        state.positionState.position = PositionState.Position.North;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                return state;
            }
        }
    }
}
