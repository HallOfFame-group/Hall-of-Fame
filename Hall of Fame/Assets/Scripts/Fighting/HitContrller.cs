using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitContrller : MonoBehaviour {

    private int oldStateHash = 0;
    private BoxState[] boxStates;


	void Start () {
        boxStates = GetComponentsInChildren<BoxState>();
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag == "Player"&&other.tag == "Enemy")
        {
            AnimatorStateInfo stateInfo = gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0);

            if (oldStateHash != stateInfo.fullPathHash)
            {
                other.gameObject.GetComponentInParent<Animator>().SetTrigger("Hit");
            }


            oldStateHash = stateInfo.fullPathHash;

        }
    }


}
