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

    Vector3 momiPos, mouseMove;
    RaycastHit rayHit;

    void Start()
    {
        momi = GameObject.Find("Momi").transform.GetChild(1).gameObject;
        momiState = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        Vector3 momiDirect = Quaternion.Euler(-rotX, rotY, 0f) * Vector3.forward;
        Vector3 camPos = momiPos + momiDirect * -distance;

        RayToWall();
        transform.position = Vector3.Lerp(transform.position, camPos, 0.8f);
        transform.LookAt(momiPos);
        // transform.position = Vector3.Lerp(transform.position, momiPos, Time.deltaTime * viewSpeed);
        // transform.localEulerAngles = new Vector3(-rotX * (viewSpeed / 2), rotY * (viewSpeed / 2), 0);
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

        // if (rayHit.transform != null) Debug.Log(rayHit.transform.name + ", " + rayHit.point);
    }

    public void PuzzleClearView(GameObject viewPos)
    {
        puzzleClear = true;

        transform.position = Vector3.Lerp(transform.position, viewPos.transform.position, Time.deltaTime * viewSpeed);
        transform.rotation = Quaternion.Lerp((Quaternion)transform.rotation, (Quaternion)viewPos.transform.rotation, Time.deltaTime * viewSpeed);

        Debug.Log(viewPos.transform.name);
    }
}

/*
 void MouseSense()
    {
        //cameraParentTransform.position = myTransform.position + Vector3.up * camHeight;  //캐릭터의 머리 높이쯤
        Vector3 TargetPos = new Vector3(momi.transform.position.x, momi.transform.position.y * momiY, momi.transform.position.z);
        transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * viewSpeed);

        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * viewSpeed, Input.GetAxisRaw("Mouse X") * viewSpeed, 0);   //마우스의 움직임을 가감
        if (mouseMove.x < -40)  //위로 볼수있는 것 제한 90이면 아예 땅바닥에서 하늘보기
            mouseMove.x = -40;
        else if (50 < mouseMove.x) //위에서 아래로 보는것 제한 
            mouseMove.x = 50;

        transform.localEulerAngles = mouseMove;
    }

    void Balance()
    {
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)   //대각선으로 틀어질 경우는 없어야하니 바로잡기
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
 */
