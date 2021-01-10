using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{
    private const float LIFETIMEMAX = 1f;
    private float lifeTime;
    private Color textColor;
    private Vector3 moveVector;

    private static int sortingOrder;

    public static DamagePopup Create(Vector3 position, float damageAmount)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.i.pfDamagePopup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();

        damagePopup.SetUp(damageAmount);

        return damagePopup;
    }

    private TextMeshPro textMesh;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }


    private void Update()
    {
        
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if(lifeTime > LIFETIMEMAX * .5f)
        {
            //first half of the popup lifetime
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            //second half of lifetime
            float decreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            float disapeparSpeed = 3f;
            textColor.a -= disapeparSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetUp(float DamageAmount)
    {
        textMesh.SetText(DamageAmount.ToString());
        textColor = textMesh.color;
        lifeTime = LIFETIMEMAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = new Vector3(.7f,1) * 60f;
        

    }
}
