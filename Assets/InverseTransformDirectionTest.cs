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

            float xz = (y.magnitude <= x.magnitude && y.magnitude <= z.magnitude) ? 1 : 0;
            float xy = (z.magnitude < x.magnitude && z.magnitude < y.magnitude) ? 1 : 0;
            float yz = (x.magnitude < y.magnitude && x.magnitude < z.magnitude) ? 1 : 0;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_local.position, x);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + y, x * xy);
            Gizmos.DrawRay(_local.position + z, x * xz);
            Gizmos.DrawRay(_local.position + y + z, x * yz);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_local.position, z);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + x, z * xz);
            Gizmos.DrawRay(_local.position + y, z * yz);
            Gizmos.DrawRay(_local.position + x + y, z * xy);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_local.position, y);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + x, y * xy);
            Gizmos.DrawRay(_local.position + z, y * yz);
            Gizmos.DrawRay(_local.position + x + z, y * xz);

            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(_local.position, (x + z) * xz);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + y, (x + z) * xz);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_local.position, (x + y) * xy);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + z, (x + y) * xy);

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(_local.position, (y + z) * yz);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_local.position + x, (y + z) * yz);


            x = new(_world.forward.x * _length, 0, 0);
            y = new(0, _world.forward.y * _length, 0);
            z = new(0, 0, _world.forward.z * _length);

            xz = (y.magnitude <= x.magnitude && y.magnitude <= z.magnitude) ? 1 : 0;
            xy = (z.magnitude < x.magnitude && z.magnitude < y.magnitude) ? 1 : 0;
            yz = (x.magnitude < y.magnitude && x.magnitude < z.magnitude) ? 1 : 0;

            Gizmos.color = Color.red;
            Gizmos.DrawRay(_world.position, x);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + y, x * xy);
            Gizmos.DrawRay(_world.position + z, x * xz);
            Gizmos.DrawRay(_world.position + y + z, x * yz);

            Gizmos.color = Color.blue;
            Gizmos.DrawRay(_world.position, z);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + x, z * xz);
            Gizmos.DrawRay(_world.position + y, z * yz);
            Gizmos.DrawRay(_world.position + x + y, z * xy);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(_world.position, y);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + x, y * xy);
            Gizmos.DrawRay(_world.position + z, y * yz);
            Gizmos.DrawRay(_world.position + x + z, y * xz);

            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(_world.position, (x + z) * xz);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + y, (x + z) * xz);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(_world.position, (x + y) * xy);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + z, (x + y) * xy);

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(_world.position, (y + z) * yz);
            Gizmos.color *= _fanc;
            Gizmos.DrawRay(_world.position + x, (y + z) * yz);


            Gizmos.color = Color.white;
            Gizmos.DrawRay(_local.position, x + y + z);
        }
    }

    enum FirstStep
    {
        XY,
        XZ,
        YZ,
    }
}
