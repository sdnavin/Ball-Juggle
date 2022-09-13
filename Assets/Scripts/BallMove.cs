using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    // Start is called before the first frame update

    public UDPReceive udpReceive;
    
    int countIn;
    float magnitude;
    [SerializeField]
    float errorValue;

    BallData ballData;

    bool checkUp;
    void Start()
    {

    }

    private void OnEnable()
    {
        ballData = AppHandler.instance.ballData;
    }

    // Update is called once per frame
    void Update()
    {

        string data = udpReceive.data;
        if (string.IsNullOrEmpty(data))
        {
            return;
        }
        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        //(255,361,50012) example data (x,y,area)
        string[] info = data.Split(',');

        float x = -1*float.Parse(info[0]) / 50;
        float y = float.Parse(info[1]) / 50;
        float z = transform.localPosition.z;// float.Parse(info[2]) / 5000;

        gameObject.transform.localPosition = Vector3.Lerp(transform.localPosition,new Vector3(x, y, z),Time.deltaTime*20);

        updateBall();
    }

    void updateBall()
    {
        if (transform.position.y < ballData.LostPoint)
        {
            countIn = 0;
            UiHandler.instance.UpdateCount(countIn);
        }else if (!checkUp&&transform.position.y < (ballData.CalculatePoint-errorValue))
        {
            checkUp = true;
            StartCoroutine(checkGoingUp());
        }
      
    }
    int balltravel=0;
    int ballstate = 0;
    private void FixedUpdate()
    {
        
    }
    private void LateUpdate()

    {
       
    }
    IEnumerator checkGoingUp()
    {
     
        yield return new WaitForSeconds(ballData.time);
        if (checkUp&&transform.position.y > (ballData.CalculatePoint+errorValue))
        {
            countIn++;
            UiHandler.instance.UpdateCount(countIn);
        }
        checkUp = false;

    }
}