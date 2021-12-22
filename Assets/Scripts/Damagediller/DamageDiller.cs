using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class DamageDiller : MonoBehaviour
{
    public GameObject prefabBow;
    public Transform pos;
    public View _view;
    private RaycastHit[] _hits;
    public void Damage(Stats _stats)
    {
        if(_stats._TypeAttack == TypeAttack.Combat)
        {
            StartCoroutine(ICombatAttack(_stats._damage, _stats._delayDamage));
        }
        if (_stats._TypeAttack == TypeAttack.Bow)
        {
            StartCoroutine(IBowAttack(_stats._damage, _stats._delayDamage));
        }
    }
    IEnumerator ICombatAttack(float damage, float delayDamage)
    {
        yield return new WaitForSeconds(delayDamage);
        _hits = Physics.SphereCastAll(pos.transform.position, _view._stats._distanceSkill, Vector3.forward, _view._stats._distanceSkill);
        foreach (var I in _hits)
        {
            if (I.collider.gameObject.GetComponent<View>() && I.collider.gameObject.GetComponent<View>()!=_view)
            {
                I.collider.gameObject.GetComponent<View>().GetDamage(damage);
            }
        }
    }
    IEnumerator IBowAttack(float damage, float delayDamage)
    {
        yield return new WaitForSeconds(delayDamage);
        var Arrow = Instantiate(prefabBow, pos.position + new Vector3(0,1,0), transform.rotation*quaternion.Euler(6.2f,0,0));
        Arrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*1500);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f,0.1f,0.1f,0.5f);
        Gizmos.DrawSphere(pos.transform.position, _view._stats._distanceSkill);
    }
}
