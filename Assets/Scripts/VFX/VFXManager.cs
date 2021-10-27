using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VFXManager : MonoBehaviour
{
    public static VFXManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void VFXEffect(GameObject VFX, float duration)
    {
        StartCoroutine(OnVFX(VFX, duration));
    }
    
    IEnumerator OnVFX(GameObject VFX, float duration)
    {
        yield return new WaitForSeconds(1);
        VFX.SetActive(true);
        VFX.GetComponent<VisualEffect>().SendEvent("OnPlay");
        //VFX.SetActive(true);
        yield return new WaitForSeconds(duration);
        VFX.GetComponent<VisualEffect>().SendEvent("OnStop");
    }
}
public abstract class VFXOutputEventTeleportObject : VFXOutputEventAbstractHandler
{
    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        
    }
}

