using UnityEngine;

namespace ET
{
    public static class TransformExtension
    {
        public static Transform ResetTransform(this Transform self)
        {
            self.localScale = Vector3.one;
            self.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            return self;
        }

        public static Transform SetPosition(this Transform self, in Vector3 position)
        {
            self.position = position;
            return self;
        }

        public static Transform SetPositionX(this Transform self, in float positionX)
        {
            Vector3 vector3 = self.position;
            vector3.x = positionX;
            self.position = vector3;
            return self;
        }

        public static Transform SetPositionY(this Transform self, in float positionY)
        {
            Vector3 vector3 = self.position;
            vector3.y = positionY;
            self.position = vector3;
            return self;
        }

        public static Transform SetPositionZ(this Transform self, in float positionZ)
        {
            Vector3 vector3 = self.position;
            vector3.z = positionZ;
            self.position = vector3;
            return self;
        }

        public static Transform SetLocalPosition(this Transform self, in Vector3 localPosition)
        {
            self.localPosition = localPosition;
            return self;
        }

        public static Transform SetLocalPositionX(this Transform self, in float localPositionX)
        {
            Vector3 vector3 = self.localPosition;
            vector3.x = localPositionX;
            self.localPosition = vector3;
            return self;
        }

        public static Transform SetLocalPositionY(this Transform self, in float localPositionY)
        {
            Vector3 vector3 = self.localPosition;
            vector3.y = localPositionY;
            self.localPosition = vector3;
            return self;
        }

        public static Transform SetLocalPositionZ(this Transform self, in float localPositionZ)
        {
            Vector3 vector3 = self.localPosition;
            vector3.z = localPositionZ;
            self.localPosition = vector3;
            return self;
        }

        public static Transform SetLocalScale(this Transform self, in Vector3 scale)
        {
            self.localScale = scale;
            return self;
        }

        public static Transform SetLocalScaleX(this Transform self, in float scaleX)
        {
            Vector3 localScale = self.localScale;
            localScale.x = scaleX;
            self.localScale = localScale;
            return self;
        }

        public static Transform SetLocalScaleY(this Transform self, in float scaleY)
        {
            Vector3 localScale = self.localScale;
            localScale.y = scaleY;
            self.localScale = localScale;
            return self;
        }

        public static Transform SetLocalScaleZ(this Transform self, in float scaleZ)
        {
            Vector3 localScale = self.localScale;
            localScale.z = scaleZ;
            self.localScale = localScale;
            return self;
        }

        public static Transform FlipX(this Transform self, bool flip)
        {
            self.SetLocalScaleX(flip ? -1f : 1f);
            return self;
        }

        public static Transform FlipY(this Transform self, bool flip)
        {
            self.SetLocalScaleY(flip ? -1f : 1f);
            return self;
        }

        public static Transform FlipZ(this Transform self, bool flip)
        {
            self.SetLocalScaleZ(flip ? -1f : 1f);
            return self;
        }

        public static Transform SetEulerAngles(this Transform self, in Vector3 eulerAngles)
        {
            self.eulerAngles = eulerAngles;
            return self;
        }

        public static Transform SetEulerAnglesX(this Transform self, in float eulerAnglesX)
        {
            Vector3 vector3 = self.eulerAngles;
            vector3.x = eulerAnglesX;
            self.eulerAngles = vector3;
            return self;
        }

        public static Transform SetEulerAnglesY(this Transform self, in float eulerAnglesY)
        {
            Vector3 vector3 = self.eulerAngles;
            vector3.y = eulerAnglesY;
            self.eulerAngles = vector3;
            return self;
        }

        public static Transform SetEulerAnglesZ(this Transform self, in float eulerAnglesZ)
        {
            Vector3 vector3 = self.eulerAngles;
            vector3.z = eulerAnglesZ;
            self.eulerAngles = vector3;
            return self;
        }

        public static Transform SetLocalEulerAngles(this Transform self, in Vector3 localEulerAngles)
        {
            self.localEulerAngles = localEulerAngles;
            return self;
        }

        public static Transform SetLocalEulerAnglesX(this Transform self, in float localEulerAnglesX)
        {
            Vector3 vector3 = self.localEulerAngles;
            vector3.x = localEulerAnglesX;
            self.localEulerAngles = vector3;
            return self;
        }

        public static Transform SetLocalEulerAnglesY(this Transform self, in float localEulerAnglesY)
        {
            Vector3 vector3 = self.localEulerAngles;
            vector3.y = localEulerAnglesY;
            self.localEulerAngles = vector3;
            return self;
        }

        public static Transform SetLocalEulerAnglesZ(this Transform self, in float localEulerAnglesZ)
        {
            Vector3 vector3 = self.localEulerAngles;
            vector3.z = localEulerAnglesZ;
            self.localEulerAngles = vector3;
            return self;
        }

        public static Transform SetRotation(this Transform self, in Quaternion rotation)
        {
            self.rotation = rotation;
            return self;
        }

        public static Transform SetLocalRotation(this Transform self, in Quaternion localRotation)
        {
            self.localRotation = localRotation;
            return self;
        }

        public static Transform SetForward(this Transform self, in Vector3 forward)
        {
            self.forward = forward;
            return self;
        }

        public static Transform SetRight(this Transform self, in Vector3 right)
        {
            self.right = right;
            return self;
        }

        public static Transform SetUp(this Transform self, in Vector3 up)
        {
            self.up = up;
            return self;
        }
    }
}