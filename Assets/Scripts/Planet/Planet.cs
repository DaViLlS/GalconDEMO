using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [HideInInspector] public SpriteRenderer planetSprite;
    [SerializeField] private GameObject shipsCountObject;
    [SerializeField] private GameObject shipObject;
    [SerializeField] public GameObject planetFrame;
    private TextMeshProUGUI _shipsCountText;

    public System.Action onShipsCountChanged;

    public PlanetState PlanetState { get; set; }
    public int ShipsCount { get; set; }

    private void Awake()
    {
        planetSprite = GetComponent<SpriteRenderer>();
        PlanetState = new NeutralPlanetState(this);
        ShipsCount = Random.Range(0, 50);
    }

    private void OnEnable()
    {
        onShipsCountChanged += UpdateShipsCountText;
    }

    private void OnDisable()
    {
        onShipsCountChanged -= UpdateShipsCountText;
    }

    private void Start()
    {
        shipsCountObject.transform.localScale = new Vector2(1, 1);
        _shipsCountText = shipsCountObject.GetComponent<TextMeshProUGUI>();
        _shipsCountText.text = ShipsCount.ToString();
    }

    public void SpawnShip(Vector3 target)
    {
        ShipsCount /= 2;
        UpdateShipsCountText();

        for (int i = 0; i < ShipsCount; i++)
        {
            ClassicShip ship = Instantiate(shipObject, transform.position, Quaternion.identity).GetComponent<ClassicShip>();
            ship.Target = target;
            ship.SentShipToTarget();
        }
    }

    private void UpdateShipsCountText()
    {
        _shipsCountText.text = ShipsCount.ToString();
    }

    private void OnMouseDown()
    {
        PlanetState.Clicked(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ClassicShip ship))
        {
            PlanetState.Triggered(this, ship);
        }
    }
}
