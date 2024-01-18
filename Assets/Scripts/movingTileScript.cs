using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using UnityEditor.MPE;
using UnityEngine;
using UnityEngine.UIElements;

public class movingTileScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1.5f;
    [SerializeField] private float distance = 5;
    private float startPos;
    private float lastPos;
    private float currPosition;
    bool completed;

    private enum directions
    {
        Left,
        Right,
    }
    [SerializeField] private directions GoingTowards = directions.Right;

    private enum actualDirection 
    {
        Left,
        Right,
        CentreDx,
        CentreLx,
    };

    private actualDirection d;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        if (GoingTowards == directions.Left)
        {
            lastPos = startPos - distance;
        }
        else { lastPos = startPos + distance; }
        
        completed = false;
    }

    // Update is called once per frame
    void Update()
    {

        currPosition = transform.position.x;

        if (!completed) d = actualDirection.Right;
        if (completed) d = actualDirection.Left;
        if (GoingTowards == directions.Left)
        if (currPosition <= startPos) d = actualDirection.CentreLx;
        if (currPosition >= lastPos) d = actualDirection.CentreDx;



        switch (d)
        {
            case actualDirection.Left:
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
                break;

            case actualDirection.Right:
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                break;

            case actualDirection.CentreLx:
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
                completed = false;
                break;

            case actualDirection.CentreDx:
                transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
                completed = true;
                break;

        }
        //position = transform.position.x;
        //print(startPos);       
    }
}
