using System.Collections;
using UnityEngine;
using Utilities;

namespace SoulEngine
{
	public class HitComponent : MonoBehaviour
	{
		//TODO: Find a way around having to link the logic component as it leads to bugs with dragging things.
		[Tooltip ("The component responsible for being hit."), SerializeField]
		private GameObject _LogicObject = null;
		[Tooltip ("The length of the flashing."), SerializeField]
		private float _Length = 0.0f;
		[Tooltip ("The number of times to flash the color."), SerializeField]
		private int _FlashCount = 2;
		[Tooltip ("The color to overlay when flashing."), SerializeField]
		private Color _FlashColor = Color.red;
		[Tooltip ("The type of tween for the flashing color changes."), SerializeField]
		private LerpCurves.LerpType _Curve = LerpCurves.LerpType.SmootherStep;
		
		private int _FlashIndex = 0;
		private int _ColorIndex = -1;
		private bool _IsFlashing = false;
		private float _FlashLength = 0.0f;
		private Renderer _Renderer = null;
		private Material _Material = null;
		private WaitForEndOfFrame _WaitForFrame = new WaitForEndOfFrame ();

		private void Awake ()
		{
			_FlashLength = _Length / _FlashCount;
			
			_Renderer = GetComponent<Renderer> ();
			_Material = _Renderer.material;
			_ColorIndex = Shader.PropertyToID ("_BaseColor");
		}

		private void OnEnable ()
		{
			LevelSignals.OnEntityHit += OnEntityHit;
		}

		private void OnEntityHit (IDamage damge, GameObject other)
		{
			if (other.GetInstanceID () == _LogicObject.GetInstanceID () && _IsFlashing == false)
			{
				StartCoroutine (Flash (_FlashColor, _FlashLength));
			}
		}

		private void OnDisable ()
		{
			LevelSignals.OnEntityHit -= OnEntityHit;
		}

		private IEnumerator Flash (Color endColor, float length)
		{
			_FlashIndex++;
			_IsFlashing = true;
			
			float time = 0.0f;
			Color startColor = _Material.GetColor (_ColorIndex);

			while (time <= length)
			{
				time += Time.deltaTime;
				float t = LerpCurves.Curve (time / length, _Curve);
				_Material.SetColor (_ColorIndex, Color.Lerp (startColor, endColor, t));

				yield return _WaitForFrame;
			}

			if (_FlashIndex == _FlashCount)
			{
				_FlashIndex = 0;
				_IsFlashing = false;
				StopAllCoroutines ();
				_Material.SetColor (_ColorIndex, endColor);
			}
			else
			{
				StartCoroutine (Flash (startColor, length));
			}
		}
	}
}
