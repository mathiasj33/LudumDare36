using UnityEngine;
using System.Collections;

public class WarpVisualsControl : MonoBehaviour {

    private WarpControl warpControl;

    private Color begin = new Color(0.26f, 0.2f, 0);
    private Color end = new Color(1.4f, 1.13f, 0);
    private ChangeEmissionColorScript changeColorScript;

    private GameObject warpCircle;
    private FadeInOutScript fadeScript;
	
    void Start()
    {
        warpControl = GameObject.Find("Player").GetComponent<WarpControl>();
        warpCircle = GameObject.Find("WarpCircle");

        gameObject.GetComponent<SkinnedMeshRenderer>().material.SetColor("_EmissionColor", begin);
    }

    void Update()
    {
        if(warpControl.IsChoosing)
        {
            warpCircle.transform.position = warpControl.WarpPos;
        }
    }

	public void Begin()
    {
        if (fadeScript != null) Destroy(fadeScript);
        fadeScript = warpCircle.AddComponent<FadeInOutScript>();
        fadeScript.Begin("FadeIn");

        if (changeColorScript != null) Destroy(changeColorScript);
        changeColorScript = gameObject.AddComponent<ChangeEmissionColorScript>();
        changeColorScript.Begin = begin;
        changeColorScript.End = end;
    }

    public void End()
    {
        if (fadeScript != null) Destroy(fadeScript);
        fadeScript = warpCircle.AddComponent<FadeInOutScript>();
        fadeScript.Begin("FadeOut");

        if (changeColorScript != null) Destroy(changeColorScript);
        changeColorScript = gameObject.AddComponent<ChangeEmissionColorScript>();
        changeColorScript.Begin = end;
        changeColorScript.End = begin;
    }
}
