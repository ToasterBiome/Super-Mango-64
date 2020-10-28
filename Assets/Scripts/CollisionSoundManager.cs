using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource treeAudioSource;
    [SerializeField]
    private AudioClip[] treeClips;

    private AudioSource grassAudioSource;
    [SerializeField]
    private AudioClip[] grassClips;

    private AudioSource bananaAudioSource;
    [SerializeField]
    private AudioClip[] bananaClips;

    private AudioSource gloveAudioSource;
    [SerializeField]
    private AudioClip[] gloveClips;

    private AudioSource wallAudioSource;
    [SerializeField]
    private AudioClip[] wallClips;

    private AudioSource enemyAudioSource;
    [SerializeField]
    private AudioClip[] enemyClips;

    private AudioSource padAudioSource;
    [SerializeField]
    private AudioClip[] padClips;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // switch this to on the object referencing the player so i don't have to make tags for everything and also other objects won't trigger it
    private IEnumerator OnTriggerEnter(Collider other)
    {
        // need to make a method to get tag from object (abstraction)
        if (other.CompareTag("Tree"))
        {
            treeAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(treeClips);
            treeAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(treeAudioSource);
        }
       if (other.CompareTag("Grass"))
        {
            grassAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(grassClips);
            grassAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(grassAudioSource);
        }
        if (other.CompareTag("Banana"))
        {
            bananaAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(bananaClips);
            bananaAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(bananaAudioSource);
        }
        if (other.CompareTag("Gloves"))
        {
            gloveAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(gloveClips);
            gloveAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(gloveAudioSource);
        }
        if (other.CompareTag("Wall"))
        {
            wallAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(wallClips);
            wallAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(wallAudioSource);
        }
        if (other.CompareTag("Enemy"))
        {
            enemyAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(enemyClips);
            enemyAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(enemyAudioSource);
        }
        if (other.CompareTag("Jump Pad"))
        {
            padAudioSource = gameObject.AddComponent<AudioSource>();
            AudioClip clip = GetRandomClip(padClips);
            padAudioSource.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            Destroy(padAudioSource);
        }

    }
    private AudioClip GetRandomClip(AudioClip[] array)
    {
        // return a random AudioClip from the array that is passed as an argument, chosen between index 0 and array.Length (the length of the array)
        return array[UnityEngine.Random.Range(0, array.Length)];
    }

 

}
