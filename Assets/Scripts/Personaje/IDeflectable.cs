using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDeflectable
{
    void Deflect(Vector2 direction);
    float ReturnSpeed { get; set; }
}
