using UnityEngine;
using System.Collections;

public class SwordControl : MonoBehaviour
{
    private SwordAnimationControl animControl;
    private Camera cam;
    private GameObject player;
    private Invoker invoker;

    void Start()
    {
        animControl = GetComponent<SwordAnimationControl>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        invoker = GameObject.Find("Main").GetComponent<Invoker>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animControl.PlayAnim();
            invoker.Invoke(.3f, () =>
            {
                RaycastHit hit;
                if(Physics.Raycast(player.transform.position, cam.gameObject.transform.forward, out hit, 1.5f))
                {
                    EnemyControl enemyControl = hit.collider.gameObject.GetComponent<EnemyControl>();
                    if (enemyControl != null) enemyControl.Die();
                }
            });
        }
    }
}
