using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    public enum MoveState
    {
        Walk,
        Run,
        Dash
    }
    [SerializeField]
    private MoveState moveState;




    [Range(1f, 100f)]
    public float moveSpeed = 10f;
    public float runTimer = 0.5f;
    public float runCD = 0.5f;


    public float dashTimer = 2f;
    public float dashCD = 2f;
    public int dashCount;
    public float dashSpeed = 20f;
    public Vector3 newDir;

    public bool moving = true;
    // Start is called before the first frame update
    void Start()
    {
        moveState = MoveState.Walk;

    }


    // Update is called once per frame
    void Update()
    {
        switch (moveState)
        {
            case MoveState.Walk:
                moveSpeed = 5;

                break;

            case MoveState.Run:
                moveSpeed = 10;

                break;

            case MoveState.Dash:
                moveSpeed = 20;

                break;
        }

        MoveObjectWithRotation();

    }

    private void FixedUpdate()
    {
        EnableDash();
        RaycastFront();

    }

    public void LerpingSpeed()
    {

    }

    public void RaycastFront()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 0))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            if (hit.collider.gameObject.CompareTag("Terrian"))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log("Did Hit");
                moving = false;
            }

        }
        else
        {
            moving = true;

            Debug.Log("Did not Hit");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrian"))
        {
            moving = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrian"))
        {
            moving = true;
        }
    }

    public void MoveObjectWithRotation()
    {

        Vector3 dir = new Vector3(1f * (Input.GetAxis("Horizontal")), 0f, 1f * (Input.GetAxis("Vertical")));
        Vector3 dirRelativeToCamera = Camera.main.transform.TransformDirection(dir);
        newDir = new Vector3 (dirRelativeToCamera.x,0f , dirRelativeToCamera.z);


        if (moving)
        {
            transform.position += newDir * Time.deltaTime * moveSpeed;
        }

        if (Input.GetAxis("Horizontal") != 0f || (Input.GetAxis("Vertical") != 0f))
        {
            transform.rotation = Quaternion.LookRotation(newDir);

            runTimer -= Time.deltaTime;
            if (runTimer <= 0)
            {
                moveState = MoveState.Run;
            }
        }
        else
        {
            runTimer = runCD;
            moveState = MoveState.Walk;
        }
    }

    public void EnableDash()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer< 0 && Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTimer = dashCD;
        }
    }

}
