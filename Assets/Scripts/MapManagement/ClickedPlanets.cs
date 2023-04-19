using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickedPlanets
{
    public static ClickedPlanets Instance;
    public PlayerPlanetState PlayerPlanetState { get; set; }
    public NeutralPlanetState NeutralPlanetState { get; set; }

    public ClickedPlanets()
    {
        if (Instance != null)
        {
            Instance = null;
        }

        Instance = this;
    }
}
