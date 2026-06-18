namespace ET
{
    [GameSignerAttribute]
    public abstract class ASigner : Object
    {
        public abstract LoginType GetLoginType();
        public abstract ETTask Initialize();
        public abstract ETTask<string> SignIn();
        public abstract ETTask<string> SignInSilently();
        public abstract void SignOut();
    }
}