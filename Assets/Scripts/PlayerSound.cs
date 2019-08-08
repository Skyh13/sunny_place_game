using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{

    public AudioClip jumpSound;
    public AudioClip stepSound;
    public AudioClip damageSound;


    public void PlayDamageSound() {
        SoundManager.Instance.PlaySound(damageSound);
    }

    public void PlayJumpSound() {
        SoundManager.Instance.PlaySound(jumpSound);
    }

    public void PlayStepSound()
    {
        SoundManager.Instance.PlaySoundJitter(stepSound, 1f, 0.2f, 1f, 0.3f);
    }
}
