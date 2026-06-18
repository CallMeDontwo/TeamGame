using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [EnableMethod]
    [ChildOf]
    public class Scene : Entity, IScene
    {
        [BsonIgnore]
        public Fiber Fiber { get; set; }

        public string Name { get; }

        public SceneType SceneType
        {
            get;
            set;
        }

        public Scene(SceneType sceneType)
        {
            this.SceneType = sceneType;
        }

        public Scene()
        {
        }

        public Scene(Fiber fiber, long id, long instanceId, SceneType sceneType, string name)
        {
            this.Id = id;
            this.Name = name;
            this.InstanceId = instanceId;
            this.SceneType = sceneType;
            this.IsCreated = true;
            this.IsNew = true;
            this.Fiber = fiber;
            this.IScene = this;
            this.IsRegister = true;
            Log.Info($"scene create: {this.SceneType} {this.Id} {this.InstanceId}");
        }

        public override void Dispose()
        {
            long instId = this.InstanceId;
            base.Dispose();
            Log.Info($"scene dispose: {this.SceneType} {this.Id} {instId}");
        }

        protected override string ViewName => $"{this.GetType().Name} ({this.SceneType})";
    }
}