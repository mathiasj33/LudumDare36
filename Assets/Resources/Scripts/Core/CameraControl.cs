using UnityEngine;
using System.Collections;
using System.IO;
using System;

public class CameraControl : MonoBehaviour
{
    public GameObject Camera { get { return gameObject; } }

    private PlayerControl playerControl;

    private float rotationY = 0.0f;
    private float rotationX = 0.0f;
    private float rotationZ = 0.0f;

    private float yOffset = 0.9f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();

        rotationY = transform.localRotation.eulerAngles.y;
    }

    void Update()
    {
        rotationY += Input.GetAxis("Mouse X") * (Globals.Sensitivity / 10);
        rotationX += Input.GetAxis("Mouse Y") * (Globals.Sensitivity / 10);
        rotationX = Mathf.Clamp(rotationX, -70, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationX, Vector3.left);
        transform.localRotation *= Quaternion.AngleAxis(rotationZ, Vector3.forward);

        Vector2 headBob = CalculateHeadBob(playerControl.Moving);
        Vector3 offset = transform.right * headBob.x;
        offset.y = yOffset + headBob.y;
        transform.position = playerControl.gameObject.transform.position + offset;
    }

    private Vector2 CalculateHeadBob(bool moving)
    {
        Vector2 bob = new Vector2();
        float period = moving ? 8 : 3.5f;

        bob.x = (float)Mathf.Sin(Time.time * period) * .07f;
        bob.y = (float)Mathf.Abs(Mathf.Cos(Time.time * period)) * .07f;

        if (!moving)
        {
            bob /= 2;
            bob.y = 0;
        }
        if (playerControl.Crouched) bob /= 2;
        return bob;
    }

    public void Crouch()
    {
        StopCoroutine("DoStandUp");
        StartCoroutine("DoCrouch");
    }

    public void StandUp()
    {
        StopCoroutine("DoCrouch");
        StartCoroutine("DoStandUp");
    }

    private IEnumerator DoCrouch()
    {
        while(yOffset > 0f)
        {
            yOffset -= .05f;
            yield return null;
        }
        yOffset = 0f;
    }

    private IEnumerator DoStandUp()
    {
        while (yOffset < 0.9f)
        {
            yOffset += .05f;
            yield return null;
        }
        yOffset = 0.9f;
    }
}
