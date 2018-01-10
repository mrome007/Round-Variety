using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayerToMousePosition : MonoBehaviour 
{
    [SerializeField]
    private float idleTime = 10f;
    private float idleTimer;

    private void Start()
    {
        idleTimer = 0;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            //need to specify how much infront of the camera or it'll spit out camera position.
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z);

            var target = Camera.main.ScreenToWorldPoint(mousePos);
            transform.LookAt(target);

            idleTimer = idleTime;
        }

        if(idleTimer > 0f)
        {
            idleTimer -= Time.deltaTime;  
        }

        if(idleTimer <= 0f)
        {
            transform.Rotate(Vector3.right * 100f * Time.deltaTime);
        }
    }
}
