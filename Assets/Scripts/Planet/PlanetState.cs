using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlanetState
{
    public Planet Planet { get; set; }
    public abstract void Captured(Planet planet);
    public abstract void Clicked(Planet planet);
    public abstract void Triggered(Planet planet, ClassicShip ship);
}
