using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _vertical, _horizontal, speed = 2;
    private void FixedUpdate()
    {
        //_horizontal = Input.GetAxis("Horizontal");
        //_vertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(_horizontal, 0, _vertical);
        //transform.LookAt(transform.position + movement);
        //transform.position+= movement*speed*Time.deltaTime;

        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");
        Vector3 movement = Vector3.forward * _vertical + Vector3.right * _horizontal;
        transform.position += movement * speed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 10f*Time.deltaTime);
    }
}
