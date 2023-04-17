using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSwapper : MonoBehaviour
{
    // ссылка на игрока
    Transform player;
    // список фонов
    [SerializeField] List<SpriteRenderer> backgrounds = new List<SpriteRenderer>();
    // список точек, между которыми происходит плавная смена фона
    [SerializeField] List<Transform> edgePoints = new List<Transform>();

    void Start()
    {
        player = FindObjectOfType<Person>().transform;
    }

    void Update()
    {
        if (backgrounds.Count != edgePoints.Count)
        {
            Debug.LogError("The number of edge points and the number of backgrounds must be the same");
            return;
        }

        if (backgrounds.Count < 2)
            return;

        for (int i = 0; i < backgrounds.Count - 1; i++)
        {
            float fractionOfJourney = (player.position.x - edgePoints[i].position.x) / (edgePoints[i + 1].position.x - edgePoints[i].position.x);
            float fractionOfJourneyClamped01 = Mathf.Clamp01(fractionOfJourney);
            
            backgrounds[i + 1].color = Color.Lerp
            (
                new Color(backgrounds[i + 1].color.r, backgrounds[i + 1].color.g, backgrounds[i + 1].color.b, 0f),
                new Color(backgrounds[i + 1].color.r, backgrounds[i + 1].color.g, backgrounds[i + 1].color.b, 1f),
                fractionOfJourneyClamped01
            );
        }
    }
}
