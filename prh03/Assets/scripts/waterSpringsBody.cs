using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpringsBody : MonoBehaviour
{
    private GameObject[] springs;
    private float[] leftDeltas;
    private float[] rightDeltas;

    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> triag;
    private List<Vector2> uv;
    private int count;

    public GameObject sprint;
    public GameObject WaterRenderer;
    public float k;
    public float d;
    public float Spread;
    public int SpringCount;
    public int width;
    public float height = 3;
    void Start()
    {
        mesh = new Mesh();
        vertices = new List<Vector3>();
        triag = new List<int>();
        uv = new List<Vector2>();

        GameObject p = Instantiate(sprint, new Vector3(transform.position.x - width, transform.position.y, 0), Quaternion.identity);
        p.transform.SetParent(transform);

        for (int i = 0; i < SpringCount; i++)
        {
            GameObject s = Instantiate(sprint, new Vector3(transform.GetChild(i).transform.position.x + 0.1f, transform.position.y, 0), Quaternion.identity);
            s.transform.SetParent(transform);
        }
        
        springs = new GameObject[transform.childCount];
        leftDeltas = new float[transform.childCount];
        rightDeltas = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            springs[i] = transform.GetChild(i).gameObject;
            WaterPhys wat = transform.GetChild(i).gameObject.GetComponent<WaterPhys>();
            wat.Initialized();
            wat.k = k;
            wat.d = d;
        }

        for (int i = 1; i < springs.Length; i++)
        {
            count += 2;
        }

        for (int i = 0; i < springs.Length; i++)
        {
            leftDeltas[i] = 0;
            rightDeltas[i] = 0;
        }

        StartCoroutine(RandomSplash());
    }



    void Update()
    {
        CalculatePhysics();
        RenderMesh();
    }

    private void RenderMesh()
    {
        mesh.Clear();
        vertices.Clear();
        triag.Clear();
        uv.Clear();

        for (int i = 0; i < springs.Length; i++)
        {
            vertices.Add(new Vector3(springs[i].transform.position.x, springs[i].transform.position.y, 0));
            vertices.Add(new Vector3(springs[i].transform.position.x, height, 0));
        }

        triag.Add(2);
        triag.Add(3);
        triag.Add(1);

        for (int i = 1; i < count; i++)
        {

            if (i % 2 != 0)
            {
                triag.Add(triag[triag.Count - 3] - 1);
            }
            else
            {
                triag.Add(triag[triag.Count - 3] + 3);
            }

            if (i % 2 != 0)
            {
                triag.Add(triag[triag.Count - 3] - 3);
            }
            else
            {
                triag.Add(triag[triag.Count - 3] + 5);
            }
            triag.Add(triag[triag.Count - 3] + 1);
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triag.ToArray();

        //mesh.uv = uv.ToArray();

        WaterRenderer.GetComponent<MeshFilter>().mesh = mesh;
    }

    private void CalculatePhysics()
    {
        for (int j = 0; j < 8; j++)
        {
            for (int i = 0; i < springs.Length; i++)
            {
                if (i > 0)
                {
                    leftDeltas[i] = Spread * (springs[i].gameObject.transform.position.y - springs[i - 1].gameObject.transform.position.y);
                    springs[i - 1].gameObject.GetComponent<WaterPhys>().velocity += leftDeltas[i];
                }
                if (i < springs.Length - 1)
                {
                    rightDeltas[i] = Spread * (springs[i].gameObject.transform.position.y - springs[i + 1].gameObject.transform.position.y);
                    springs[i + 1].gameObject.GetComponent<WaterPhys>().velocity += rightDeltas[i];
                }
            }

            for (int i = 0; i < springs.Length; i++)
            {
                if (i > 0)
                {
                    springs[i - 1].gameObject.transform.position = new Vector3(springs[i - 1].gameObject.transform.position.x, springs[i - 1].gameObject.transform.position.y + leftDeltas[i], 0);
                }
                if (i < springs.Length - 1)
                {
                    springs[i + 1].gameObject.transform.position = new Vector3(springs[i + 1].gameObject.transform.position.x, springs[i + 1].gameObject.transform.position.y + rightDeltas[i], 0);
                }
            }
        }
    }

    IEnumerator RandomSplash()
    {
        yield return new WaitForSeconds(0.3f);
        WaterPhys wat = transform.GetChild(Random.Range(0, transform.childCount)).gameObject.GetComponent<WaterPhys>();
        wat.Splash(Random.Range(-0.04f, 0.04f));
        StartCoroutine(RandomSplash());
    }
}
