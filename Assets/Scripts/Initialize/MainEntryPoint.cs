using System;
using Initialize.Core;
using Initialize.Tasks.Core;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

namespace Initialize
{
    public class MainEntryPoint : EntryPointBase
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected override void Bind()
        {
            _initializer.RegisterTask(new LoadBalanceTask(_container));
            _initializer.RegisterTask(new LoadStateTask(_container));
            _initializer.RegisterTask(new StatsProviderInitTask(_container));

            _initializer.OnAllCompleted += OnComplete;
            _initializer.Run();
        }

        private void OnComplete()
        {
            _initializer.OnAllCompleted -= OnComplete;
            
#if UNITY_EDITOR
            IsLoaded = true;
#endif
            
            SceneManager.LoadScene("GameplayScene");
        }

#if UNITY_EDITOR
        public static bool IsLoaded { get; set; }
        
        public static void Load()
        {
            SceneManager.LoadScene("InitScene");
        }
#endif
    }
}