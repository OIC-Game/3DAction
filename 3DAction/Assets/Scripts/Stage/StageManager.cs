using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Action3D.Stage
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private List<LaneObject> prefabLaneObjects;

        [SerializeField] private List<LaneObject> currentLaneObjects;

        private Transform player;

        private float generationDistance = 80;

        private float currentScroll = 0;

        private int usedPrefabIndex = 0;

        void Start()
        {
            player = FindObjectOfType<Player.PlayerController>().transform;
        }

        void Update()
        {
            var lastLane = currentLaneObjects.Last();
            while (lastLane.transform.position.z - player.position.z < 80)
            {
                lastLane = GenerateLane(lastLane);
                currentLaneObjects.Add(lastLane);
            }

            var headLane = currentLaneObjects[0];
            while (headLane.transform.position.z - player.position.z < -40)
            {
                DestroyLane();
                headLane = currentLaneObjects[0];
            }

        }

        public void StageScroll(float value)
        {
            currentScroll += value;
            foreach (var lane in currentLaneObjects)
            {
                lane.transform.position = new Vector3(lane.transform.position.x, lane.transform.position.y, lane.transform.position.z - value);
            }
        }

        LaneObject GenerateLane(LaneObject lastLane)
        {
            usedPrefabIndex += 1;
            if (usedPrefabIndex > prefabLaneObjects.Count - 1)
            {
                usedPrefabIndex = 0;
            }
            return Instantiate(prefabLaneObjects[usedPrefabIndex], lastLane.NextPosition(), Quaternion.identity);
        }

        void DestroyLane()
        {
            Destroy(currentLaneObjects[0].gameObject);
            currentLaneObjects.RemoveAt(0);
        }
    }
}