using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    private LineRenderer line;
    private List<Vector3> positions;



    [SerializeField] private Material lineColor;
    [SerializeField] private Player player;

    private float xLimitMax = 2.5f;
    private float xLimitMin = -2.5f;

    private float yLimitMin = -1;
    private float yLimitMax = -4;

    void Update()
    {

        Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && ((mousePos.x >= xLimitMin && mousePos.x <= xLimitMax) && (mousePos.y <= yLimitMin && mousePos.y >= yLimitMax)))
        {

            StartCoroutine(painter());
        }
        else if (Input.GetMouseButtonUp(0) || !((mousePos.x >= xLimitMin && mousePos.x <= xLimitMax) && (mousePos.y <= yLimitMin && mousePos.y >= yLimitMax)))
        {

            StopCoroutine(painter());
            if (positions != null && !player.hasLeg)
                player.setLegPosCells(positions);
            StartCoroutine(eraser());
            StopCoroutine(eraser());

        }

    }



    IEnumerator painter()
    {
        line = new GameObject().AddComponent<LineRenderer>();
        line.transform.SetParent(transform);
        line.name = "leg";
        positions = new List<Vector3>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = lineColor;
        player.hasLeg =false;

        while (Input.GetMouseButton(0) && line != null)
        {
            positions.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 4.5f);
            line.positionCount = positions.Count;
            line.SetPositions(positions.ToArray());

            yield return new WaitForSeconds(0);

        }
    }

    IEnumerator eraser()
    {
        if (transform.childCount > 0)
        {
            GameObject leg = transform.Find("leg").gameObject;
            Destroy(leg);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public List<Vector3> getPositions()
    {
        return positions;
    }
}
