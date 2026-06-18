namespace ET
{
    [ComponentOf(typeof(Scene))]
    public sealed class CurrentSceneSessionComponent : Entity, IAwake
    {
        private EntityRef<SessionComponent> component;

        public SessionComponent Component
        {
            get => this.component;
            set => this.component = value;
        }

        public PSession Session => this.Component?.Session;
    }

    public static class CurrentSceneSessionComponentSystem
    {
        public static ETTask<T> Call<T>(this CurrentSceneSessionComponent self, MessageObject req, int timeout = 0) where T : MessageObject
        {
            return self.Session.Call<T>(req, timeout);
        }
    }
}