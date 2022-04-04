using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime); //rotates object around its y-axel
    }
}
