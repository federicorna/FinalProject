
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Header("Cursor Settings")]
    [SerializeField] private Texture2D _crosshairTexture;
    [SerializeField] private Texture2D _arrowTexture;

    [SerializeField] private Vector2 _crosshairHotspot;

 
    // Quando inizia gioco o esc pausa
    public void SetGameplayCursor()
    {
        // Imposta texture mirino e centro
        Vector2 hotspot = _crosshairHotspot == Vector2.zero ? new Vector2(_crosshairTexture.width / 2, _crosshairTexture.height / 2) : _crosshairHotspot;

        Cursor.SetCursor (_crosshairTexture, hotspot, CursorMode.Auto);
    }

    // Quando aprerto menu di pausa
    public void SetMenuCursor()
    {
        // Sblocca cursore per muoversi liberamente
        Cursor.lockState = CursorLockMode.None;

        // Torna freccia se _arrowTexture è null
        Cursor.SetCursor(_arrowTexture, Vector2.zero, CursorMode.Auto);
    }

    public void SetCursorVisibility(bool visible)
    {
        Cursor.visible = visible;
    }
}