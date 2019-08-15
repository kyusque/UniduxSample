using System;
using System.Collections;
using System.Collections.Generic;
using App;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Dispatcher : MonoBehaviour
{
    private void Start()
    {
        this.UpdateAsObservable()
            .TakeUntilDisable(this)
            .Where(_ => Input.anyKeyDown)
            .Subscribe(_ =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    App.Unidux.Dispatch(PersonAction.ActionCreator.ChangeToNextPerson(App.Unidux.State));
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    App.Unidux.Dispatch(ClothAction.ActionCreator.ChangeCloth(App.Unidux.State));
                }
                
                if(Input.GetKeyDown(KeyCode.RightArrow)) App.Unidux.Dispatch(PositionAction.ActionCreator.ToEast());
                if (Input.GetKeyDown(KeyCode.LeftArrow)) App.Unidux.Dispatch(PositionAction.ActionCreator.ToWest());
                if (Input.GetKeyDown(KeyCode.DownArrow)) App.Unidux.Dispatch(PositionAction.ActionCreator.ToSouth());
                if (Input.GetKeyDown(KeyCode.UpArrow)) App.Unidux.Dispatch(PositionAction.ActionCreator.ToNorth());


            });
    }
}
