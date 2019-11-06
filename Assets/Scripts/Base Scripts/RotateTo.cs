using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTo : MonoBehaviour
{
    public Quaternion origin;
    public Quaternion end;
    [Range (0, 359)]
    public float rotationAngle;
    public float rotationSpeed;
    public bool opening;
    public bool active;
    private float timeCount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeCount = 0;
        origin = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        SetRotation();    
    }

    void SetRotation()
    {
        end = Quaternion.Euler(0f, 0f, rotationAngle);
        if (active)
        {
            if (opening)
            {

                gameObject.transform.rotation = (Quaternion.Slerp(origin, end, timeCount));
                timeCount += Time.deltaTime * rotationSpeed;
                if (timeCount >= 1)
                {
                    //opening = false;
                    //timeCount = 0;
                }
            }
            else
            {

                gameObject.transform.rotation = (Quaternion.Slerp(end, origin, timeCount));
                timeCount += Time.deltaTime * rotationSpeed;
                if (timeCount >= 1)
                {
                    //opening = true;
                    //timeCount = 0;
                }
            }
        }
    }
}
