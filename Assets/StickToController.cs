using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToController : MonoBehaviour {

    Rigidbody m_Rigidbody;

    // Use this for initialization
    void Start () {
		
	}

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update () {
        transform.position = transform.parent.position;
        transform.rotation = transform.parent.rotation;
        m_Rigidbody.isKinematic = false;
	}
}
