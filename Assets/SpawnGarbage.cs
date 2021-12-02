using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour
{
    public Transform garbageHolder;
    public ObjOnSphere garbagePrefab;
    public float spawnRate = 1;
    [Space]
    public float surfacePosition = 52f;
    public float spawnPosition = 80f;
    public float fallSpeed = 5f;

    float spawnValue = 0;
    Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
    }

    void Update()
    {
        spawnValue += Time.deltaTime * spawnRate;

        while(spawnValue > 1)
        {
            int vertId = Random.Range(0, mesh.vertexCount);
            Vector3 position = mesh.vertices[vertId];
            var newGarbage = Instantiate(garbagePrefab, position, Quaternion.identity, garbageHolder);
            StartCoroutine(Garbage_fall(newGarbage));

            spawnValue -= 1;
        }
    }

    IEnumerator Garbage_fall(ObjOnSphere newGarbage)
    {
        yield return new WaitForEndOfFrame();
        newGarbage.heightPosition = spawnPosition;
        while(newGarbage.heightPosition > surfacePosition)
        {
            yield return new WaitForEndOfFrame();
            newGarbage.heightPosition -= fallSpeed * Time.deltaTime;
        }
        yield return new WaitForEndOfFrame();
        newGarbage.heightPosition = surfacePosition;
    }
}
