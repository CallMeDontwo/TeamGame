namespace ET
{
    public class Logger: Singleton<Logger>, ISingletonAwake, ISingletonAwake<ILog>
    {
        private ILog log;

        public ILog Log
        {
            set
            {
                this.log = value;
            }
            get
            {
                return this.log;
            }
        }
        
        public void Awake()
        {
        }

        public void Awake(ILog a)
        {
            this.log = a;
        }
    }
}