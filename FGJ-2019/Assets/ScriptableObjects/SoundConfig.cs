using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum SoundType
{
    None,
    PlayerDie,
    DogBark,
    SwitchOn,
    SwitchOff,
    LightOn,
    LightOff,
    WindowReached,
    NextLevel
}

[CreateAssetMenu(fileName = "NewSoundConfig", menuName = "New SoundConfig")]
public class SoundConfig : ScriptableObject
{

    [SerializeField]
    private string objectName = "New SoundConfig";
    public string Name { get { return objectName; } }


    [SerializeField]
    private List<GameSound> sounds;

    public List<GameSound> Sounds { get { return sounds; } }

}


[System.Serializable]
public class GameSound : System.Object
{

    public SoundType soundType;
    public AudioClip sound;

    public List<AudioClip> sounds;
}