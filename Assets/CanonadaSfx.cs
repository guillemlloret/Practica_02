using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource soundSource; 

    private void Start()
    {
        
        if (soundSource == null)
        {
            soundSource = GetComponent<AudioSource>();
        }

       
        if (soundSource == null)
        {
            Debug.LogError("No s'ha trobat cap AudioSource assignat o al component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
                Debug.Log("Jugador dins del trigger, so activat.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            soundSource.Stop();
            Debug.Log("Jugador fora del trigger, so desactivat.");
        }
    }
}
