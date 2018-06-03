using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade2 : ISingleton<Fade2>
{
    protected Fade2()
    {
    }

    private Image image;
    private bool black = true;
    public float fadeDuration;
    private bool isFading;
    private bool fadeOut;
    private float duration;
    private float transition;
    private float start;

    public delegate void VoidNoParam();
    public VoidNoParam onFadeEndEvent;

    // Use this for initialization
    void Start()
    {
        image = GetComponent<Image>();

        image.color = Color.black;

        FadeOut(false, fadeDuration);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFading) return;

        transition = (Time.time - start) / duration;
        if (!fadeOut) transition = 1 - transition;

        image.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);


        if (transition > 1 || transition < 0)
        {
            isFading = false;
            black = !black;

            if (onFadeEndEvent != null)
            {
                onFadeEndEvent.Invoke();
            }

        }
    }

    public void FadeOut(bool _fadeOut, float _duration)
    {
        if (isFading == false)
        {
            isFading = true;
            fadeOut = _fadeOut;
            duration = _duration;
            start = Time.time;
        }

    }
}
