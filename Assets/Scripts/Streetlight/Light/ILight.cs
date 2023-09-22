using UnityEngine;

public interface ILight {
    Transform Transform { get; }
    bool IsOn();
    void Turn(bool on_off);
}