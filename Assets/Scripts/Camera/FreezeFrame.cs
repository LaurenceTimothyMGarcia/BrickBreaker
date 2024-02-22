using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeFrame : MonoBehaviour
{

    [SerializeField] private float freezeDuration = 0.25f;
    [SerializeField] private float freezeAmount = 0.25f;

    private float pendingFreezeDur = 0f;
    private bool isFrozen = false;

    void Update()
    {
        if (pendingFreezeDur > 0 && !isFrozen)
        {
            StartCoroutine(DoFreeze());
        }
    }

    public void Freeze()
    {
        pendingFreezeDur = freezeDuration;
    }

    IEnumerator DoFreeze()
    {
        isFrozen = true;

        float ogTimeScale = Time.timeScale;

        Time.timeScale = freezeAmount;

        yield return new WaitForSecondsRealtime(freezeDuration);

        Time.timeScale = ogTimeScale;
        pendingFreezeDur = 0;
        
        isFrozen = false;
    }
}
