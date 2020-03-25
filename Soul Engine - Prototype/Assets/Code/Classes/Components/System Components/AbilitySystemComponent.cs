using UnityEngine;

namespace SoulEngine
{
	public class AbilitySystemComponent : MonoBehaviour
	{
		[Header ("Ability Settings")]
		[Tooltip ("The major ability which the player can use."), SerializeField]
		private AbilityComponent _MajorAbility = null;
		[Tooltip ("The minor ability which the player can use."), SerializeField]
		private AbilityComponent _MinorAbility = null;
		[Header ("Input Settings")]
		[Tooltip ("The keyboard button to use for major abilities"), SerializeField]
		private KeyCode _KeyboardMajorButton = KeyCode.A;
		[Tooltip ("The keyboard button to use for minor abilities"), SerializeField]
		private KeyCode _KeyboardMinorButton = KeyCode.A;
		[Tooltip ("The joystick button to use for major abilities"), SerializeField]
		private KeyCode _JoystickMajorButton = KeyCode.A;
		[Tooltip ("The joystick button to use for minor abilities"), SerializeField]
		private KeyCode _JoystickMinorButton = KeyCode.A;

		private void Update ()
		{
			if (Input.GetKeyDown (_KeyboardMajorButton) | Input.GetKeyDown (_JoystickMajorButton))
			{
				_MajorAbility.Use ();
			}

			if (Input.GetKeyDown (_KeyboardMinorButton) | Input.GetKeyDown (_JoystickMinorButton))
			{
				_MinorAbility.Use ();
			}
		}
	}
}
