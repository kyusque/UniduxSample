using System;
using Unidux;

namespace App
{
    public static class PersonAction
    {
        public enum ActionType
        {
            ChangeToKohaku,
            ChangeToMisaki,
            ChangeToYuko,
        }
        
        public class Action
        {
            public ActionType type;
        }

        public static class ActionCreator
        {
            public static Action ChangeToNextPerson(State state)
            {
                switch (state.personState.person)
                {
                    case PersonState.Person.Kohaku:
                        return new Action(){type = ActionType.ChangeToMisaki};
                    case PersonState.Person.Misaki:
                        return new Action(){type = ActionType.ChangeToYuko};
                    case PersonState.Person.Yuko:
                        return new Action(){type = ActionType.ChangeToKohaku};
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
                    case ActionType.ChangeToKohaku:
                        state.personState.person = PersonState.Person.Kohaku;
                        break;
                    case ActionType.ChangeToMisaki:
                        state.personState.person = PersonState.Person.Misaki;
                        break;
                    case ActionType.ChangeToYuko:
                        state.personState.person = PersonState.Person.Yuko;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
                return state;
            }
        }
    }
}
