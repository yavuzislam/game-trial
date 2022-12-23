using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator _animator;
    public GameObject bg;
    public FloatingJoystick _floatingJoystick;
    float _vertical, _horizontal, speed = 4f;
    public List<GameObject> boxes;
    public GameObject[] collecBox;
    public GameObject box;
    GameObject bx;
    private void FixedUpdate()
    {
        //_horizontal = Input.GetAxis("Horizontal");
        //_vertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3(_horizontal, 0, _vertical);
        //transform.LookAt(transform.position + movement);
        //transform.position+= movement*speed*Time.deltaTime;
        if (bg.activeInHierarchy)
        {
            _horizontal = _floatingJoystick.Horizontal;
            _vertical = _floatingJoystick.Vertical;
            Vector3 movement = Vector3.forward * _vertical + Vector3.right * _horizontal;
            transform.LookAt(transform.position + movement);
            transform.position += movement * speed * Time.deltaTime;
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 10f*Time.deltaTime);

            Vector3 clampmove = transform.position;
            clampmove.x = Mathf.Clamp(clampmove.x, -4.5f, 4.5f);
            clampmove.z = Mathf.Clamp(clampmove.z, -4.5f, 4.5f);
            transform.position = clampmove;
            _animator.SetBool("Move",true);
        }
        else
        {
            _animator.SetBool("Move", false);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "GreenCylinder":
                Collectable(Color.green, "Green");
                break;
            case "RedCylinder":
                Collectable(Color.red, "Red");
                break;
            case "black":
                for (int i = 0; i < boxes.Count; i++)
                {
                    if (boxes[i].tag == "Red")
                    {
                        boxes[i].transform.position = collecBox[0].transform.position + new Vector3(0f, 1.5f, 0f) + new Vector3(0f, 0.5f, 0f) * collecBox[0].transform.childCount;
                        boxes[i].transform.SetParent(collecBox[0].transform);

                    }
                    else if (boxes[i].tag == "Green")
                    {
                        boxes[i].transform.position = collecBox[1].transform.position + new Vector3(0f, 1.5f, 0f) + new Vector3(0f, 0.5f, 0f) * collecBox[1].transform.childCount;
                        boxes[i].transform.SetParent(collecBox[1].transform);
                    }
                }
                boxes.Clear();
                break;
        }
    }
    public void Collectable(Color color, string tag)
    {
        bx = Instantiate(box, new Vector3(0f, 0.3f, 0f) * boxes.Count + transform.position + new Vector3(0f, 1.5f, 0f), Quaternion.identity);
        bx.transform.SetParent(transform);
        bx.GetComponent<MeshRenderer>().material.color = color;
        bx.tag = tag;
        boxes.Add(bx);
    }
}

