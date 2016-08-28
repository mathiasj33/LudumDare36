using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

    private EnemyAnimationControl animControl;

	void Start () {
        animControl = GetComponent<EnemyAnimationControl>();
    }
	
	void Update () {
	
	}

    public void Die()
    {
        animControl.PlayDeathAnim();
        Destroy(this);
    }
}
