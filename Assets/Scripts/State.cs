using System;
using Unidux;

namespace App
{
    [Serializable]
    public partial class State : StateBase
    {
        public SeasonState seasonState = new SeasonState(){season = SeasonState.Season.Summer};
    }
}
