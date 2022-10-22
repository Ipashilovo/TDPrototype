using System;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Common
{
    public interface IUnitModel
    {
        public IAnimatorProvider AnimatorProvider { get; } 
        public Vector3 Position { get; }
        public event Action<Amount> TakedDamage;
        public void TakeDamage(Amount damage);
    }

    public interface IAnimatorProvider
    {
        public void Play();
    }
}