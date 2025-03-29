using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("PlayerSFX")]

    [field: SerializeField] public EventReference Walk { get; private set; }




    [field: Header("StationSFX")]

    [field: SerializeField] public EventReference LoadTorpedo { get; private set; }

    [field: SerializeField] public EventReference TorpedoFired { get; private set; }

    [field: SerializeField] public EventReference InitiateNuke { get; private set; }

    [field: SerializeField] public EventReference RadarBeep { get; private set; }

    [field: SerializeField] public EventReference BallastWheel { get; private set; }

    [field: SerializeField] public EventReference FaxShort { get; private set; }

    [field: SerializeField] public EventReference FaxMedium { get; private set; }

    [field: SerializeField] public EventReference FaxLong { get; private set; }

    [field: SerializeField] public EventReference RequestFax { get; private set; }


    [field: Header("ObjectSFX")]

    


    [field: Header("UISFX")]

    [field: SerializeField] public EventReference NeutralClick { get; private set; }

    [field: SerializeField] public EventReference PositiveClick { get; private set; }

    [field: SerializeField] public EventReference NegativeClick { get; private set; }


    [field: Header("Ambience")]
     
    [field: SerializeField] public EventReference submarineAmbience { get; private set; }

    




    [field: Header("Music")]
   

  

    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance");
        }
        instance = this;
    }
}
