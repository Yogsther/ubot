using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class SoundFromObjectInWorld : MonoBehaviour
{
    /*
    private EventInstance ObjectInWorld;
    private bool isActive;
  
    private void Start()
    {
        isActive = true;
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(ObjectInWorld, transform, GetComponent<Rigidbody>());
        ObjectInWorld = AudioManager.instance.CreateInstance(FMODEvents.instance.ObjectInWorld);
    }


    void Update()
    {
        UpdateSound();
        ObjectInWorld.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.transform.position));
        
    }
    public void StopSound()
    {
       ObjectInWorld.stop(STOP_MODE.ALLOWFADEOUT);
       

    }
    private void UpdateSound()
    {
        if (isActive)
        {
            PLAYBACK_STATE playbackState;
            ObjectInWorld.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {

                ObjectInWorld.start();
            }
        }
        else
        {
            ObjectInWorld.stop(STOP_MODE.ALLOWFADEOUT);
        }

      

    }*/
}
