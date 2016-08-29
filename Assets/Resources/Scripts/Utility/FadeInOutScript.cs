using UnityEngine;
using System.Collections;

public class FadeInOutScript : MonoBehaviour {

	public void Begin(string routineName)
    {
        StartCoroutine(routineName);
    }

    private IEnumerator FadeIn()
    {
        float a = 0;
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        Color color = mat.GetColor("_Color");
        while (a < 1)
        {
            a += .05f;
            mat.SetColor("_Color", new Color(color.r, color.g, color.b, a));
            yield return null;
        }
        Destroy(this);
    }

    private IEnumerator FadeOut()
    {
        float a = 1;
        Material mat = gameObject.GetComponent<MeshRenderer>().material;
        Color color = mat.GetColor("_Color");
        while (a > 0)
        {
            a -= .05f;
            mat.SetColor("_Color", new Color(color.r, color.g, color.b, a));
            yield return null;
        }
        Destroy(this);
    }
}
