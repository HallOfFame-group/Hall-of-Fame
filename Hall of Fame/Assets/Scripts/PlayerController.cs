using UnityEngine;
using System.Collections;
using Spine.Unity;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private float speed = 12;
    public Animator ChAnimator;
    private bool m_isAxisYInUse = false;
    private bool m_isAxisXInUse = false;
    private Rigidbody rig;
    private bool jumping = false;
  
	// Use this for initialization
	void Start ()
    {
        rig = GetComponent<Rigidbody>();
        ChAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {

        float dPadX = Input.GetAxis("X360_dPadX");

        //float dPadY = Input.GetAxis("X360_dPadY");

        float triggerAxis = Input.GetAxis("X360_Triggers");

        //Move X axis 
        if (Input.GetAxis("X360_dPadX") > 0)
        {
            ChAnimator.SetBool("X Axis Positive", true);
            ChAnimator.SetBool("X Axis Negative", false);
        }
        if (Input.GetAxisRaw("X360_dPadX") < 0)
        {
            ChAnimator.SetBool("X Axis Negative", true);
            ChAnimator.SetBool("X Axis Positive", false);
        }
        if (Input.GetAxisRaw("X360_dPadX") == 0)
        {
            ChAnimator.SetBool("X Axis Negative", false);
            ChAnimator.SetBool("X Axis Positive", false);
        }
        //Move Y axis 
        if (Input.GetAxisRaw("X360_dPadY") > 0)
        {
           
        }

        if (Input.GetAxisRaw("X360_dPadY") < 0) 
        {
            ChAnimator.SetBool("Y Axis up", true);
            jumping = true;
            
        }
        if (jumping == true)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 15 * Time.deltaTime, 0);
        }

        if (triggerAxis != 0)
        {
            print("Trigger Value: " + triggerAxis);
        }
        if (Input.GetButtonDown("X360_LBumper"))
        {
            print("Left Bumper");
        }
        if (Input.GetButtonDown("X360_RBumper"))
        {
            print("Right Bumper");
        }
        //A Button
        if (Input.GetButton("X360_A") && !Input.GetButtonDown("X360_X"))
        {
            print("A Button");
            ChAnimator.SetBool("A Button Pressed", true);
        }
        if (Input.GetButtonUp("X360_A"))
        {
            ChAnimator.SetBool("A Button Pressed", false);
        }
        //B Button
        if (Input.GetButtonDown("X360_B"))
        {
            print("B Button");
        }
        //X Button
        if (Input.GetButton("X360_X") && !Input.GetButtonDown("X360_A"))
        {
            print("X Button");
            ChAnimator.SetBool("X Button Pressed", true);
        }
        if (Input.GetButtonUp("X360_X"))
        {
            ChAnimator.SetBool("X Button Pressed", false);
            ChAnimator.SetBool("A+X", false);
        }
        if (Input.GetButtonDown("X360_X") && Input.GetButtonDown("X360_A"))
        {
            ChAnimator.SetBool("A+X", true);
        }
        //Y Button
        if (Input.GetButtonDown("X360_Y"))
        {
            print("Y Button");
        }
        if (Input.GetButtonDown("X360_Back"))
        {
            print("Back Button");
        }
        if (Input.GetButtonDown("X360_Start"))
        {
            print("Start Button");
        }
        if (Input.GetButtonDown("X360_LStickClick"))
        {
            print("Clicked Left Stick");
        }
        if (Input.GetButtonDown("X360_RStickClick"))
        {
            print("Clicked Right Stick");
        }

        float hAxis = Input.GetAxis("Horizontal");
        //float vAxis = Input.GetAxis("Vertical");

        float rStickX = Input.GetAxis("X360_RStickX");

        Vector3 movement = transform.TransformDirection(new Vector3(hAxis, 0, 0) * speed * Time.deltaTime);

        rig.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, rStickX, 0) * turnSpeed * Time.deltaTime);

        //transform.Rotate(new Vector3(0, rStickX, 0), turnSpeed * Time.deltaTime);
	}
}
