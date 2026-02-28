using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    //its probably unecessary to make this its own script
    //but the music is on the game manager so here we are

    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ClickNoise()
    {
        audioSource.Play();
    }
}
