using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [Header("Cinemachine")]
    [SerializeField] private CinemachineVirtualCamera[] _vCams;
    [SerializeField] private CinemachineBrain _brain;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _storyText;
    [SerializeField] private GameObject _startButton;

    [Header("Testi")]
    [TextArea(3, 6)]
    [SerializeField] private string[] _storyLines;

    private int _currentIndex = 0;
    private bool _isMoving = false;

    private void Start()
    {
        SetActiveVCam(0);
        _storyText.text = _storyLines[0];
        _startButton.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isMoving)
        {
            Advance();
        }
    }


    //--[f.ni]--

    private void Advance()
    {
        /// Se ultimo testo non fare nulla
        if (_currentIndex >= _storyLines.Length - 1) return;

        _currentIndex++;
        StartCoroutine(BlendToNext());
    }

    private IEnumerator BlendToNext()
    {
        _isMoving = true;

        /// Dissolvenza 
        yield return StartCoroutine(FadeText(0f));

        /// Attiva VCam successiva
        SetActiveVCam(_currentIndex);

        /// Aspetta che il blend finisca
        yield return new WaitUntil(() => !_brain.IsBlending);

        /// Mostra nuovo testo
        _storyText.text = _storyLines[_currentIndex];
        yield return StartCoroutine(FadeText(1f));

        /// Se  ultimo testo mostra  bottone
        if (_currentIndex == _storyLines.Length - 1)
            _startButton.SetActive(true);

        _isMoving = false;
    }

    private void SetActiveVCam(int index)
    {
        for (int i = 0; i < _vCams.Length; i++)
            _vCams[i].Priority = (i == index) ? 10 : 0;
    }

    private IEnumerator FadeText(float targetAlpha)
    {
        float startAlpha = _storyText.alpha;
        float elapsed = 0f;
        float duration = 0.4f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _storyText.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            yield return null;
        }

        _storyText.alpha = targetAlpha;
    }

    public void OnStartButton()
    {
        SceneManager.LoadScene("Level1");
    }
}