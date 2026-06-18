using System.Collections.Generic;
public class AOTGenericReferences : UnityEngine.MonoBehaviour
{

	// {{ AOT assemblies
	public static readonly IReadOnlyList<string> PatchedAOTAssemblyList = new List<string>
	{
		"System.Core.dll",
		"System.dll",
		"Unity.Core.dll",
		"Unity.InputSystem.dll",
		"Unity.ThirdParty.dll",
		"UnityEngine.CoreModule.dll",
		"YooAsset.dll",
		"mscorlib.dll",
	};
	// }}

	// {{ constraint implement type
	// }} 

	// {{ AOT generic types
	// ET.AEvent<object,ET.ChangePosition>
	// ET.AEvent<object,ET.ChangeRotation>
	// ET.AEvent<object,ET.ChangeSpeed>
	// ET.AEvent<object,ET.CoinAward>
	// ET.AEvent<object,ET.CoinDieDieLeAward>
	// ET.AEvent<object,ET.CoinDieDieLeDoubleAward>
	// ET.AEvent<object,ET.CoinDoubleAward>
	// ET.AEvent<object,ET.CoinJPAward>
	// ET.AEvent<object,ET.CoinJPDoubleAward>
	// ET.AEvent<object,ET.EnterGame>
	// ET.AEvent<object,ET.EnterWorld>
	// ET.AEvent<object,ET.MoveStart>
	// ET.AEvent<object,ET.MoveStop>
	// ET.AEvent<object,ET.NumbericChange>
	// ET.AEvent<object,ET.SceneChangeFinish>
	// ET.AEvent<object,ET.SceneChangeStart>
	// ET.AEvent<object,ET.ShowSpinLine>
	// ET.AEvent<object,ET.ShowSpinResult>
	// ET.AEvent<object,ET.ShowSpinResultAndLine>
	// ET.AEvent<object,ET.SpinDieDieLeCoinChange>
	// ET.AEvent<object,ET.SpinDieDieLeMultipleChange>
	// ET.AEvent<object,ET.SpinDieDieLeWait>
	// ET.AEvent<object,ET.SpinEnterDieDieLe>
	// ET.AEvent<object,ET.SpinEnterDouble>
	// ET.AEvent<object,ET.SpinEnterLittleMary>
	// ET.AEvent<object,ET.SpinExitDieDieLe>
	// ET.AEvent<object,ET.SpinExitLittleMary>
	// ET.AEvent<object,ET.SpinLittleMaryShow>
	// ET.AInvokeHandler<ET.Client.AppInit,object>
	// ET.AInvokeHandler<ET.Client.BackLogin>
	// ET.AInvokeHandler<ET.ConfigLoader.GetAllConfigBytes,object>
	// ET.AInvokeHandler<ET.ConfigLoader.GetOneConfigBytes,object>
	// ET.AInvokeHandler<ET.FiberCreate,object>
	// ET.AInvokeHandler<ET.MailBoxInvoker>
	// ET.AInvokeHandler<ET.NavmeshComponent.RecastFileLoader,object>
	// ET.AInvokeHandler<ET.NetComponentOnRead>
	// ET.AInvokeHandler<ET.NetProtoComponentOnRead>
	// ET.AInvokeHandler<ET.TimerCallback>
	// ET.AwakeSystem<object,int,int>
	// ET.AwakeSystem<object,int>
	// ET.AwakeSystem<object,object,int>
	// ET.AwakeSystem<object,object,object>
	// ET.AwakeSystem<object,object>
	// ET.AwakeSystem<object>
	// ET.DestroySystem<object>
	// ET.DictionaryComponent<int,int>
	// ET.DictionaryComponent<object,object>
	// ET.ETAsyncTaskMethodBuilder<System.ValueTuple<int,object>>
	// ET.ETAsyncTaskMethodBuilder<byte>
	// ET.ETAsyncTaskMethodBuilder<object>
	// ET.ETTask<ET.CreateSceneComplete>
	// ET.ETTask<System.ValueTuple<int,object>>
	// ET.ETTask<byte>
	// ET.ETTask<int>
	// ET.ETTask<object>
	// ET.EntityRef<object>
	// ET.EntityWeakRef<object>
	// ET.HashSetComponent<int>
	// ET.HashSetComponent<object>
	// ET.IAwake<int,int>
	// ET.IAwake<int>
	// ET.IAwake<object,int>
	// ET.IAwake<object,object>
	// ET.IAwake<object>
	// ET.IAwakeSystem<int,int>
	// ET.IAwakeSystem<int>
	// ET.IAwakeSystem<object,object>
	// ET.IAwakeSystem<object>
	// ET.ListComponent<Unity.Mathematics.float3>
	// ET.ListComponent<int>
	// ET.ListComponent<object>
	// ET.MultiDictionary<object,int,object>
	// ET.MultiDictionary<object,long,object>
	// ET.MultiDictionary<object,object,object>
	// ET.Singleton<object>
	// ET.StateMachineWrap<ET.ADataSceneCreater.<TryCreate>d__1>
	// ET.StateMachineWrap<ET.ASceneCreater.<OnCreateComplete>d__4>
	// ET.StateMachineWrap<ET.AViewSceneCreater.<LoadScene>d__2>
	// ET.StateMachineWrap<ET.AViewSceneCreater.<TryCreate>d__1>
	// ET.StateMachineWrap<ET.AWorldViewSceneCreater.<OnCreate>d__1>
	// ET.StateMachineWrap<ET.AWorldViewSceneCreater.<OnDestroy>d__2>
	// ET.StateMachineWrap<ET.AWorldViewSceneCreater.<TryCreate>d__0>
	// ET.StateMachineWrap<ET.AppInit_Invoke.<Handle>d__0>
	// ET.StateMachineWrap<ET.AssetHandleExtension.<WaitAsync>d__0>
	// ET.StateMachineWrap<ET.Authentication.<InitializeAsync>d__2>
	// ET.StateMachineWrap<ET.ChangePosition_Event.<Run>d__0>
	// ET.StateMachineWrap<ET.ChangeRotation_Event.<Run>d__0>
	// ET.StateMachineWrap<ET.ChangeSpeed_Event.<Run>d__0>
	// ET.StateMachineWrap<ET.ChaoJiMoFang.Com_DieDieLeList.<AddNewOne>d__22>
	// ET.StateMachineWrap<ET.ChaoJiMoFang.Com_DieDieLeText.<ChangeText>d__20>
	// ET.StateMachineWrap<ET.Client.FiberCreate_NetClient.<Handle>d__0>
	// ET.StateMachineWrap<ET.Client.PingComponentSystem.<PingAsync>d__2>
	// ET.StateMachineWrap<ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>>
	// ET.StateMachineWrap<ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>>
	// ET.StateMachineWrap<ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5>
	// ET.StateMachineWrap<ET.ClientSessionErrorComponentSystem.<SessionDisposeTip>d__2>
	// ET.StateMachineWrap<ET.ClientSessionErrorComponentSystem.<ShowTipLater>d__3>
	// ET.StateMachineWrap<ET.CoinPusher.CoinAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinDieDieLeAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinDieDieLeDoubleAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinDoubleAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinJPAward_Current.<PlayAnimation>d__1>
	// ET.StateMachineWrap<ET.CoinPusher.CoinJPAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinJPDoubleAward_Current.<PlayAnimation>d__1>
	// ET.StateMachineWrap<ET.CoinPusher.CoinJPDoubleAward_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<<ShowSpin>g__ScrollAsync|30_1>d>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<AddDieDieLeMultiple>d__35>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<LoadSpine>d__23>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<ShowDieDieLeResult>d__37>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<ShowLine>d__31>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<ShowLittleMary>d__33>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<ShowSpin>d__30>
	// ET.StateMachineWrap<ET.CoinPusher.CoinPusherPanel.<ShowSpinAndLine>d__32>
	// ET.StateMachineWrap<ET.CoinPusher.ShowSpinLine_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.ShowSpinResultAndLine_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.ShowSpinResult_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinDieDieLeCoinChange_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinDieDieLeMultipleChange_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinDieDieLeWait_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterDieDieLe_Current.<PlayAnimation>d__1>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterDieDieLe_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterDouble_Current.<PlayAnimation>d__1>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterDouble_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterLittleMary_Current.<PlayAnimation>d__1>
	// ET.StateMachineWrap<ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinExitDieDieLe_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinExitLittleMary_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusher.SpinLittleMaryShow_Current.<Run>d__0>
	// ET.StateMachineWrap<ET.CoinPusherManager.<>c__DisplayClass65_0.<<RunSpin>g__IncMultipleCoroutine|5>d>
	// ET.StateMachineWrap<ET.CoinPusherManager.<>c__DisplayClass66_0.<<ReawardDouble>g__IncMultipleCoroutine|5>d>
	// ET.StateMachineWrap<ET.CoinPusherManager.<ReawardDouble>d__66>
	// ET.StateMachineWrap<ET.CoinPusherManager.<RunSpin>d__65>
	// ET.StateMachineWrap<ET.CoinPusherSceneCreater0.<OnCreate>d__1>
	// ET.StateMachineWrap<ET.CoinPusherSceneCreater0.<OnDestroy>d__2>
	// ET.StateMachineWrap<ET.CoinPusherSceneCreater1.<OnCreate>d__1>
	// ET.StateMachineWrap<ET.CoinPusherSceneCreater1.<OnDestroy>d__2>
	// ET.StateMachineWrap<ET.ConsoleComponentSystem.<Start>d__1>
	// ET.StateMachineWrap<ET.CoroutineHelper.<GetAwaiter>d__0>
	// ET.StateMachineWrap<ET.CoroutineHelper.<HttpGet>d__1>
	// ET.StateMachineWrap<ET.CurrentSceneFactory.<Create>d__2>
	// ET.StateMachineWrap<ET.CurrentSceneFactory.<CreateInternal>d__3>
	// ET.StateMachineWrap<ET.CurrentSceneFactory.<EnterGame>d__1>
	// ET.StateMachineWrap<ET.CurrentSceneFactory.<EnterScene>d__0>
	// ET.StateMachineWrap<ET.EnterGame_
	// .<Run>d__0>
	// ET.StateMachineWrap<ET.EnterWorld_Main.<Run>d__0>
	// ET.StateMachineWrap<ET.Entry.<StartAsync>d__2>
	// ET.StateMachineWrap<ET.FiberCreate_Main.<Handle>d__0>
	// ET.StateMachineWrap<ET.GetAllConfigBytes.<Handle>d__0>
	// ET.StateMachineWrap<ET.GetAllConfigBytes.<LoadConfigAsyncByYooAsset>d__2>
	// ET.StateMachineWrap<ET.GetOneConfigBytes.<Handle>d__0>
	// ET.StateMachineWrap<ET.MailBoxType_OrderedMessageHandler.<HandleInner>d__1>
	// ET.StateMachineWrap<ET.MailBoxType_UnOrderedMessageHandler.<HandleAsync>d__1>
	// ET.StateMachineWrap<ET.MainSceneCreater1.<OnCreate>d__3>
	// ET.StateMachineWrap<ET.MainSceneCreater1.<OnCreateComplete>d__4>
	// ET.StateMachineWrap<ET.MainSceneCreater3.<OnCreate>d__3>
	// ET.StateMachineWrap<ET.MainSceneCreater3.<OnCreateComplete>d__4>
	// ET.StateMachineWrap<ET.MainSceneCreater4.<OnCreate>d__3>
	// ET.StateMachineWrap<ET.MainSceneCreater5.<OnCreate>d__3>
	// ET.StateMachineWrap<ET.MainSceneCreater6.<OnCreate>d__2>
	// ET.StateMachineWrap<ET.MainSceneCreater7.<CreateAsync>d__4<object>>
	// ET.StateMachineWrap<ET.MainSceneCreater7.<OnCreate>d__3>
	// ET.StateMachineWrap<ET.MessageHandler.<Handle>d__1<object,object,object>>
	// ET.StateMachineWrap<ET.MessageHandler.<Handle>d__1<object,object>>
	// ET.StateMachineWrap<ET.MessageSessionHandler.<HandleAsync>d__2<object,object>>
	// ET.StateMachineWrap<ET.MessageSessionHandler.<HandleAsync>d__2<object>>
	// ET.StateMachineWrap<ET.MoveComponentSystem.<MoveToAsync>d__5>
	// ET.StateMachineWrap<ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>
	// ET.StateMachineWrap<ET.ObjectEvent.<InvokeInternal>d__4>
	// ET.StateMachineWrap<ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<object>>
	// ET.StateMachineWrap<ET.ObjectWaitSystem.<Wait>d__4<object>>
	// ET.StateMachineWrap<ET.ObjectWaitSystem.<Wait>d__5<object>>
	// ET.StateMachineWrap<ET.PSessionSystem.<>c__DisplayClass10_0.<<Call>g__Timeout|0>d<object>>
	// ET.StateMachineWrap<ET.PSessionSystem.<Call>d__10<object>>
	// ET.StateMachineWrap<ET.PSessionSystem.<Call>d__7>
	// ET.StateMachineWrap<ET.PSessionSystem.<Call>d__8<object>>
	// ET.StateMachineWrap<ET.PSessionSystem.<Call>d__9>
	// ET.StateMachineWrap<ET.ProtoMessageHelper.<DeserializeAsync>d__1>
	// ET.StateMachineWrap<ET.ProtoMessageHelper.<ToMessageAsync>d__6>
	// ET.StateMachineWrap<ET.ReloadConfigConsoleHandler.<Run>d__0>
	// ET.StateMachineWrap<ET.ReloadDllConsoleHandler.<Run>d__0>
	// ET.StateMachineWrap<ET.RpcInfo.<Wait>d__7>
	// ET.StateMachineWrap<ET.SceneChangeFinish_Event.<Run>d__0>
	// ET.StateMachineWrap<ET.SceneChangeStart_Event.<Run>d__0>
	// ET.StateMachineWrap<ET.SceneCreatDispatcher.<OnCreate>d__4>
	// ET.StateMachineWrap<ET.SceneCreatDispatcher.<OnCreateComplete>d__5>
	// ET.StateMachineWrap<ET.SceneCreatDispatcher.<OnDestory>d__6>
	// ET.StateMachineWrap<ET.SceneCreatDispatcher.<TryCreate>d__3>
	// ET.StateMachineWrap<ET.SceneEventDispatcher.<Handle>d__2<object>>
	// ET.StateMachineWrap<ET.SessionSystem.<>c__DisplayClass4_0.<<Call>g__Timeout|0>d>
	// ET.StateMachineWrap<ET.SessionSystem.<Call>d__3>
	// ET.StateMachineWrap<ET.SessionSystem.<Call>d__4>
	// ET.StateMachineWrap<ET.UIPackageHelper.<<AddPackageAsync>g__LoadDescAsync|5_0>d>
	// ET.StateMachineWrap<ET.UIPackageHelper.<<AddPackageAsync>g__LoadResourceTask|5_2>d>
	// ET.StateMachineWrap<ET.UIPackageHelper.<AddPackageAsync>d__5>
	// ET.StateMachineWrap<ET.UnitComponentSystem.<CreateWithEvent>d__3>
	// ET.StateMachineWrap<ET.UnitComponentSystem.<CreateWithEvent>d__4>
	// ET.StateMachineWrap<ET.ViewController.<InitChildren>d__16<object>>
	// ET.StateMachineWrap<ET.ViewController.<InitPanel>d__15<object>>
	// ET.StateMachineWrap<ET.ViewController.MutipileViewPanel.<InitAsync>d__3<object>>
	// ET.StateMachineWrap<ET.ViewPanel.<InitAsync>d__4<object>>
	// ET.UpdateSystem<object>
	// System.Action<DotRecast.Detour.StraightPathItem>
	// System.Action<ET.MessageSessionDispatcherInfo>
	// System.Action<ET.NumericWatcherInfo>
	// System.Action<ET.PRpcInfo>
	// System.Action<ET.RpcInfo>
	// System.Action<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Action<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Action<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Action<Unity.Mathematics.float3>
	// System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>
	// System.Action<float,float>
	// System.Action<int,int>
	// System.Action<int,object>
	// System.Action<int>
	// System.Action<long,int>
	// System.Action<long,object>
	// System.Action<long>
	// System.Action<object,object,object>
	// System.Action<object,object>
	// System.Action<object>
	// System.Collections.Concurrent.ConcurrentQueue.<Enumerate>d__28<ET.TArgs>
	// System.Collections.Concurrent.ConcurrentQueue.<Enumerate>d__28<object>
	// System.Collections.Concurrent.ConcurrentQueue.Segment<ET.TArgs>
	// System.Collections.Concurrent.ConcurrentQueue.Segment<object>
	// System.Collections.Concurrent.ConcurrentQueue<ET.TArgs>
	// System.Collections.Concurrent.ConcurrentQueue<object>
	// System.Collections.Generic.ArraySortHelper<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ArraySortHelper<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.ArraySortHelper<ET.NumericWatcherInfo>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ArraySortHelper<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ArraySortHelper<Unity.Mathematics.float3>
	// System.Collections.Generic.ArraySortHelper<int>
	// System.Collections.Generic.ArraySortHelper<long>
	// System.Collections.Generic.ArraySortHelper<object>
	// System.Collections.Generic.Comparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.Comparer<ET.ActorId>
	// System.Collections.Generic.Comparer<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.Comparer<ET.NumericWatcherInfo>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.Comparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.Comparer<Unity.Mathematics.float3>
	// System.Collections.Generic.Comparer<float>
	// System.Collections.Generic.Comparer<int>
	// System.Collections.Generic.Comparer<long>
	// System.Collections.Generic.Comparer<object>
	// System.Collections.Generic.Comparer<ushort>
	// System.Collections.Generic.ComparisonComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ComparisonComparer<ET.ActorId>
	// System.Collections.Generic.ComparisonComparer<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.ComparisonComparer<ET.NumericWatcherInfo>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ComparisonComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ComparisonComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ComparisonComparer<float>
	// System.Collections.Generic.ComparisonComparer<int>
	// System.Collections.Generic.ComparisonComparer<long>
	// System.Collections.Generic.ComparisonComparer<object>
	// System.Collections.Generic.ComparisonComparer<ushort>
	// System.Collections.Generic.Dictionary.Enumerator<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary.Enumerator<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.KeyCollection<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary.KeyCollection<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.KeyCollection<int,int>
	// System.Collections.Generic.Dictionary.KeyCollection<int,long>
	// System.Collections.Generic.Dictionary.KeyCollection<int,object>
	// System.Collections.Generic.Dictionary.KeyCollection<long,object>
	// System.Collections.Generic.Dictionary.KeyCollection<object,int>
	// System.Collections.Generic.Dictionary.KeyCollection<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection.Enumerator<object,object>
	// System.Collections.Generic.Dictionary.ValueCollection<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary.ValueCollection<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary.ValueCollection<int,int>
	// System.Collections.Generic.Dictionary.ValueCollection<int,long>
	// System.Collections.Generic.Dictionary.ValueCollection<int,object>
	// System.Collections.Generic.Dictionary.ValueCollection<long,object>
	// System.Collections.Generic.Dictionary.ValueCollection<object,int>
	// System.Collections.Generic.Dictionary.ValueCollection<object,object>
	// System.Collections.Generic.Dictionary<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.Dictionary<int,ET.PRpcInfo>
	// System.Collections.Generic.Dictionary<int,ET.RpcInfo>
	// System.Collections.Generic.Dictionary<int,int>
	// System.Collections.Generic.Dictionary<int,long>
	// System.Collections.Generic.Dictionary<int,object>
	// System.Collections.Generic.Dictionary<long,object>
	// System.Collections.Generic.Dictionary<object,int>
	// System.Collections.Generic.Dictionary<object,object>
	// System.Collections.Generic.EqualityComparer<ET.ActorId>
	// System.Collections.Generic.EqualityComparer<ET.PRpcInfo>
	// System.Collections.Generic.EqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.EqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.EqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.EqualityComparer<float>
	// System.Collections.Generic.EqualityComparer<int>
	// System.Collections.Generic.EqualityComparer<long>
	// System.Collections.Generic.EqualityComparer<object>
	// System.Collections.Generic.EqualityComparer<ushort>
	// System.Collections.Generic.HashSet.Enumerator<int>
	// System.Collections.Generic.HashSet.Enumerator<object>
	// System.Collections.Generic.HashSet<int>
	// System.Collections.Generic.HashSet<object>
	// System.Collections.Generic.HashSetEqualityComparer<int>
	// System.Collections.Generic.HashSetEqualityComparer<object>
	// System.Collections.Generic.ICollection<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ICollection<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.ICollection<ET.NumericWatcherInfo>
	// System.Collections.Generic.ICollection<ET.TArgs>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,object>,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,ET.PRpcInfo>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.ICollection<Unity.Mathematics.float3>
	// System.Collections.Generic.ICollection<int>
	// System.Collections.Generic.ICollection<long>
	// System.Collections.Generic.ICollection<object>
	// System.Collections.Generic.IComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IComparer<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.IComparer<ET.NumericWatcherInfo>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.IComparer<int>
	// System.Collections.Generic.IComparer<long>
	// System.Collections.Generic.IComparer<object>
	// System.Collections.Generic.IEnumerable<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IEnumerable<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.IEnumerable<ET.NumericWatcherInfo>
	// System.Collections.Generic.IEnumerable<ET.PRpcInfo>
	// System.Collections.Generic.IEnumerable<ET.RpcInfo>
	// System.Collections.Generic.IEnumerable<ET.TArgs>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,object>,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,ET.PRpcInfo>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerable<Unity.Mathematics.float3>
	// System.Collections.Generic.IEnumerable<int>
	// System.Collections.Generic.IEnumerable<long>
	// System.Collections.Generic.IEnumerable<object>
	// System.Collections.Generic.IEnumerator<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IEnumerator<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.IEnumerator<ET.NumericWatcherInfo>
	// System.Collections.Generic.IEnumerator<ET.PRpcInfo>
	// System.Collections.Generic.IEnumerator<ET.RpcInfo>
	// System.Collections.Generic.IEnumerator<ET.TArgs>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.ValueTuple<object,object>,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,ET.PRpcInfo>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,long>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<int,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,int>>
	// System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<object,object>>
	// System.Collections.Generic.IEnumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.IEnumerator<int>
	// System.Collections.Generic.IEnumerator<long>
	// System.Collections.Generic.IEnumerator<object>
	// System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IEqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.IEqualityComparer<int>
	// System.Collections.Generic.IEqualityComparer<long>
	// System.Collections.Generic.IEqualityComparer<object>
	// System.Collections.Generic.IList<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.IList<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.IList<ET.NumericWatcherInfo>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.IList<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.IList<Unity.Mathematics.float3>
	// System.Collections.Generic.IList<int>
	// System.Collections.Generic.IList<long>
	// System.Collections.Generic.IList<object>
	// System.Collections.Generic.IReadOnlyDictionary<int,int>
	// System.Collections.Generic.KeyValuePair<System.ValueTuple<object,object>,object>
	// System.Collections.Generic.KeyValuePair<int,ET.PRpcInfo>
	// System.Collections.Generic.KeyValuePair<int,ET.RpcInfo>
	// System.Collections.Generic.KeyValuePair<int,int>
	// System.Collections.Generic.KeyValuePair<int,long>
	// System.Collections.Generic.KeyValuePair<int,object>
	// System.Collections.Generic.KeyValuePair<long,object>
	// System.Collections.Generic.KeyValuePair<object,int>
	// System.Collections.Generic.KeyValuePair<object,object>
	// System.Collections.Generic.List.Enumerator<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.List.Enumerator<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.List.Enumerator<ET.NumericWatcherInfo>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.List.Enumerator<Unity.Mathematics.float3>
	// System.Collections.Generic.List.Enumerator<int>
	// System.Collections.Generic.List.Enumerator<long>
	// System.Collections.Generic.List.Enumerator<object>
	// System.Collections.Generic.List<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.List<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.List<ET.NumericWatcherInfo>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.List<Unity.Mathematics.float3>
	// System.Collections.Generic.List<int>
	// System.Collections.Generic.List<long>
	// System.Collections.Generic.List<object>
	// System.Collections.Generic.ObjectComparer<DotRecast.Detour.StraightPathItem>
	// System.Collections.Generic.ObjectComparer<ET.ActorId>
	// System.Collections.Generic.ObjectComparer<ET.MessageSessionDispatcherInfo>
	// System.Collections.Generic.ObjectComparer<ET.NumericWatcherInfo>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.Generic.ObjectComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectComparer<Unity.Mathematics.float3>
	// System.Collections.Generic.ObjectComparer<float>
	// System.Collections.Generic.ObjectComparer<int>
	// System.Collections.Generic.ObjectComparer<long>
	// System.Collections.Generic.ObjectComparer<object>
	// System.Collections.Generic.ObjectComparer<ushort>
	// System.Collections.Generic.ObjectEqualityComparer<ET.ActorId>
	// System.Collections.Generic.ObjectEqualityComparer<ET.PRpcInfo>
	// System.Collections.Generic.ObjectEqualityComparer<ET.RpcInfo>
	// System.Collections.Generic.ObjectEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.ObjectEqualityComparer<System.ValueTuple<object,object>>
	// System.Collections.Generic.ObjectEqualityComparer<float>
	// System.Collections.Generic.ObjectEqualityComparer<int>
	// System.Collections.Generic.ObjectEqualityComparer<long>
	// System.Collections.Generic.ObjectEqualityComparer<object>
	// System.Collections.Generic.ObjectEqualityComparer<ushort>
	// System.Collections.Generic.Queue.Enumerator<object>
	// System.Collections.Generic.Queue<object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_0<long,object>
	// System.Collections.Generic.SortedDictionary.<>c__DisplayClass34_1<long,object>
	// System.Collections.Generic.SortedDictionary.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.KeyCollection<long,object>
	// System.Collections.Generic.SortedDictionary.KeyValuePairComparer<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass5_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.<>c__DisplayClass6_0<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection.Enumerator<long,object>
	// System.Collections.Generic.SortedDictionary.ValueCollection<long,object>
	// System.Collections.Generic.SortedDictionary<long,object>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass52_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass53_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<>c__DisplayClass85_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.<Reverse>d__94<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Enumerator<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.Node<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet.<>c__DisplayClass9_0<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet.TreeSubSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.SortedSetEqualityComparer<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.Stack.Enumerator<object>
	// System.Collections.Generic.Stack<object>
	// System.Collections.Generic.TreeSet<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.Generic.TreeWalkPredicate<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<DotRecast.Detour.StraightPathItem>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.MessageSessionDispatcherInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<ET.NumericWatcherInfo>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Collections.ObjectModel.ReadOnlyCollection<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Collections.ObjectModel.ReadOnlyCollection<Unity.Mathematics.float3>
	// System.Collections.ObjectModel.ReadOnlyCollection<int>
	// System.Collections.ObjectModel.ReadOnlyCollection<long>
	// System.Collections.ObjectModel.ReadOnlyCollection<object>
	// System.Comparison<DotRecast.Detour.StraightPathItem>
	// System.Comparison<ET.ActorId>
	// System.Comparison<ET.MessageSessionDispatcherInfo>
	// System.Comparison<ET.NumericWatcherInfo>
	// System.Comparison<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Comparison<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Comparison<Unity.Mathematics.float3>
	// System.Comparison<float>
	// System.Comparison<int>
	// System.Comparison<long>
	// System.Comparison<object>
	// System.Comparison<ushort>
	// System.EventHandler<object>
	// System.Func<System.Collections.Generic.KeyValuePair<int,int>,byte>
	// System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Func<System.Threading.Tasks.VoidTaskResult>
	// System.Func<int,byte>
	// System.Func<object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,byte>
	// System.Func<object,int>
	// System.Func<object,object,System.Threading.Tasks.VoidTaskResult>
	// System.Func<object,object,byte>
	// System.Func<object,object,object>
	// System.Func<object,object>
	// System.Func<object>
	// System.IEquatable<float>
	// System.IEquatable<int>
	// System.IEquatable<object>
	// System.Linq.Enumerable.<CastIterator>d__99<object>
	// System.Linq.Enumerable.<SkipWhileIterator>d__33<object>
	// System.Linq.Enumerable.Iterator<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Linq.Enumerable.Iterator<int>
	// System.Linq.Enumerable.Iterator<object>
	// System.Linq.Enumerable.WhereArrayIterator<object>
	// System.Linq.Enumerable.WhereEnumerableIterator<int>
	// System.Linq.Enumerable.WhereEnumerableIterator<object>
	// System.Linq.Enumerable.WhereListIterator<object>
	// System.Linq.Enumerable.WhereSelectArrayIterator<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Linq.Enumerable.WhereSelectEnumerableIterator<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Linq.Enumerable.WhereSelectListIterator<System.Collections.Generic.KeyValuePair<int,int>,int>
	// System.Predicate<DotRecast.Detour.StraightPathItem>
	// System.Predicate<ET.MessageSessionDispatcherInfo>
	// System.Predicate<ET.NumericWatcherInfo>
	// System.Predicate<System.Collections.Generic.KeyValuePair<int,int>>
	// System.Predicate<System.Collections.Generic.KeyValuePair<long,object>>
	// System.Predicate<Unity.Mathematics.float3>
	// System.Predicate<int>
	// System.Predicate<long>
	// System.Predicate<object>
	// System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<object>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.ConfiguredTaskAwaitable<object>
	// System.Runtime.CompilerServices.TaskAwaiter<System.Threading.Tasks.VoidTaskResult>
	// System.Runtime.CompilerServices.TaskAwaiter<object>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.ContinuationTaskFromResultTask<object>
	// System.Threading.Tasks.Task<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.Task<object>
	// System.Threading.Tasks.TaskCompletionSource<object>
	// System.Threading.Tasks.TaskFactory.<>c<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass32_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass32_0<object>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory.<>c__DisplayClass35_0<object>
	// System.Threading.Tasks.TaskFactory<System.Threading.Tasks.VoidTaskResult>
	// System.Threading.Tasks.TaskFactory<object>
	// System.ValueTuple<ET.ActorId,object>
	// System.ValueTuple<float,float>
	// System.ValueTuple<int,object>
	// System.ValueTuple<object,object>
	// System.ValueTuple<ushort,object>
	// UnityEngine.Events.InvokableCall<UnityEngine.InputSystem.InputAction.CallbackContext>
	// UnityEngine.Events.InvokableCall<byte>
	// UnityEngine.Events.InvokableCall<int>
	// UnityEngine.Events.InvokableCall<long>
	// UnityEngine.Events.InvokableCall<object>
	// UnityEngine.Events.UnityAction<UnityEngine.InputSystem.InputAction.CallbackContext>
	// UnityEngine.Events.UnityAction<byte>
	// UnityEngine.Events.UnityAction<int>
	// UnityEngine.Events.UnityAction<long>
	// UnityEngine.Events.UnityAction<object>
	// UnityEngine.Events.UnityEvent<UnityEngine.InputSystem.InputAction.CallbackContext>
	// UnityEngine.Events.UnityEvent<byte>
	// UnityEngine.Events.UnityEvent<int>
	// UnityEngine.Events.UnityEvent<long>
	// UnityEngine.Events.UnityEvent<object>
	// UnityEngine.InputSystem.InputBindingComposite<UnityEngine.Vector2>
	// UnityEngine.InputSystem.InputControl<UnityEngine.Vector2>
	// UnityEngine.InputSystem.InputProcessor<UnityEngine.Vector2>
	// UnityEngine.InputSystem.Utilities.InlinedArray<object>
	// }}

