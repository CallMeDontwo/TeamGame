namespace ET
{
    public static partial class UnitComponentSystem
    {

        public static Unit Get(this UnitComponent self, in long id)
        {
            return self.GetChild<Unit>(id);
        }

        public static void Remove(this UnitComponent self, in long id)
        {
            self.GetChild<Unit>(id)?.Dispose();
        }
    }
}