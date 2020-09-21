using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{
    // ref
    [HideInInspector]
    public GameObject turretGo; // 保存当前cube身上的炮台
    public GameObject buildEffectPrefab;
    private Renderer renderer;

    // attr
    public int hp = 200;

    private void Start()
    {
        this.renderer = GetComponent<Renderer>();
    }

    public void BuildTurret(GameObject turretPrefab)
    {
        turretGo = GameObject.Instantiate(turretPrefab, transform.position, Quaternion.identity);
        turretGo.GetComponent<Turret>().SetMapCubeGo(gameObject);
        GameObject.Instantiate(buildEffectPrefab, transform.position, transform.rotation);
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
        if (hp <= 0)
        {
            Destroy(gameObject);
            Destroy(turretGo.gameObject);
        }
    }
}
