using System;
using System.Collections;
using System.Collections.Generic;
using App;
using UniRx;
using UnityEngine;

public class ClothChange : MonoBehaviour
{
    public GameObject summerStyle;
    public GameObject winterStyle;
    void Start()
    {
        App.Unidux
            .Subject
            .TakeUntilDisable(this)
            .StartWith(App.Unidux.State)
            .Subscribe(state =>
            {
                switch (state.seasonState.season)
                {
                    case SeasonState.Season.Summer:
                        summerStyle.SetActive(true);
                        winterStyle.SetActive(false);
                        break;
                    case SeasonState.Season.Winter:
                        winterStyle.SetActive(true);
                        summerStyle.SetActive(false);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            })
            .AddTo(this);

    }
}
