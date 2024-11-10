using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFXAsh : MonoBehaviour
{
    private SpriteRenderer sr_;

    [Header("Flash FX")]
    [SerializeField] private float flashDuration_;
    [SerializeField] private Material hitMat_;
    private Material originalMat_;

    private void Start()
    {
        sr_ = GetComponentInChildren<SpriteRenderer>();
        originalMat_ = sr_.material;
    }

    private IEnumerator FlashFX()
    {
        sr_.material = hitMat_;
        yield return new WaitForSeconds(flashDuration_);
        sr_.material = originalMat_;
    }

    private void RedColorBlink()
    {
        if (sr_.color != Color.white)
            sr_.color = Color.white;
        else
            sr_.color = Color.red;
    }

    private void CancelRedBlink()
    {
        CancelInvoke("RedColorBlink");
        sr_.color = Color.white;
    }
}
