using UnityEngine;
using System.Collections;

public class EnemyAnimationControl : MonoBehaviour {

    public GameObject chest;

    private Animator animator;
    private CharacterController characterController;
    private Camera cam;
    private ParticleSystem blood;

    void Start () {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        blood = GameObject.Find("Blood").GetComponent<ParticleSystem>();
    }
	
	void Update () {
	
	}

    public void PlayDeathAnim()
    {
        Destroy(characterController);
        animator.enabled = false;
        blood.transform.position = GameObject.Find("BloodPos").transform.position;
        blood.transform.rotation = GameObject.Find("BloodPos").transform.rotation;
        blood.Play();
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
        }
        chest.GetComponent<Rigidbody>().AddForce(cam.transform.forward * 100, ForceMode.Impulse);
        Destroy(this);
    }
}
