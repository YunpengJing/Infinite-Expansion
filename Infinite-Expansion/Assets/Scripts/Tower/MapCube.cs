using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapCube : MonoBehaviour
{
    // ref
    [HideInInspector]
    public GameObject turretGo; // 保存当前cube身上的炮台
    public GameObject buildEffectPrefab;
    private Renderer renderer;
    public Slider hpSlider;

    // attr
    public int hp = 200;
    private int totalHp;
    public float height;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        totalHp = hp;
        height = transform.localScale.y / 2 - 0.5f;
    }

    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position + new Vector3(0, height, 0), Quaternion.identity);
        turretGo.GetComponent<Turret>().SetMapCubeGo(gameObject);
        GameObject effect = GameObject.Instantiate(buildEffectPrefab, transform.position + new Vector3(0, height, 0), transform.rotation);
        Destroy(effect, 1.5f);
    }

    private void OnMouseEnter()
    {
        // 只有有选中炮塔，才高亮方块
        if (BuildManager.selectedTurretData == null) return;

        if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    public void TakeDamage(int damage)
    {
        if (hp <= 0) return;
        hp -= damage;
        hpSlider.value = (float)hp / totalHp;

        if (hp <= 0)
        {
            Destroy(gameObject);
            Destroy(turretGo.gameObject);
        }
    }
}
