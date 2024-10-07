using DG.Tweening;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _player;
    
    private void Update()
    {
        Vector3 followPosition = new Vector3(_player.position.x, _player.position.y, -10f);
        transform.DOMove(followPosition, 0);
    }
}