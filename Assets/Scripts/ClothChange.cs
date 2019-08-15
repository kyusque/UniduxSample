using System;
using System.Collections;
using System.Collections.Generic;
using App;
using UniRx;
using UnityEngine;

public class ClothChange : MonoBehaviour
{
    public GameObject summer;
    public GameObject winter;

    private void OnEnable()
    {
        App.Unidux.Subject
            .TakeUntilDisable(this)
            .StartWith(App.Unidux.State)
            .Subscribe(state =>
            {
                switch (state.clothState.cloth)
                {
                    case ClothState.Cloth.Summer:
                        summer.SetActive(true);
                        winter.SetActive(false);
                        break;
                    case ClothState.Cloth.Winter:
                        summer.SetActive(false);
                        winter.SetActive(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }).AddTo(this);
    }
}
