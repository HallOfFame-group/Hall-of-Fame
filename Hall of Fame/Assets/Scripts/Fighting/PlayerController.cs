using UnityEngine;
using System.Collections;
using Spine.Unity;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Vector2 moveSpeed = new Vector2(10,0);
    [SerializeField] private int[] test;

    private BoxState[] boxStates;
    private Animator animator;
    private Rigidbody2D rigidbody2d;

	void Start ()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxStates = GetComponentsInChildren<BoxState>();
    }
	
	void Update () 
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        float horizontal = Input.GetAxis("360Controller_LS_XAxis");
        horizontal = ( horizontal == 0)? Input.GetAxis("Keyboard_Horizontal") : horizontal;
        animator.SetFloat("WalkSpeed", horizontal);

        if(stateInfo.IsName("Walk")||stateInfo.IsName("Back Walk"))
        {
            transform.position = rigidbody2d.position+horizontal*moveSpeed*Time.deltaTime;
        }

        if (Input.GetButtonDown("360Controller_Button_X") || Input.GetButtonDown("Keyboard_J"))
        {
            animator.SetTrigger("Punch");
        }

        if(Input.GetButtonDown("360Controller_Button_Y")||Input.GetButtonDown("Keyboard_K"))
        {
            animator.SetTrigger("Kick");
        }


	}

    private void FixedUpdate()
    {
        
    }

}
