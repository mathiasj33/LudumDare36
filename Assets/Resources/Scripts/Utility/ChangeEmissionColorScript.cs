using UnityEngine;
using System.Collections;

public class ChangeEmissionColorScript : MonoBehaviour {

    public Color Begin;
    public Color End;

	void Start () {
        StartCoroutine("ChangeEmissionColor");
	}
	
    private IEnumerator ChangeEmissionColor()
    {
        Material mat = gameObject.GetComponent<SkinnedMeshRenderer>().material;

        for (int i = 0; i < 20; i++)
        {
            mat.SetColor("_EmissionColor", Color.Lerp(Begin, End, ((float) i / 20)));
            yield return null;
        }
        Destroy(this);
    }
}
