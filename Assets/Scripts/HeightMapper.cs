using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightMapper : MonoBehaviour {

    public Mesh mesh;
    public MeshFilter meshFilter;
    public Texture2D texture;

	private void Start () {
        mesh = meshFilter.mesh;
        
        Vector3[] vertices = mesh.vertices;
        Vector3 min, max;
        min = vertices[0];
        max = vertices[0];
        foreach(var r in vertices)
        {
            if (r.x < min.x && r.z < min.z)
            {
                min = r;
            } else if(r.x > max.x && r.z > max.z)
            {
                max = r;
            }
        }
        
        float width = Mathf.Abs(max.x - min.x);
        float height = Mathf.Abs(max.z - min.z);
        float widthRatio = texture.width / width;
        float heightRatio = texture.height / height;

        for(int i = 0; i < vertices.Length; i++)
        {
            float wX = Mathf.Abs(vertices[i].x - min.x);
            float wZ = Mathf.Abs(vertices[i].z - min.z);
            
            int x = (int)(wX * widthRatio);
            int y = (int)(wZ * heightRatio);
            Color c = texture.GetPixel(x, y);
            vertices[i].y = (c.r + 0.01f) * ( c.g + 0.01f) * (c.b + 0.01f) * 5;
        }

        meshFilter.mesh.vertices = vertices;
        meshFilter.mesh.RecalculateBounds();
        meshFilter.mesh.RecalculateNormals();
    }
}
