using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private Texture[] _texs;
    [SerializeField] private AudioClip _catchSFX;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider is MeshCollider)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Collider>().enabled = false;
            transform.parent = collision.collider.transform.parent.parent;
            collision.collider.transform.parent.GetComponent<AudioSource>().PlayOneShot(_catchSFX);
        }
    }
}
