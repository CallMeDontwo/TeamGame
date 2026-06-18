namespace ET
{
    public static class EntityExtension
    {
        public static Entity AddComponentSafe<TEntity>(this TEntity entity, Entity component) where TEntity : Entity
        {
            if (component != null)
            {
                entity.AddComponent(component);
            }
            return component;
        }

        public static bool TryGetComponent<TEntity, TComponent>(this TEntity entity, out TComponent component) where TEntity : Entity where TComponent : Entity
        {
            component = entity.GetComponent<TComponent>();
            return component != null;
        }
    }
}