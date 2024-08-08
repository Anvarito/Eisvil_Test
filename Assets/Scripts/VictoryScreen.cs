using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button _button;
    public UnityAction OnCLick;
    private void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        OnCLick?.Invoke();
    }
}
