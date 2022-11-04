using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Glowdragon.VariableDisplay
{
    public class VariableDisplay : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField]
        private bool visibleAtStart;
        
        [Header("References")]
        [SerializeField]
        private GameObject parent;

        [SerializeField]
        private RectTransform background;

        [SerializeField]
        private TextMeshProUGUI textMesh;
        
        private readonly Dictionary<string, Dictionary<string, string>> variablesByCategory = new();

        private bool _active;

        private float fpsRefreshTimer;
        private int fps;

        public bool Active
        {
            get => this._active;
            set
            {
                this._active = value;
                this.parent.gameObject.SetActive(this._active);
            }
        }

        private void Start()
        {
            this.Active = this.visibleAtStart;
        }

        public void Set(string category, string name, System.Object value, bool active = true)
        {
            if (active)
            {
                if (!this.variablesByCategory.ContainsKey(category))
                {
                    this.variablesByCategory[category] = new Dictionary<string, string>();
                }

                this.variablesByCategory[category][name] = value?.ToString();
            }
            else
            {
                this.Unset(category, name);
            }
        }

        public void Set(string name, System.Object value, bool active = true)
        {
            this.Set("Unnamed", name, value, active);
        }

        public void Unset(string category, string name)
        {
            if (this.variablesByCategory.ContainsKey(category))
            {
                this.variablesByCategory[category].Remove(name);

                if (this.variablesByCategory[category].Count == 0)
                {
                    this.variablesByCategory.Remove(category);
                }
            }
        }

        public void ClearCategory(string category)
        {
            this.variablesByCategory.Remove(category);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                this.Active = !this.Active;
            }
            
            this.UpdateDisplay(false);
        }

        private void UpdateDisplay(bool spaceBetweenCategories)
        {
            string leftText = "Variable Display\n";
            if (this.Active)
            {
                foreach (KeyValuePair<string, Dictionary<string, string>> categoryVariablesMapEntry in this.variablesByCategory)
                {
                    string category = categoryVariablesMapEntry.Key;
                    Dictionary<string, string> variables = categoryVariablesMapEntry.Value;

                    leftText += (spaceBetweenCategories && leftText.Contains("\n") ? "\n" : "") + category + "\n";
                    foreach (KeyValuePair<string, string> variable in variables)
                    {
                        leftText += "<color=#D3D3D3>" + variable.Key + ":</color> " + variable.Value + "\n";
                    }
                }
            }
            this.textMesh.text = leftText;

            this.background.sizeDelta = new Vector2(this.background.sizeDelta.x, this.textMesh.rectTransform.sizeDelta.y + 23);
        }
    }
}