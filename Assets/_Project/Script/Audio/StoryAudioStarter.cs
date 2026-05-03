
using UnityEngine;

public class StoryAudioStarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayStoryMusic();
    }
}

