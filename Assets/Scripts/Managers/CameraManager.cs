using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    public void SetCameraTargetFollow(Transform target) => cinemachineVirtualCamera.Follow = target;
}
