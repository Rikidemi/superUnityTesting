using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class fallingTile : MonoBehaviour
{
    private Rigidbody rb;
    private float deadZone = -35;
    [SerializeField] private float fallingTime = 1;

    //shake vars
    private bool shaking = false;
    [SerializeField] private float shakeAmt = 40;
    private Vector3 originalPos;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shaking)
        {
            Vector3 newPos = originalPos + Random.insideUnitSphere * (Time.deltaTime * shakeAmt);
            newPos.y = originalPos.y;
            newPos.z = originalPos.z;
            transform.position = newPos;
        }
        if (transform.position.y < deadZone)
        {
            Debug.Log("FallingTile Deleted");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            ShakeMe();
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(fallingTime);
        rb.useGravity = true;
    }
    public void ShakeMe()
    {
        Debug.Log("shaking");
        StartCoroutine("ShakeNow");
    }

    IEnumerator ShakeNow()
    {
        if (shaking == false)
        {
            shaking = true;
        }

        yield return new WaitForSeconds(0.5f);
        shaking = false;
        transform.position = originalPos;
    }

    
}
