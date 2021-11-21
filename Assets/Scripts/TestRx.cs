
using System;
using UniRx;
using UnityEngine;
public class TestRx : MonoBehaviour
{
    void Start()
    {
        Observable.EveryUpdate().
            Where(x => Input.anyKeyDown).
            Select(x => Input.inputString).
            Subscribe(x => { Debug.Log(123123); }).
            AddTo(this);
    }
}
