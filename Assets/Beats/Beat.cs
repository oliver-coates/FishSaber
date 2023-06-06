using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AffordanceSystem.Receiver.Primitives;

public class Beat : MonoBehaviour
{
    [SerializeField]
    private float timer;
    [SerializeField]
    private float timeToArrive;

    [SerializeField]
    public float jumpTime;



    private Vector3 startPos;

    private Transform targetTransform;

    private float flyAwayTimer;

    private Vector3 jumpPoint;

    public bool jumping;

    public bool finishedJumping;

    private float distanceFactor;


    public GameObject fish1;

    public GameObject fish2;

    public GameObject fish3;
    private void Start()
    {
        startPos = transform.position;

        int random = Random.Range(0, 3);

        if (random == 0)
        {
            fish1.SetActive(true);
        }
        else if (random == 1)
        {
            fish2.SetActive(true);
        }
        else
        {
            fish3.SetActive(true);
        }
    }

    public void setTargetPosition(Transform target)
    {
        targetTransform = target;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;


        distanceFactor = timer / timeToArrive;

        if (finishedJumping == true)
        {
            FlyAway();
        }
        else if (jumping == true)
        {
            JumpingUpdate();
        }
        else
        {
            SwimmingUpdate();
        }

        
    }

    void SwimmingUpdate()
    {
        if (timer < (timeToArrive - jumpTime) )
        {
            Vector3 feetPlayerPos = new Vector3(targetTransform.position.x, 0, targetTransform.position.z);

            Debug.DrawLine(transform.position, feetPlayerPos);

            transform.position = Vector3.Lerp(startPos, feetPlayerPos, distanceFactor);

            // Align to sea:
            SitOnSea();
        }
        else
        {
            jumpPoint = transform.position;
            jumping = true;
        }
    }

    void JumpingUpdate()
    {

        float jumpDistanceFactor = (timer - jumpTime) / (timeToArrive- jumpTime);

        Debug.DrawLine(transform.position, targetTransform.position, Color.green);

        transform.position = Vector3.Lerp(jumpPoint, targetTransform.position, jumpDistanceFactor);
    
        if (jumpDistanceFactor >= 1)
        {
            finishedJumping = true;
        }
    }

    void FlyAway()
    {
        transform.position += transform.forward * 4f * Time.deltaTime * 1.1f;
        flyAwayTimer += Time.deltaTime;

        if (flyAwayTimer > 0.25f)
        {
            //GameObject.Find("Game Manager").GetComponent<GameManager>().Miss();
            Destroy(gameObject);
        }

    }

    void SitOnSea()
    {
        RaycastHit[] hit = null;
        Vector3 origin = transform.position + Vector3.up * 3f;
        origin += Vector3.back * 0.5f;

        hit = Physics.RaycastAll(origin, Vector3.down, 10f);
        // bad bad bad
        if (hit.Length != 0)
        {
            Debug.Log(hit[0].collider.name);
            Debug.DrawLine(origin, hit[0].point);
            transform.position = hit[0].point - Vector3.back * 0.5f;
        }
    }
    

}
