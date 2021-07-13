using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Sphere")
        {
            other.gameObject.transform.position = new Vector3(0, 0, 0);
        }
    }
}
