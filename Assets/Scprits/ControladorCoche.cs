using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCoche : MonoBehaviour
{
    public int v = 20;
    private int NUM_SENSORES = 3;
    private char estado;
    private ControladorSensores[] sensores;

    private bool[] distancia;

	void Start ()
    {
        estado = 'p';
        sensores = new ControladorSensores[NUM_SENSORES];
        distancia = new bool[NUM_SENSORES];
        sensores[0] = this.gameObject.transform.GetChild(0).GetComponent<ControladorSensores>();
        sensores[1] = this.gameObject.transform.GetChild(1).GetComponent<ControladorSensores>();
        sensores[2] = this.gameObject.transform.GetChild(2).GetComponent<ControladorSensores>();
    }
	
	void Update ()
    {

	}

    private void FixedUpdate()
    {
        LeerSensores();
        Autonomo2(v);
        //Manual(v);

    }
    private void Manual(int v)
    {
        if (Input.GetKey(KeyCode.D))
        {
            GirarDerecha(v - 2);
        }
        if (Input.GetKey(KeyCode.A))
        {
            GirarIzquierda(v - 2);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Avanzar(v);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Retroceder(v);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            PararMotor(v);
        }
        if (Input.GetKey(KeyCode.M))
        {
            PararGiro(v);
        }
    }
    private void Autonomo(int v)
    {
        if (!distancia[0] && !distancia[1] && !distancia[2])
        {
            Avanzar(v);
        }
        else
        {
            if (!distancia[0] && distancia[1] && !distancia[2])
            {
                Avanzar(v);
            }
            else if (distancia[0] && !distancia[2])
            {
                PararMotor(v);
                PararGiro(v);
                GirarDerecha(v);
            }
            else if (!distancia[0] && distancia[2])
            {
                PararMotor(v);
                PararGiro(v);
                GirarIzquierda(v);
            }

            else
            {
                PararMotor(v);
                PararGiro(v);
                int ran = Random.Range(0, 2);
                Debug.Log(ran);
                if (ran == 0)
                {
                    GirarDerecha(v);
                }
                else if (ran == 1)
                {
                    GirarIzquierda(v);
                }
            }
        }
    }
    private void Autonomo2(int v)
    {
        if (!distancia[0] && !distancia[1] && !distancia[2])
        {
            Avanzar(v);
        }
        else if (!distancia[0] && !distancia[1] && distancia[2])
        {
            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
               Avanzar(v);
            }
            else if (ran == 1)
            {
                GirarIzquierda(v);
            }
        }
        else if (!distancia[0] && distancia[1] && !distancia[2])
        {
            GirarDerecha(v);
            GirarDerecha(v);
            GirarDerecha(v);
            GirarDerecha(v);
        }
        else if (!distancia[0] && distancia[1] && distancia[2])
        {
            GirarIzquierda(v);
        }
        else if (distancia[0] && !distancia[1] && !distancia[2])
        {

            int ran = Random.Range(0, 2);
            if (ran == 0)
            {
                Avanzar(v);
            }
            else if (ran == 1)
            {
                GirarDerecha(v);
            }
        }
        else if (distancia[0] && !distancia[1] && distancia[2])
        {
            Avanzar(v);
        }
        else if (distancia[0] && distancia[1] && !distancia[2])
        {
            GirarDerecha(v);
        }
        else if (distancia[0] && distancia[1] && distancia[2])
        {
            GirarDerecha(v);
            GirarDerecha(v);
            GirarDerecha(v);
            GirarDerecha(v);
        }
    }
    private void GirarDerecha(int v)
    {
        transform.Rotate((new Vector3(0f, 1f, 0f) * Time.fixedDeltaTime) * v * 10);
    }
    private void GirarIzquierda(int v)
    {
        transform.Rotate((new Vector3(0f, -1f, 0f) * Time.fixedDeltaTime) * v * 10);
    }
    private void Avanzar(int v)
    {
        estado = 'w';
        transform.Translate((Vector3.forward * Time.fixedDeltaTime) * v);
    }
    private void Retroceder(int v)
    {
        estado = 's';
        transform.Translate((Vector3.back * Time.fixedDeltaTime) * v);
    }
    private void PararMotor(int v)
    {
        estado = 'p';
        transform.Translate((Vector3.zero * Time.fixedDeltaTime) * v);
        transform.Rotate((Vector3.zero * Time.fixedDeltaTime) * v * 10);
    }
    private void PararGiro(int v)
    {
        if(estado == 'w')
        {
            Avanzar(v);
        }
        else if(estado == 's')
        {
            Retroceder(v);
        }
        else if(estado == 'p')
        {
            PararMotor(v);
        }
    }

    private void LeerSensores()
    {
        for(int i = 0; i <  NUM_SENSORES; i++)
        {
            distancia[i] = sensores[i].GetEstado();
        }
    }

    private void MostrarSensores()
    {
        string lectura = "";
        for (int i = 0; i < NUM_SENSORES; i++)
        {
          lectura += i + " " + distancia[i]  + " ";
        }
        Debug.Log(lectura);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "obstaculo")
        {
            Debug.Log("chocque");
        }
    }
}
