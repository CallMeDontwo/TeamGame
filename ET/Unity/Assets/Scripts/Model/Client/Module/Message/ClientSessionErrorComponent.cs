namespace ET.Client
{
    [ComponentOf(typeof(PSession))]
    public class ClientSessionErrorComponent : Entity, IAwake, IDestroy
    {
        public int Times { get; set; }
    }
}