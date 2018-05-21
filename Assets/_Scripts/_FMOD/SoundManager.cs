using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// description
/// </summary>
//[RequireComponent(typeof(CircleCollider2D))]
public class SoundManager : ISingleton<SoundManager>                                   //commentaire
{
    #region public variable
    /// <summary>
    /// variable public
    /// </summary>

    public Dictionary<string, FmodEventEmitter> soundsEmitter = new Dictionary<string, FmodEventEmitter>();

    //public FmodEventEmitter musicEmitterScript;

    public int MusicState
    {
        get
        {
            return (musicState);
        }
        set
        {
            if (musicState != value)
            {
                musicState = value;
                StateMusicChanged();
            }
        }
    }

    private int musicState = 0;
    //[FMODUnity.EventRef]

    protected SoundManager()
    {
    }
   

    #endregion



    #region core script

    /// <summary>
    /// appelé lorsque la state de la musique a changé
    /// </summary>
    private void StateMusicChanged()
    {
        PlaySound(GetEmitter("BackgroundMusic"), "Checkpoint", musicState);
    }

    /// <summary>
    /// ajoute une key dans la liste
    /// </summary>
    public void AddKey(string key, FmodEventEmitter value)
    {
        foreach (KeyValuePair<string, FmodEventEmitter> sound in soundsEmitter)
        {
            if (key == sound.Key)
            {
                soundsEmitter[sound.Key] = value;
                return;
            }
        }
        soundsEmitter.Add(key, value);
    }

    /// <summary>
    /// ajoute une key dans la liste
    /// </summary>
    public void DeleteKey(string key, FmodEventEmitter value)
    {
        foreach (KeyValuePair<string, FmodEventEmitter> sound in soundsEmitter)
        {
            if (key == sound.Key)
            {
                soundsEmitter.Remove(key);
                return;
            }
        }
        Debug.Log("key sound not found");
    }

    private FmodEventEmitter GetEmitter(string soundTag)
    {
        foreach (KeyValuePair<string, FmodEventEmitter> sound in soundsEmitter)
        {
            if (soundTag == sound.Key)
            {
                return (sound.Value);
            }
        }
        return (null);
    }

    /// <summary>
    /// joue un son de menu (sans emmiter)
    /// </summary>
    public void PlaySound(string soundTag, bool stop = false)
    {
        if (soundTag == null || soundTag == "")
            return;

        if (!soundTag.Contains("event:/"))
            soundTag = "event:/" + soundTag; //attention à la hierarchie
        PlaySound(GetEmitter(soundTag), stop);
        //FMODUnity.RuntimeManager.PlayOneShot("2D sound");   //methode 1 
    }

    /// <summary>
    /// ici play l'emitter (ou le stop)
    /// </summary>
    /// <param name="emitterScript"></param>
    public void PlaySound(FmodEventEmitter emitterScript, bool stop = false)
    {

        if (!emitterScript)
        {
                Debug.LogWarning("Emmiter SOund not found !!");
            return;
        }

        if (!stop)
            emitterScript.play();
        else
            emitterScript.stop();
    }

    /// <summary>
    /// ici change le paramettre de l'emitter
    /// </summary>
    /// <param name="emitterScript"></param>
    public void PlaySound(FmodEventEmitter emitterScript, string paramName, float value)
    {;
        emitterScript.SetParameterValue(paramName, value);
    }

    #endregion
}
