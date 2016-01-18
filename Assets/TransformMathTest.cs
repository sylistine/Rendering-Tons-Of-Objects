using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class TransformMathTest : MonoBehaviour
{
    Mesh combinedMesh;
    Vector3[] v, n, translation, transformedV, translationDirection;
    int[] tris, subMeshId;
    Matrix4x4[] trsMatrix;

    public int numQuads;
    public float explodeSpeed;

    void Start ()
    {
        MeshFilter mf = this.GetComponent<MeshFilter>();
        int numVerts = CubePrototype.verts.Length * numQuads;

        combinedMesh = new Mesh();

        v = new Vector3[numVerts];
        n = new Vector3[numVerts];
        translation = new Vector3[numVerts];
        transformedV = new Vector3[numVerts];
		translationDirection = new Vector3[numQuads];
		tris = new int[CubePrototype.tris.Length * numQuads];
        subMeshId = new int[numVerts];
		trsMatrix = new Matrix4x4[numVerts];


		// trsMatrix[0, 0] = Mathf.Cos(Mathf.Deg2Rad * -1);
        // trsMatrix[0, 1] = -Mathf.Sin(Mathf.Deg2Rad * -1);
        // trsMatrix[1, 0] = Mathf.Sin(Mathf.Deg2Rad * -1);
        // trsMatrix[1, 1] = Mathf.Cos(Mathf.Deg2Rad * -1);

        // Define verts, norms, and submesh ids
        for (int i = 0, j = 0, id = 0; i < v.Length; i++)
        {
            v[i] = CubePrototype.verts[j];
            n[i] = new Vector3(0, 0, -1);
            translation[i] = Vector3.zero;
            subMeshId[i] = id;
            transformedV[i] = v[i] + translation[i];

            if (j < CubePrototype.verts.Length - 1)
            {
                j = j + 1;
            }
            else
            {
                j = 0;
				Vector3 t = new Vector3(Random.Range(-1f, 1f), Random.Range(0, 1f), Random.Range(-1f, 1f)).normalized / 8;
				trsMatrix[id] = Matrix4x4.TRS(t, Quaternion.identity, Vector3.one);
                id++;
            }
        }

        // Define tris.
        for (int i = 0; i < numQuads; i++)
        {
            for (int j = 0; j < CubePrototype.tris.Length; j++)
            {
                tris[i * CubePrototype.tris.Length + j] = i * CubePrototype.verts.Length + CubePrototype.tris[j];
            }
        }
        
        combinedMesh.vertices = transformedV;
        combinedMesh.normals = n;
        combinedMesh.triangles = tris;

        mf.mesh = combinedMesh;
    }
    
    void Update ()
    {
        for (int i = 0; i < v.Length; i++)
        {
            // Apply translation, rotation, and scale directly to mesh
            Vector3 pivot = new Vector3(0.5f, 0.5f);
			v[i] = trsMatrix[subMeshId[i]].MultiplyPoint3x4(v[i] - pivot) + pivot;

            transformedV[i] = v[i] + translation[i];
        }
        combinedMesh.vertices = transformedV;
	}

    private class QuadPrototype
    {
        public static Vector3[] verts = {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0)
        };

        public static int[] tris = {0, 1, 2, 2, 3, 0};
    }
    
    private class CubePrototype
    {
        public static Vector3[] verts = {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1)
        };

        public static int[] tris = {
            0, 1, 2, 2, 3, 0,
            0, 4, 5, 5, 1, 0,
            4, 0, 7, 7, 0, 3,
            2, 6, 3, 3, 6, 7,
            6, 5, 7, 7, 5, 4,
            1, 5, 6, 6, 2, 1
        };
    }
}
