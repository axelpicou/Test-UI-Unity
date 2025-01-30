using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    float m_Speed;
    public bool canmove;

    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        m_Rigidbody = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        m_Speed = 10.0f;
        canmove = true;
    }

    void Update()
    {
        if (canmove == true)
        {
            if (Input.GetKey(KeyCode.W))
                {
                   //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
                  m_Rigidbody.velocity = -transform.forward * m_Speed;
                }

        if (Input.GetKey(KeyCode.S))
        {
            //Move the Rigidbody backwards constantly at the speed you define (the blue arrow axis in Scene view)
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Rotate the sprite about the Y axis in the positive direction
            transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime * m_Speed, Space.World);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //Rotate the sprite about the Y axis in the negative direction
            transform.Rotate(new Vector3(0, -10, 0) * Time.deltaTime * m_Speed, Space.World);
        }

        }
       
    }
}
