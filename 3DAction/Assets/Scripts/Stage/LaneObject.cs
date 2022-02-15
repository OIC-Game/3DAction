using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Action3D.Stage
{
    public class LaneObject : MonoBehaviour
    {
        [SerializeField] private float laneLength;
        [SerializeField] private float laneWidth;

        public Vector3 NextPosition()
        {
            return transform.position + new Vector3(0, 0, laneLength);
        }
    }
}