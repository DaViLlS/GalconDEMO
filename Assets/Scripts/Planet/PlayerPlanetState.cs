using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlanetState : PlanetState
{
    public PlayerPlanetState(Planet planet)
    {
        Planet = planet;
        planet.planetSprite.color = Color.blue;
        planet.StartCoroutine(ShipsAddCoroutine(planet));
    }

    public override void Clicked(Planet planet)
    {
        if (ClickedPlanets.Instance.PlayerPlanetState != null)
        {
            ClickedPlanets.Instance.PlayerPlanetState.Planet.planetFrame.SetActive(false);
        }

        ClickedPlanets.Instance.PlayerPlanetState = this;
        planet.planetFrame.SetActive(true);
        //planet.ShipsCount++;
        //planet.onShipsCountChanged?.Invoke();
    }

    public override void Captured(Planet planet)
    {
        planet.PlanetState = new NeutralPlanetState(planet);
        planet.StopAllCoroutines();
        planet.ShipsCount = 0;
    }

    IEnumerator ShipsAddCoroutine(Planet planet)
    {
        yield return new WaitForSeconds(1);
        planet.ShipsCount += 5;
        planet.onShipsCountChanged?.Invoke();
        planet.StartCoroutine(ShipsAddCoroutine(planet));
    }

    public override void Triggered(Planet planet, ClassicShip ship)
    {
        if (ship.Target == Planet.transform.position)
        {
            ship.DestroyShip();

            planet.ShipsCount++;
            planet.onShipsCountChanged?.Invoke();
        }
    }
}
