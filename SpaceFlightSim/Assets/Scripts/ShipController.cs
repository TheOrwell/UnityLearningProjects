using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 25f, strafeSpeed = 7.5f, hoverSpeed = 5f;
    private float activeForwardSpeed, activeStrafeSpeed, activeHoverSpeed;

    // used as parameters in Mathf.Lerp so that it takes as long as the float says to get up to speed
    public float forwardAccel = 2.5f, strafeAccel = 2f, hoverAccel = 2f;

    public float lookRotateSpeed = 90f;
    private Vector2 lookInput, screenCenter, mouseDist;

    private float rollInput;
    public float rollSpeed = 90f;
    public float rollAccel = 2.5f;

    
    // Start is called before the first frame update
    void Start()
    {
        // Stores the center of your screen, dynamically
        
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.width * .5f;

    }

    // Update is called once per frame
    void Update()
    {
        // Gets the location of the mouse
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDist.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDist.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDist = Vector2.ClampMagnitude(mouseDist, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Roll"), rollAccel * Time.deltaTime);

        transform.Rotate(-mouseDist.y * lookRotateSpeed * Time.deltaTime, mouseDist.x * lookRotateSpeed * Time.deltaTime, rollInput * rollSpeed * Time.deltaTime, Space.Self);


        activeForwardSpeed = Mathf.Lerp(activeForwardSpeed, Input.GetAxisRaw("Vertical") * forwardSpeed, forwardAccel * Time.deltaTime);
        activeStrafeSpeed = Mathf.Lerp(activeStrafeSpeed, Input.GetAxisRaw("Horizontal") * strafeSpeed, strafeAccel * Time.deltaTime);
        activeHoverSpeed =  Mathf.Lerp(activeHoverSpeed, Input.GetAxisRaw("Hover") * hoverSpeed, hoverAccel * Time.deltaTime);

        transform.position += transform.forward * activeForwardSpeed * Time.deltaTime;
        transform.position += (transform.right * activeStrafeSpeed * Time.deltaTime) + (transform.up * activeHoverSpeed * Time.deltaTime);
    }
}
