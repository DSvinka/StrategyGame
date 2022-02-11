using System;
using System.Reflection;

namespace Utils
{
    public static class AssetsInjector
    {
        private static readonly Type InjectAssetAttributeType = typeof(InjectAssetAttribute);
        
        public static T Inject<T>(this AssetsContext context, T target)
        {
            var targetType = target.GetType();
            var allFields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            for (var i = 0; i < allFields.Length; i++)
            {
                var fieldInfo = allFields[i];
                var injectAssetAttribute = fieldInfo.GetCustomAttribute(InjectAssetAttributeType) as InjectAssetAttribute;
                if (injectAssetAttribute == null)
                    continue;

                var objectToInject = context.GetObjectOfType(fieldInfo.FieldType, injectAssetAttribute.AssetName);
                fieldInfo.SetValue(target, objectToInject);
            }
            return target;
        }
    }
}