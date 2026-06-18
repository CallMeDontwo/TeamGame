using System;
using System.Collections.Generic;

namespace ET
{
    public sealed class Authentication : Singleton<Authentication>, ISingletonAwake
    {
        private readonly Dictionary<LoginType, ASigner> Signers = new Dictionary<LoginType, ASigner>();

        public void Awake()
        {
            HashSet<Type> signers = CodeTypes.Instance.GetTypes(typeof(GameSignerAttribute));
            foreach (Type type in signers)
            {
                ASigner signer = Activator.CreateInstance(type) as ASigner;
                LoginType loginType = signer.GetLoginType();
                this.Signers.Add(loginType, signer);
            }
        }

        public async ETTask InitializeAsync()
        {
            foreach (ASigner item in this.Signers.Values)
            {
                await item.Initialize();
            }
        }

        public static ETTask<string> SignIn(LoginType loginType)
        {
            return Instance.Signers[loginType].SignIn();
        }

        public static ETTask<string> SignInSilently(LoginType loginType)
        {
            return Instance.Signers[loginType].SignInSilently();
        }

        public static void SignOut(LoginType loginType)
        {
            Instance.Signers[loginType].SignOut();
        }
    }
}