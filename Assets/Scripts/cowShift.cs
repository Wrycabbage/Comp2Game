using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cowShift : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0, 1f, 0);
        Debug.Log("more");
        if (transform.position.y <= -3.47)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        }
        else if(transform.position.y >= 3.79)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        }
    }
}
