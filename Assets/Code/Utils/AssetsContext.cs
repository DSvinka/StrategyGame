using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    [CreateAssetMenu(fileName = nameof(AssetsContext), menuName = "Game/"+nameof(AssetsContext))]
    public sealed class AssetsContext : ScriptableObject
    {
        [SerializeField] private Object[] _objects;

        public Object GetObjectOfType(Type targetType, string targetName = null)
        {
            for (var i = 0; i < _objects.Length; i++)
            {
                var obj = _objects[i];
                if (!obj.GetType().IsAssignableFrom(targetType)) 
                    continue;
                
                if (targetName == null || obj.name == targetName)
                    return obj;
            }

            return null;
        }
    }
}