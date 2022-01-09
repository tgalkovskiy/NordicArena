
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.VFX.Utility;

public class VFXManager : MonoBehaviour
{
    public ParticleSystem blood;

    public void PlayVFXBlood()
    {
        blood.Play();
    }
    
    [SerializeField] private ParticleSystem[] _vfx; 
    public void VFXEffect(int index)
    {
        _vfx[index].Play();
    }
    /*IEnumerator OnVFX(GameObject VFX, float duration)
    {
        yield return new WaitForSeconds(1);
        VFX.SetActive(true);
        VFX.GetComponent<VisualEffect>().SendEvent("OnPlay");
        //VFX.SetActive(true);
        yield return new WaitForSeconds(duration);
        VFX.GetComponent<VisualEffect>().SendEvent("OnStop");
    }*/
}
public abstract class VFXOutputEventTeleportObject : VFXOutputEventAbstractHandler
{
    public override void OnVFXOutputEvent(VFXEventAttribute eventAttribute)
    {
        
    }
}

