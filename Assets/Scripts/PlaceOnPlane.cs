using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnPlane : MonoBehaviour
{
    public GameObject mapPrefab;
    public GameObject initCanvas;

    bool openedMap = false;

    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    ARRaycastManager m_RaycastManager;

    // Start is called before the first frame update
    void Start()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
        initCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        if (m_RaycastManager.Raycast(touch.position, m_Hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitpose = m_Hits[0].pose;
            if (!openedMap)
            {
                initCanvas.SetActive(false);
                Instantiate(mapPrefab, hitpose.position, hitpose.rotation);
                openedMap = true;
            }
            else
            {
                /*GameObject newCannon = Instantiate(cannonPrefab, hitpose.position, hitpose.rotation);
                if (cannons[currindex] != null) Destroy(cannons[currindex]);
                cannons[currindex] = newCannon;
                currindex++;
                if (currindex == maxCannonNum) currindex = 0;*/
            }
        }
    }

    /*private void buildNavMesh(Vector3 pos, Quaternion rot)
    {
        NavMesh.RemoveAllNavMeshData();

        m_NavMeshData.position = pos;
        m_NavMeshData.rotation = rot;
        NavMesh.AddNavMeshData(m_NavMeshData);
    }*/
}
