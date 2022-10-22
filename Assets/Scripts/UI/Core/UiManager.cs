using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Core
{
    public class UiManager : MonoBehaviour, IUiManager
    {
        [SerializeField] private Canvas _canvas;
        private Dictionary<Type, IView> _views = new Dictionary<Type, IView>();
        private Dictionary<Type, IView> _showingScreens = new Dictionary<Type, IView>();
        private Dictionary<Type, IView> _staticScreens = new Dictionary<Type, IView>();

        public void Init(IEnumerable<IView> views)
        {
            _views = views.ToDictionary(k => k.Target);
        }
        
        public void ShowStaticScreen<T>(T type) where T : IViewModel
        {
            var targetType = typeof(T);
            if (_showingScreens.TryGetValue(targetType, out var showingView))
            {
                showingView.Hide();
                showingView.Dispose();
                showingView.Bind(type);
                showingView.Show();
                return;
            }

            if (_views.TryGetValue(targetType, out IView view))
            {
                view.Bind(type);
                view.Show();
                return;
            }

            throw new Exception($"Cant show screen {type.GetType()}");
        }

        public void BindAndShow<T>(T type) where T : IViewModel
        {
            var targetType = typeof(T);
            if (_showingScreens.TryGetValue(targetType, out var showingView))
            {
                showingView.Hide();
                showingView.Bind(type);
                showingView.Show();
                return;
            }
            
            if (_views.TryGetValue(targetType, out IView view))
            {
                view.Bind(type);
                view.Show();
                _showingScreens.Add(targetType, view);
                return;
            }
        }

        public void Hide<T>(T type) where T : IViewModel
        {
            var targetType = typeof(T);
            
            if (_showingScreens.TryGetValue(targetType, out var showingView))
            {
                showingView.Hide();
                return;
            }
            
            throw new Exception($"Cant hide screen {type.GetType()}");
        }

        public bool TryGet<T>(out IView screen)
        {
            var type = typeof(T);
            if (_views.TryGetValue(type, out screen))
            {
                return true;
            }

            return false;
        }
    }
}