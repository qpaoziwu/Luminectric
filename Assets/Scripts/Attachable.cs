using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]

public class Attachable : MonoBehaviour
{
    //Item Infos
    public AttachableScriptableData ObjectData; //Reading Object Data
    [System.Serializable]
    public enum ItemType
    {
        Source,
        Medium,
        Output,
        Shell,
        Protocol
    }
    public ItemType itemType;
    public string itemName;

    //Value for offsets
    public float xRadius;
    public float yRadius;
    public float zRadius;

    //Points of the attach points
    public Vector3[] contactPoints;
    [Range(0, 1)]
    public int[] attachValue; // Current Attach Status
    [SerializeField]
    [Range(0, 1)]
    int[] defaultValue; //Default Attach Status

    public bool root;



    //gameObject will set parent to this gameObject
    public Attachable ParentPoint;
    public GameObject Waypoint;

    //Local Point
    [Range(0, 5)]
    public int childPoint;

    //Target Point
    [Range(0, 5)]
    public int parentPoint;

    public bool confirm;

    void Start()
    {
        //Set Array
        contactPoints = new Vector3[6];
        attachValue = new int[6];
        defaultValue = new int[6];

        //Read data & Set default 
        ReadObjectData();
        StoreAttachValue();
        SetItemType();

        //if root = true, then the object has a base
        RootCheck();
    }

    private void Update()
    {
        //Set 6 Sides
        GetRadius();
        SetPoints();


        //Test Codes
        //Delete these test code when done**

        //ReadObjectData();
        //StoreAttachValue();
        //SetItemType();
        PrintNameWithValue();
        LockPositionToPoint(parentPoint, childPoint);
        MoveThisObject();
        Debug.DrawRay(transform.position, contactPoints[2]*3, Color.red);
        Debug.DrawRay(transform.position, contactPoints[1]*3, Color.green);

        //Delete these test code when done**

    }

    void MoveThisObject()
    {
        //if (confirm)
        {
            if (ParentPoint != null)
            {
                confirm = false;
                attachValue[0] = 1;
                attachValue[1] = 1;
                attachValue[2] = 1;
                attachValue[3] = 1;
                attachValue[4] = 1;
                attachValue[5] = 1;
                ParentPoint.attachValue[0] = 1;
                ParentPoint.attachValue[1] = 1;
                ParentPoint.attachValue[2] = 1;
                ParentPoint.attachValue[3] = 1;
                ParentPoint.attachValue[4] = 1;
                ParentPoint.attachValue[5] = 1;
            }
        }

    }
    //Debug Function
    void PrintNameWithValue()
    {
        Debug.Log(itemName + ": " 
            + attachValue[0] + attachValue[1] + attachValue[2]
            + attachValue[3] + attachValue[4] + attachValue[5]);
    }

    //Get object radius and set the Attachable Points
    void GetRadius()
    {
        xRadius = gameObject.transform.localScale.x / 2f;
        yRadius = gameObject.transform.localScale.y / 2f;
        zRadius = gameObject.transform.localScale.z / 2f;
    }
    void SetPoints()
    {
        contactPoints[0] = new Vector3(0, -yRadius, 0); //Bottom
        contactPoints[1] = new Vector3(0,  yRadius, 0); //Top

        contactPoints[2] = new Vector3(0, 0, zRadius);  //Front
        contactPoints[3] = new Vector3(0, 0, -zRadius); //Back

        contactPoints[4] = new Vector3(-xRadius, 0, 0);  //Left
        contactPoints[5] = new Vector3(xRadius, 0, 0); //Right
    }

