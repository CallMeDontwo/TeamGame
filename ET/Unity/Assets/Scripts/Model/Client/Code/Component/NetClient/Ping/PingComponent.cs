namespace ET.Client
{
    [ComponentOf]
    public class PingComponent : Entity, IAwake, IDestroy
    {
        public long Ping { get; set; } //延迟值
    }
}