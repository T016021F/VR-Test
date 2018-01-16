using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourChanger : MonoBehaviour {

	MeshRenderer m_Renderer;

	int m_MaterialIndex;

	public Material[] m_Material;

	// Use this for initialization
	void Start () {
		m_Renderer = GetComponent<MeshRenderer> ();

		m_MaterialIndex = 0;
		m_Renderer.material = m_Material [m_MaterialIndex];

	}

	void ChangeMaterial()
	{
		m_MaterialIndex++;
		if(m_MaterialIndex > m_Material.Length - 1)
		{
			m_MaterialIndex = 0;
		}
		m_Renderer.material = m_Material[m_MaterialIndex];

	}

	private void OnCollisionEnter(Collision collision)
	{
		ChangeMaterial ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
