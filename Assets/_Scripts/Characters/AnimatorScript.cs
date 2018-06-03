using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorScript : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () 
    {
        animator = GetComponent<Animator>();
	}
	

    public void PlayAnimation(GameData.Animation animation)
    {
        animator.Play(animation.ToString());
    }
}
