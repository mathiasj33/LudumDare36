using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public bool Moving { get; private set; }
    public bool Crouched { get; set; }
    public float speed = 1;

    private CameraControl camControl;
    private CharacterController characterControl;
    private float yMovement;

    private WarpControl warpControl;

    void Start()
    {
        camControl = GameObject.Find("Main Camera").GetComponent<CameraControl>();
        characterControl = gameObject.GetComponent<CharacterController>();
        warpControl = GetComponent<WarpControl>();
    }

    void Update()
    {
        if (warpControl.IsWarping) return;
        transform.forward = camControl.Camera.transform.forward;
        ApplyCrouch();
        characterControl.Move(CalculateMovementVector() * Time.deltaTime * 60);
    }

    private void ApplyCrouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!Crouched) camControl.Crouch();
            else camControl.StandUp();
            Crouched = !Crouched;
        }
    }

    private Vector3 CalculateMovementVector()
    {
        ApplyGravity();
        ApplyJump();
        Vector3 inputVec = GetInputVector();
        return inputVec + new Vector3(0, yMovement, 0);
    }

    private void ApplyGravity()
    {
        if (!characterControl.isGrounded)
            yMovement -= 9.81f * Time.deltaTime / 20;
        else
            yMovement = -9.81f * 0.016f / 20;
    }

    private void ApplyJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!characterControl.isGrounded) return;
            float factor = 75f;
            yMovement += factor * 0.0016f;
        }
    }

    private Vector3 GetInputVector()
    {
        Vector3 dir = Vector3.zero;
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        if (Util.FloatEquals(vertical, 0) && Util.FloatEquals(horizontal, 0)) Moving = false;
        else Moving = true;

        if (vertical > 0) dir += transform.forward;
        else if (vertical < 0) dir -= transform.forward * .3f;

        if (horizontal > 0) dir += transform.right * .6f;
        else if (horizontal < 0) dir -= transform.right * .6f;

        dir.y = 0;
        if (vertical > 0) dir.Normalize();

        float factor = Crouched ? 3f : 6f;
        dir *= speed * factor * 0.016f;
        return dir;
    }
}
