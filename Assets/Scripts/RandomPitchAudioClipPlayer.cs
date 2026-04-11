using UnityEngine;

public class RandomPitchAudioClipPlayer : MonoBehaviour
{
    [SerializeField] private Vector2 randomPitchRange, randomVolumeRange; 
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    public void PlayClip(AudioClip clip)
    {
        audioSource.pitch = Random.Range(randomPitchRange.x, randomPitchRange.y); 
        audioSource.volume = Random.Range(randomVolumeRange.x, randomVolumeRange.y); 
        audioSource.PlayOneShot(clip); 
    }
}
