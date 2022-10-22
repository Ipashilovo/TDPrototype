using Core.GameplaySystems.Unit.Common;
using UnityEngine;

namespace Core.GameplaySystems.Unit.Player
{
    public interface IPlayerModel : IUnitModel
    {
        public CharacterController CharacterController { get; }
    }
}