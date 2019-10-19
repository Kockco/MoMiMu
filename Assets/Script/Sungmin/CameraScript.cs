using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject momi;
    MomiFSMManager momiState;
    public GameObject[] moveToObject;
    public float momiY;

    public float rotationSpeed, rotationXMax, scrollSpeed, viewSpeed; 
    public float distance, minDis, maxDis; 
    public bool isClear, puzzleClear;
    float rotX, rotY;

    Vector3 momiPos, momiDirect, camPos;
    Quaternion rotation;
    RaycastHit rayHit;

    void Start()
    {
        momi = GameObject.Find("Momi").transform.GetChild(1).gameObject;
        momiState = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
        
        Cursor.visible = false;
        // Cursor.lockState = CursorLockMode.Locked;
    }
    
    void LateUpdate()
    {
        if (momiState.CurrentState != MomiState.Handle && puzzleClear == false)
            LookAtMomi();
    }

    public void CamMoveToObject(int num)
    {
        transform.position = Vector3.Lerp(transform.position, moveToObject[num].transform.position, Time.deltaTime * viewSpeed / 2);
        transform.rotation = Quaternion.Lerp((Quaternion)transform.rotation, (Quaternion)moveToObject[num].transform.rotation, Time.deltaTime * viewSpeed);
    }

    void LookAtMomi()
    {
        rotX += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;
        rotY += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        // distance += -Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -rotationXMax, rotationXMax);
        distance = Mathf.Clamp(distance, minDis, maxDis);

        momiPos = momi.transform.position + Vector3.up * momiY;
        momiDirect = Quaternion.Euler(-rotX, rotY, 0f) * Vector3.forward;
        camPos = momiPos + momiDirect * -distance;

        RayToWall();
        transform.position = Vector3.Lerp(transform.position, camPos, Time.deltaTime * viewSpeed);
        transform.LookAt(momiPos);
    }

    void RayToWall()
    {
        rayHit = new RaycastHit();
        Vector3 tempVec = transform.position - momiPos;

        if (Physics.Raycast(momiPos, tempVec.normalized, out rayHit, maxDis))
        {
            if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                transform.position = momiPos + (tempVec.normalized * rayHit.distance);
                distance = rayHit.distance;
            }
        }
        else
            distance = maxDis;

        // if (rayHit.transform != null) Debug.Log(rayHit.transform.name + ", " + rayHit.point);
    }

    public void PuzzleClearView(ParticleSystem tempParticle, bool ifClear)
    {
        puzzleClear = ifClear;

        if (puzzleClear)
            transform.LookAt(tempParticle.transform);

        // if (tempParticle.isStopped) puzzleClear = false;

        Invoke("ExitPuzzleView", 3);
    }

    void ExitPuzzleView()
    {
        puzzleClear = false;

        Debug.Log("ExitPuzzle");
    }
}