using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Initialize.Core
{
    public interface IAddressableHandler<T> : IDisposable
    {
        public T Result { get; }

        public UniTask Load();
    }

    public class AddressableHandler<T> : IAddressableHandler<T>
    {
        private string _path;
        public T Result { get; private set; }

        public AddressableHandler(string path)
        {
            _path = path;
        }

        public async UniTask Load()
        {
            var request = Addressables.LoadAssetAsync<T>(_path);
            await UniTask.WaitUntil(() => request.IsDone);
            Result = request.Result;
        }

        public void Dispose()
        {
            Addressables.Release(Result);
        }
    }
}