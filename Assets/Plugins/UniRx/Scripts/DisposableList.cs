using System;
using System.Collections.Generic;

namespace UniRx
{
    public class DisposableList : IDisposable
    {
        private List<IDisposable> _disposables = new List<IDisposable>();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
            _disposables.Clear();
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }

    public static class DisposableListUtils
    {
        public static void AddTo(this IDisposable disposable, DisposableList disposableList)
        {
            disposableList.Add(disposableList);
        }
    }
}