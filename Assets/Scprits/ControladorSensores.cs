using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorSensores : MonoBehaviour
{
    public bool estado;
    public Material material;
    public Material materialDetectado;

    private MeshRenderer msRender;

    void Start ()
    {
        estado = false;
        msRender = GetComponent<MeshRenderer>();
    }
	
	void Update ()
    {
		
	}

    public bool GetEstado()
    {
        return estado;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "obstaculo")
        {
            estado = true;
            msRender.material = materialDetectado;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "obstaculo")
        {
            estado = false;
            msRender.material = material;
        }
    }
}
