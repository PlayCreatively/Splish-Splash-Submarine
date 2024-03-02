using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public struct NormalCurve
{
    public float attack, sustain, release;

    public float attackEase;
    public float releaseEase;

    readonly float Time => attack + sustain + release;
    public readonly float AttackRatio => attack / Time;
    public readonly float SustainRatio => sustain / Time;
    public readonly float ReleaseRatio => release / Time;

    public static NormalCurve Default => new()
    {
        attack = 1,
        sustain = 1,
        release = 1
    };
    

    readonly float EvaluateIn(float t)
    {
        return 1 - Ease(1 - t / AttackRatio, attackEase);
    }

    readonly float EvaluateOut(float t)
    {
        t -= AttackRatio + SustainRatio;
        return Ease(1 - t / ReleaseRatio, releaseEase);
    }

    public readonly float Evaluate(float t)
    {
        if(Time == 0)
            return 0;

        t = Mathf.Clamp01(t);

        return t < AttackRatio
            ? EvaluateIn(t)
            :  t > AttackRatio + SustainRatio
            ? EvaluateOut(t)
            : 1;
    }

    static float Ease(float p_x, float p_c)
    {
        if (p_c == 0)
            return p_x;
        else if (p_c < 0)
            return 1.0f - Mathf.Pow(1.0f - p_x, -p_c + 1);
        else
            return Mathf.Pow(p_x, p_c + 1);
    }
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NormalCurve))]
public class NormalCurveDrawer : PropertyDrawer
{
    const int propCount = 7;
    bool foldout;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        float h = EditorGUIUtility.singleLineHeight;
        return !foldout ? h : propCount * h;
    }

    NormalCurve adsr;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        float h = EditorGUIUtility.singleLineHeight;
        float fullWidth = position.width;

        EditorGUI.BeginProperty(position, label, property);

        float startX = position.x;

        // Draw label foldout
        Rect foldRect = position;
        foldRect.height = h;
        foldout = EditorGUI.Foldout(foldRect, foldout, label, true);
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), new GUIContent(" "));

        var attackProp = property.FindPropertyRelative("attack");
        var sustainProp = property.FindPropertyRelative("sustain");
        var releaseProp = property.FindPropertyRelative("release");

        var attackEaseProp = property.FindPropertyRelative("attackEase");
        var releaseEaseProp = property.FindPropertyRelative("releaseEase");

        adsr.attack = attackProp.floatValue;
        adsr.sustain = sustainProp.floatValue;
        adsr.release = releaseProp.floatValue;

        adsr.attackEase = attackEaseProp.floatValue;
        adsr.releaseEase = releaseEaseProp.floatValue;

        Rect curveRect = position;

        if (!foldout)
            curveRect.height = h;
        else
        {
            curveRect.x = startX;
            curveRect.y = position.y + h;

            curveRect.width = fullWidth;
            curveRect.height = h * 3;
        }

        Vector2 curveStart = curveRect.position - new Vector2(0, -curveRect.height);
        Vector2 lastP = curveStart + new Vector2(0, -adsr.Evaluate(0) * curveRect.height);
        int viewWidth = (int)curveRect.width;

        EditorGUI.DrawRect(curveRect, new Color(0,0,0, .25f));

        float sustainBegin = adsr.AttackRatio;
        float releaseBegin = adsr.AttackRatio + adsr.SustainRatio;

        const float br = .55f;


        if (foldout)
        {
            Handles.DrawLine(curveStart + new Vector2(sustainBegin * viewWidth, -curveRect.height), curveStart + new Vector2(sustainBegin * viewWidth, 0));
            Handles.DrawLine(curveStart + new Vector2(releaseBegin * viewWidth, -curveRect.height), curveStart + new Vector2(releaseBegin * viewWidth, 0));
        }

        for (int i = 0; i < viewWidth; i++)
        {
            float t = (float)i / viewWidth;

            if(foldout)
                Handles.color = 
                      t < sustainBegin 
                    ? new Color(1, 1, br) 
                    : t < releaseBegin 
                    ? new Color(br, 1, 1) 
                    : new Color(1, br, 1);

            float v = adsr.Evaluate(t);
            Vector2 p = curveStart + new Vector2(i, -v * curveRect.height);
            Handles.DrawLine(lastP, p);
            lastP = p;
        }


        if (foldout)
        {
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            position.x = startX;
            position.y += curveRect.height + h + 5;
            position.width = fullWidth;

            EditorGUIUtility.labelWidth = 52;

            float margin = 3;
            float propWidth = position.width / 3 - margin;
            var valueRect = new Rect(position.x, position.y, propWidth, h);

            EditorGUI.PropertyField(valueRect, attackProp); valueRect.x += propWidth + margin;
            EditorGUI.PropertyField(valueRect, sustainProp); valueRect.x += propWidth + margin;
            EditorGUI.PropertyField(valueRect, releaseProp); valueRect.x = startX; valueRect.y += h;

            EditorGUI.PropertyField(valueRect, attackEaseProp, new GUIContent("Ease")); valueRect.x += propWidth + margin;
            valueRect.x += propWidth + margin;
            EditorGUI.PropertyField(valueRect, releaseEaseProp, new GUIContent("Ease")); valueRect.x += propWidth + margin;

            if (attackProp.floatValue < 0)
                attackProp.floatValue = 0;

            if (releaseProp.floatValue < 0)
                releaseProp.floatValue = 0;

            if (sustainProp.floatValue < 0)
                sustainProp.floatValue = 0;

            EditorGUI.indentLevel = indent;

            EditorGUIUtility.labelWidth = 0;
        }

        EditorGUI.EndProperty();

    }
}
#endif