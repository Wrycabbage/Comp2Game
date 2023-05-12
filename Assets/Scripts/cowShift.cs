using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class cowShift : MonoBehaviour
{

    float idleSpeed = 2;

    // Update is called once per frame
    void Idle()
    {

		transform.position += new Vector3(0, idleSpeed, 0) * Time.deltaTime;
		if (transform.position.y <= -3.47)
        {
			idleSpeed = 2;
		}
        else if(transform.position.y >= 3.79)
        {
			idleSpeed = -2;
		}
    }

    private void Update()
    {
        Idle();
    }
}
