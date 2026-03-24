using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Egg : MonoBehaviour
{
    [SerializeField] private Texture[] _texs;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.mainTexture = _texs[Random.Range(0, _texs.Length)];
    }

    private void Update()
    {
        if (transform.position.y < -200)
        {
            Destroy(gameObject);
        }
    }
}
