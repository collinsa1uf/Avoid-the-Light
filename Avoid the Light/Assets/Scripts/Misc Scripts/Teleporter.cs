﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameObject sp1, sp2;

    private GameObject trig;
    
    void Start() {
        sp1 = this.gameObject;
    }

    void Update()
    {
        if (trig != null)
        {
            if (Input.GetButtonDown("Teleport"))
            {
                trig.gameObject.transform.position = sp2.gameObject.transform.position;

            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag.Equals("Player"))
        {
            trig = null;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag.Equals("Player"))
        {
            trig = collision.gameObject;
        }
    }

}
