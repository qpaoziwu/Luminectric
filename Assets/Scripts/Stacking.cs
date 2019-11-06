using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stacking : MonoBehaviour
{
    public bool top = false;
    public bool stacked = false;

    public bool carrot = false;
    public bool hat = false;

    Rigidbody rb;

    public GameObject StackingPartner;

    public float TopSize;
    public float BotSize;

    public bool OnsafeZone;
    Transform LockPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        StackBalls();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!stacked && other.gameObject.tag == "Snowball" && other.gameObject.GetComponent<Stacking>().top == false && (OnsafeZone || other.gameObject.GetComponent<Stacking>().OnsafeZone))
        {
            StackingPartner = other.gameObject;
            if (rb.velocity.magnitude > StackingPartner.gameObject.GetComponent<Rigidbody>().velocity.magnitude)
            {
                top = true;
            }

            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;

            if (!top)
            {
                rb.detectCollisions = false;
            }

            stacked = true;

            UpdateSizes();
            StackBalls();
            //UpdateScore();
        }

        if (!stacked && other.gameObject.tag == "Safe_Zone")
        {
            OnsafeZone = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!stacked && other.gameObject.tag == "Safe_Zone")
        {
            OnsafeZone = false;
        }
    }

    void StackBalls()
    {
        if (top)
        {
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(StackingPartner.transform.position.x, (gameObject.transform.localScale.y / 2f + StackingPartner.transform.localScale.y / 2f), StackingPartner.transform.position.z);
            //
            //Stack SFX
            //
            //FindObjectOfType<Audio_Manager>().Reproduce("SnowMan Built");
        }

    }

    void UpdateSizes()
    {
        if (top)
        {
            //TopSize = gameObject.GetComponent<Snowball_IncreaseWithSize>().size;
            //BotSize = StackingPartner.GetComponent<Snowball_IncreaseWithSize>().size;
        }
    }

    //public void UpdateScore()
    //{
    //    RaycastHit Hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out Hit, 10f, LayerMask.GetMask("Score")))
    //    {
    //        Hit.collider.gameObject.GetComponent<PointCalculator>().CalculateScore(TopSize, BotSize);
    //        Debug.Log("TestPass1");

    //    }
    //}
}
