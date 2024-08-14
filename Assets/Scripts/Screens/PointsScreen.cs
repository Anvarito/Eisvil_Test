using TMPro;
using UnityEngine;

public class PointsScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void SetPoints(int points)
    {
        _text.text = points.ToString();
    }
}
