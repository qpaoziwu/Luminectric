using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class MoveToWithLookAt : MonoBehaviour
{
    public Transform lookAt;
    public Transform standingPoint;


    public Vector3 dir;
    public Vector3 distanceDir;
    [Range(1, 50)]
    public float speed;
    [Range (0, 1000)]
    public float cameraChaseSpeed;
    [Range(1, 5)]
    public float positionThreshold;

    public bool flipLookDir;
    public bool moving;
    public bool looking;

    void Update()
    {
        if (standingPoint != null)
        {
            Move(standingPoint);
        }

    }

    private void Move(Transform destination)
    { 
        dir = Vector3.Normalize(destination.position - transform.position);
        Vector3 fromDir = new Vector3(this.lookAt.position.x - transform.position.x, this.lookAt.position.y - transform.position.y, this.lookAt.position.z - transform.position.z).normalized;
        Vector3 lookingFrom = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 lerpDir = Vector3.Lerp(transform.position, dir, cameraChaseSpeed);
        distanceDir = new Vector3(lerpDir.x, lerpDir.y, lerpDir.z);
        if (Vector3.Distance(transform.position, destination.position) >= positionThreshold)
        {
            if (moving)
            {
                transform.position += distanceDir * Time.deltaTime * speed;
            }

            if (looking)
            {
                if (flipLookDir)
                {
                    transform.rotation = Quaternion.LookRotation(-Vector3.Lerp(lookingFrom, fromDir, cameraChaseSpeed));
                }
                else { transform.rotation = Quaternion.LookRotation(Vector3.Lerp(lookingFrom, fromDir, cameraChaseSpeed)); }

            }
        }
    }

}
