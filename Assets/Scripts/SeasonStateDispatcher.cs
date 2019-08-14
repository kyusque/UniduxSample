using System.Collections;
using System.Collections.Generic;
using App;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class SeasonStateDispatcher : MonoBehaviour
{
    public ChangeSeason.Action action = ChangeSeason.ActionCreator.ToSummer();
    
    void Start()
    {
        this.GetComponent<Button>()
            .OnClickAsObservable()
            .Subscribe(_ =>
            {
                action = ChangeSeason.ActionCreator.Change(action.ActionType);
                App.Unidux.Store.Dispatch(action);
            })
            .AddTo(this);
    }
}
