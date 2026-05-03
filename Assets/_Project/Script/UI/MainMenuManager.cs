using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannelli")]
    [SerializeField] private GameObject _leftPanel;
    [SerializeField] private GameObject _creditsPanel;

    [Header("Fade")]
    [SerializeField] private Image _fadePanel;
    [SerializeField] private float _fadeDuration = 3f;

    private void Start()
    {
        _leftPanel.SetActive(true);
        _creditsPanel.SetActive(false);

        // Assicura che il fade parta trasparente
        SetFadeAlpha(0f);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


    //--[f.ni]--

    public void OnPlayButton()
    {
        StartCoroutine(FadeAndLoad("Level1"));
    }

    public void OnCreditsButton()
    {
        _leftPanel.SetActive(false);
        _creditsPanel.SetActive(true);
    }

    public void OnBackButton()
    {
        _creditsPanel.SetActive(false);
        _leftPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        Application.Quit();
        Debug.Log("[Menu] Quit");
    }


    private IEnumerator FadeAndLoad(string sceneName)
    {
        // Disabilita i bottoni durante il fade per evitare doppi click
        _leftPanel.SetActive(false);

        float elapsed = 0f;
        while (elapsed < _fadeDuration)
        {
            elapsed += Time.deltaTime;
            SetFadeAlpha(Mathf.Clamp01(elapsed / _fadeDuration));
            yield return null;
        }

        SetFadeAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }

    private void SetFadeAlpha(float alpha)
    {
        if (_fadePanel == null) return;
        Color c = _fadePanel.color;
        c.a = alpha;
        _fadePanel.color = c;
    }
}