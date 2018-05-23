using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : Interactable {
    private bool inTransition;
    private bool fadeOut;
    public float duration;
    private float transition;
    public Image image;
    private bool thenReverse;



    public override void Interact()
    {
        Fade(true, duration, true);
    }

    public void Fade(bool _fadeOut, float _duration, bool _thenReverse)
    {
        fadeOut = _fadeOut;
        thenReverse = _thenReverse;
        inTransition = true;
        duration = _duration;
        transition = fadeOut ? 0 : 1;
    }

    protected override void Update()
	{
        base.Update();

        if (!inTransition) return;

        transition += fadeOut ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        image.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0) 
        {
            inTransition = false;
            if (thenReverse)
            {
                TimeManager.Instance.NextDay();
                TimeManager.Instance.clock = TimeManager.Instance.wakeUp;
                Quaternion rotation = Quaternion.LookRotation(new Vector3(0,0,1));
                PlayerControler.Instance.transform.rotation = rotation;
                Fade(!fadeOut, duration, false);
            }
        }


	}
}

/*

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class House : Interactable {
    private bool inTransition;
    private bool fadeOut;
    public float duration;
    private float transition;
    //public Image image;
    public bool thenReverse;



    public override void Interact()
    {
        Debug.Log("hellooo");
        TimeManager.Instance.NextDay();
        Fade(true, duration, true);
    }

    public void Fade(bool _fadeOut, float _duration, bool _thenReverse)
    {
        fadeOut = _fadeOut;
        thenReverse = _thenReverse;
        inTransition = true;
        duration = _duration;
        transition = fadeOut ? 0 : 1;
    }

    private void Update()
    {
        if (!inTransition) return;

        transition += fadeOut ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        //image.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);

        if (transition > 1 || transition < 0) inTransition = false;

        if (thenReverse) Fade(!fadeOut, duration, false);
    }
}

*/
