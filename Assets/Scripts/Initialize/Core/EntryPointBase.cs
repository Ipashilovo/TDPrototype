using System;
using UnityEngine;
using Zenject;

namespace Initialize.Core
{
    public abstract class EntryPointBase : MonoBehaviour, IDisposable
    {
        [SerializeField] private DiContainerFolder _diContainerFolder;
        protected DiContainer _container;
        protected Initializer _initializer;

        private void OnDestroy()
        {
            Dispose();
        }

#if UNITY_EDITOR
        protected virtual void Start()
        {
            Init();
        } 
#else
        private void Start()
        {
            Init();
        }
#endif
        

        private void Init()
        {
            enabled = false;
            _initializer = new Initializer();
            _container = new DiContainer();
            _container = _diContainerFolder.Init().Get();
            Bind();
        }

        protected abstract void Bind();

        public virtual void Dispose()
        {
            var disposables = _container?.ResolveAll<IDisposable>(InjectSources.Local);
            if (disposables != null)
            {
                foreach (var disposable in disposables)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}