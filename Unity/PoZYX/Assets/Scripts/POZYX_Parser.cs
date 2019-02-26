using UnityEngine;

public class POZYX_Parser : MonoBehaviour {

    public StringVariable UDP_Message;
    public POZYXVariable PoZYX;

    private void Update() {

        Debug.Log(UDP_Message.Value);

        if (UDP_Message.Value.Length != 0) {
            string[] words = UDP_Message.Value.Split(',');
            float newX = float.Parse(words[0]);
            float newY = float.Parse(words[1]);
            //float newZ = float.Parse(words[2]);

            PoZYX.x = newX / 100;
            PoZYX.z = newY / 100;
            PoZYX.yaw = float.Parse(words[2]);

            //PoZYX.y = 2;// / 100;
            //PoZYX.roll = float.Parse(words[4]);
            //PoZYX.pitch = float.Parse(words[5]);

            Debug.Log(newX + " " + newY + " " + float.Parse(words[2]));
        }

    }

}
