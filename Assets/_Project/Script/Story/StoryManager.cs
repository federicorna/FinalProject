using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class StoryManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Transform _camera;
    [SerializeField] private Transform[] _cameraPoints;  // Point1, Point2, Point3
    [SerializeField] private float _moveDuration = 1.5f;

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
        _camera.position = _cameraPoints[0].position;
        _camera.rotation = _cameraPoints[0].rotation;

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
        StartCoroutine(MoveToNextPoint());
    }

    private IEnumerator MoveToNextPoint()
    {
        _isMoving = true;

        /// Dissolvenza 
        yield return StartCoroutine(FadeText(0f));

        // Muovi camera
        Vector3 startPos = _camera.position;
        Quaternion startRot = _camera.rotation;
        Vector3 endPos = _cameraPoints[_currentIndex].position;
        Quaternion endRot = _cameraPoints[_currentIndex].rotation;

        float elapsed = 0f;
        while (elapsed < _moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / _moveDuration);
            _camera.position = Vector3.Lerp(startPos, endPos, t);
            _camera.rotation = Quaternion.Lerp(startRot, endRot, t);
            yield return null;
        }

        _camera.position = endPos;
        _camera.rotation = endRot;

        /// Mostra nuovo testo
        _storyText.text = _storyLines[_currentIndex];
        yield return StartCoroutine(FadeText(1f));

        /// Se  ultimo testo mostra  bottone
        if (_currentIndex == _storyLines.Length - 1)
            _startButton.SetActive(true);

        _isMoving = false;
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