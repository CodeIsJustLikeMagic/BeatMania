using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraAnalogStick : MonoBehaviour
{
    public float speedMult = 1.0f;
    public GameObject RightAnalogStick;
    private PlayerMovement playerMovement;
    private float newValueX = 0, newValueY = 0, inputX = 0, inputY = 0, lastInputX = 0, lastInputY = 0;
    public float thresholdValue = 3.0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    public void OnViewHorizontal(InputAction.CallbackContext value)
    {
        inputX = value.ReadValue<float>();
    }

    public void OnViewVertical(InputAction.CallbackContext value)
    {
        inputY = value.ReadValue<float>();
    }
    void Update()
    {

        if(playerMovement.getHorizontalMove() > 0.3 || playerMovement.getHorizontalMove() < -0.3)
        {
            return;
        }
        //inputX = Input.GetAxisRaw("HorizontalRightStick");
        //inputY = Input.GetAxisRaw("VerticalRightStick");

        newValueX = this.transform.position.x + (inputX - lastInputX) * speedMult;
        newValueY = this.transform.position.y + (inputY - lastInputY) * speedMult;
        
        if (newValueX - this.transform.position.x < thresholdValue * -1) newValueX = this.transform.position.x + thresholdValue * -1;
        else if (newValueX - this.transform.position.x > thresholdValue) newValueX = -this.transform.position.x + thresholdValue;

        if (newValueY - this.transform.position.y < thresholdValue * -1) newValueY = this.transform.position.y + thresholdValue * -1;
        else if (newValueY - this.transform.position.y > thresholdValue) newValueY = this.transform.position.y + thresholdValue;
        
        RightAnalogStick.transform.position = new Vector3(newValueX, newValueY, 0);
    }
}
