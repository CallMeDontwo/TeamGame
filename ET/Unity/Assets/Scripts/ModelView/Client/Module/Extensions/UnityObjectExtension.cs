public static class UnityObjectExtension
{
    public static void Destroy(this UnityEngine.Object obj)
    {
        UnityEngine.Object.Destroy(obj);
    }

    public static void Destroy(this UnityEngine.Object obj, float t)
    {
        UnityEngine.Object.Destroy(obj, t);
    }

    public static void DestroyImmediate(this UnityEngine.Object obj)
    {
        UnityEngine.Object.DestroyImmediate(obj);
    }

    public static void DestroyImmediate(this UnityEngine.Object obj, bool allowDestroyingAssets)
    {
        UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets);
    }

    public static void DontDestroyOnLoad(this UnityEngine.Object target)
    {
        UnityEngine.Object.DontDestroyOnLoad(target);
    }

    public static UnityEngine.Object Instantiate(this UnityEngine.Object original)
    {
        return UnityEngine.Object.Instantiate(original);
    }

    public static UnityEngine.Object Instantiate(this UnityEngine.Object original, UnityEngine.Transform parent)
    {
        return UnityEngine.Object.Instantiate(original, parent);
    }

    public static UnityEngine.Object Instantiate(this UnityEngine.Object original, UnityEngine.Transform parent, bool instantiatelnWorldSpace)
    {
        return UnityEngine.Object.Instantiate(original, parent, instantiatelnWorldSpace);
    }

    public static UnityEngine.Object Instantiate(this UnityEngine.Object original, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
    {
        return UnityEngine.Object.Instantiate(original, position, rotation);
    }

    public static UnityEngine.Object Instantiate(this UnityEngine.Object original, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation, UnityEngine.Transform parent)
    {
        return UnityEngine.Object.Instantiate(original, position, rotation, parent);
    }

    public static T Instantiate<T>(this T original) where T : UnityEngine.Object
    {
        return UnityEngine.Object.Instantiate(original);
    }

    public static T EnsureComponent<T>(this UnityEngine.GameObject gameObject) where T : UnityEngine.Component
    {
        return gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>();
    }

    public static T EnsureComponent<T>(this UnityEngine.Component gameComponent) where T : UnityEngine.Component
    {
        return gameComponent.TryGetComponent(out T component) ? component : gameComponent.gameObject.AddComponent<T>();
    }
}