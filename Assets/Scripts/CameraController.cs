using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float MIN_FOLLOW_Y_OFFSET =2f;
    [SerializeField] private float MAX_FOLLOW_Y_OFFSET =50f;
    private CinemachineTransposer cinemachineTransposer;
    private Vector3 targetFollowOffset;
    private float moveSpeed = 10f;
    private float rotationSpeed = 100f;
    private float zoonAmount = 5f;
    // Update is called once per frame
    private void Start(){
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }
    void Update()
    {
        HandeMoveMent();
        HandleRotation();
        HandleZoom();
    }

    private void HandeMoveMent()
    {
        Vector3 inputMoveDir = new Vector3(0,0,0);

        if(Input.GetKey(KeyCode.W)){
            inputMoveDir.z = +1f;
        }
        if(Input.GetKey(KeyCode.S)){
            inputMoveDir.z = -1f;
        }
        if(Input.GetKey(KeyCode.A)){
            inputMoveDir.x = -1f;
        }
        if(Input.GetKey(KeyCode.D)){
            inputMoveDir.x = +1f;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }
    private void HandleRotation()
    {
        Vector3 rotationVector = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.Q)){
            rotationVector.y = +1f;
        }
        if(Input.GetKey(KeyCode.E)){
            rotationVector.y = -1f;
        }
    }
    private void HandleZoom()
    {
        Vector3 rotationVector = new Vector3(0,0,0);
        if(Input.GetKey(KeyCode.Q)){
            rotationVector.y = +1f;
        }
        if(Input.GetKey(KeyCode.E)){
            rotationVector.y = -1f;
        }

        transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;

        if(Input.mouseScrollDelta.y > 0)
        {
            targetFollowOffset.y -= zoonAmount;

        }
        if(Input.mouseScrollDelta.y < 0)
        {
            targetFollowOffset.y += zoonAmount;
        }
        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoonAmount);
    }
}
