using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Player : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //todo:
        if (collision.gameObject.tag == "")
        {
            //todo:そのオブジェクトを消す？
        }
    }
}
