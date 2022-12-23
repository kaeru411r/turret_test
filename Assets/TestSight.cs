using UnityEngine;

public class TestSight : MonoBehaviour
{
    [Tooltip("ターゲットのTransform")]
    [SerializeField] Transform _target;
    [Tooltip("飛ばすRayの色")]
    [SerializeField] Color _color = Color.red;


    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        if (_target)    // _targetにオブジェクトを向け、_targetまで線を引く
        {
            transform.LookAt(_target);
            Gizmos.DrawLine(transform.position, _target.position);
        }
        else            // _targetが無いので、正面に10ｍ線を引く
        {
            Gizmos.DrawRay(transform.position, transform.forward * 10);
        }
    }
}
