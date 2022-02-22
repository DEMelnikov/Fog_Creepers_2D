using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RuneData : MonoBehaviour
{
    [SerializeField] private float _awakingStatus = 0f;
    [SerializeField] private float _pointsToAwake = 100f;
    [SerializeField] private bool _isSleeping = true;
    [SerializeField] private float _chalengeRating = 5f;
    [SerializeField] private float _awakingTryDelay = 5f;
    [SerializeField] private GameObject _progressBar;
    [SerializeField] private Image _filler;

    //private float _collisionsRange = 2;

    public float GetAwakingStatus { get => _awakingStatus; }
    public float GetChalengeRating { get => _chalengeRating; }
    public bool IsSleeping { get => _isSleeping; }
    public float GetAwakingDelay { get => _awakingTryDelay; }

    public void Awake()
    {
        _progressBar.SetActive(false);
    }


    public void AwakeRune()
    {
        _isSleeping = false;
    }

    public void ProgressAwaking (float amount)
    {
        _awakingStatus += amount;
        FillProgressBar();

        if (_awakingStatus >= 100f)
        {
            AwakeRune();
            Sprite awakenSprite = Resources.Load<Sprite>("Sprites/AwakenRune");
            GetComponentInParent<SpriteRenderer>().sprite = awakenSprite;
            _progressBar.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isSleeping && collision.gameObject.TryGetComponent<HeroAI>(out HeroAI hero) && hero.IsMage)
        {
            _progressBar.SetActive(true);
            FillProgressBar();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isSleeping && collision.gameObject.TryGetComponent<HeroAI>(out HeroAI hero) && hero.IsMage)
        {
            _progressBar.SetActive(false);
        }
    }

    private void FillProgressBar()
    {
        _filler.GetComponent<RuneProgressBar>().UpdateProgress(_awakingStatus, _pointsToAwake);
    }
}


