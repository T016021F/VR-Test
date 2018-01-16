using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float m_PlayerHeight;

	// Use this for initialization
	void Start () {
        m_PlayerHeight = transform.position.y;
        //transform.position.y = m_PlayerHeight;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
