using UnityEngine;

public class ZombieSound : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip zombieGroan;
    public AudioClip headBangSound;
    public AudioClip zombieDeathSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    // Fungsi-fungsi ini dipanggil lewat Animation Event
    public void PlayGroan()
    {
        audioSource.PlayOneShot(zombieGroan, 0.1f);
    }

    public void PlayHeadBang()
    {
        audioSource.PlayOneShot(headBangSound, 0.1f);
    }

    public void PlayDeath()
    {
        audioSource.PlayOneShot(zombieDeathSound, 0.1f);
    }
}