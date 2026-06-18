using FairyGUI;

namespace ET.ChaoJiMoFang
{
    public partial class Com_DieDieLeList
    {
        public int Id { get; set; }
        public string DefaultItem { get; set; }
        public GObjectPool Pool { get; private set; }

        partial void AfterCreate()
        {
            this.DefaultItem = "ui://hoqdodepvdhu5g";
            this.Pool = new GObjectPool(this.displayObject.cachedTransform);
        }

        public Label_DieIcon GetFromPool()
        {
            Label_DieIcon gObj = Label_DieIcon.GetFromPool(this.Pool.GetObject(this.DefaultItem));
            this.AddChild(gObj);
            return gObj;
        }

        public void RemoveChildrenToPool()
        {
            for (int i = this.numChildren - 1; i >= 0; i--)
            {
                this.Pool.ReturnObject(this.RemoveChildAt(i));
            }
        }

        public async ETTask AddNewOne()
        {
            await ETTask.CompletedTask;
            //Label_DieIcon gObj = this.GetFromPool();
            //gObj.y = -gObj.height;
            //gObj.icon = SlotItemConfigCategory.Instance.Get(this.Id).IconWithoutBg;
            //await gObj.TweenMoveY(this.height - this.numChildren * gObj.height, 0.3f).SetEase(EaseType.Linear).PlayAsync();
        }
    }
}