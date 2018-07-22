using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    public GameObject coche;

    void Start ()
    {
		
	}
	
	void Update ()
    {
        Transform posicionCoche = coche.transform;
        Vector3 nuevaPos =
            new Vector3(posicionCoche.position.x, posicionCoche.position.y + 2, posicionCoche.position.z - 10f);

        transform.position = nuevaPos;
        this.transform.LookAt(posicionCoche);
    }
}
