using UnityEngine.Events;

public interface IStreetlight {
    void TurnOff();
    bool TurnOn();
    UnityEvent<IStreetlight> OnTurnOff { get;  }
    UnityEvent<IStreetlight> OnTurnOn { get;  }
}