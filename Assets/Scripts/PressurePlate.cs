using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PressurePlate : MonoBehaviour
{
    public List<Door> LinkedDoors;
    public bool isPressed;
    private Vector3 InitialPosition;
    private float lerpSpeed = 7f;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pickable"))
        {
            isPressed = true;
            foreach (Door door in LinkedDoors)
            {
                door.open();
            }   
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Pickable"))
        {
            isPressed = false;
            foreach (Door door in LinkedDoors)
            {
                door.close();
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (isPressed)
        {
            AnimatePressed();
        }
        else
        {
            AnimateUnPressed();
        }
    }
    public void AnimatePressed()
    {
        
        Vector3 GoToPosition = new Vector3(transform.position.x, InitialPosition.y - 0.1f, transform.position.z);
        Vector3 newPosition = Vector3.Lerp(transform.position, GoToPosition, Time.deltaTime * lerpSpeed);
        
        transform.position = newPosition;
    }
    public void AnimateUnPressed()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, InitialPosition, Time.deltaTime * lerpSpeed);
        transform.position = newPosition;
    }
}
