using System;
using System.Diagnostics;
using UnityEngine;

namespace UI.Core
{
    public interface IView : IDisposable
    {
        public Type Target { get; }
        public void Bind(object model);
        public void Show(bool isOverAll = false);
        void Hide();
    }

    public abstract class AbstractViewBase : MonoBehaviour, IView
    {
        public abstract void Dispose();

        public abstract Type Target { get; }
        public abstract void Bind(object model);

        public abstract void Show(bool isOverAll = false);

        public abstract void Hide();
    }

    public abstract class AbstractView<TSource> : AbstractViewBase, IView where TSource : IViewModel 
    {
        public override Type Target => typeof(TSource);
        protected TSource _model;
        
        public override void Bind(object model)
        {
            var screenModel = (TSource) model;
        }

        public override void Show(bool isOverAll)
        {
            if (isOverAll)
            {
                transform.SetAsLastSibling();
            }

            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
            _model?.Close();
            Dispose();
        }

        protected abstract void Bind(TSource model);

        public override void Dispose()
        {
            _model?.Dispose();
        }
    }
}