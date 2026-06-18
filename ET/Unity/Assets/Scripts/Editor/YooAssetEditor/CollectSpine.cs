using Spine.Unity;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

public class CollectSpine : IFilterRule
{
    public bool IsCollectAsset(FilterRuleData data)
    {
        return AssetDatabase.LoadAssetAtPath<Object>(data.AssetPath) is SkeletonDataAsset;
    }
}