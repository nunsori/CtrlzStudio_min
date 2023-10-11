using UnityEditor;
using UnityEngine;

namespace ABKaspo.Assets.AURW.AURW_Editor.Shaders
{
    enum refractionQuality
    {
        Advanced = 0,
        Normal = 1
    }
    public class AURW_Free_Shader_GUI : ShaderGUI
    {
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            GUIStyle titleStyle = new GUIStyle(EditorStyles.boldLabel);
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.fontSize = 16;
            GUILayout.Label("A.U.R.W.", titleStyle);
            EditorGUILayout.Separator();
            GUILayout.Label("Rendering", EditorStyles.boldLabel);
            // base.OnGUI(materialEditor, properties);
            Material material = materialEditor.target as Material;

            MaterialProperty qualityWaterEnum = FindProperty("_WATER_QUALITY", properties);
            MaterialProperty qualityRefractionEnum = FindProperty("_REFRACTION_QUALITY", properties);
            MaterialProperty alphaBool = FindProperty("_ALPHA_CHANNEL", properties);
            MaterialProperty refractionFloat = FindProperty("_Refraction", properties);
            MaterialProperty refractionBool = FindProperty("_REFRACTION", properties);
            MaterialProperty reflectionFloat = FindProperty("_Reflection", properties);
            MaterialProperty reflectionBool = FindProperty("_REFLECTION", properties);
            MaterialProperty displacementBool = FindProperty("_DISPLACEMENT", properties);
            MaterialProperty waveFrequencyFloat = FindProperty("_HeighFrequency", properties);
            MaterialProperty waveAmplitudeFloat = FindProperty("_WaveAmplitude", properties);
            MaterialProperty waveSpeedFloat = FindProperty("_WaveSpeed", properties);
            MaterialProperty smoothnessFloat = FindProperty("_Smoothness", properties);
            MaterialProperty TillingFloat = FindProperty("_Tilling", properties);
            MaterialProperty speedFloat = FindProperty("_Speed", properties);
            MaterialProperty directionVector_2 = FindProperty("_Direction", properties);
            MaterialProperty depthFloat = FindProperty("_Depth", properties);
            MaterialProperty foamTexture = FindProperty("_Foam", properties);
            MaterialProperty n_MainTexture = FindProperty("_MainNormal", properties);
            MaterialProperty n_SecondTexture = FindProperty("_SecondNormal", properties);
            MaterialProperty n_BiggerTexture = FindProperty("_BiggerNormal", properties);
            MaterialProperty n_foamTexture = FindProperty("_FoamNormal", properties);
            MaterialProperty normalStrengthFloat = FindProperty("_Normal_Strength", properties);

            GUIStyle keywordStyle = new GUIStyle(EditorStyles.label);
            Color dividerColor = new Color(0.5f, 0.5f, 0.5f, 1f);
            //keywordStyle.fontSize = 12;

