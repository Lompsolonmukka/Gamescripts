using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    //minimap rotates with player
    public Transform player;

    void LateUpdate()
    {
        Vector3 NewPosition = player.position;
        NewPosition.y = transform.position.y;
        transform.position = NewPosition;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


}
