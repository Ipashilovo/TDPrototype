using Cysharp.Threading.Tasks;
using Zenject;

namespace Initialize.Core
{
    public static class AdressableUtils
    {
        public static async UniTask<IAddressableHandler<T>> GetAsset<T>(string path)
        {
            var result = new AddressableHandler<T>(path);
            await result.Load();
            return result;
        }
    }
}