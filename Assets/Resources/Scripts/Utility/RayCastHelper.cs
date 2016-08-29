using UnityEngine;
using System.Collections;

public class RayCastHelper : MonoBehaviour
{
    public GameObject CastFromPlayer(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public Vector3 GetHit(Vector3 dir)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit))
        {
            return hit.point;
        }
        return Vector3.zero;
    }

    //public GameObject GetSphereUnderPlayer()
    //{
    //    RaycastHit hit;
    //    Vector3 characterMiddle = new Vector3(transform.position.x, transform.position.y + .9f, transform.position.z);
    //    if (Physics.SphereCast(characterMiddle, .5f, -Vector3.up, out hit))
    //    {
    //        return hit.collider.gameObject;
    //    }
    //    return null;
    //}
}
