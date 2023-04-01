using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 点击发光 : MonoBehaviour {
    private MeshRenderer meshRenderer;
    private void Awake() {
        meshRenderer = this.GetComponent<MeshRenderer>();
        meshRenderer.material.SetFloat("_RimIntensity",0f);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit)) {
                if (hit.transform.name.Equals("边缘发光Shader")) {
                    meshRenderer = this.GetComponent<MeshRenderer>();
                    meshRenderer.material.SetFloat("_RimIntensity",4f);
                }
            }
        }
        
    }
}
