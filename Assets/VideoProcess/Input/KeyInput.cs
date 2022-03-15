using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : InputBase
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SelectScene(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SelectScene(1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SelectScene(2);
            }
        }
    }

}
