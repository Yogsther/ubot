using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private EventInstance submarineAmbienceEventInstance;
       
    

    //private FMOD.Studio.Bus masterBus;

	private void Start()
    {
        InititializeSubmarineAmbience(FMODEvents.instance.submarineAmbience);
    }
    private void Update()
    {

    }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		// Get reference to the FMOD Master Bus
		//masterBus = RuntimeManager.GetBus("bus:/");
	}

	public void StopSubmarineAmbience()
    {
        submarineAmbienceEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    /*public void StopWalk()
    {
        WalkingDirt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        RunningDirt.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }*/

    /*public void StopWendigoSound()
    {
        WendigoBreath.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        
    }*/

    private void InititializeSubmarineAmbience(EventReference submarineAmbienceEventReference)
    {
        submarineAmbienceEventInstance = CreateInstance(submarineAmbienceEventReference);
        submarineAmbienceEventInstance.start();
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);

        return eventInstance;
    }

	/*public void SetMasterVolume(float volume)
	{
		// Convert slider value (0-1) to decibels (-80 to 0)
		float dB = Mathf.Lerp(-80f, 0f, volume);
		masterBus.setVolume(Mathf.Pow(10f, dB / 20f)); // FMOD uses linear values internally
	}*/
}
