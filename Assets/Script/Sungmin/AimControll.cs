using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimControll : MonoBehaviour
{
    MomiFSMManager momiManager;
    Momi_Handle momiHandle;
    GameObject ui;

    // Start is called before the first frame update
    void Start()
    {
        momiManager = GameObject.Find("Momi").GetComponent<MomiFSMManager>();
        momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        ui = GameObject.Find("Press_E");
        ui.SetActive(false);
    }

    void OnTriggerStay(Collider col)
    {
        if ((col.transform.tag == "Star_Handle" || col.transform.tag == "Star_Handle_2"
            || col.transform.tag == "Potato_Handle" || col.transform.tag == "Potato_Handle_2"
            || col.transform.tag == "Planet_Handle") && momiManager.CurrentState != MomiState.Handle)
            ui.SetActive(true);

        if (col.transform.tag == "Star_Handle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 0;
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        }

        if (col.transform.tag == "Star_Handle_2" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 1;
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        }

        if (col.transform.tag == "Potato_Handle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 2;
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        }
        if (col.transform.tag == "Potato_Handle_2" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 3;
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        }

        if (col.transform.tag == "Planet_Handle" && Input.GetKeyDown(KeyCode.E) && momiManager.CurrentState != MomiState.Handle)
        {
            momiHandle.col = col.gameObject;
            momiHandle.handleNum = 4;
            momiManager.SetState(MomiState.Handle);
            momiHandle = GameObject.Find("Momi").GetComponent<Momi_Handle>();
        }
    }

    void OnTriggerExit(Collider col)
    {
        ui.SetActive(false);
    }
}