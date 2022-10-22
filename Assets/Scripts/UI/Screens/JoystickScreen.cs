using UI.Core;
using UnityEngine;

namespace UI.Screens
{
    public class JoystickScreen : AbstractView<JoystickScreenModel>
    {
        [SerializeField] private Joystick _joystick;
        public Joystick Joystick => _joystick;


        protected override void Bind(JoystickScreenModel model)
        {
            
        }
    }

    public class JoystickScreenModel : AbstractViewModel
    {
    }
}