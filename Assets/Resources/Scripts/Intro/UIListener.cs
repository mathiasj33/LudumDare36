using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIListener : MonoBehaviour {

    private AudioSource clickSound;

    public void Start()
    {
        clickSound = GameObject.Find("Sounds").GetComponents<AudioSource>()[2];
    }

    public void OnContinueClick()
    {
        clickSound.Play();
        LoadLevel(1);
    }

	private void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
}
