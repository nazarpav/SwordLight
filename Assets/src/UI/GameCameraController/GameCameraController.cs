using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCameraController : MonoBehaviour
{
    public new Camera camera;
    public GameObject FollowedObject;
    public GameObject TopRightMarker;
    public GameObject BottomLeftMarker;
    public bool IsLockOnY;
    public bool IsLockOnX;
    public float CamerMoveSpeed;
    private Vector2 TR_MarkerPos;
    private Vector2 BL_MarkerPos;
    private Vector2 CameraSize;
    public Vector2 Offset;
    private Vector3 NewCameraPos;
    private Vector3 NewCameraPosBuf;
    void Start()
    {
        if (TopRightMarker == null || BottomLeftMarker == null)
        {
            TR_MarkerPos = new Vector2(float.MaxValue, float.MaxValue);
            BL_MarkerPos = new Vector2(float.MaxValue, float.MaxValue);
            CameraSize = new Vector2(0.0f, 0.0f);
        }
        else
        {
            TR_MarkerPos = TopRightMarker.transform.position;
            BL_MarkerPos = BottomLeftMarker.transform.position;
            float height = camera.orthographicSize;
            CameraSize = new Vector2(height * camera.aspect, height);
        }
        NewCameraPos = new Vector3();
        NewCameraPosBuf = new Vector3();
        NewCameraPos.z = camera.transform.position.z;
        NewCameraPosBuf.z = camera.transform.position.z;
    }
    bool IsCameraInMarkersRectX()
    {
        return (BL_MarkerPos.x > NewCameraPos.x - CameraSize.x || TR_MarkerPos.x < NewCameraPos.x + CameraSize.x) == false;
    }
    bool IsCameraInMarkersRectY()
    {
        return (BL_MarkerPos.y > NewCameraPos.y - CameraSize.y || TR_MarkerPos.y < NewCameraPos.y + CameraSize.y) == false;
    }
    private void FixedUpdate()
    {
        NewCameraPos.x = (IsLockOnX ? camera.transform.position.x : FollowedObject.transform.position.x) - Offset.x;
        NewCameraPos.y = (IsLockOnY ? camera.transform.position.y : FollowedObject.transform.position.y) - Offset.y;
        //Vector3 velocity = Vector3.zero;
        //NewCameraPos = Vector3.SmoothDamp(camera.transform.position, NewCameraPos, ref velocity, CamerMoveSpeed);
        //NewCameraPos = Vector3.Lerp(camera.transform.position, NewCameraPos, 0.1f);
        if (IsCameraInMarkersRectX())
        {
            NewCameraPosBuf.x = NewCameraPos.x;
            NewCameraPosBuf.y = camera.transform.position.y;
            camera.transform.position = NewCameraPosBuf;
        }
        if (IsCameraInMarkersRectY())
        {
            NewCameraPosBuf.x = camera.transform.position.x;
            NewCameraPosBuf.y = NewCameraPos.y;
            camera.transform.position = NewCameraPosBuf;
        }
    }
}
