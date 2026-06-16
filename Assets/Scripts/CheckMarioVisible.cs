using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMarioVisible : MonoBehaviour
{
    public AudioSource marioAudioSource;
    public AudioClip marioDie;
    private void OnBecameInvisible()
    {
        marioAudioSource.PlayOneShot(marioDie);
        MarioGameManager.Instance.GameFailed();
    }
}
