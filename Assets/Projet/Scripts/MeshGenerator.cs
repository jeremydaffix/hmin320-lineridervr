using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

	void Start ()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(1.0f , 0.0f, 1.0f),
            new Vector3(-1.0f, 0.0f, 1.0f),
            new Vector3(1.0f, 0.0f, -1.0f),
            new Vector3(-1.0f, 0.0f, -1.0f),
        };

        Vector3[] normals = new Vector3[]{
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 )
        };

        Vector2[] uvs = new Vector2[]{
            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 )
        };

        int[] triangleIndices = new int[]{
            0, 2, 3,
            3, 1, 0
        };

        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf.mesh == null)
            mf.mesh = new Mesh();
        Mesh mesh = mf.mesh;

        mesh.Clear();
        mesh.name = "MyMesh";
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.triangles = triangleIndices;
    }

    void Update ()
    {
		
	}
}
