namespace ET
{
    [ComponentOf]
    public class SessionIdleCheckerComponent: Entity, IAwake, IDestroy
    {
        public long RepeatedTimer;
    }
}