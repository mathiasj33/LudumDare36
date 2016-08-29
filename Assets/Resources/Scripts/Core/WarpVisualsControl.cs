using UnityEngine;
using System.Collections;

public class WarpVisualsControl : MonoBehaviour {

    private WarpControl warpControl;

    private Color begin = new Color(0.26f, 0.2f, 0);
    private Color end = new Color(1.4f, 1.13f, 0);
    private ChangeEmissionColorScript changeColorScript;

    private GameObject warpCircle;
    private GameObject circle;
    private GameObject circle2;
    private GameObject down;
    private FadeInOutScript fadeScript;
	
    void Start()
    {
        warpControl = GameObject.Find("Player").GetComponent<WarpControl>();
        warpCircle = GameObject.Find("WarpCircle");
        down = warpCircle.transform.GetChild(0).gameObject;
        circle = warpCircle.transform.GetChild(1).gameObject;
        circle2 = warpCircle.transform.GetChild(2).gameObject;

        gameObject.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", begin);
    }

    void Update()
    {
        if (warpControl.IsChoosing)
        {
            warpCircle.transform.position = warpControl.WarpPos;
            if (!warpControl.Blocked)
            {
                SetAlpha(down, 1);
                SetAlpha(circle2, 1);
                Vector3 pos = circle2.transform.position;
                pos.y = warpControl.NotBlockedY;
                circle2.transform.position = pos;
            }
            else
            {
                SetAlpha(down, 0);
                SetAlpha(circle2, 0);
            }
        }
    }

    private void SetAlpha(GameObject go, float a)
    {
        Material mat = go.GetComponent<MeshRenderer>().material;
        Color color = mat.GetColor("_Color");
        mat.SetColor("_Color", new Color(color.r, color.g, color.b, a));
    }

    public void Begin()
    {
        if (fadeScript != null) Destroy(fadeScript);
        fadeScript = circle.AddComponent<FadeInOutScript>();
        fadeScript.Begin("FadeIn");

        if (changeColorScript != null) Destroy(changeColorScript);
        changeColorScript = gameObject.AddComponent<ChangeEmissionColorScript>();
        changeColorScript.Begin = begin;
        changeColorScript.End = end;
    }

    public void End()
    {
        SetAlpha(down, 0);
        SetAlpha(circle2, 0);

        if (fadeScript != null) Destroy(fadeScript);
        fadeScript = circle.AddComponent<FadeInOutScript>();
        fadeScript.Begin("FadeOut");

        if (changeColorScript != null) Destroy(changeColorScript);
        changeColorScript = gameObject.AddComponent<ChangeEmissionColorScript>();
        changeColorScript.Begin = end;
        changeColorScript.End = begin;
    }
}
