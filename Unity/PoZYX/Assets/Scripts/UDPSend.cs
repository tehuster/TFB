using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPSend : MonoBehaviour
{
    public MotorSpeed motorSpeed;
    private static int localPort;
    // prefs
    public string IP;  // define in init
    public int port;  // define in init

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;  
    // gui
    string strMessage = "";

    // call it from shell (as program)
    private static void Main()
    {
        UDPSend sendObj = new UDPSend();
        sendObj.init();

        // testing via console
        // sendObj.inputFromConsole();

        // as server sending endless
       // sendObj.sendEndless(" endless infos \n");

    }
    // start from unity3d
    public void Start()
    {
        init();
    }   

    public void init()
    {      
        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        Debug.Log("Sending to " + IP + ":" + port);
    }

    public void turnOffBelt(){
            try
            {
               
                // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
                byte[] data = Encoding.UTF8.GetBytes("0000000000000000;");

                // Den message zum Remote-Client senden.
                client.Send(data, data.Length, remoteEndPoint);
                //}
            }
            catch (Exception err)
            {
                print(err.ToString());
            }
    }
    public void sendMotorInfo()
    {
        string motorInfo = "";
        for (int i = 0; i < 8; i++)
        {
            motorInfo += motorSpeed.MotorsSpeed[i].ToString("D2");
        }
        motorInfo += ";";
        try
        {
           
            // Daten mit der UTF8-Kodierung in das Binärformat kodieren.
            byte[] data = Encoding.UTF8.GetBytes(motorInfo);

            // Den message zum Remote-Client senden.
            client.Send(data, data.Length, remoteEndPoint);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

}