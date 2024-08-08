using TMPro;
using UnityEngine;

public class StartTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    public void SetTimer(int value)
    {
        _timerText.text = value.ToString();
        if (value == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
