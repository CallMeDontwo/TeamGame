namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class UnitComponent : Entity, IAwake, IDestroy
    {
        public int SelfId { get; set; }
        public Unit SelfUnit => this.GetChild<Unit>(this.SelfId);
    }
}