	public void RefMethods()
	{
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ASceneCreater.<OnCreateComplete>d__4>(ET.ETTaskCompleted&,ET.ASceneCreater.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.AWorldViewSceneCreater.<OnCreate>d__1>(ET.ETTaskCompleted&,ET.AWorldViewSceneCreater.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.AWorldViewSceneCreater.<OnDestroy>d__2>(ET.ETTaskCompleted&,ET.AWorldViewSceneCreater.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ChangePosition_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.ChangePosition_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ChangeRotation_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.ChangeRotation_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ChangeSpeed_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.ChangeSpeed_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.Client.FiberCreate_NetClient.<Handle>d__0>(ET.ETTaskCompleted&,ET.Client.FiberCreate_NetClient.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusher.SpinDieDieLeCoinChange_Current.<Run>d__0>(ET.ETTaskCompleted&,ET.CoinPusher.SpinDieDieLeCoinChange_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0>(ET.ETTaskCompleted&,ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusher.SpinExitDieDieLe_Current.<Run>d__0>(ET.ETTaskCompleted&,ET.CoinPusher.SpinExitDieDieLe_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusher.SpinExitLittleMary_Current.<Run>d__0>(ET.ETTaskCompleted&,ET.CoinPusher.SpinExitLittleMary_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusherSceneCreater0.<OnCreate>d__1>(ET.ETTaskCompleted&,ET.CoinPusherSceneCreater0.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusherSceneCreater0.<OnDestroy>d__2>(ET.ETTaskCompleted&,ET.CoinPusherSceneCreater0.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.CoinPusherSceneCreater1.<OnDestroy>d__2>(ET.ETTaskCompleted&,ET.CoinPusherSceneCreater1.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MainSceneCreater1.<OnCreate>d__3>(ET.ETTaskCompleted&,ET.MainSceneCreater1.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MainSceneCreater3.<OnCreate>d__3>(ET.ETTaskCompleted&,ET.MainSceneCreater3.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MainSceneCreater3.<OnCreateComplete>d__4>(ET.ETTaskCompleted&,ET.MainSceneCreater3.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MainSceneCreater4.<OnCreate>d__3>(ET.ETTaskCompleted&,ET.MainSceneCreater4.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.MainSceneCreater5.<OnCreate>d__3>(ET.ETTaskCompleted&,ET.MainSceneCreater5.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.ETTaskCompleted&,ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ETTaskCompleted&,ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.SceneChangeFinish_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.SceneChangeFinish_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.SceneChangeStart_Event.<Run>d__0>(ET.ETTaskCompleted&,ET.SceneChangeStart_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ViewPanel.<InitAsync>d__4<object>>(ET.ETTaskCompleted&,ET.ViewPanel.<InitAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.GetAllConfigBytes.<LoadConfigAsyncByYooAsset>d__2>(System.Runtime.CompilerServices.TaskAwaiter&,ET.GetAllConfigBytes.<LoadConfigAsyncByYooAsset>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.UIPackageHelper.<<AddPackageAsync>g__LoadResourceTask|5_2>d>(System.Runtime.CompilerServices.TaskAwaiter&,ET.UIPackageHelper.<<AddPackageAsync>g__LoadResourceTask|5_2>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.AppInit_Invoke.<Handle>d__0>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.AppInit_Invoke.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.ConsoleComponentSystem.<Start>d__1>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.ConsoleComponentSystem.<Start>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.MainSceneCreater6.<OnCreate>d__2>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.MainSceneCreater6.<OnCreate>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.MainSceneCreater7.<CreateAsync>d__4<object>>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.MainSceneCreater7.<CreateAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AViewSceneCreater.<LoadScene>d__2>(object&,ET.AViewSceneCreater.<LoadScene>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.AppInit_Invoke.<Handle>d__0>(object&,ET.AppInit_Invoke.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Authentication.<InitializeAsync>d__2>(object&,ET.Authentication.<InitializeAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ChaoJiMoFang.Com_DieDieLeList.<AddNewOne>d__22>(object&,ET.ChaoJiMoFang.Com_DieDieLeList.<AddNewOne>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ChaoJiMoFang.Com_DieDieLeText.<ChangeText>d__20>(object&,ET.ChaoJiMoFang.Com_DieDieLeText.<ChangeText>d__20&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.PingComponentSystem.<PingAsync>d__2>(object&,ET.Client.PingComponentSystem.<PingAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5>(object&,ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ClientSessionErrorComponentSystem.<SessionDisposeTip>d__2>(object&,ET.ClientSessionErrorComponentSystem.<SessionDisposeTip>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ClientSessionErrorComponentSystem.<ShowTipLater>d__3>(object&,ET.ClientSessionErrorComponentSystem.<ShowTipLater>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinDieDieLeAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinDieDieLeAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinDieDieLeDoubleAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinDieDieLeDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinDoubleAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinJPAward_Current.<PlayAnimation>d__1>(object&,ET.CoinPusher.CoinJPAward_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinJPAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinJPAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinJPDoubleAward_Current.<PlayAnimation>d__1>(object&,ET.CoinPusher.CoinJPDoubleAward_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinJPDoubleAward_Current.<Run>d__0>(object&,ET.CoinPusher.CoinJPDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<<ShowSpin>g__ScrollAsync|30_1>d>(object&,ET.CoinPusher.CoinPusherPanel.<<ShowSpin>g__ScrollAsync|30_1>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<AddDieDieLeMultiple>d__35>(object&,ET.CoinPusher.CoinPusherPanel.<AddDieDieLeMultiple>d__35&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<LoadSpine>d__23>(object&,ET.CoinPusher.CoinPusherPanel.<LoadSpine>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<ShowDieDieLeResult>d__37>(object&,ET.CoinPusher.CoinPusherPanel.<ShowDieDieLeResult>d__37&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<ShowLine>d__31>(object&,ET.CoinPusher.CoinPusherPanel.<ShowLine>d__31&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<ShowLittleMary>d__33>(object&,ET.CoinPusher.CoinPusherPanel.<ShowLittleMary>d__33&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<ShowSpin>d__30>(object&,ET.CoinPusher.CoinPusherPanel.<ShowSpin>d__30&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.CoinPusherPanel.<ShowSpinAndLine>d__32>(object&,ET.CoinPusher.CoinPusherPanel.<ShowSpinAndLine>d__32&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.ShowSpinLine_Current.<Run>d__0>(object&,ET.CoinPusher.ShowSpinLine_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.ShowSpinResultAndLine_Current.<Run>d__0>(object&,ET.CoinPusher.ShowSpinResultAndLine_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.ShowSpinResult_Current.<Run>d__0>(object&,ET.CoinPusher.ShowSpinResult_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinDieDieLeMultipleChange_Current.<Run>d__0>(object&,ET.CoinPusher.SpinDieDieLeMultipleChange_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinDieDieLeWait_Current.<Run>d__0>(object&,ET.CoinPusher.SpinDieDieLeWait_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterDieDieLe_Current.<PlayAnimation>d__1>(object&,ET.CoinPusher.SpinEnterDieDieLe_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterDieDieLe_Current.<Run>d__0>(object&,ET.CoinPusher.SpinEnterDieDieLe_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterDouble_Current.<PlayAnimation>d__1>(object&,ET.CoinPusher.SpinEnterDouble_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterDouble_Current.<Run>d__0>(object&,ET.CoinPusher.SpinEnterDouble_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterLittleMary_Current.<PlayAnimation>d__1>(object&,ET.CoinPusher.SpinEnterLittleMary_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0>(object&,ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusher.SpinLittleMaryShow_Current.<Run>d__0>(object&,ET.CoinPusher.SpinLittleMaryShow_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusherManager.<>c__DisplayClass65_0.<<RunSpin>g__IncMultipleCoroutine|5>d>(object&,ET.CoinPusherManager.<>c__DisplayClass65_0.<<RunSpin>g__IncMultipleCoroutine|5>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusherManager.<>c__DisplayClass66_0.<<ReawardDouble>g__IncMultipleCoroutine|5>d>(object&,ET.CoinPusherManager.<>c__DisplayClass66_0.<<ReawardDouble>g__IncMultipleCoroutine|5>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusherManager.<ReawardDouble>d__66>(object&,ET.CoinPusherManager.<ReawardDouble>d__66&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusherManager.<RunSpin>d__65>(object&,ET.CoinPusherManager.<RunSpin>d__65&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoinPusherSceneCreater1.<OnCreate>d__1>(object&,ET.CoinPusherSceneCreater1.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ConsoleComponentSystem.<Start>d__1>(object&,ET.ConsoleComponentSystem.<Start>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.CoroutineHelper.<GetAwaiter>d__0>(object&,ET.CoroutineHelper.<GetAwaiter>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.EnterGame_Main.<Run>d__0>(object&,ET.EnterGame_Main.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.EnterWorld_Main.<Run>d__0>(object&,ET.EnterWorld_Main.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.Entry.<StartAsync>d__2>(object&,ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.FiberCreate_Main.<Handle>d__0>(object&,ET.FiberCreate_Main.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MailBoxType_OrderedMessageHandler.<HandleInner>d__1>(object&,ET.MailBoxType_OrderedMessageHandler.<HandleInner>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MailBoxType_UnOrderedMessageHandler.<HandleAsync>d__1>(object&,ET.MailBoxType_UnOrderedMessageHandler.<HandleAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MainSceneCreater1.<OnCreateComplete>d__4>(object&,ET.MainSceneCreater1.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MainSceneCreater6.<OnCreate>d__2>(object&,ET.MainSceneCreater6.<OnCreate>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MainSceneCreater7.<CreateAsync>d__4<object>>(object&,ET.MainSceneCreater7.<CreateAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MainSceneCreater7.<OnCreate>d__3>(object&,ET.MainSceneCreater7.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MessageHandler.<Handle>d__1<object,object,object>>(object&,ET.MessageHandler.<Handle>d__1<object,object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MessageHandler.<Handle>d__1<object,object>>(object&,ET.MessageHandler.<Handle>d__1<object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MessageSessionHandler.<HandleAsync>d__2<object,object>>(object&,ET.MessageSessionHandler.<HandleAsync>d__2<object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.MessageSessionHandler.<HandleAsync>d__2<object>>(object&,ET.MessageSessionHandler.<HandleAsync>d__2<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ObjectEvent.<InvokeInternal>d__4>(object&,ET.ObjectEvent.<InvokeInternal>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<object>>(object&,ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.PSessionSystem.<>c__DisplayClass10_0.<<Call>g__Timeout|0>d<object>>(object&,ET.PSessionSystem.<>c__DisplayClass10_0.<<Call>g__Timeout|0>d<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ReloadConfigConsoleHandler.<Run>d__0>(object&,ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.SceneCreatDispatcher.<OnCreate>d__4>(object&,ET.SceneCreatDispatcher.<OnCreate>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.SceneCreatDispatcher.<OnCreateComplete>d__5>(object&,ET.SceneCreatDispatcher.<OnCreateComplete>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.SceneCreatDispatcher.<OnDestory>d__6>(object&,ET.SceneCreatDispatcher.<OnDestory>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.SceneEventDispatcher.<Handle>d__2<object>>(object&,ET.SceneEventDispatcher.<Handle>d__2<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<>c__DisplayClass4_0.<<Call>g__Timeout|0>d>(object&,ET.SessionSystem.<>c__DisplayClass4_0.<<Call>g__Timeout|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.UIPackageHelper.<AddPackageAsync>d__5>(object&,ET.UIPackageHelper.<AddPackageAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ViewController.<InitChildren>d__16<object>>(object&,ET.ViewController.<InitChildren>d__16<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<object,ET.ViewController.MutipileViewPanel.<InitAsync>d__3<object>>(object&,ET.ViewController.MutipileViewPanel.<InitAsync>d__3<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<int,object>>.AwaitUnsafeOnCompleted<object,ET.ProtoMessageHelper.<ToMessageAsync>d__6>(object&,ET.ProtoMessageHelper.<ToMessageAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.ADataSceneCreater.<TryCreate>d__1>(ET.ETTaskCompleted&,ET.ADataSceneCreater.<TryCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.AViewSceneCreater.<TryCreate>d__1>(ET.ETTaskCompleted&,ET.AViewSceneCreater.<TryCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.AWorldViewSceneCreater.<TryCreate>d__0>(object&,ET.AWorldViewSceneCreater.<TryCreate>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.MoveComponentSystem.<MoveToAsync>d__5>(object&,ET.MoveComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.AwaitUnsafeOnCompleted<object,ET.SceneCreatDispatcher.<TryCreate>d__3>(object&,ET.SceneCreatDispatcher.<TryCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<ET.ETTaskCompleted,ET.GetOneConfigBytes.<Handle>d__0>(ET.ETTaskCompleted&,ET.GetOneConfigBytes.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.AssetHandleExtension.<WaitAsync>d__0>(System.Runtime.CompilerServices.TaskAwaiter&,ET.AssetHandleExtension.<WaitAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>>(System.Runtime.CompilerServices.TaskAwaiter&,ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.GetAllConfigBytes.<Handle>d__0>(System.Runtime.CompilerServices.TaskAwaiter&,ET.GetAllConfigBytes.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter,ET.UIPackageHelper.<<AddPackageAsync>g__LoadDescAsync|5_0>d>(System.Runtime.CompilerServices.TaskAwaiter&,ET.UIPackageHelper.<<AddPackageAsync>g__LoadDescAsync|5_0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.ProtoMessageHelper.<DeserializeAsync>d__1>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.ProtoMessageHelper.<DeserializeAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>>(object&,ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>>(object&,ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.CoroutineHelper.<HttpGet>d__1>(object&,ET.CoroutineHelper.<HttpGet>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.CurrentSceneFactory.<Create>d__2>(object&,ET.CurrentSceneFactory.<Create>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.CurrentSceneFactory.<CreateInternal>d__3>(object&,ET.CurrentSceneFactory.<CreateInternal>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.CurrentSceneFactory.<EnterGame>d__1>(object&,ET.CurrentSceneFactory.<EnterGame>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.CurrentSceneFactory.<EnterScene>d__0>(object&,ET.CurrentSceneFactory.<EnterScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.GetAllConfigBytes.<Handle>d__0>(object&,ET.GetAllConfigBytes.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<Wait>d__4<object>>(object&,ET.ObjectWaitSystem.<Wait>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.ObjectWaitSystem.<Wait>d__5<object>>(object&,ET.ObjectWaitSystem.<Wait>d__5<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.PSessionSystem.<Call>d__10<object>>(object&,ET.PSessionSystem.<Call>d__10<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.PSessionSystem.<Call>d__7>(object&,ET.PSessionSystem.<Call>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.PSessionSystem.<Call>d__8<object>>(object&,ET.PSessionSystem.<Call>d__8<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.PSessionSystem.<Call>d__9>(object&,ET.PSessionSystem.<Call>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.RpcInfo.<Wait>d__7>(object&,ET.RpcInfo.<Wait>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__3>(object&,ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.SessionSystem.<Call>d__4>(object&,ET.SessionSystem.<Call>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.UnitComponentSystem.<CreateWithEvent>d__3>(object&,ET.UnitComponentSystem.<CreateWithEvent>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.UnitComponentSystem.<CreateWithEvent>d__4>(object&,ET.UnitComponentSystem.<CreateWithEvent>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.AwaitUnsafeOnCompleted<object,ET.ViewController.<InitPanel>d__15<object>>(object&,ET.ViewController.<InitPanel>d__15<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ASceneCreater.<OnCreateComplete>d__4>(ET.ASceneCreater.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AViewSceneCreater.<LoadScene>d__2>(ET.AViewSceneCreater.<LoadScene>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AWorldViewSceneCreater.<OnCreate>d__1>(ET.AWorldViewSceneCreater.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AWorldViewSceneCreater.<OnDestroy>d__2>(ET.AWorldViewSceneCreater.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.AppInit_Invoke.<Handle>d__0>(ET.AppInit_Invoke.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Authentication.<InitializeAsync>d__2>(ET.Authentication.<InitializeAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ChangePosition_Event.<Run>d__0>(ET.ChangePosition_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ChangeRotation_Event.<Run>d__0>(ET.ChangeRotation_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ChangeSpeed_Event.<Run>d__0>(ET.ChangeSpeed_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ChaoJiMoFang.Com_DieDieLeList.<AddNewOne>d__22>(ET.ChaoJiMoFang.Com_DieDieLeList.<AddNewOne>d__22&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ChaoJiMoFang.Com_DieDieLeText.<ChangeText>d__20>(ET.ChaoJiMoFang.Com_DieDieLeText.<ChangeText>d__20&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.FiberCreate_NetClient.<Handle>d__0>(ET.Client.FiberCreate_NetClient.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.PingComponentSystem.<PingAsync>d__2>(ET.Client.PingComponentSystem.<PingAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5>(ET.Client.ResourcesLoaderComponentSystem.<LoadSceneAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ClientSessionErrorComponentSystem.<SessionDisposeTip>d__2>(ET.ClientSessionErrorComponentSystem.<SessionDisposeTip>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ClientSessionErrorComponentSystem.<ShowTipLater>d__3>(ET.ClientSessionErrorComponentSystem.<ShowTipLater>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinAward_Current.<Run>d__0>(ET.CoinPusher.CoinAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinDieDieLeAward_Current.<Run>d__0>(ET.CoinPusher.CoinDieDieLeAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinDieDieLeDoubleAward_Current.<Run>d__0>(ET.CoinPusher.CoinDieDieLeDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinDoubleAward_Current.<Run>d__0>(ET.CoinPusher.CoinDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinJPAward_Current.<PlayAnimation>d__1>(ET.CoinPusher.CoinJPAward_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinJPAward_Current.<Run>d__0>(ET.CoinPusher.CoinJPAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinJPDoubleAward_Current.<PlayAnimation>d__1>(ET.CoinPusher.CoinJPDoubleAward_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinJPDoubleAward_Current.<Run>d__0>(ET.CoinPusher.CoinJPDoubleAward_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<<ShowSpin>g__ScrollAsync|30_1>d>(ET.CoinPusher.CoinPusherPanel.<<ShowSpin>g__ScrollAsync|30_1>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<AddDieDieLeMultiple>d__35>(ET.CoinPusher.CoinPusherPanel.<AddDieDieLeMultiple>d__35&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<LoadSpine>d__23>(ET.CoinPusher.CoinPusherPanel.<LoadSpine>d__23&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<ShowDieDieLeResult>d__37>(ET.CoinPusher.CoinPusherPanel.<ShowDieDieLeResult>d__37&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<ShowLine>d__31>(ET.CoinPusher.CoinPusherPanel.<ShowLine>d__31&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<ShowLittleMary>d__33>(ET.CoinPusher.CoinPusherPanel.<ShowLittleMary>d__33&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<ShowSpin>d__30>(ET.CoinPusher.CoinPusherPanel.<ShowSpin>d__30&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.CoinPusherPanel.<ShowSpinAndLine>d__32>(ET.CoinPusher.CoinPusherPanel.<ShowSpinAndLine>d__32&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.ShowSpinLine_Current.<Run>d__0>(ET.CoinPusher.ShowSpinLine_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.ShowSpinResultAndLine_Current.<Run>d__0>(ET.CoinPusher.ShowSpinResultAndLine_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.ShowSpinResult_Current.<Run>d__0>(ET.CoinPusher.ShowSpinResult_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinDieDieLeCoinChange_Current.<Run>d__0>(ET.CoinPusher.SpinDieDieLeCoinChange_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinDieDieLeMultipleChange_Current.<Run>d__0>(ET.CoinPusher.SpinDieDieLeMultipleChange_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinDieDieLeWait_Current.<Run>d__0>(ET.CoinPusher.SpinDieDieLeWait_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterDieDieLe_Current.<PlayAnimation>d__1>(ET.CoinPusher.SpinEnterDieDieLe_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterDieDieLe_Current.<Run>d__0>(ET.CoinPusher.SpinEnterDieDieLe_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterDouble_Current.<PlayAnimation>d__1>(ET.CoinPusher.SpinEnterDouble_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterDouble_Current.<Run>d__0>(ET.CoinPusher.SpinEnterDouble_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterLittleMary_Current.<PlayAnimation>d__1>(ET.CoinPusher.SpinEnterLittleMary_Current.<PlayAnimation>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0>(ET.CoinPusher.SpinEnterLittleMary_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinExitDieDieLe_Current.<Run>d__0>(ET.CoinPusher.SpinExitDieDieLe_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinExitLittleMary_Current.<Run>d__0>(ET.CoinPusher.SpinExitLittleMary_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusher.SpinLittleMaryShow_Current.<Run>d__0>(ET.CoinPusher.SpinLittleMaryShow_Current.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherManager.<>c__DisplayClass65_0.<<RunSpin>g__IncMultipleCoroutine|5>d>(ET.CoinPusherManager.<>c__DisplayClass65_0.<<RunSpin>g__IncMultipleCoroutine|5>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherManager.<>c__DisplayClass66_0.<<ReawardDouble>g__IncMultipleCoroutine|5>d>(ET.CoinPusherManager.<>c__DisplayClass66_0.<<ReawardDouble>g__IncMultipleCoroutine|5>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherManager.<ReawardDouble>d__66>(ET.CoinPusherManager.<ReawardDouble>d__66&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherManager.<RunSpin>d__65>(ET.CoinPusherManager.<RunSpin>d__65&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherSceneCreater0.<OnCreate>d__1>(ET.CoinPusherSceneCreater0.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherSceneCreater0.<OnDestroy>d__2>(ET.CoinPusherSceneCreater0.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherSceneCreater1.<OnCreate>d__1>(ET.CoinPusherSceneCreater1.<OnCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoinPusherSceneCreater1.<OnDestroy>d__2>(ET.CoinPusherSceneCreater1.<OnDestroy>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ConsoleComponentSystem.<Start>d__1>(ET.ConsoleComponentSystem.<Start>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.CoroutineHelper.<GetAwaiter>d__0>(ET.CoroutineHelper.<GetAwaiter>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EnterGame_Main.<Run>d__0>(ET.EnterGame_Main.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EnterWorld_Main.<Run>d__0>(ET.EnterWorld_Main.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.Entry.<StartAsync>d__2>(ET.Entry.<StartAsync>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.AfterCreateCurrentScene>>(ET.EventSystem.<PublishAsync>d__4<object,ET.AfterCreateCurrentScene>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.AfterCreateUnit>>(ET.EventSystem.<PublishAsync>d__4<object,ET.AfterCreateUnit>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.BeforeDestroyCurrentScene>>(ET.EventSystem.<PublishAsync>d__4<object,ET.BeforeDestroyCurrentScene>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDieDieLeAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDieDieLeAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDieDieLeDoubleAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDieDieLeDoubleAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDoubleAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinDoubleAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinJPAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinJPAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.CoinJPDoubleAward>>(ET.EventSystem.<PublishAsync>d__4<object,ET.CoinJPDoubleAward>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.EnterGame>>(ET.EventSystem.<PublishAsync>d__4<object,ET.EnterGame>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.EnterWorld>>(ET.EventSystem.<PublishAsync>d__4<object,ET.EnterWorld>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SceneChangeFinish>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SceneChangeFinish>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SceneChangeStart>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SceneChangeStart>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.ShowSpinLine>>(ET.EventSystem.<PublishAsync>d__4<object,ET.ShowSpinLine>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.ShowSpinResult>>(ET.EventSystem.<PublishAsync>d__4<object,ET.ShowSpinResult>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinDieDieLeMultipleChange>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinDieDieLeMultipleChange>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinDieDieLeWait>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinDieDieLeWait>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterDieDieLe>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterDieDieLe>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterDouble>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterDouble>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterLittleMary>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinEnterLittleMary>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinExitDieDieLe>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinExitDieDieLe>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinExitLittleMary>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinExitLittleMary>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.EventSystem.<PublishAsync>d__4<object,ET.SpinLittleMaryShow>>(ET.EventSystem.<PublishAsync>d__4<object,ET.SpinLittleMaryShow>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.FiberCreate_Main.<Handle>d__0>(ET.FiberCreate_Main.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.GetAllConfigBytes.<LoadConfigAsyncByYooAsset>d__2>(ET.GetAllConfigBytes.<LoadConfigAsyncByYooAsset>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MailBoxType_OrderedMessageHandler.<HandleInner>d__1>(ET.MailBoxType_OrderedMessageHandler.<HandleInner>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MailBoxType_UnOrderedMessageHandler.<HandleAsync>d__1>(ET.MailBoxType_UnOrderedMessageHandler.<HandleAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater1.<OnCreate>d__3>(ET.MainSceneCreater1.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater1.<OnCreateComplete>d__4>(ET.MainSceneCreater1.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater3.<OnCreate>d__3>(ET.MainSceneCreater3.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater3.<OnCreateComplete>d__4>(ET.MainSceneCreater3.<OnCreateComplete>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater4.<OnCreate>d__3>(ET.MainSceneCreater4.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater5.<OnCreate>d__3>(ET.MainSceneCreater5.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater6.<OnCreate>d__2>(ET.MainSceneCreater6.<OnCreate>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater7.<CreateAsync>d__4<object>>(ET.MainSceneCreater7.<CreateAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MainSceneCreater7.<OnCreate>d__3>(ET.MainSceneCreater7.<OnCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MessageHandler.<Handle>d__1<object,object,object>>(ET.MessageHandler.<Handle>d__1<object,object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MessageHandler.<Handle>d__1<object,object>>(ET.MessageHandler.<Handle>d__1<object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MessageSessionHandler.<HandleAsync>d__2<object,object>>(ET.MessageSessionHandler.<HandleAsync>d__2<object,object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.MessageSessionHandler.<HandleAsync>d__2<object>>(ET.MessageSessionHandler.<HandleAsync>d__2<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.NumericChangeEvent_NotifyWatcher.<Run>d__0>(ET.NumericChangeEvent_NotifyWatcher.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ObjectEvent.<InvokeInternal>d__4>(ET.ObjectEvent.<InvokeInternal>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<object>>(ET.ObjectWaitSystem.<>c__DisplayClass5_0.<<Wait>g__WaitTimeout|0>d<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.PSessionSystem.<>c__DisplayClass10_0.<<Call>g__Timeout|0>d<object>>(ET.PSessionSystem.<>c__DisplayClass10_0.<<Call>g__Timeout|0>d<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadConfigConsoleHandler.<Run>d__0>(ET.ReloadConfigConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ReloadDllConsoleHandler.<Run>d__0>(ET.ReloadDllConsoleHandler.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneChangeFinish_Event.<Run>d__0>(ET.SceneChangeFinish_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneChangeStart_Event.<Run>d__0>(ET.SceneChangeStart_Event.<Run>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneCreatDispatcher.<OnCreate>d__4>(ET.SceneCreatDispatcher.<OnCreate>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneCreatDispatcher.<OnCreateComplete>d__5>(ET.SceneCreatDispatcher.<OnCreateComplete>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneCreatDispatcher.<OnDestory>d__6>(ET.SceneCreatDispatcher.<OnDestory>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SceneEventDispatcher.<Handle>d__2<object>>(ET.SceneEventDispatcher.<Handle>d__2<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.SessionSystem.<>c__DisplayClass4_0.<<Call>g__Timeout|0>d>(ET.SessionSystem.<>c__DisplayClass4_0.<<Call>g__Timeout|0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.UIPackageHelper.<<AddPackageAsync>g__LoadResourceTask|5_2>d>(ET.UIPackageHelper.<<AddPackageAsync>g__LoadResourceTask|5_2>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.UIPackageHelper.<AddPackageAsync>d__5>(ET.UIPackageHelper.<AddPackageAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ViewController.<InitChildren>d__16<object>>(ET.ViewController.<InitChildren>d__16<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ViewController.MutipileViewPanel.<InitAsync>d__3<object>>(ET.ViewController.MutipileViewPanel.<InitAsync>d__3<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder.Start<ET.ViewPanel.<InitAsync>d__4<object>>(ET.ViewPanel.<InitAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<System.ValueTuple<int,object>>.Start<ET.ProtoMessageHelper.<ToMessageAsync>d__6>(ET.ProtoMessageHelper.<ToMessageAsync>d__6&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.ADataSceneCreater.<TryCreate>d__1>(ET.ADataSceneCreater.<TryCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.AViewSceneCreater.<TryCreate>d__1>(ET.AViewSceneCreater.<TryCreate>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.AWorldViewSceneCreater.<TryCreate>d__0>(ET.AWorldViewSceneCreater.<TryCreate>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.MoveComponentSystem.<MoveToAsync>d__5>(ET.MoveComponentSystem.<MoveToAsync>d__5&)
		// System.Void ET.ETAsyncTaskMethodBuilder<byte>.Start<ET.SceneCreatDispatcher.<TryCreate>d__3>(ET.SceneCreatDispatcher.<TryCreate>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.AssetHandleExtension.<WaitAsync>d__0>(ET.AssetHandleExtension.<WaitAsync>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>>(ET.Client.ResourcesLoaderComponentSystem.<LoadAllAssetsAsync>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>>(ET.Client.ResourcesLoaderComponentSystem.<LoadAssetAsync>d__3<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.CoroutineHelper.<HttpGet>d__1>(ET.CoroutineHelper.<HttpGet>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.CurrentSceneFactory.<Create>d__2>(ET.CurrentSceneFactory.<Create>d__2&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.CurrentSceneFactory.<CreateInternal>d__3>(ET.CurrentSceneFactory.<CreateInternal>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.CurrentSceneFactory.<EnterGame>d__1>(ET.CurrentSceneFactory.<EnterGame>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.CurrentSceneFactory.<EnterScene>d__0>(ET.CurrentSceneFactory.<EnterScene>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.GetAllConfigBytes.<Handle>d__0>(ET.GetAllConfigBytes.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.GetOneConfigBytes.<Handle>d__0>(ET.GetOneConfigBytes.<Handle>d__0&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.ObjectWaitSystem.<Wait>d__4<object>>(ET.ObjectWaitSystem.<Wait>d__4<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.ObjectWaitSystem.<Wait>d__5<object>>(ET.ObjectWaitSystem.<Wait>d__5<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.PSessionSystem.<Call>d__10<object>>(ET.PSessionSystem.<Call>d__10<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.PSessionSystem.<Call>d__7>(ET.PSessionSystem.<Call>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.PSessionSystem.<Call>d__8<object>>(ET.PSessionSystem.<Call>d__8<object>&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.PSessionSystem.<Call>d__9>(ET.PSessionSystem.<Call>d__9&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.ProtoMessageHelper.<DeserializeAsync>d__1>(ET.ProtoMessageHelper.<DeserializeAsync>d__1&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.RpcInfo.<Wait>d__7>(ET.RpcInfo.<Wait>d__7&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__3>(ET.SessionSystem.<Call>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.SessionSystem.<Call>d__4>(ET.SessionSystem.<Call>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.UIPackageHelper.<<AddPackageAsync>g__LoadDescAsync|5_0>d>(ET.UIPackageHelper.<<AddPackageAsync>g__LoadDescAsync|5_0>d&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.UnitComponentSystem.<CreateWithEvent>d__3>(ET.UnitComponentSystem.<CreateWithEvent>d__3&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.UnitComponentSystem.<CreateWithEvent>d__4>(ET.UnitComponentSystem.<CreateWithEvent>d__4&)
		// System.Void ET.ETAsyncTaskMethodBuilder<object>.Start<ET.ViewController.<InitPanel>d__15<object>>(ET.ViewController.<InitPanel>d__15<object>&)
		// object ET.Entity.AddChild<object>(bool)
		// object ET.Entity.AddChildWithId<object,object,object>(long,object,object,bool)
		// object ET.Entity.AddChildWithId<object,object>(long,object,bool)
		// object ET.Entity.AddChildWithId<object>(long,bool)
		// object ET.Entity.AddComponent<object,int,int>(int,int,bool)
		// object ET.Entity.AddComponent<object,int>(int,bool)
		// object ET.Entity.AddComponent<object,object>(object,bool)
		// object ET.Entity.AddComponent<object>(bool)
		// object ET.Entity.AddComponentWithId<object,int,int>(long,int,int,bool)
		// object ET.Entity.AddComponentWithId<object,int>(long,int,bool)
		// object ET.Entity.AddComponentWithId<object,object>(long,object,bool)
		// object ET.Entity.AddComponentWithId<object>(long,bool)
		// object ET.Entity.GetChild<object>(long)
		// object ET.Entity.GetComponent<object>()
		// object ET.Entity.GetParent<object>()
		// System.Void ET.Entity.RemoveComponent<object>()
		// System.Void ET.EntitySystemSingleton.Awake<int,int>(ET.Entity,int,int)
		// System.Void ET.EntitySystemSingleton.Awake<int>(ET.Entity,int)
		// System.Void ET.EntitySystemSingleton.Awake<object,object>(ET.Entity,object,object)
		// System.Void ET.EntitySystemSingleton.Awake<object>(ET.Entity,object)
		// System.Void ET.EventSystem.Invoke<ET.Client.BackLogin>(ET.Client.BackLogin)
		// System.Void ET.EventSystem.Invoke<ET.Client.BackLogin>(long,ET.Client.BackLogin)
		// System.Void ET.EventSystem.Invoke<ET.NetComponentOnRead>(long,ET.NetComponentOnRead)
		// System.Void ET.EventSystem.Invoke<ET.NetProtoComponentOnRead>(long,ET.NetProtoComponentOnRead)
		// object ET.EventSystem.Invoke<ET.Client.AppInit,object>(ET.Client.AppInit)
		// object ET.EventSystem.Invoke<ET.Client.AppInit,object>(long,ET.Client.AppInit)
		// System.Void ET.EventSystem.Publish<object,ET.ChangePosition>(object,ET.ChangePosition)
		// System.Void ET.EventSystem.Publish<object,ET.ChangeRotation>(object,ET.ChangeRotation)
		// System.Void ET.EventSystem.Publish<object,ET.MoveStart>(object,ET.MoveStart)
		// System.Void ET.EventSystem.Publish<object,ET.MoveStop>(object,ET.MoveStop)
		// System.Void ET.EventSystem.Publish<object,ET.NumbericChange>(object,ET.NumbericChange)
		// System.Void ET.EventSystem.Publish<object,ET.SpinDieDieLeCoinChange>(object,ET.SpinDieDieLeCoinChange)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.AfterCreateCurrentScene>(object,ET.AfterCreateCurrentScene)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.AfterCreateUnit>(object,ET.AfterCreateUnit)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.BeforeDestroyCurrentScene>(object,ET.BeforeDestroyCurrentScene)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinAward>(object,ET.CoinAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinDieDieLeAward>(object,ET.CoinDieDieLeAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinDieDieLeDoubleAward>(object,ET.CoinDieDieLeDoubleAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinDoubleAward>(object,ET.CoinDoubleAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinJPAward>(object,ET.CoinJPAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.CoinJPDoubleAward>(object,ET.CoinJPDoubleAward)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EnterGame>(object,ET.EnterGame)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.EnterWorld>(object,ET.EnterWorld)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SceneChangeFinish>(object,ET.SceneChangeFinish)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SceneChangeStart>(object,ET.SceneChangeStart)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.ShowSpinLine>(object,ET.ShowSpinLine)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.ShowSpinResult>(object,ET.ShowSpinResult)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinDieDieLeMultipleChange>(object,ET.SpinDieDieLeMultipleChange)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinDieDieLeWait>(object,ET.SpinDieDieLeWait)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinEnterDieDieLe>(object,ET.SpinEnterDieDieLe)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinEnterDouble>(object,ET.SpinEnterDouble)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinEnterLittleMary>(object,ET.SpinEnterLittleMary)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinExitDieDieLe>(object,ET.SpinExitDieDieLe)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinExitLittleMary>(object,ET.SpinExitLittleMary)
		// ET.ETTask ET.EventSystem.PublishAsync<object,ET.SpinLittleMaryShow>(object,ET.SpinLittleMaryShow)
		// object ET.World.AddSingleton<object>()
		// object System.Activator.CreateInstance<object>()
		// object[] System.Array.Empty<object>()
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Cast<object>(System.Collections.IEnumerable)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.CastIterator<object>(System.Collections.IEnumerable)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Select<System.Collections.Generic.KeyValuePair<int,int>,int>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>,System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.SkipWhile<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.SkipWhileIterator<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// int System.Linq.Enumerable.Sum<System.Collections.Generic.KeyValuePair<int,int>>(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,int>>,System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>)
		// System.Collections.Generic.List<object> System.Linq.Enumerable.ToList<object>(System.Collections.Generic.IEnumerable<object>)
		// System.Collections.Generic.IEnumerable<object> System.Linq.Enumerable.Where<object>(System.Collections.Generic.IEnumerable<object>,System.Func<object,bool>)
		// System.Collections.Generic.IEnumerable<int> System.Linq.Enumerable.Iterator<System.Collections.Generic.KeyValuePair<int,int>>.Select<int>(System.Func<System.Collections.Generic.KeyValuePair<int,int>,int>)
		// object System.Reflection.CustomAttributeExtensions.GetCustomAttribute<object>(System.Reflection.MemberInfo)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Threading.Tasks.VoidTaskResult>.AwaitUnsafeOnCompleted<System.Runtime.CompilerServices.TaskAwaiter<object>,ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1>(System.Runtime.CompilerServices.TaskAwaiter<object>&,ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1&)
		// System.Void System.Runtime.CompilerServices.AsyncTaskMethodBuilder.Start<ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1>(ET.GetAllConfigBytes.<LoadConfigAsyncByFile>d__1&)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.Task.Run<object>(System.Func<object>)
		// System.Threading.Tasks.Task<object> System.Threading.Tasks.TaskFactory.StartNew<object>(System.Func<object>,System.Threading.CancellationToken)
		// System.Void* Unity.Collections.LowLevel.Unsafe.UnsafeUtility.AddressOf<UnityEngine.Vector2>(UnityEngine.Vector2&)
		// int Unity.Collections.LowLevel.Unsafe.UnsafeUtility.SizeOf<UnityEngine.Vector2>()
		// object UnityEngine.Component.GetComponent<object>()
		// object UnityEngine.Component.GetComponentInChildren<object>()
		// bool UnityEngine.Component.TryGetComponent<object>(object&)
		// object UnityEngine.GameObject.AddComponent<object>()
		// object UnityEngine.GameObject.GetComponent<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>()
		// object UnityEngine.GameObject.GetComponentInChildren<object>(bool)
		// bool UnityEngine.GameObject.TryGetComponent<object>(object&)
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputAction.CallbackContext.ReadValue<UnityEngine.Vector2>()
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputActionState.ApplyProcessors<UnityEngine.Vector2>(int,UnityEngine.Vector2,UnityEngine.InputSystem.InputControl<UnityEngine.Vector2>)
		// UnityEngine.Vector2 UnityEngine.InputSystem.InputActionState.ReadValue<UnityEngine.Vector2>(int,int,bool)
		// object UnityEngine.Object.FindAnyObjectByType<object>()
		// object UnityEngine.Object.FindObjectOfType<object>()
		// object UnityEngine.Object.Instantiate<object>(object)
		// object UnityEngine.Object.Instantiate<object>(object,UnityEngine.Vector3,UnityEngine.Quaternion,UnityEngine.Transform)
		// object UnityEngine.Resources.Load<object>(string)
		// object YooAsset.AssetHandle.GetAssetObject<object>()
		// YooAsset.AllAssetsHandle YooAsset.ResourcePackage.LoadAllAssetsAsync<object>(string,uint)
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetAsync<object>(string,uint)
		// YooAsset.AssetHandle YooAsset.ResourcePackage.LoadAssetSync<object>(string)
		// YooAsset.AssetHandle YooAsset.YooAssets.LoadAssetAsync<object>(string,uint)
		// YooAsset.AssetHandle YooAsset.YooAssets.LoadAssetSync<object>(string)
		// string string.Join<object>(string,System.Collections.Generic.IEnumerable<object>)
		// string string.JoinCore<object>(System.Char*,int,System.Collections.Generic.IEnumerable<object>)
	}
}