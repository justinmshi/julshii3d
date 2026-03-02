using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const float RotationSpeed = 0.1f;

    [SerializeField] private AudioSource _bgmAS;

    private bool _leftButtonPressed;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (!_bgmAS.enabled)
            {
                _bgmAS.enabled = true;
            }

            _leftButtonPressed = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            _leftButtonPressed = false;
        }

        if (_leftButtonPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();

            if (Mathf.Abs(mouseDelta.x) > 0)
            {
                transform.Rotate(0, RotationSpeed * mouseDelta.x, 0);
            }
        }
    }
}
