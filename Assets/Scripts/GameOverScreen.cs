using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button _button;
    public UnityAction OnCLick;
    protected virtual void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    protected virtual void Click()
    {
        OnCLick?.Invoke();
    }
}