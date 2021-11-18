using UnityEngine;
public class DamageDiller : MonoBehaviour
{
    public Transform pos;
    public View _view;
    private RaycastHit[] _hits;
    public void Damage()
    {
        _hits = Physics.SphereCastAll(pos.transform.position, _view._stats._distanceSkill, Vector3.forward, _view._stats._distanceSkill);
        foreach (var I in _hits)
        {
            if (I.collider.gameObject.GetComponent<View>() && I.collider.gameObject.GetComponent<View>()!=_view)
            {
                I.collider.gameObject.GetComponent<View>().GetDamage(10);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f,0.1f,0.1f,0.5f);
        Gizmos.DrawSphere(pos.transform.position, _view._stats._distanceSkill);
    }
}
