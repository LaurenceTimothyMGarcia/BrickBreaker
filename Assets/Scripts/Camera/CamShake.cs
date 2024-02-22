using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CamShake : MonoBehaviour
{
    public static CamShake Instance;

    [SerializeField] private float duration;
    [SerializeField] private float posStrength;
    [SerializeField] private float rotStrength;

    private void Awake() => Instance = this;

    private void OnShake(float brickCount)
    {
        transform.DOComplete();
        transform.DOShakePosition(duration, posStrength * brickCount);
        transform.DOShakeRotation(duration, rotStrength * brickCount);
    }

    public static void Shake(float brickCount) => Instance.OnShake(brickCount);
}
