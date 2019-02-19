using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSphere : MonoBehaviour
{
    public POZYXVariable POZYX;
    private Vector3 oldPos;
    private Vector3 velocity;
    public float velocityTresshold = 0f;
    public float lerpValue = 0f;


    [SerializeField]
    private float velX;
    [SerializeField]
    private float velY;
    [SerializeField]
    private float velZ;

    private float oldVelZ = 0;
    private float oldVelX = 0;

    private bool Movement_Toggle = true;
 
    void Start()
    {
        oldPos = transform.position;
    }
 
    void Update()
    {
        if (Movement_Toggle)
        {
            moveUser();
        }
    }
    
    public void toggleMovement(bool toggle)
    {
        Debug.Log(toggle);
        Movement_Toggle = toggle;
    }

    private void moveUser()
    {
        Vector3 newPos = new Vector3(POZYX.x, POZYX.y, POZYX.z);

        velocity = (newPos - oldPos);

        velX = velocity.x;
        velY = velocity.y;
        velZ = velocity.z;

        if (velocity.x > velocityTresshold || velocity.x < (velocityTresshold * -1))
        {
            //    Debug.Log("VelocityX");
            POZYX.x = oldVelX;
        }
        else
        {
            oldVelX = POZYX.x;
        }
        // if(velocity.y > velocityTresshold || velocity.y < (velocityTresshold * -1)){
        //     Debug.Log("VelocityY");
        //     y = transform.position.y;
        // }
        if (velocity.z > velocityTresshold || velocity.z < (velocityTresshold * -1))
        {
            //   Debug.Log("VelocityZ");
            POZYX.z = oldVelZ;
        }
        else
        {
            oldVelZ = POZYX.z;
        }

        newPos = new Vector3(POZYX.x, POZYX.y, POZYX.z);

        transform.position = Vector3.Lerp(transform.position, newPos, lerpValue);
        transform.eulerAngles = new Vector3(0, POZYX.yaw, 0);

        oldPos = transform.position;
    }
}
