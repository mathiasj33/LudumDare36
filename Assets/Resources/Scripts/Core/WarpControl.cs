using UnityEngine;
using System.Collections;

public class WarpControl : MonoBehaviour {

    public bool IsChoosing { get; private set; }
    public bool IsWarping { get; private set; }
    public Vector3 WarpPos { get; private set; }

    private WarpVisualsControl visuals;
    private RayCastHelper rayCastHelper;
    private Camera cam;

    private Invoker invoker;

    void Start () {
        visuals = GameObject.Find("SwordMesh").GetComponent<WarpVisualsControl>();
        rayCastHelper = GameObject.Find("Player").GetComponent<RayCastHelper>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        invoker = GameObject.Find("Main").GetComponent<Invoker>();
    }
	
	// Update is called once per frame
	void Update () {
        if (IsWarping) return;
        ApplyInput();
        if(IsChoosing)
        {
            FindWarpPoint(10);
        }
    }

    private void FindWarpPoint(float distance)  //TODO: raycasthelper wegmachen; Clean everything, noch bisschen fixen
    {
        Vector3 dir = cam.gameObject.transform.forward;
        Vector3 airPos = Vector3.zero;
        bool maxDistance = false;

        if (!Physics.Raycast(transform.position, dir, distance))
        {
            airPos = transform.position + cam.gameObject.transform.forward * distance;
            maxDistance = true;
        }
        else
        {
            airPos = GetPosAboveHit(dir);
            //if (distance == 0)
            //{
            //    Vector3 pos = transform.position;
            //    pos.y = 0;
            //    WarpPos = pos;
            //    return;
            //}

            //FindWarpPoint(distance - .05f);
        }

        Vector3 groundPos = GetGroundFromAir(airPos);
        WarpPos = groundPos;

        if(maxDistance)
        {
            dir = groundPos - transform.position;
            if(Physics.Raycast(transform.position, dir, distance))
            {
                WarpPos = GetGroundFromAir(GetPosAboveHit(dir));
            }
        }
    }

    private Vector3 GetPosAboveHit(Vector3 dir)
    {
        RaycastHit objectHit;
        Physics.Raycast(transform.position, dir, out objectHit);
        Vector3 airPos = objectHit.point;
        airPos += dir * 0.01f; //move a little bit into collider
        airPos.y = 50;
        return airPos;
    }

    private Vector3 GetGroundFromAir(Vector3 airPos)
    {
        RaycastHit hit;
        Physics.Raycast(airPos, -Vector3.up, out hit);

        if(hit.collider.gameObject.tag == "NoWarp")
        {
            Vector3 playerPosHigh = transform.position;
            playerPosHigh.y = airPos.y;

            Vector3 dir = playerPosHigh - airPos;
            dir.Normalize();
            airPos += dir / 2;
            return GetGroundFromAir(airPos);
        }

        Vector3 groundPos = hit.point;
        return groundPos;
    }

    private void ApplyInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            IsChoosing = true;
            invoker.Invoke(.05f, visuals.Begin);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1) && IsChoosing)
        {
            StartCoroutine("Warp");
            IsChoosing = false;
            visuals.End();
        }
        else if(Input.GetKeyDown(KeyCode.Q) && IsChoosing)
        {
            IsChoosing = false;
            visuals.End();
        }
    }

    private IEnumerator Warp()
    {
        IsWarping = true;
        Vector3 actualWarpPos = WarpPos + new Vector3(0, 0.9f, 0);
        Vector3 dir = actualWarpPos - transform.position;
        dir /= 5;
        while(Vector3.Distance(transform.position, actualWarpPos) > .5f)
        {
            transform.position += dir;
            yield return null;
        }
        IsWarping = false;
    }
}
