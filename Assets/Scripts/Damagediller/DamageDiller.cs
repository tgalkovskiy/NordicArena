
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class DamageDiller : MonoBehaviour
{
    public GameObject prefabBow;
    public Transform pos;
    private View view;
    private RaycastHit[] _hits;

    private void Awake()
    {
        view = GetComponent<View>();
    }

    public void SetDamage(Stats stats)
    {
        switch(stats.TypeAttack)
        {
            case TypeAttack.Combat: StartCoroutine(ICombatAttack(stats)); break;
            case TypeAttack.Bow: StartCoroutine(IBowAttack(stats)); break;
            case TypeAttack.Magic: StartCoroutine(IMagicAttack(stats)); break;
        }
    }
    IEnumerator ICombatAttack(Stats stats)
    {
        yield return new WaitForSeconds(stats.delayDamage);
        _hits = Physics.SphereCastAll(pos.transform.position, view.stats.distanceSkill, Vector3.forward, view.stats.distanceSkill);
        foreach (var I in _hits)
        {
            if(I.collider.gameObject.GetComponent<View>() && I.collider.gameObject.GetComponent<View>()!=view)
            {
                I.collider.gameObject.GetComponent<View>().GetDamage(stats.damage);
            }
        }
    }
    IEnumerator IBowAttack(Stats stats)
    {
        yield return new WaitForSeconds(stats.delayDamage);
        var Arrow = Instantiate(prefabBow, pos.position + new Vector3(0,1,0), transform.rotation*quaternion.Euler(6.2f,0,0));
        Arrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward*1500);
    }

    IEnumerator IMagicAttack(Stats stats)
    {
        yield return new WaitForSeconds(stats.delayDamage);
    }
}
