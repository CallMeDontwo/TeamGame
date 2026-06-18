using UnityEngine;
using Spine;
using Spine.Unity;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.TeamGame
{
    [ComponentOf(typeof(Unit))]
    public class UnitGameObjectComponent : Entity, IAwake<GameObject>, IDestroy,IAwake
    {
        private GameObject gameObject;

        [BsonIgnore]
        public SkeletonAnimation Animation;
        public GameObject GameObject
        {
            get
            {
                return this.gameObject;
            }
            set
            {
                this.gameObject = value;
                if (value != null)
                {
                    this.Transform = value.transform;
                    Animation = value.GetComponentInChildren<SkeletonAnimation>();
                }
                else
                {
                    this.Transform = null;
                    Animation = null;
                }
            }
        }

        public Transform Transform { get; private set; }
    }
}