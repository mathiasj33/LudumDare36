using UnityEngine;
using System.Collections;

public class WarpControl : MonoBehaviour
{

    public bool IsChoosing { get; private set; }
    public bool IsWarping { get; private set; }
    public Vector3 WarpPos { get; private set; }
    public bool Blocked { get; private set; }
    public float NotBlockedY { get; private set; }

    private WarpVisualsControl visuals;
    private Camera cam;

    private Invoker invoker;

    void Start()
    {
        visuals = GameObject.Find("SwordMesh").GetComponent<WarpVisualsControl>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        invoker = GameObject.Find("Main").GetComponent<Invoker>();
    }

    void Update()
    {
        if (IsWarping) return;
        ApplyInput();
        if (IsChoosing)
        {
            FindWarpPoint(10);
        }
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
        else if (Input.GetKeyDown(KeyCode.Q) && IsChoosing)
        {
            IsChoosing = false;
            visuals.End();
        }
    }

    private IEnumerator Warp()
    {
        IsWarping = true;
        Vector3 actualWarpPos = WarpPos + new Vector3(0, 0.9f, 0);
        if(IsInRoom(WarpPos) && WarpPos.y >= 5)  //TODO: Beachten: Restriction -> Ceiling ab 5m. Umgehen indem Raumhöhe berechnen mit Bounds und dann hier als Varianble...
        {
            actualWarpPos -= new Vector3(0, 2, 0);
        }
        Vector3 dir = actualWarpPos - transform.position;
        dir /= 5;
        while (Vector3.Distance(transform.position, actualWarpPos) > .5f)
        {
            transform.position += dir;
            yield return null;
        }
        IsWarping = false;
    }

    private void FindWarpPoint(float distance)
    {
        Ray ray = new Ray(transform.position, cam.gameObject.transform.forward);
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, distance))
        {
            Blocked = false;
            Vector3 airPos = transform.position + cam.gameObject.transform.forward * distance;
            WarpPos = MakeValid(airPos);
        }
        else
        {
            Blocked = true;
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.tag == "Ground")
            {
                WarpPos = MakeValid(hit.point);
            }
            else if(hitObject.tag == "NoWarp")
            {
                Vector3 pos = MakeValid(hit.point);
                pos.y = 0;
                WarpPos = pos;
            }
            else if(hitObject.tag == "Ceiling")
            {
                Vector3 pos = hit.point;
                pos.y -= 1;
                WarpPos = pos;
            }
            else
            {
                WarpPos = GetOnTopOf(hit.point);
            }
        }
    }

    private Vector3 MakeValid(Vector3 pos)
    {
        Ray ray = new Ray(pos + new Vector3(0, 50, 0), -Vector3.up);
        int notCeilingMask = 1 << 8 | 1 << 2;
        notCeilingMask = ~notCeilingMask;
        RaycastHit hit;

        if (Physics.SphereCast(ray, .5f, out hit, float.MaxValue, notCeilingMask) && hit.collider.gameObject.tag == "NoWarp")
        {
            Vector3 playerPosHigh = transform.position;
            playerPosHigh.y = pos.y;

            Vector3 dir = playerPosHigh - pos;
            dir.Normalize();
            pos += dir / 5;
            return MakeValid(pos);
        }
        else
        {
            return pos;
        }
    }

    private Vector3 GetOnTopOf(Vector3 pos)
    {
        Ray ray = new Ray(pos + new Vector3(0, 50, 0), -Vector3.up);
        int notCeilingMask = 1 << 8 | 1 << 2;
        notCeilingMask = ~notCeilingMask;
        RaycastHit hit;

        if (Physics.SphereCast(ray, .5f, out hit, float.MaxValue, notCeilingMask))
        {
            return hit.point;
        }

        return Vector3.zero;
    }

    private bool IsInRoom(Vector3 pos)
    {
        Ray ray = new Ray(pos, Vector3.up);
        int ceilingMask = 1 << 8;

        return Physics.Raycast(ray, float.MaxValue, ceilingMask);
    }
}
