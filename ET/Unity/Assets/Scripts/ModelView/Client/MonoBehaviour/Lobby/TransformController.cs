using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class TransformController : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        public void SetPositionX(float positionX)
        {
            this.transform.SetPositionX(positionX);
        }

        public void SetPositionY(float positionY)
        {
            this.transform.SetPositionY(positionY);
        }

        public void SetPositionZ(float positionZ)
        {
            this.transform.SetPositionZ(positionZ);
        }

        public void SetLocalPosition(Vector3 localPosition)
        {
            this.transform.localPosition = localPosition;
        }

        public void SetLocalPositionX(float localPositionX)
        {
            this.transform.SetLocalPositionX(localPositionX);
        }

        public void SetLocalPositionY(float localPositionY)
        {
            this.transform.SetLocalPositionY(localPositionY);
        }

        public void SetLocalPositionZ(float localPositionZ)
        {
            this.transform.SetLocalPositionZ(localPositionZ);
        }

        public void SetLocalScale(Vector3 scale)
        {
            this.transform.localScale = scale;
        }

        public void SetLocalScaleX(float scaleX)
        {
            this.transform.SetLocalScaleX(scaleX);
        }

        public void SetLocalScaleY(float scaleY)
        {
            this.transform.SetLocalScaleY(scaleY);
        }

        public void SetLocalScaleZ(float scaleZ)
        {
            this.transform.SetLocalScaleZ(scaleZ);
        }

        public void FlipX(bool flip)
        {
            this.SetLocalScaleX(flip ? -1f : 1f);
        }

        public void FlipY(bool flip)
        {
            this.SetLocalScaleY(flip ? -1f : 1f);
        }

        public void FlipZ(bool flip)
        {
            this.SetLocalScaleZ(flip ? -1f : 1f);
        }

        public void SetEulerAngles(Vector3 eulerAngles)
        {
            this.transform.eulerAngles = eulerAngles;
        }

        public void SetEulerAnglesX(float eulerAnglesX)
        {
            this.transform.SetEulerAnglesX(eulerAnglesX);
        }

        public void SetEulerAnglesY(float eulerAnglesY)
        {
            this.transform.SetEulerAnglesY(eulerAnglesY);
        }

        public void SetEulerAnglesZ(float eulerAnglesZ)
        {
            this.transform.SetEulerAnglesZ(eulerAnglesZ);
        }

        public void SetLocalEulerAngles(Vector3 localEulerAngles)
        {
            this.transform.localEulerAngles = localEulerAngles;
        }

        public void SetLocalEulerAnglesX(float localEulerAnglesX)
        {
            this.transform.SetLocalEulerAnglesX(localEulerAnglesX);
        }

        public void SetLocalEulerAnglesY(float localEulerAnglesY)
        {
            this.transform.SetLocalEulerAnglesY(localEulerAnglesY);
        }

        public void SetLocalEulerAnglesZ(float localEulerAnglesZ)
        {
            this.transform.SetLocalEulerAnglesZ(localEulerAnglesZ);
        }

        public void SetRotation(Quaternion rotation)
        {
            this.transform.rotation = rotation;
        }

        public void SetLocalRotation(Quaternion localRotation)
        {
            this.transform.localRotation = localRotation;
        }
    }
}