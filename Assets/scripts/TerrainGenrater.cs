using Unity.VisualScripting;
using UnityEngine;

public class TerrainGenrater : MonoBehaviour
{
    
    public int width;
    public int height;
    public float scale;
    public float heightMultiplier;
    Vector3[] vertices;
    int[] triangles;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vertices = CreateVertices(width, height);
        triangles = CreateTriangles(width, height);
        CreateGroundMesh(vertices, triangles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateGroundMesh(Vector3[] vertices, int[] triangles){
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals(); // for lighting
        mesh.RecalculateBounds(); // to make sure bounds are accurate
    }
   Vector3[] CreateVertices(int width, int height) {
        vertices = new Vector3[(width + 1) * (height + 1)];

        for(int index = 0, z = 0; z <= height; z++) {
            for(int x = 0; x <= width; x++) {
                float y = HeightMapGenrater.GenrateHeightMap(x * scale, z * scale, 6, 0.5f, 2.0f);
                
                vertices[index] = new Vector3(x , y, z);
                index++;
            }
        }
        return vertices;
    }

    int[] CreateTriangles(int width, int height) {
        triangles = new int[width * height * 6];

        int vertex = 0;
        int trianglesCounter = 0;

        for(int z = 0; z < height; z++) {
            for(int x = 0; x < width; x++) {
                triangles[trianglesCounter + 0] = vertex + 0;
                triangles[trianglesCounter + 1] = vertex + width + 1;
                triangles[trianglesCounter + 2] = vertex + 1;
                triangles[trianglesCounter + 3] = vertex + 1;
                triangles[trianglesCounter + 4] = vertex + width + 1;
                triangles[trianglesCounter + 5] = vertex + width + 2;

                vertex++;
                trianglesCounter += 6;
            }
            vertex++;
        }
        return triangles;
    }

    void OnDrawGizmosSelected(){
        if (vertices == null) {
            return;
        }
        Gizmos.color = Color.green;
        for (int i = 0; i < vertices.Length; i++) {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }

}
