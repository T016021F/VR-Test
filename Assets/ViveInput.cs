using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour {

    SteamVR_TrackedObject m_Controller;
    SteamVR_Controller.Device m_InputDevice;
    List<Interactable> m_Interactables;
    Interactable m_PickedUpObject;

    PlayerController m_PlayerController;
    public GameObject m_TeleportMarker;
    float m_teleportDistance = 20;

    public GameObject CubeSummon;
    public GameObject SphereSummon;
    
   // Vector3 sumPos;
   // Quaternion sumRot;

	// Use this for initialization
	void Start () 
	{
        m_Controller = GetComponent<SteamVR_TrackedObject>();
        m_Interactables = new List<Interactable>();
        // sumPos = new Vector3(0,0,0);
        //   sumRot = new Quaternion (0,0,0,0);
        m_PlayerController = transform.root.GetComponent<PlayerController>();

    }

    Interactable FindClosestObject()
    {
        float distance = float.MaxValue;
        int closestObjectIndex = -1;

        for (int i = 0; i < m_Interactables.Count; i++)
        {
            if (m_Interactables[i].m_Holder == null)
            {
                float tempDistance = Vector3.Distance(transform.position, m_Interactables[i].transform.position);

                if (tempDistance < distance)
                {
                    distance = tempDistance;
                    closestObjectIndex = i;
                }
            }
        }

        if(closestObjectIndex >= 0)
        {
            return m_Interactables[closestObjectIndex];
        }
        return null;
    }

	/// <summary>
	/// Raises the trigger enter event.
	/// </summary>
	/// <param name="other">the collider that entered the trigger/param>
    /// 

        void Teleport()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, m_teleportDistance , LayerMask.GetMask("TeleportLocation")))
        {
            //Turn the teleport marker on if raycast has hit a surface
            if (!m_TeleportMarker.activeSelf)
            {
                m_TeleportMarker.SetActive(true);
            }
            //set teleport marker position
            m_TeleportMarker.transform.position = hit.point;

            if (m_InputDevice.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad))
            {
                //SteamVR_Render.Top().origin.position = new Vector3(hit.point.x, hit.point.y + m_PlayerController.m_PlayerHeight, hit.point.z);
                SteamVR_Render.Top().origin.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
            else
            {
                if (m_TeleportMarker.activeSelf)
                    m_TeleportMarker.SetActive(false);
            }


        }
    }

	private void OnTriggerEnter(Collider other)
    {
		Debug.Log ("TRIGGER PULLED");
		//Debug.Log (other.name);
        if (other.tag == "Interactable")
        {
			m_Interactables.Add (other.GetComponent<Interactable> ());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            m_Interactables.Remove(other.GetComponent<Interactable>());
        }
    }
	
	// Update is called once per frame
	void Update () {
        m_InputDevice = SteamVR_Controller.Input((int)m_Controller.index);
            

        //Trigger

        //Checks if trigger is pressed down

        if(m_InputDevice.GetHairTriggerDown())
        {
            Debug.Log("Trigger Down!");
            Interactable closestObject = FindClosestObject();

            if (closestObject != null)
            {
                m_PickedUpObject = closestObject;
                m_PickedUpObject.Pickup(transform);
            }
        }
        //Checks if trigger is released
        if (m_InputDevice.GetHairTriggerUp())
        {
            Debug.Log("Trigger Up!");

            if (m_PickedUpObject != null)
            {
                m_PickedUpObject.Drop(m_InputDevice.velocity, m_InputDevice.angularVelocity);
                m_PickedUpObject = null;
            }
        }

        //Checks value of the trigger button. Get axis returns as a vector 2 however only x value is needed to track the value
        float triggerValue = m_InputDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
        if (triggerValue > 0)
            Debug.Log(triggerValue);

        //Touchpad

        if (m_InputDevice.GetPressDown(EVRButtonId.k_EButton_SteamVR_Touchpad))
            Debug.Log("Touch down!");

        if (m_InputDevice.GetPressUp(EVRButtonId.k_EButton_SteamVR_Touchpad))
            Debug.Log("Touch up!");

        //Returns the current touch position of the touchpad
        Vector2 touchPad = m_InputDevice.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        if (touchPad != Vector2.zero)
        {
            if (m_PickedUpObject != null)
            {
                m_PickedUpObject.Interact(touchPad);
            }
            else
            {
                Teleport();
            }
        }

        //Grip

        if (m_InputDevice.GetPressDown(EVRButtonId.k_EButton_Grip))
        {
            Debug.Log("Grip down!");
            for(int i = 0; i < 1; i++)
            {
                Instantiate(CubeSummon, transform.position, transform.rotation);
            }
        }

        if (m_InputDevice.GetPressUp(EVRButtonId.k_EButton_Grip))
            Debug.Log("Grip up!");

        //Menu Button
        //Grip

        if (m_InputDevice.GetPressDown(EVRButtonId.k_EButton_ApplicationMenu))
        {
            Instantiate(SphereSummon, transform.position, transform.rotation);
        }
        if (m_InputDevice.GetPressUp(EVRButtonId.k_EButton_ApplicationMenu))
            Debug.Log("Menu button up!");

    }
}
