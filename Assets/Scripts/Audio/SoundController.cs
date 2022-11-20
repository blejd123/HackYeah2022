using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Audio
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _soundAudioSource;

        private void Awake()
        {
            _musicAudioSource.loop = true;
        }

        public void PlayMusic(AudioClip audioClip, bool fade)
        {
            StartCoroutine(Play(_musicAudioSource, audioClip, fade));
        }

        public void PlaySound(AudioClip audioClip)
        {
            _soundAudioSource.PlayOneShot(audioClip);
        }
        
        private IEnumerator Play(AudioSource audioSource, AudioClip audioClip, bool fade)
        {
            if (!fade)
            {
                audioSource.clip = audioClip;
            }
            else
            {
                audioSource.DOKill();

                if (audioSource.isPlaying)
                {
                    yield return audioSource.DOFade(0.0f, 0.25f).WaitForCompletion();    
                }

                audioSource.clip = audioClip;
                audioSource.Play();
                yield return audioSource.DOFade(1.0f, 0.25f).WaitForCompletion();
            }
        }
    }
}
