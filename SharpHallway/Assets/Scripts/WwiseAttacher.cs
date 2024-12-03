using UnityEngine;
using System.Collections;


public class WwiseAttacher : MonoBehaviour
{
    [Header("Wwise Event Configuration")]
    public string eventName;
    public uint eventId;

    void Start()
    {
        StartCoroutine(DelayedPlay());
    }

    IEnumerator DelayedPlay()
    {
        // Ensure the object is registered before posting
        yield return null; // Wait a frame

        if (!gameObject.activeInHierarchy)
        {
            Debug.LogError("GameObject is not active in hierarchy.");
            yield break;
        }

        if (gameObject.GetComponent<AkGameObj>() == null)
        {
            Debug.Log("Adding AkGameObj to GameObject.");
            gameObject.AddComponent<AkGameObj>();
        }

        PlayWwiseEvent();
    }

    public void PlayWwiseEvent()
    {
        if (!string.IsNullOrEmpty(eventName))
        {
            Debug.Log($"Playing Wwise event by name: {eventName}");
            AkSoundEngine.PostEvent(eventName, gameObject);
        }
        else if (eventId != 0)
        {
            Debug.Log($"Playing Wwise event by ID: {eventId}");
            AkSoundEngine.PostEvent(eventId, gameObject);
        }
        else
        {
            Debug.LogError("No valid Wwise event name or ID provided.");
        }
    }
}