    //Get Scriptable Object data and store values to local
    void ReadObjectData()
    {
        if (ObjectData != null)
        {
            Debug.Log("Storing Data");
            itemName = ObjectData.itemName;
            attachValue[0] = ObjectData.attachValue[0];
            attachValue[1] = ObjectData.attachValue[1];
            attachValue[2] = ObjectData.attachValue[2];
            attachValue[3] = ObjectData.attachValue[3];
            attachValue[4] = ObjectData.attachValue[4];
            attachValue[5] = ObjectData.attachValue[5];
        }

    }
    void StoreAttachValue()
    {
        if (ObjectData != null)
        {
            defaultValue[0] = attachValue[0];
            defaultValue[1] = attachValue[1];
            defaultValue[2] = attachValue[2];
            defaultValue[3] = attachValue[3];
            defaultValue[4] = attachValue[4];
            defaultValue[5] = attachValue[5];
        }
    }
    void SetItemType()
    {
        itemType = (ItemType)ObjectData.itemType;
    }
    void RootCheck()
    {
        if (defaultValue[0] == 0)
        {
            root = true;
        }
    }

    //Set Obj to parent and move it to the port
    void LockPositionToPoint(int parent, int child)
    {
        if (ParentPoint != null) {
            //Check if is valid
            if (ParentPoint.contactPoints[parent] != Vector3.zero)
            {
                //Check if both points are usable
                if (ParentPoint.attachValue[parent] == 1 && attachValue[child] == 1)
                {
                    //Set both points to unusable
                    ParentPoint.attachValue[parent] = 0;
                    attachValue[child] = 0;

                    if (gameObject.transform.parent != ParentPoint.gameObject.transform)
                    {
                        print("Setting " + gameObject.name+ " as child of "+ ParentPoint.gameObject.name);
                        //Set new parent
                        gameObject.transform.SetParent(ParentPoint.gameObject.transform);
                    }
                    //Get offset distance
                    Vector3 contactDistance = new Vector3(Mathf.Abs(contactPoints[child].x), Mathf.Abs(contactPoints[child].y), Mathf.Abs(contactPoints[child].z));

                    //Get offset direction
                    Vector3 contactDirection = SetVectorToNorm(ParentPoint.contactPoints[parent]);

                    Vector3 localDirection = SetVectorToNorm(contactPoints[child]);
                    //Get offset vector3
                    // Vector3 offsetPoint = Vector3.Scale(contactDistance, contactDirection);
                    Vector3 offsetPoint = new Vector3(contactDistance.x * contactDirection.x, contactDistance.y * contactDirection.y, contactDistance.z * contactDirection.z);



                    //Set position to new point
                    //gameObject.transform.position = ParentPoint.contactPoints[parent] + offsetPoint;//
                    gameObject.transform.position = contactDirection;//

                    Debug.Log(ParentPoint.contactPoints[parent] + offsetPoint);

                    //Set rotation relative to both points
                    //gameObject.transform.rotation = Quaternion.LookRotation(SetVectorToNorm(contactPoints[child] - contactDirection), contactPoints[child]);
                    //gameObject.transform.rotation = Quaternion.LookRotation(-localDirection , ParentPoint.contactPoints[parent]);
                    gameObject.transform.rotation = Quaternion.LookRotation(localDirection, -contactDirection);

                }
            }
        }

    }

    //This function set incVector to -1 0 1
    Vector3 SetVectorToNorm(Vector3 incVector)
    {
        Vector3 outVector3 = Vector3.zero;

        if (incVector.x > 0)
        {
            outVector3.x = 1;
        }
        if (incVector.x < 0)
        {
            outVector3.x = -1;
        }
        if (incVector.x == 0)
        {
            outVector3.x = 0;
        }

        if (incVector.y > 0)
        {
            outVector3.y = 1;
        }
        if (incVector.y < 0)
        {
            outVector3.y = -1;
        }
        if (incVector.y == 0)
        {
            outVector3.y = 0;
        }
        if (incVector.z > 0)
        {
            outVector3.z = 1;
        }
        if (incVector.z < 0)
        {
            outVector3.z = -1;
        }
        if (incVector.z == 0)
        {
            outVector3.z = 0;
        }

        return outVector3;
    }

    void PairAttachPoint()
    {
        // if this pairing point is 
    }

}
