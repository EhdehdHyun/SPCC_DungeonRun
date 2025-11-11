using Unity.Mathematics;
using UnityEngine;

namespace Polyperfect.Universal
{
    public class MouseLook : MonoBehaviour
    {
        [Header("카메라 설정")]
        public float mouseSensitivity = 3f; //마우스 감도
        public Transform target; //카메라가 볼 대상
        public float distance = 5f; //대상과의 거리 

        [Header("회전 반경")]
        public float minXAngle = -30f;
        public float maxXAngle = 60f;


        private float xRotation;
        private float yRotation;



        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            xRotation = angles.x;
            yRotation = angles.y;
        }

        void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);

            Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

            Vector3 tartgetPosition = target.position - rotation * Vector3.forward * distance;

            transform.position = tartgetPosition;
            transform.LookAt(target);

            target.parent.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
