using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Vector3 InitialPoisition;
    internal bool isOpened = false;
    private float lerpSpeed = 7f;
    // if true then open it when pressed if not do the opposite
    public bool toOpen = true; 
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = InitialPoisition;
    }
    public void open()
    {
        isOpened = toOpen;
    }
    public void close()
    {
        isOpened= !toOpen;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(isOpened)
        {
            
            Vector3 GoToPosition = new Vector3(InitialPoisition.x, InitialPoisition.y - 4f, InitialPoisition.z) ;
            Vector3 newPosition = Vector3.Lerp(transform.position, GoToPosition, Time.deltaTime * lerpSpeed);
            transform.position = newPosition;
        }
        else
        {
            Vector3 newPosition = Vector3.Lerp(this.transform.position, InitialPoisition, Time.deltaTime * lerpSpeed);
            transform.position = newPosition;
        }
    }
}
