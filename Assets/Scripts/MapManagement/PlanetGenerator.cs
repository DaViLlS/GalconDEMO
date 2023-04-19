using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGenerator : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private GameObject planetObject;
    [SerializeField] private int planetsCount;
    private ClickedPlanets _clickedPlanets;
    private float _width;
    private float _height;
    private bool _isHasPlayerPlanet;

    private void Awake()
    {
        CalculateCameraSize();
        _isHasPlayerPlanet = false;
        _clickedPlanets = new ClickedPlanets();
        GeneratePlanets();
    }

    private void GeneratePlanets()
    {
        for (int i = 0; i < planetsCount; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-_width, _width), Random.Range(-_height, _height));
            float randomScale = Random.Range(0.5f, 1);
            planetObject.transform.localScale = new Vector2(randomScale, randomScale);

            Collider2D collider;

            while (!CheckNeighboorsPosition(randomPosition, out collider) || !CheckPlanetCameraPsition(randomPosition))
            {
                randomPosition = new Vector2(Random.Range(-_width, _width), Random.Range(-_height, _height));
            }

            if (!_isHasPlayerPlanet)
            {
                Planet planet = Instantiate(planetObject, randomPosition, Quaternion.identity).GetComponent<Planet>();
                planet.PlanetState = new PlayerPlanetState(planet);
                planet.ShipsCount = 50;
                _isHasPlayerPlanet = true;
            }
            else
            {
                Instantiate(planetObject, randomPosition, Quaternion.identity);
            }
        }
    }

    private bool CheckNeighboorsPosition(Vector2 randomPosition, out Collider2D collider)
    {
        float width = planetObject.transform.localScale.x;
        width += width;
        collider = Physics2D.OverlapBox(randomPosition, new Vector2(width, width), 0, LayerMask.GetMask("Planet"));

        if (collider != null)
        {
            return false;
        }

        return true;
    }

    private bool CheckPlanetCameraPsition(Vector2 randomPosition)
    {
        if (randomPosition.x < -_width || randomPosition.x > _width)
        {
            return false;
        }

        if (randomPosition.y < -_height || randomPosition.y > _height)
        {
            return false;
        }

        return true;
    }

    private void ChangePlanetPosition(Collider2D collider, ref Vector2 randomPosition)
    {
        float width = planetObject.transform.localScale.x;
        float height = planetObject.transform.localScale.x;

        randomPosition += new Vector2(width + width, height + height);
    }

    private void CalculateCameraSize()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;

        _width = camHalfWidth - offset;
        _height = camHalfHeight - offset;
    }
}
