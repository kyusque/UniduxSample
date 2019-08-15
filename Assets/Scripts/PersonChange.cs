using System;
using System.Collections;
using System.Collections.Generic;
using App;
using UniRx;
using UnityEngine;

public class PersonChange : MonoBehaviour
{
    public GameObject kohaku;
    public GameObject misaki;
    public GameObject yuko;

    private void OnEnable()
    {
        App.Unidux
            .Subject
            .TakeUntilDisable(this)
            .StartWith(App.Unidux.State)
            .Subscribe(state =>
            {
                switch (state.personState.person)
                {
                    case PersonState.Person.Kohaku:
                        kohaku.SetActive(true);
                        misaki.SetActive(false);
                        yuko.SetActive(false);
                        break;
                    case PersonState.Person.Misaki:
                        kohaku.SetActive(false);
                        misaki.SetActive(true);
                        yuko.SetActive(false);
                        break;
                    case PersonState.Person.Yuko:
                        kohaku.SetActive(false);
                        misaki.SetActive(false);
                        yuko.SetActive(true);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }).AddTo(this);
    }
}
