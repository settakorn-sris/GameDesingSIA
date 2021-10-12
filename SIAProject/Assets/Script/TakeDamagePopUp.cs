using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TakeDamagePopUp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI takeDamageText;
    [SerializeField] private float time;
    [SerializeField] private float timeOut = 0.5f;
    [SerializeField] private float minDistance = 2;
    [SerializeField] private float maxDistance = 3;
     private Vector3 spawnPosition;
     private Vector3 targetPosition;
   
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);
        float direction = Random.rotation.eulerAngles.z;
        spawnPosition = transform.position;
        float distance = Random.Range(minDistance, maxDistance);
        targetPosition = spawnPosition + (Quaternion.Euler(0, 0, direction) * new Vector3(distance, distance, 0));

        transform.localScale = new Vector3(0, 0, 0);
        

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float half = timeOut / 2;

        if (time > timeOut) Destroy(gameObject);
        else if(time>half)
        {
            takeDamageText.color = Color.Lerp(takeDamageText.color, Color.clear, (time - half) / (timeOut - half));
        }

        transform.position = Vector3.Lerp(spawnPosition, targetPosition, Mathf.Sin(time / timeOut));
        transform.localScale = Vector3.Lerp(new Vector3(0,0,0),new Vector3(1,1,1), Mathf.Sin(time / timeOut));

    }

    public void PopupActive(int damage)
    {
        takeDamageText.text = damage.ToString();
    }

    public void GetFontSize(float size)
    {
        takeDamageText.fontSize = size;
    }
}
