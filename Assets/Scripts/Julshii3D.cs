using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(AudioSource))]
public class Julshii3D : MonoBehaviour
{
    private const float RotationSpeed = 60;
    private const float EggLinearVelocityScale = 3;
    private const float EggAngularVelocityComponentLimit = 2 * Mathf.PI / 2;

    [SerializeField] private GameObject _eggPrefab;
    [SerializeField] private AudioClip _eggSFX;

    private AudioSource _audioSrc;

    public void SpawnEgg()
    {
        GameObject egg = Instantiate(_eggPrefab);
        Rigidbody eggRB = egg.GetComponent<Rigidbody>();

        Vector3 eggLV = Vector3.ProjectOnPlane(Camera.main.transform.position - transform.position, Vector3.up);
        eggLV.Normalize();
        eggLV += Random.Range(-1f, 1f) * Camera.main.transform.right;
        eggLV += Random.Range(0f, 1f) * Vector3.up;
        eggLV.Normalize();
        eggLV *= EggLinearVelocityScale;

        Vector3 eggAV = new Vector3(
            Random.Range(-EggAngularVelocityComponentLimit, EggAngularVelocityComponentLimit),
            Random.Range(-EggAngularVelocityComponentLimit, EggAngularVelocityComponentLimit),
            Random.Range(-EggAngularVelocityComponentLimit, EggAngularVelocityComponentLimit)
        );

        eggRB.position = transform.position;
        eggRB.linearVelocity = eggLV;
        eggRB.angularVelocity = eggAV;

        _audioSrc.PlayOneShot(_eggSFX);
    }

    private void Awake()
    {
        _audioSrc = GetComponent<AudioSource>();

        Mesh mesh = CreateMesh();
        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    private void Update()
    {
        transform.Rotate(0, RotationSpeed * Time.deltaTime, 0);
    }

    private Mesh CreateMesh()
    {
        Mesh mesh = new();

        Vector3[] vertices = new Vector3[5];

        vertices[0] = new(0, 0.5f, 0);
        vertices[1] = new(-0.5f, -0.5f, 0.5f);
        vertices[2] = new(0.5f, -0.5f, 0.5f);
        vertices[3] = new(-0.5f, -0.5f, -0.5f);
        vertices[4] = new(0.5f, -0.5f, -0.5f);

        mesh.vertices = vertices;

        int[] triangles = new int[6 * 3];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[3 + 1] = 3;
        triangles[3 + 2] = 1;

        triangles[3 * 2] = 0;
        triangles[3 * 2 + 1] = 2;
        triangles[3 * 2 + 2] = 4;

        triangles[3 * 3] = 0;
        triangles[3 * 3 + 1] = 4;
        triangles[3 * 3 + 2] = 3;

        triangles[3 * 4] = 1;
        triangles[3 * 4 + 1] = 4;
        triangles[3 * 4 + 2] = 2;

        triangles[3 * 5] = 1;
        triangles[3 * 5 + 1] = 3;
        triangles[3 * 5 + 2] = 4;

        mesh.triangles = triangles;

        Vector2[] uv = new Vector2[5];

        uv[0] = new(0.6f, 0.92f);
        uv[1] = new(0.8f, 0.11f);
        uv[2] = new(0.3f, 0.11f);
        uv[3] = new(0.3f, 0.11f);
        uv[4] = new(0.8f, 0.11f);

        mesh.uv = uv;

        return mesh;
    }
}
