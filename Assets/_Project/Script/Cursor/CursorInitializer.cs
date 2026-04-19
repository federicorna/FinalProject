
using UnityEngine;

public class CursorInitializer : MonoBehaviour
{
    [SerializeField] private CursorManager _cursorManager;
    [SerializeField] private bool _startWithGameplayCursor = true;

    void Start()
    {
        if (_cursorManager == null)
        {
            _cursorManager = GetComponent<CursorManager>();
        }

        if (_startWithGameplayCursor)
        {
            _cursorManager.SetGameplayCursor();
        }
        else
        {
            _cursorManager.SetMenuCursor();
        }
    }
}