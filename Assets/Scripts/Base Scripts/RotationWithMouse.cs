using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class RotationWithMouse : MonoBehaviour
{
    
    public Transform Target;
    public Transform Player;

    public int moveAngle;
    public float xSpeed = 1;
    public float ySpeed = 1;

    float mouseX;
    float mouseY;
    public bool yInverted;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        CamControl();
    }

    void CamControl()

    {

        mouseX += Input.GetAxis("Mouse X") * xSpeed;

        //Check y Inverted
        if (yInverted)
        {
            mouseY -= Input.GetAxis("Mouse Y") * ySpeed;
        }
        else
            mouseY += Input.GetAxis("Mouse Y") * ySpeed;
        //Lock Min Max
        mouseY = Mathf.Clamp(mouseY, -35f, 60f);

        transform.LookAt(Target);
        Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        if (Player != null)
        {
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }

    }
}
