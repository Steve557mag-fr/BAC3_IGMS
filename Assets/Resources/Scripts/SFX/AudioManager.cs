using COL1.Utilities;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    private void Awake()
    {
        Singleton.Make(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnVolumeChanged(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
}
/*
Slider qui gère le volume du master DONE


Script qui sait si tu vas ou reviens du magasin

 
 
 
 */