using UnityEngine.Events;

public interface IStreetlight {
    void TurnOff();
    void TurnOn();
    UnityEvent<IStreetlight> OnTurnOff { get;  }
    UnityEvent<IStreetlight> OnTurnOn { get;  }
}