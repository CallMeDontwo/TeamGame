using System;

namespace ET
{
    public class TimeInfo : Singleton<TimeInfo>, ISingletonAwake
    {
        private int timeZone;

        public int TimeZone
        {
            get
            {
                return this.timeZone;
            }
            set
            {
                this.timeZone = value;
                this.dtTimeZone = this.dt1970.AddHours(this.TimeZone);
            }
        }

        private DateTime dt1970;
        private DateTime dtTimeZone;

        // ping消息会设置该值，原子操作
        public long ServerMinusClientTime { private get; set; }

        public long StartTime { get; private set; }

        public long FrameTime { get; private set; }

        public float Time => (this.FrameTime - this.StartTime) / 1000f;

        public void Awake()
        {
            this.dt1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            this.dtTimeZone = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            this.FrameTime = this.ClientNow();
            this.StartTime = this.FrameTime;
        }

        public void Update()
        {
            // 赋值long型是原子操作，线程安全
            this.FrameTime = this.ClientNow();
        }

        /// <summary> 
        /// 根据时间戳获取时间 
        /// </summary>  
        public DateTime ToDateTime(long timeStamp)
        {
            return this.dtTimeZone.AddTicks(timeStamp * 10000);
        }

        // 线程安全
        public long ClientNow()
        {
            return (DateTime.UtcNow.Ticks - this.dt1970.Ticks) / 10000;
        }

        public long ServerNow()
        {
            return this.ClientNow() + this.ServerMinusClientTime;
        }

        public long ClientFrameTime()
        {
            return this.FrameTime;
        }

        public long ServerFrameTime()
        {
            return this.FrameTime + this.ServerMinusClientTime;
        }

        public long Transition(DateTime d)
        {
            return (d.Ticks - this.dtTimeZone.Ticks) / 10000;
        }
    }
}