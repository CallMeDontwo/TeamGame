using System.Diagnostics;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET
{
    [ChildOf]
    [DebuggerDisplay("ViewName,nq")]
    [EnableMethod]
    public partial class Unit : Entity, IAwake, IAwake<int>
    {
        public int ConfigId { get; set; }

        [BsonElement]
        private float3 position;

        [BsonIgnore]
        public float3 Position
        {
            get => this.position;
            set => this.SetPositon(value, true);
        }

        [BsonElement]
        private quaternion rotation;

        [BsonIgnore]
        public quaternion Rotation
        {
            get => this.rotation;
            set => this.SetRotation(value, true);
        }

        [BsonIgnore]
        public float3 Forward
        {
            get => math.mul(this.Rotation, math.forward());
            set => this.SetForward(value, true);
        }

        protected override string ViewName => $"{this.GetType().FullName} ({this.Id})";

        public void SetPositon(float3 positon, bool publish = false)
        {
            float3 oldPos = this.position;
            this.position = positon;
            if (publish)
                EventSystem.Instance.Publish(this.Scene(), new ChangePosition() { Unit = this, OldPos = oldPos });
        }

        public void SetRotation(quaternion rotation, bool publish = false)
        {
            this.rotation = rotation;
            if (publish)
                EventSystem.Instance.Publish(this.Scene(), new ChangeRotation() { Unit = this });
        }

        public void SetForward(float3 forward, bool publish = false)
        {
            this.rotation = quaternion.LookRotation(forward, math.up());
            if (publish)
                EventSystem.Instance.Publish(this.Scene(), new ChangeRotation() { Unit = this });
        }
    }
}