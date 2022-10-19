using System;

namespace UI.Core
{
    public interface IViewModel : IDisposable
    {
        public void Close();
    }

    public class AbstractViewModel : IViewModel
    {
        public virtual void Dispose()
        {
        }

        public void Close()
        {
            Dispose();
        }
    }
}