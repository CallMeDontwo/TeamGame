namespace ET
{
    public sealed class ProtoIdAttribute : BaseAttribute
    {
        public int ID { get; set; }

        private ProtoIdAttribute()
        {
        }

        public ProtoIdAttribute(int id)
        {
            this.ID = id;
        }
    }
}