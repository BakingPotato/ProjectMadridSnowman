using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseAnimations : MonoBehaviour
{
    [SerializeField] Button[] _bouncingBtns;

    void Start()
    {
        foreach (var btn in _bouncingBtns)
        {
            btn.onClick.AddListener(() => btn.transform.DOShakeScale(0.2f, 0.1f, 2, 0, randomnessMode: ShakeRandomnessMode.Harmonic).SetEase(Ease.OutBounce));
        }
    }
}
