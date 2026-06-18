using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ET
{
    [EnableClass]
    public sealed class CameraControl : MonoBehaviour
    {
        public float MinSize = 9.6f;
        public float MaxSize = 25.0f;

        public float OrthographicSize
        {
            get
            {
                this.EnsureCamera();
                if (this.m_VirtualCamera)
                    return this.m_VirtualCamera.m_Lens.OrthographicSize;
                if (this.m_Camera)
                    return this.m_Camera.orthographicSize;
                return 9.6f;
            }
            set
            {
                this.EnsureCamera();
                if (this.m_VirtualCamera)
                {
                    this.m_VirtualCamera.m_Lens.OrthographicSize = value;
                    this.InvalidateCache();
                    return;
                }
                if (this.m_Camera)
                {
                    this.m_Camera.orthographicSize = value;
                    return;
                }
            }
        }

        private Camera m_Camera;
        private CinemachineVirtualCamera m_VirtualCamera;

        private void Start()
        {
            this.EnsureCamera();
        }

        public void OnScale(InputAction.CallbackContext callbackContext)
        {
            this.EnsureCamera();
            if (callbackContext.phase == InputActionPhase.Performed)
            {
                Vector2 delta = callbackContext.ReadValue<Vector2>();
                if (delta.y != 0)
                {
                    float oldOrth0Size = this.OrthographicSize;
                    float newOrthoSize = Mathf.Clamp(oldOrth0Size * (1 - delta.y / 1000f), this.MinSize, this.MaxSize);
                    this.OrthographicSize = newOrthoSize;
                }
            }
        }

        private void InvalidateCache()
        {
            CinemachineConfiner2D confiner2D = this.GetComponent<CinemachineConfiner2D>();
            if (confiner2D)
                confiner2D.InvalidateCache();
        }

        private void EnsureCamera()
        {
            if (this.m_VirtualCamera)
            {
                return;
            }
            this.m_VirtualCamera = this.GetComponent<CinemachineVirtualCamera>();
            if (this.m_Camera)
            {
                return;
            }
            this.m_Camera = this.GetComponent<Camera>();
        }
    }
}