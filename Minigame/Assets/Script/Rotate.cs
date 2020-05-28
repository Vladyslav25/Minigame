using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float m_rotateSpeed = 20f;
    private Vector3 m_hover;

    void Update()
    {
        //Rotation
        transform.Rotate(Vector3.left, Time.deltaTime * m_rotateSpeed);
        transform.Rotate(Vector3.up, Time.deltaTime * m_rotateSpeed);
        //Hover Effect
        m_hover = Vector3.up *0.008f* Mathf.Cos(Time.time );
        transform.position += m_hover;
    }
}
