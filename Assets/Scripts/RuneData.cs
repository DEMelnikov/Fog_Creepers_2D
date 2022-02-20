using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneData : MonoBehaviour
{
    //[SerializeField] float range = 1.5f;
    [SerializeField] private float _awakingStatus = 0f;
    [SerializeField] private bool _isSleeping = true;
    [SerializeField] private float _chalengeRating = 5f;
    [SerializeField] private float _awakingTryDelay = 5f;

    public float GetAwakingStatus { get => _awakingStatus; }
    public float GetChalengeRating { get => _chalengeRating; }
    public bool IsSleeping { get => _isSleeping; }
    public float GetAwakingDelay { get => _awakingTryDelay; }

    public void AwakeRune()
    {
        _isSleeping = false;
    }

    public void ProgressAwaking (float amount)
    {
        _awakingStatus += amount;
        if (_awakingStatus >= 100f)
        {
            AwakeRune();
        }
    }

    //private void Update()
    //{
    //    if (_awakingStatus >= 100f)
    //    {
    //        AwakeRune();
    //    }
    //}
}


