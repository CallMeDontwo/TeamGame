namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class SessionComponent : Entity, IAwake, IDestroy
    {
        private EntityRef<PSession> session;

        public PSession Session
        {
            get => this.session;
            set => this.session = value;
        }
    }
}