using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Initialize.Core
{
    [CreateAssetMenu(fileName = "DiContainerFolder", menuName = "ScriptableObject/DiContainerFolder", order = 0)]
    public class DiContainerFolder : ScriptableObject
    {
        [SerializeField] private DiContainerFolder[] _parents;
        private DiContainer _container;

        public DiContainerFolder Init()
        {
            List<DiContainer> parentContainers = new List<DiContainer>();
            foreach (var parent in _parents)
            {
                parentContainers.Add(parent.Get());
            }

            _container = new DiContainer(parentContainers);
            return this;
        }

        public DiContainer Get() => _container;
    }
}