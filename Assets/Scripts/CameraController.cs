using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const float RotationSpeed = 0.1f;

    [SerializeField] private AudioSource _bgmAS;

    private bool _leftButtonPressed;

    private void Update()
    {
        if (Pointer.current.press.wasPressedThisFrame)
        {
            if (!_bgmAS.isPlaying)
            {
                _bgmAS.Play();
            }

            _leftButtonPressed = true;
        }
        else if (Pointer.current.press.wasReleasedThisFrame)
        {
            _leftButtonPressed = false;
        }

        if (_leftButtonPressed)
        {
            Vector2 delta = Pointer.current.delta.ReadValue();

            if (Mathf.Abs(delta.x) > 0)
            {
                transform.Rotate(0, RotationSpeed * delta.x, 0);
            }
        }
    }
}
