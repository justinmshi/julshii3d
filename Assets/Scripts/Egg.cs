using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Egg : MonoBehaviour
{
    [SerializeField] private Texture[] _texs;
    [SerializeField] private AudioClip _catchSFX;
    [SerializeField] private Texture[] _faceTexs;

    private void Awake()
    {
        Texture tex;
        if (Random.Range(0, 4) == 0)
        {
            tex = _faceTexs[Random.Range(0, _faceTexs.Length)];
        } else
        {
            tex = _texs[Random.Range(0, _texs.Length)];
        }

        GetComponent<MeshRenderer>().material.mainTexture = tex;
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
