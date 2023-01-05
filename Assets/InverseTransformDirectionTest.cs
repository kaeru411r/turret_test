using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseTransformDirectionTest : MonoBehaviour
{
    [SerializeField] Transform _world;
    [SerializeField] Transform _local;

    [SerializeField] float _length = 2.0f;
    [SerializeField] float _fanc = 1.5f;


    private void OnDrawGizmos()
    {
        if (_world && _local)
        {
            Vector3 inverse = _local.InverseTransformDirection(_world.forward);

            Gizmos.color = Color.white;
            Gizmos.DrawRay(_world.position, _world.forward * _length);

            //Gizmos.color = Color.black;
            //Gizmos.DrawRay(_local.position, _local.forward * _length);

            Vector3 x = _local.right * inverse.x * _length;
            Vector3 y = _local.up * inverse.y * _length;
            Vector3 z = _local.forward * inverse.z * _length;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_local.position, x);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + y, x);
            Gizmos.DrawRay(_local.position + z, x);
            Gizmos.DrawRay(_local.position + y + z, x);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_local.position, z);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + x, z);
            Gizmos.DrawRay(_local.position + y, z);
            Gizmos.DrawRay(_local.position + x + y, z);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_local.position, y);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + x, y);
            Gizmos.DrawRay(_local.position + z, y);
            Gizmos.DrawRay(_local.position + x + z, y);

            Gizmos.color = Color.white;
            Gizmos.DrawRay(_local.position, x + y + z);


            x = new(_world.forward.x * _length, 0, 0);
            y = new(0, _world.forward.y * _length, 0);
            z = new(0, 0, _world.forward.z * _length);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_world.position, x);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + y, x);
            Gizmos.DrawRay(_world.position + z, x);
            Gizmos.DrawRay(_world.position + y + z, x);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_world.position, z);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + x, z);
            Gizmos.DrawRay(_world.position + y, z);
            Gizmos.DrawRay(_world.position + x + y, z);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_world.position, y);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + x, y);
            Gizmos.DrawRay(_world.position + z, y);
            Gizmos.DrawRay(_world.position + x + z, y);
        }
    }

    enum FirstStep
    {
        XY,
        XZ,
        YZ,
    }
}
