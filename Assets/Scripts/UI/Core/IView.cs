using System;
using System.Diagnostics;
using UnityEngine;

namespace UI.Core
{
    public interface IView : IDisposable
    {
        public Type Target { get; }
        public void Bind<TModel>(TModel model) where TModel : IViewModel;
        public void Show(bool isOverAll = false);
        void Hide();
    }

    public abstract class AbstractView<TSource> : MonoBehaviour, IView where TSource : IViewModel 
    {
        public Type Target => typeof(TSource);
        protected TSource _model;
        
        public void Bind<TModel>(TModel model) where TModel : TSource
        {
            _model = model;
        }

        public void Show(bool isOverAll)
        {
            if (isOverAll)
            {
                transform.SetAsLastSibling();
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _model?.Close();
            Dispose();
        }

        public virtual void Dispose()
        {
            _model?.Dispose();
        }
    }
}