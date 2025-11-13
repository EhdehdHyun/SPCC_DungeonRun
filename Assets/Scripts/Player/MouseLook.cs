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
        public Vector3 targetOffset = new Vector3(0, 1.6f, 0); //카메라 y값 수정 기능

        [Header("회전 반경")]
        public float minXAngle = -30f;
        public float maxXAngle = 60f;

        [Header("시점 변경")]
        public KeyCode toggleViewKey = KeyCode.V;
        public float fpsDistance = 0.1f;
        public float tpsDistance = 5f;
        public Camera FPVCam;
        public UIHotbar hotbar;

        private bool isFps = false;

        private float xRotation;
        private float yRotation;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 angles = transform.eulerAngles;
            xRotation = angles.x;
            yRotation = angles.y;
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleViewKey))
            {
                isFps = !isFps;
                distance = isFps ? fpsDistance : tpsDistance;

                FPVCam.enabled = isFps;
                hotbar.isFPS = isFps;

                hotbar.SelectSlot(hotbar.selectedIndex);
            }
        }

        void LateUpdate()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            yRotation += mouseX;
            xRotation = Mathf.Clamp(xRotation, minXAngle, maxXAngle);

            Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);

            Vector3 targetPosition = target.position + targetOffset; // 기준점을 머리쪽으로
            Vector3 cameraPosition = targetPosition - rotation * Vector3.forward * distance;

            transform.position = cameraPosition;
            transform.LookAt(targetPosition);

            target.parent.rotation = Quaternion.Euler(0, yRotation, 0);
        }
    }
}
