using System;
using Unidux;

namespace App
{
    public static class ChangeSeason
    {
        public enum ActionType
        {
            ToWinter,
            ToSummer,
        }
        
        public class Action
        {
            public ActionType ActionType;
        }

        public static class ActionCreator
        {
            public static Action Create(ActionType type) => new Action{ActionType = type};

            public static Action Change(ActionType type)
            {
                switch (type)
                {
                    case ActionType.ToWinter:
                        return ToSummer();
                        break;
                    case ActionType.ToSummer:
                        return ToWinter();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type), type, null);
                }
            }
            public static Action ToSummer() => new Action {ActionType = ActionType.ToSummer};
            public static Action ToWinter() => new Action {ActionType = ActionType.ToWinter};
        }

        public class Reducer : ReducerBase<State, Action>
        {
            public override State Reduce(State state, Action action)
            {
                switch (action.ActionType)
                {
                    case ActionType.ToWinter:
                        state.seasonState.season = SeasonState.Season.Winter;
                        break;
                    case ActionType.ToSummer:
                        state.seasonState.season = SeasonState.Season.Summer;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return state;
            }
        }
    }
}
