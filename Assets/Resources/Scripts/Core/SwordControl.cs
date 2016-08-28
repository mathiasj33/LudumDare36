using UnityEngine;
using System.Collections;

public class SwordControl : MonoBehaviour
{
    private SwordAnimationControl animControl;
    private RayCastHelper rayCastHelper;
    private Camera cam;
    private GameObject player;
    private Invoker invoker;

    void Start()
    {
        animControl = GetComponent<SwordAnimationControl>();
        rayCastHelper = GameObject.Find("Player").GetComponent<RayCastHelper>();
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
                GameObject hit = rayCastHelper.GetInFrontOf(cam.gameObject.transform.forward);
                Vector3 hitPos = hit.transform.position;
                hitPos.y = 0;
                Vector3 playerPos = player.transform.position;
                playerPos.y = 0;
                if (hit != null && Vector3.Distance(playerPos, hitPos) < 1.4f)
                {
                    EnemyControl enemyControl = hit.GetComponent<EnemyControl>();
                    if (enemyControl != null) enemyControl.Die();
                }
            });
        }
    }
}
