using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;
     [SerializeField]
    private AudioClip[] music;


    private void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }


        DontDestroyOnLoad(this.gameObject);
        audio = GetComponent<AudioSource>();
    }
    public void CheckMusic()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        print(activeScene.buildIndex);
        switch (activeScene.buildIndex)
        {
            case 0:
                audio.Stop();
                audio.clip = music[0];
                audio.Play();
                break;
            case 2:
                audio.Stop();
                audio.clip = music[1];
                audio.Play();
                break;
            case 3:
                audio.Stop();
                audio.clip = music[2];
                audio.Play();
                break;
            default:
                break;
        }
    }
}