            EditorGUI.BeginChangeCheck();
            ShowKeywordEnumField("Water Quality", qualityWaterEnum, keywordStyle, materialEditor);
            ShowKeywordEnumField("Refraction Type", qualityRefractionEnum, keywordStyle, materialEditor);
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), dividerColor);
            EditorGUILayout.Space();
            GUILayout.Label("Colours", EditorStyles.boldLabel);
            foreach (MaterialProperty property in properties)
            {
                if (property.type == MaterialProperty.PropType.Color)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUIUtility.labelWidth = 0;

                    // Renderizar el nombre del color alineado a la izquierda
                    EditorGUILayout.LabelField("    " + property.displayName, GUILayout.Width(120));

                    // Renderizar el campo de selección de color HDR
                    EditorGUI.indentLevel--;
                    EditorGUI.showMixedValue = property.hasMixedValue;
                    //property.colorValue = EditorGUILayout.ColorField(GUIContent.none, property.colorValue, true, true, false, null);
                    property.colorValue = EditorGUILayout.ColorField(GUIContent.none, property.colorValue, true, true, true);
                    EditorGUI.indentLevel++;
                    EditorGUI.showMixedValue = false;
                    EditorGUILayout.EndHorizontal();
                }
            }
            ShowKeywordBoolFIeld("      Alpha Channel", alphaBool, keywordStyle, materialEditor);
            if (alphaBool.floatValue == 1)
            {
                EditorGUILayout.HelpBox("If refraction is on, to a better render, turn off this.", MessageType.Info);
            }
            EditorGUILayout.Space();
            GUILayout.Label("Surface", EditorStyles.boldLabel);
            ShowFloatField("    Refraction", refractionFloat, keywordStyle, materialEditor);
            if (refractionBool.floatValue == 1 && qualityRefractionEnum.floatValue == 0)
            {
                EditorGUILayout.HelpBox("Refraction is based on normal strength, this values doesn't change its strength, but the distorion.", MessageType.Info);
            }
            ShowKeywordBoolFIeld("        Enable", refractionBool, keywordStyle, materialEditor);
            ShowFloatField("    Reflection", reflectionFloat, keywordStyle, materialEditor);
            if (reflectionBool.floatValue == 1)
            {
                EditorGUILayout.HelpBox("Add 'reflections' script to start reflecting (Beta).", MessageType.Info);
            }
            ShowKeywordBoolFIeld("        Enable", reflectionBool, keywordStyle, materialEditor);
            ShowFloatSlider("    Smoothness", smoothnessFloat, keywordStyle, 1, 0);
            EditorGUILayout.Space();
            GUILayout.Label("Tilling & Offset", EditorStyles.boldLabel);
            ShowFloatField("    Tilling", TillingFloat, keywordStyle, materialEditor);
            ShowFloatField("    Speed", speedFloat, keywordStyle, materialEditor);
            ShowVector2Property("    Direction", directionVector_2);
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), dividerColor);
            EditorGUILayout.Space();
            GUILayout.Label("Foam", EditorStyles.boldLabel);
            ShowTextureProperty("       Foam Texture", foamTexture, materialEditor);
            ShowFloatField("    Depth", depthFloat, keywordStyle, materialEditor);
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), dividerColor);
            EditorGUILayout.Space();
            GUILayout.Label("Normal Mapping", EditorStyles.boldLabel);
            ShowTextureProperty("       Main Normal", n_MainTexture, materialEditor);
            ShowTextureProperty("       Second Normal", n_SecondTexture, materialEditor);
            ShowTextureProperty("       Bigger Normal", n_BiggerTexture, materialEditor);
            ShowTextureProperty("       Foam Normal", n_foamTexture, materialEditor);
            ShowFloatField("       NormalStrength", normalStrengthFloat, keywordStyle, materialEditor);
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), dividerColor);
            EditorGUILayout.Space();
            GUILayout.Label("Displacement", EditorStyles.boldLabel);
            ShowKeywordBoolFIeld("    Enable", displacementBool, keywordStyle, materialEditor);
            if(displacementBool.floatValue == 1)
            {
                ShowFloatField("    Wave Speed", waveSpeedFloat, keywordStyle, materialEditor);
                ShowFloatField("    Wave Amplitude", waveAmplitudeFloat, keywordStyle, materialEditor);
                ShowFloatField("    Wave Frequency", waveFrequencyFloat, keywordStyle, materialEditor);
            }
            EditorGUILayout.Space();
            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, 2), dividerColor);
            EditorGUILayout.Space();


            EditorGUILayout.HelpBox("ABKaspo's Ultra Realistic Water (A.U.R.W.), for more informations go to documentation window in ABKaspo -> About -> Documentation. If you wanna contact us send an e-mail to ABKaspo -> About -> Contact Us -> Send an E-Mail.", MessageType.None);


            if (EditorGUI.EndChangeCheck())
            {
                
            }

        }
        private void ShowFloatField(string label, MaterialProperty property, GUIStyle style, MaterialEditor materialEditor)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style, GUILayout.Width(200));
            materialEditor.ShaderProperty(property, GUIContent.none);

            EditorGUILayout.EndHorizontal();
        }
        private void ShowFloatSlider(string label, MaterialProperty property, GUIStyle style, float max, float min)
        {
            EditorGUILayout.BeginHorizontal();
            if (property.type == MaterialProperty.PropType.Float)
            {
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.LabelField(label, style, GUILayout.Width(120));
                EditorGUI.indentLevel--;
                EditorGUI.showMixedValue = property.hasMixedValue;
                property.floatValue = EditorGUILayout.Slider(property.floatValue, min, max);
                EditorGUI.indentLevel++;
                EditorGUI.showMixedValue = false;

                EditorGUILayout.EndHorizontal();
            }
        }

        private void ShowKeywordEnumField(string label, MaterialProperty property, GUIStyle style, MaterialEditor materialEditor)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style, GUILayout.Width(200));
            materialEditor.ShaderProperty(property, GUIContent.none);

            EditorGUILayout.EndHorizontal();
        }
        
        private void ShowKeywordBoolFIeld(string label, MaterialProperty property, GUIStyle style, MaterialEditor materialEditor)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, style);
            materialEditor.ShaderProperty(property, GUIContent.none);

            EditorGUILayout.EndHorizontal();
        }

        private void ShowTextureProperty(string label, MaterialProperty property, MaterialEditor materialEditor)
        {
            if (property.type == MaterialProperty.PropType.Texture)
            {
                EditorGUIUtility.labelWidth = 0;
               // EditorGUILayout.LabelField(label, GUILayout.Width(120));
                EditorGUI.indentLevel--;
                EditorGUI.showMixedValue = property.hasMixedValue;
                materialEditor.TextureProperty(property, label);
                EditorGUI.indentLevel++;
                EditorGUI.showMixedValue = false;
            }
        }
        private void ShowVector2Property(string label, MaterialProperty property)
        {
            EditorGUILayout.BeginHorizontal();
            if (property.type == MaterialProperty.PropType.Vector)
            {
                EditorGUIUtility.labelWidth = 0;
                EditorGUILayout.LabelField(label, GUILayout.Width(120));
                EditorGUI.indentLevel--;
                EditorGUI.showMixedValue = property.hasMixedValue;
                Vector2 vectorValue = property.vectorValue;
                EditorGUI.BeginChangeCheck();
                vectorValue = EditorGUILayout.Vector2Field("", vectorValue);
                if (EditorGUI.EndChangeCheck())
                {
                    property.vectorValue = vectorValue;
                }
                EditorGUI.indentLevel++;
                EditorGUI.showMixedValue = false;
            }

            EditorGUILayout.EndHorizontal();
        }
    }
}