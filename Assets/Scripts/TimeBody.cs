using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeBody : MonoBehaviour
{

    public bool isRewinding = false;

    // If it's defined as a player make it reteleport to its initial position when started the rewinding 
    public bool isThePlayer = false;

    [SerializeField] public PointInTime InitialPositionStartRewind;
    
    public float recordTime = 5f;

    List<PointInTime> pointsInTime;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        pointsInTime = new List<PointInTime>();
        if(!TryGetComponent<Rigidbody>(out rb))
        {
            rb = null;
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            StartRewind();
        if (Input.GetKeyUp(KeyCode.Return) && isRewinding)
            StopRewind();
    }

    void FixedUpdate()
    {
        if (isRewinding)
            Rewind();
        else
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }

    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        isRewinding = true;
        if (rb != null)
            rb.isKinematic = true;
        if (InitialPositionStartRewind == null && isThePlayer)
            InitialPositionStartRewind = pointsInTime[0];
    }

    public void StopRewind()
    {
        isRewinding = false;
        if(rb != null)
            rb.isKinematic = false;
        // Make the player teleport to the inital position start rewind
        if (isThePlayer)
        {
            this.transform.SetPositionAndRotation(InitialPositionStartRewind.position, InitialPositionStartRewind.rotation);

            if (InitialPositionStartRewind != null)
                InitialPositionStartRewind = null;
        }
    }
}