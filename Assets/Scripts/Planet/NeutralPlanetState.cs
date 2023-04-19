using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeutralPlanetState : PlanetState
{
    public NeutralPlanetState(Planet planet)
    {
        Planet = planet;
        planet.planetSprite.color = Color.gray;
    }

    public override void Clicked(Planet planet)
    {
       if (ClickedPlanets.Instance.PlayerPlanetState != null)
       {
            ClickedPlanets.Instance.PlayerPlanetState.Planet.SpawnShip(planet.transform.position);
       }
    }

    public override void Triggered(Planet planet, ClassicShip ship)
    {
        if(ship.Target == Planet.transform.position)
        {
            ship.DestroyShip();

            if (!(planet.ShipsCount <= 0))
            {
                planet.ShipsCount--;
                planet.onShipsCountChanged?.Invoke();
            }

            if (planet.ShipsCount <= 0)
                Captured(planet);
        }
    }

    public override void Captured(Planet planet)
    {
        planet.PlanetState = new PlayerPlanetState(planet);
        planet.ShipsCount = 0;
    }
}
