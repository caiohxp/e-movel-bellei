using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;
    public float speedRotation = 2.0f;
    public bool moveAble = true;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void CameraMove(GameObject cameraTarget)
    {
        
        StartCoroutine(SmoothLookAt(cameraTarget.transform.position));
    }

    private IEnumerator SmoothLookAt(Vector3 targetPosition)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPosition - mainCamera.transform.position);
        
        while (Quaternion.Angle(mainCamera.transform.rotation, targetRotation) > 0.5f)
        {
            moveAble = false;
            Debug.Log("TESTE " + Quaternion.Angle(mainCamera.transform.rotation, targetRotation));
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, Time.deltaTime * speedRotation);
            yield return null;
        }
        if(Quaternion.Angle(mainCamera.transform.rotation, targetRotation)<= 0.5f){
            moveAble = true;
        }

        mainCamera.transform.rotation = targetRotation;
    }
}
