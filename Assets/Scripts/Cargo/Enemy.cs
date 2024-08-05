using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    public class Factory : PlaceholderFactory<Enemy>
    {
        
    }
}