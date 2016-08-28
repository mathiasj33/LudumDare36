using UnityEngine;
using System.Collections;

namespace Scripts
{
    public class FlyCameraControl : MonoBehaviour
    {
        public float cameraSensitivity = 90;
        public float moveSpeed = 10;
        private float rotationY = 0.0f;
        private float rotationX = 0.0f;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            rotationY = transform.rotation.eulerAngles.y;
        }

        void Update()
        {
            rotationY += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;
            rotationX += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(rotationY, Vector3.up);
            transform.localRotation *= Quaternion.AngleAxis(rotationX, Vector3.left);

            transform.position += transform.forward * moveSpeed * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            transform.position += transform.right * moveSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        }
    }
}