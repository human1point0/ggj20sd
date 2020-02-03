using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BunnyBlinker : MonoBehaviour
{
    private Material eyeMaterial;

    public Vector2[] offsets;

    private void Awake()
    {
        var mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        eyeMaterial = mr.materials[1];
    }

    IEnumerator Start()
    {
        yield return StartCoroutine(Next());
    }

    IEnumerator Next()
    {
        while (enabled)
        {
            eyeMaterial.mainTextureOffset =  offsets[0];
            var wait = Random.Range(0.5f, 1.52f);
            yield return new WaitForSeconds(wait);
            eyeMaterial.mainTextureOffset =  offsets[1];
            wait = Random.Range(0.1f, 0.3f);
            yield return new WaitForSeconds(wait);
            eyeMaterial.mainTextureOffset =  offsets[2];
            wait = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds(wait);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Update is called once per frame
}
