using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpherePoints : MonoBehaviour
{
    [Range(1, 32)]
    public int heightSubdivision;

    [Range(3, 32)]
    public int widthSubdivision;

    public float circleRadius;
    public float circleHeight;

    public bool lockHeight;
    public bool halfSphere;
    public bool reset;
    public GameObject Point;

    public Vector3[] circularPoints;
    public Vector3[] verticalPoints;
    public Transform[] sphericalPoints;
    int x = 0;
    int y = 0;

    private void Start()
    {
        //CreateCircularPoints(widthSubdivision);

    }
    // Update is called once per frame
    void Update()
    {

        LockOptions();
        CreateCircularPoints(widthSubdivision);
        //print(circularPoints[0]);
    }

    //void CreateSphericalPoints()
    //{
    //    if (heightSubdivision > 1)
    //    {
    //        for (int i = 0; i < heightSubdivision; i++)
    //        {
    //            spherePoints[i] = new Vector3(circularPoints[]);
    //        }

    //    }

    //}

    //void CreateVerticalPoints()
    //{
    //    verticalPoints = new Vector3[heightSubdivision];

    //    for (int i = 0; i < heightSubdivision; i++)
    //    {
    //        verticalPoints[i] = new Vector3(0, (circleHeight / heightSubdivision * i) / circleHeight, 0);
    //    }
    //}

    void LockOptions()
    {
        if (lockHeight)
        {
            circleHeight = circleRadius;
        }

        if (circleHeight <= 0)
        {
            circleHeight = 0.01f;
        }
        if (reset)
        {
            x = 0;
            y = 0;
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);

            }

            reset = false;

        }
    }

    void CreateCircularPoints(int widthSub)
    {

        verticalPoints = new Vector3[heightSubdivision];

        circularPoints = new Vector3[widthSubdivision * heightSubdivision];

        float stepAngle = 2 * Mathf.PI / widthSubdivision;

        float vertAngle = 2 * Mathf.PI / heightSubdivision;

        //float height = circleRadius / heightSub;

        //circularPoints[0] = Vector3.zero;


        if (y <= (widthSubdivision * heightSubdivision)-1)
        {
            for (int v = 0; v < heightSubdivision; v++)
            {
                x = v + 1;
                if (halfSphere)
                {
                    verticalPoints[v] = new Vector3(0, (circleHeight / circleRadius) / heightSubdivision * v, 0);
                }
                if (!halfSphere)
                {
                    //Circle
                    verticalPoints[v] = new Vector3(0, Mathf.Tan(vertAngle * v), 0);
                }

                for (int i = 0; i < widthSubdivision; i++)
                {



                    Vector3 NormalizedPoint;

                    float currentAngle = i * stepAngle;
                    NormalizedPoint = new Vector3(Mathf.Cos(currentAngle), verticalPoints[v].y, Mathf.Sin(currentAngle)).normalized;
                    Vector3 PointWithOffset = (circleRadius * NormalizedPoint + transform.position);
                    circularPoints[y] = PointWithOffset;

                    GameObject Waypoint = Instantiate(Point, circularPoints[y], Quaternion.identity, gameObject.transform) as GameObject;
                    Waypoint.name = "sce_Camera " + (v + 1) + ", " + (i + 1);

                    y += 1;
                    //Draw Line from origin
                    //Debug.DrawLine(transform.position, circularPoints[i], Color.green);
                    //Draw Lines to every vertices
                }
            }


        }
        for (int d = circularPoints.Length; d > 0; d--)
        {
            if (circularPoints.Length != d)
            {
                Debug.DrawLine(circularPoints[d - 1], circularPoints[d], Color.blue);
                Debug.DrawLine(circularPoints[circularPoints.Length - 1], circularPoints[0], Color.yellow);
            }if (d == 0)
            {
                d = circularPoints.Length;
            }

        }
    }



}
            //Draw Lines in Circle
            //for (int d = circularPoints.Length; d > 1; d--)
            //{
            //    Debug.DrawLine(circularPoints[d], circularPoints[d-1], Color.yellow);
            //    Debug.DrawLine(circularPoints[circularPoints.Length], circularPoints[1], Color.yellow);
            //}

    
    
    //void DrawLines(Vector3 Startpoint,Vector3 Endpoint)
    //{
        
    //    vertices[i]

    //    Debug.DrawLine(, if (vertices[i + 1] == null) { vertices[i + 1] });
    //}