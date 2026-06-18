using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class FlyMove : MonoBehaviour
    {
        public float Speed;

        private void Update()
        {
            float axisx = Input.GetAxis("Mouse X");
            float axisy = Input.GetAxis("Mouse Y");
            Vector3 forward = this.transform.forward;
            forward.y = 0;
            forward.Normalize();
            Vector3 euler = forward;
            if (Input.GetKey(KeyCode.W))
            {
                this.transform.position += this.Speed * Time.deltaTime * euler;
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.transform.position -= this.Speed * Time.deltaTime * euler;
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.transform.position += this.Right(this.Speed * Time.deltaTime * euler);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.transform.position += this.Left(this.Speed * Time.deltaTime * euler);
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                this.transform.position += this.Speed * Time.deltaTime * Vector3.up;
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                this.transform.position += this.Speed * Time.deltaTime * Vector3.down;
            }
            this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x - axisy, this.transform.eulerAngles.y + axisx, this.transform.eulerAngles.z);
        }

        private Vector3 Right(Vector3 forward)
        {
            return forward.x * Vector3.back + forward.y * Vector3.up + forward.z * Vector3.right;
        }

        private Vector3 Left(Vector3 forward)
        {
            return forward.x * Vector3.forward + forward.y * Vector3.up + forward.z * Vector3.left;
        }
    }
}