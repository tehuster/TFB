using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUp : MonoBehaviour
{
    public POZYXVariable POZYX;

    // Update is called once per frame
    void Update()
    {        
       Debug.Log(POZYX.yaw * -1);
      // transform.rotation = Quaternion.Euler(0, 0, (POZYX.yaw * -1) - POZYX.yaw) ;
    }
}
