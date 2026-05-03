
using UnityEngine;

public class LevelAudioStarter : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayLevel1Music();
    }
}