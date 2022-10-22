using System.Reflection;
using Zenject;

namespace Initialize.Core
{
    public static class DiExtentions
    {
        public static void BindFields<T>(this T obj, DiContainer container)
        {
            foreach (var field in obj.GetType().GetFields(BindingFlags.Instance|BindingFlags.Public))
            {
                // Debug.Log($"[BIND] Bind {field.Name} : {field.FieldType} from {obj.GetType().Name}");
                container.BindInterfacesAndSelfTo(field.FieldType).FromInstance(field.GetValue(obj)).AsSingle();
            }
        }

        public static void BindProperties<T>(this T obj, DiContainer container)
        {
            foreach (var prop in obj.GetType().GetProperties(BindingFlags.Instance|BindingFlags.Public))
            {
                // Debug.Log($"[BIND] Bind {prop.Name} : {prop.PropertyType} from {obj.GetType().Name}");
                container.BindInterfacesAndSelfTo(prop.PropertyType).FromInstance(prop.GetValue(obj)).AsSingle();
            }
        }
    }
}