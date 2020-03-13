using UnityEngine;

namespace SoulEngine
{
	public class InputFocusComponent : MonoBehaviour
	{
		[Tooltip("Should the component use the keyboard for input?"), SerializeField]
		private bool _UseKeyboard = true;
		[Tooltip ("Should the focus slow everything?"), SerializeField]
		private bool _SlowGlobally = true;
		[Tooltip ("The key to use for a keyboard/mouse setup."), SerializeField]
		private KeyCode _KeyboardSetup = KeyCode.LeftShift;
		[Tooltip ("The key to use for a controller/joystick."), SerializeField]
		private KeyCode _ControllerSetup = KeyCode.Joystick1Button4;
		
		private InputMoveComponent _InputMoveComponent = null;

		private void Start ()
		{
			_InputMoveComponent = GetComponent<InputMoveComponent> ();
		}

		private void Update ()
		{
			Focus (_UseKeyboard ? _KeyboardSetup : _ControllerSetup);
		}

		private void Focus (KeyCode key)
		{
			if (_SlowGlobally)
			{
				if (Input.GetKey (key))
				{
					Time.timeScale = Globals.SlowedTimeScale;
				}
				else if (Input.GetKeyUp (key))
				{
					Time.timeScale = Globals.StandardTimeScale;
				}
			}
			else
			{
				if (Input.GetKeyDown (key))
				{
					_InputMoveComponent.Speed *= Globals.SlowedTimeScale;
				}
				else if (Input.GetKeyUp (key))
				{
					_InputMoveComponent.Speed /= Globals.SlowedTimeScale;
				}
			}
		}
	}
}
