using System.Collections.Generic;
using UnityEngine;

public class ChoreographyScene01 : Choreographer
{
    public Actor crystal, damien, hunter, teddy;
    public GameObject prop01;
    public AudioClip sfx01, sfx02;

    private Dictionary<Actor, GameObject> speechBubbles = new Dictionary<Actor, GameObject>();
    private AudioSource audioSource;

    void Start()
    {
        speechBubbles.Add(crystal, crystal.transform.Find("SpeechBubble").gameObject);
        speechBubbles.Add(damien, damien.transform.Find("SpeechBubble").gameObject);
        speechBubbles.Add(hunter, hunter.transform.Find("SpeechBubble").gameObject);
        speechBubbles.Add(teddy, teddy.transform.Find("SpeechBubble").gameObject);

        foreach (GameObject speechBubble in speechBubbles.Values)
        {
            speechBubble.SetActive(false);
        }

        crystal.GetComponent<Animator>().Play("IdleRight", 0);
        damien.GetComponent<Animator>().Play("IdleDown", 0);
        hunter.GetComponent<Animator>().Play("IdleLeft", 0);
        teddy.GetComponent<Animator>().Play("IdleRight", 0);

        audioSource = GetComponent<AudioSource>();
    }

    public override void SetSpeaker(string speaker)
    {
        foreach (GameObject speechBubble in speechBubbles.Values)
        {
            speechBubble.SetActive(false);
        }
        if (speaker.Contains("Crystal"))
        {
            speechBubbles[crystal].SetActive(true);
        }
        else if (speaker.Contains("Damien"))
        {
            speechBubbles[damien].SetActive(true);
        }
        else if (speaker.Contains("Hunter"))
        {
            speechBubbles[hunter].SetActive(true);
        }
        else if (speaker.Contains("Teddy"))
        {
            speechBubbles[teddy].SetActive(true);
        }
    }

    public override void CueBlocking(int index)
    {
        switch (index)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
            case 7:

                break;
            case 8:

                break;
            case 9:

                break;
            case 10:

                break;
            case 11:
                prop01.GetComponent<Animator>().SetTrigger("cue");
                audioSource.clip = sfx01;
                Invoke("PlaySFX", 0.5f);
                break;
            case 12:

                break;
            case 13:

                break;
            case 14:

                break;
            case 15:

                break;
            case 16:

                break;
            case 17:

                break;
            case 18:

                break;
            case 19:

                break;
            case 20:

                break;
            case 21:

                break;
            case 22:

                break;
            case 23:

                break;
            case 24:
                break;
            default:
                DontDestroyOnLoad(this.gameObject);
                audioSource.clip = sfx02;
                audioSource.Play();
                Destroy(this.gameObject, sfx02.length);
                break;
        }
    }

    private void PlaySFX()
    {
        audioSource.Play();
    }
}
