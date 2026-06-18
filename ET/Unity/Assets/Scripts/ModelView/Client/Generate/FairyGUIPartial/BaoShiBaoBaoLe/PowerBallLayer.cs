using System;
using FairyGUI;
using Spine.Unity;
using UnityEngine;
using YooAsset;

namespace ET.BaoShiBaoBaoLe
{
    public partial class PowerBallLayer
    {
        private AssetHandle powerLight;

        private readonly GObjectPool pool;

        public PowerBallLayer()
        {
            this.pool = new GObjectPool(this.displayObject.cachedTransform);
            this.pool.initCallback = (obj) =>
            {
                PowerBall ball = PowerBall.GetFromPool(obj);
                ball.Loader_Front.SetLoaderSpine(this.powerLight.GetAssetObject<SkeletonDataAsset>());
                ball.Loader_Front.fill = FillType.ScaleFree;
                ball.Loader_Front.spineAnimation.loop = true;
                ball.Loader_Front.spineAnimation.AnimationName = "play";
            };
        }

        public async ETTask LoadSpineAsync()
        {
            this.powerLight = YooAssets.LoadAssetAsync<SkeletonDataAsset>("power light_SkeletonData");
            await this.powerLight.Task;
        }

        public PowerBall FetchBall()
        {
            GObject gObject = this.pool.GetObject(PowerBall.URL);
            this.AddChild(gObject);
            return PowerBall.GetFromPool(gObject);
        }

        public async ETTask PowerBallMove(Vector2 sPos, Vector2 tPos, float duration)
        {
            PowerBall ball = this.FetchBall();
            ball.xy = sPos;
            ball.scale = Vector2.one;
            ETTask task1 = ball.TweenScale(Vector2.one * 0.3f, duration).PlayAsync();
            ETTask task2 = ball.TweenMove(tPos, duration).PlayAsync();
            await ETTaskHelper.WaitAll(task1, task2);
            this.Recycle(ball);
        }


        public void Recycle(GObject gObject)
        {
            this.RemoveChild(gObject);
            this.pool.ReturnObject(gObject);
        }
    }
}