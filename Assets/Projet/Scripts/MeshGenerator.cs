using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

	void Start ()
    {
        float edgeHeight = 0.15f;
        float edgeWidth = 0.15f;
        float roadWidth = 1.0f;

        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0.0f , 0.0f, 0.0f),
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, edgeHeight, 0.0f),
            new Vector3(1.0f, edgeHeight, 0.0f),

            new Vector3(0.0f, edgeHeight, 0.0f),
            new Vector3(1.0f, edgeHeight, 0.0f),
            new Vector3(0.0f, edgeHeight, edgeWidth),
            new Vector3(1.0f, edgeHeight, edgeWidth),

            new Vector3(0.0f , 0.0f, edgeWidth),
            new Vector3(0.0f, edgeHeight, edgeWidth),
            new Vector3(1.0f, 0.0f, edgeWidth),
            new Vector3(1.0f, edgeHeight, edgeWidth),

            new Vector3(0.0f , 0.0f, edgeWidth),
            new Vector3(1.0f, 0.0f, edgeWidth),
            new Vector3(0.0f, 0.0f, edgeWidth + roadWidth),
            new Vector3(1.0f, 0.0f, edgeWidth + roadWidth),

            new Vector3(0.0f , 0.0f, 0.0f + roadWidth + edgeWidth),
            new Vector3(1.0f, 0.0f, 0.0f + roadWidth + edgeWidth),
            new Vector3(0.0f, edgeHeight, 0.0f + roadWidth + edgeWidth),
            new Vector3(1.0f, edgeHeight, 0.0f + roadWidth + edgeWidth),

            new Vector3(0.0f, edgeHeight, 0.0f + roadWidth + edgeWidth),
            new Vector3(1.0f, edgeHeight, 0.0f + roadWidth + edgeWidth),
            new Vector3(0.0f, edgeHeight, edgeWidth + roadWidth + edgeWidth),
            new Vector3(1.0f, edgeHeight, edgeWidth + roadWidth + edgeWidth),

            new Vector3(0.0f , 0.0f, edgeWidth + roadWidth + edgeWidth),
            new Vector3(0.0f, edgeHeight, edgeWidth + roadWidth + edgeWidth),
            new Vector3(1.0f, 0.0f, edgeWidth + roadWidth + edgeWidth),
            new Vector3(1.0f, edgeHeight, edgeWidth + roadWidth + edgeWidth),
        };

        Vector3[] normals = new Vector3[]{
            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),

            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),

            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 ),

            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),

            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),
            new Vector3( 0, 0, 1 ),

            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),
            new Vector3( 0, 1, 0 ),

            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 ),
            new Vector3( 0, 0, -1 )

        };

        Vector2[] uvs = new Vector2[]{
            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),

            new Vector2( 0, 1 ),
            new Vector2( 0, 0 ),
            new Vector2( 1, 1 ),
            new Vector2( 1, 0 ),
        };

        int[] triangleIndices = new int[]{
            0, 2, 3,
            3, 1, 0,
            4, 6, 7,
            7, 5, 4,
            8, 10, 11,
            11, 9, 8,
            12, 14, 15,
            15, 13, 12,
            16, 18, 19,
            19, 17, 16,
            20, 22, 23,
            23, 21, 20,
            24, 26, 27,
            27, 25, 24,

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
