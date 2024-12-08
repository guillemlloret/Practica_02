using UnityEngine;

public class PlaySoundOnTrigger : MonoBehaviour
{
    public AudioSource soundSource; // Referència al component AudioSource.

    private void Start()
    {
        // Assignar l'AudioSource automàticament si no està assignat al Inspector.
        if (soundSource == null)
        {
            soundSource = GetComponent<AudioSource>();
        }

        // Comprovar si s'ha trobat un AudioSource.
        if (soundSource == null)
        {
            Debug.LogError("No s'ha trobat cap AudioSource assignat o al component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprovar si l'objecte que entra és el jugador (Tag "Player").
        if (other.CompareTag("Player"))
        {
            // Reproduir el so si no està ja sonant.
            if (!soundSource.isPlaying)
            {
                soundSource.Play();
                Debug.Log("Jugador dins del trigger, so activat.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprovar si l'objecte que surt és el jugador (Tag "Player").
        if (other.CompareTag("Player"))
        {
            // Aturar el so quan el jugador surt del trigger.
            soundSource.Stop();
            Debug.Log("Jugador fora del trigger, so desactivat.");
        }
    }
}
