using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour {

    Rigidbody m_Rigidbody;

    public Transform m_Holder { get; private set; }

    //How fast the object will rotate
    float m_RotationSpeed = 75;


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    public void Pickup(Transform parent)
    {
        m_Rigidbody.isKinematic = true;

        m_Holder = parent;
        transform.SetParent(m_Holder);
        transform.localPosition = Vector3.zero;
    }

    public void Drop(Vector3 velocity, Vector3 angularVelocity)
    {
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.velocity = velocity;
        m_Rigidbody.angularVelocity = angularVelocity;
        transform.SetParent(null);
        m_Holder = null;
    }

    public void Interact(Vector2 touchPadPosition)
    {
        transform.Rotate(Camera.main.transform.up * -touchPadPosition.x * m_RotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Camera.main.transform.right * -touchPadPosition.y * m_RotationSpeed * Time.deltaTime, Space.World);
    }

}
