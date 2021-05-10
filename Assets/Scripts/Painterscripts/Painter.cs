using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe utilizada para denhar a linha no espaço alocado e gerar as posições para a criação da "perna" do player
///</summary>
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
        Debug.Log(Application.platform);
        if (Application.platform == RuntimePlatform.Android || SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.touchCount == 1)
            {
                
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                if ((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) && ((touchPos.x >= xLimitMin + transform.position.x && touchPos.x <= xLimitMax + transform.position.x) && (touchPos.y <= yLimitMin + transform.position.y && touchPos.y >= yLimitMax + transform.position.y)))
                {
                    StartCoroutine(painterAndroid());
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended || !((touchPos.x >= xLimitMin + transform.position.x && touchPos.x <= xLimitMax + transform.position.x) && (touchPos.y <= yLimitMin + transform.position.y && touchPos.y >= yLimitMax + transform.position.y)))
                {
                    StopCoroutine(painterAndroid());
                    if (positions != null && !player.hasLeg)
                        player.setLegPosCells(positions);
                    StartCoroutine(eraser());
                    StopCoroutine(eraser());
                }

            }
        }
        else if (Application.platform == RuntimePlatform.WebGLPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0) && ((mousePos.x >= xLimitMin + transform.position.x && mousePos.x <= xLimitMax + transform.position.x) && (mousePos.y <= yLimitMin + transform.position.y && mousePos.y >= yLimitMax + transform.position.y)))
            {

                StartCoroutine(painterWeb());
            }
            else if (Input.GetMouseButtonUp(0) || !((mousePos.x >= xLimitMin + transform.position.x && mousePos.x <= xLimitMax + transform.position.x) && (mousePos.y <= yLimitMin + transform.position.y && mousePos.y >= yLimitMax + transform.position.y)))
            {

                StopCoroutine(painterWeb());
                if (positions != null && !player.hasLeg)
                    player.setLegPosCells(positions);
                StartCoroutine(eraser());
                StopCoroutine(eraser());

            }
        }


    }
    ///<summary>
    /// Rotina responsável pela captura das posições do touch no android
    ///</summary>
    IEnumerator painterAndroid()
    {
        line = new GameObject().AddComponent<LineRenderer>();
        line.transform.SetParent(transform);
        line.name = "leg";
        positions = new List<Vector3>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = lineColor;
        player.hasLeg = false;


        while ((Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary) && line != null)
        {
            lineDraw(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position));

            yield return new WaitForSeconds(0);

        }
    }
    ///<summary>
    /// Rotina responsável pela captura das posições do mouse no webgl
    ///</summery>
    IEnumerator painterWeb()
    {
        line = new GameObject().AddComponent<LineRenderer>();
        line.transform.SetParent(transform);
        line.name = "leg";
        positions = new List<Vector3>();
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.material = lineColor;
        player.hasLeg = false;


        while (Input.GetMouseButton(0) && line != null)
        {

            lineDraw(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            yield return new WaitForSeconds(0);

        }
    }


    IEnumerator eraser()
    {
        if (transform.childCount > 1)
        {
            GameObject leg = transform.Find("leg").gameObject;
            Destroy(leg);
            yield return new WaitForSeconds(0.1f);
        }
    }

    ///<summary>
    /// Dado um vetor de posições <paramref name="pos"/> desenha a linha 
    ///</summary>
    private void lineDraw(Vector3 pos)
    {
        positions.Add(pos + Vector3.forward * 4.5f);
        line.positionCount = positions.Count;
        line.SetPositions(positions.ToArray());
    }
}
