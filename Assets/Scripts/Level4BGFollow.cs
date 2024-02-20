using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4BGFollow : MonoBehaviour
{
    [SerializeField] private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        if(Camera.main == null) return;
        transform.position = new Vector3(Camera.main.transform.position.x + offset.x, Camera.main.transform.position.y + offset.y, transform.position.z);
    }
}
