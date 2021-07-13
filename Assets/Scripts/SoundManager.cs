using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource wallSound;
    public AudioSource racketSound;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "RacketPlayer1" || other.gameObject.name == "RacketPlayer2")
        {
            this.racketSound.Play();
        }
        else
        {
            this.wallSound.Play();
        }
    }
}
