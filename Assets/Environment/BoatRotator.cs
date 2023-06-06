using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatRotator : MonoBehaviour
{

    private Vector3 normalTarget;
    public float normalSmoothing;

    private Vector3 origin = new Vector3(0, 5f, 0);

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(origin, Vector3.down, out hit))
        {
            Debug.DrawLine(hit.point, hit.normal + Vector3.up, Color.red);
            normalTarget = hit.normal;
            transform.position = hit.point;
        }

        Vector3 normalChange = transform.up - normalTarget;

       // normalChange = new Vector3(
        //    Mathf.Clamp(normalChange.x, -normalSmoothing, normalSmoothing),
         //   Mathf.Clamp(normalChange.y, -normalSmoothing, normalSmoothing),
          //  Mathf.Clamp(normalChange.z, -normalSmoothing, normalSmoothing));

        transform.up = Vector3.Lerp(transform.up, normalTarget, 0.02f);

    }
}
