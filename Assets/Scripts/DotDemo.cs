using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DotDemo : MonoBehaviour
{
    [SerializeField] Transform _up;
    [SerializeField] Transform _forward;
    [SerializeField] Text _text;
    [SerializeField] float _length = 1f;
    [SerializeField] float _speed = 1f;


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            _forward.Rotate(Vector3.right, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if(!_up || !_forward || !_text) { return; }
        Gizmos.DrawRay(_up.position, _up.up * _length);
        Gizmos.DrawRay(_forward.position, _forward.forward * _length);
        _text.text = Vector3.Dot(_up.up, _forward.forward).ToString("F2");
    }
}
