
using UnityEngine;

public class MiniMapTranslate : MonoBehaviour
{
    public Transform target;

    private void LateUpdate()
    {
        
        transform.position = new Vector3(target.transform.position.x, 200, target.transform.position.z);
    }
}
