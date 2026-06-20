using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private CubeManager _cubeManager;

    [Header("Настройки")]
    [SerializeField] private float _restartDelay = 2f;

    private bool _autoRestart = false;

    private void Awake()
    {
        if (_cubeManager == null)
        {
            _cubeManager = FindAnyObjectByType<CubeManager>();

            if (_cubeManager == null)
            {
                Debug.LogError("CubeManager not found in scene!");
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (_autoRestart && _cubeManager != null && _cubeManager.GetActiveCubesCount() == 0)
        {
            Invoke(nameof(RestartGame), _restartDelay);
        }
    }

    private void RestartGame()
    {
        if (_cubeManager == null) return;

        _cubeManager.Restart();
    }
}