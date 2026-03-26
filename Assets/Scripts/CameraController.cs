using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private const float RotationSpeed = 0.1f;
    private const float ZoomSpeed = 0.0025f;
    private const int ClickableLayerMask = 1 << 3;

    [SerializeField] private AudioSource _bgmAS;
    [SerializeField] private Transform _camTransform;
    [SerializeField] private Transform _nearTransform;
    [SerializeField] private Transform _farTransform;

    private bool _leftButtonPressed;
    private float _zoom;

    private void Update()
    {
        if (Pointer.current.press.wasPressedThisFrame)
        {
            if (!_bgmAS.isPlaying)
            {
                _bgmAS.Play();
            }

            _leftButtonPressed = true;

            if (
                Physics.Raycast(
                    Camera.main.ScreenPointToRay(Pointer.current.position.ReadValue()),
                    out RaycastHit hitInfo,
                    Mathf.Infinity,
                    ClickableLayerMask
                )
            )
            {
                Julshii3D julshii3D = hitInfo.collider.GetComponent<Julshii3D>();
                if (julshii3D != null)
                {
                    julshii3D.SpawnEgg();
                }
            }
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

            if (Mathf.Abs(delta.y) > 0)
            {
                _zoom = Mathf.Clamp(_zoom + ZoomSpeed * delta.y, 0, 1);
                _camTransform.SetLocalPositionAndRotation(
                    Vector3.Lerp(_nearTransform.localPosition, _farTransform.localPosition, _zoom),
                    Quaternion.Slerp(_nearTransform.localRotation, _farTransform.localRotation, _zoom)
                );
            }
        }
    }
}
