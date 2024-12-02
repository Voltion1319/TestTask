using System;
using UnityEngine;

public class EnemyMoveComponent
{
    [Serializable]
    public class Settings
    {
        public float Speed = .5f;
    }

    private readonly Transform _moveTransform;
    private readonly Settings _settings;

    public Transform MoveTransform => _moveTransform;

    public EnemyMoveComponent(EnemySettings settings, Transform moveTransform)
    {
        _settings = settings.MoveSettings;
        _moveTransform = moveTransform;
    }
    
    public virtual void Initialize()
    {
    }

    public virtual void Dispose()
    {
        
    }

    public void Move(Vector3 targetPosition)
    {
        MoveTransform.position = MoveTransform.position + (targetPosition - MoveTransform.position).normalized * _settings.Speed * Time.deltaTime;
    }
}
