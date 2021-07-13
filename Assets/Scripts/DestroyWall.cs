using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public float waitUntilDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyWallAfter());
    }

    private IEnumerator DestroyWallAfter()
    {
        yield return new WaitForSeconds(waitUntilDestroy);
        Destroy(this.gameObject);
    }
}
