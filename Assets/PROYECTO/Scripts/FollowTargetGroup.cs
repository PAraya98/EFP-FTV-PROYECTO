using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FollowTargetGroup : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachinevirtualcamera;

    // Start is called before the first frame update
    void Start()
    {
        cinemachinevirtualcamera = GetComponent<CinemachineVirtualCamera>();
        cinemachinevirtualcamera.Follow = GameObject.FindGameObjectWithTag("CameraGroup").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
