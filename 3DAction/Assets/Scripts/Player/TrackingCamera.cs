using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingCamera : MonoBehaviour
{
    [SerializeField] Transform tracking;

    // Update is called once per frame
    void Update()
    {
        if(tracking)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, tracking.position.z);
        }
    }
}
