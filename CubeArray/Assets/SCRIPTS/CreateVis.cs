using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVis : MonoBehaviour {

    public Material material;
    public Material transparent = Resources.Load<Material>("MATERIALS/Ticks") as Material;
    public Material textMat;
    public Font font;
    public string[] matNames = {   "White", //text color
                                    "Blue", "Blue Light",
                                    "Green", "Green Light",
                                    "Orange", "Orange Light",
                                    "Purple", "Purple Light",
                                    "Red", "Red Light" };
    public Material LoadMaterial(int num)
    {
        Material m = Resources.Load<Material>("MATERIALS/" + matNames[num]) as Material;
        return m;
    }

    public float FindMax(List<List<object>> Input)
    {
        //Basic find max: Iterate through entire array with largest number in tow
        float max = 0;
        for (int i = 1; i < 11; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                float temp = System.Convert.ToSingle(Input[i][j]);
                if (temp > max) { max = temp; }
            }
        }

        return max;
    }

    public List<List<object>> Normalize(List<List<object>> Input, float max)
    {
        List<List<object>> Output = new List<List<object>>();
        //Iterate and Normalize values by largest number
        for (int i = 0; i < 11; i++)
        {
            List<object> row = new List<object>();
            for (int j = 0; j < 11; j++)
            {
                if (i == 0 || j == 0)
                {
                    row.Add(Input[i][j]);
                }
                else
                {
                    row.Add(System.Convert.ToSingle(Input[i][j]) / max);
                }
            }
            Output.Add(row);
        }
        return Output;
    }

    public float CalculateMultiple(float max)
    {
        //Very basic finding tick marks, with min ticks = 5, max ticks = 10. cheating because dealing with percentage data
        //multiples of 2, 5 or 10.
        float multiple;
        if (max <= 20)
        {
            multiple = 2;
        }
        else if (max <= 50)
        {
            multiple = 5;
        }
        else if (max <= 100)
        {
            multiple = 10;
        }
        else
        {
            multiple = max;
        }

        return multiple;
    }

    public void CreateBase(Transform parent, float length, float height, float offset)
    {
        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottom.name = "Base";
        bottom.transform.parent = parent;
        bottom.transform.localScale = new Vector3(length, height, length);
        bottom.transform.position = new Vector3((length + offset) / 2, -height / 2, (length + offset) / 2);
    }

    public void CreatePanes(Transform parent, float length, float offset, float max)
    {
        //make panes
        GameObject paneZ = Instantiate(Resources.Load("Pane")) as GameObject; //pane parallel with z axis
        paneZ.name = "Pane Z Axis";
        paneZ.transform.parent = parent;
        CreatePaneTicks(max, length, "z", paneZ);
        paneZ.transform.localScale = new Vector3(length, length, 1);
        paneZ.transform.position = new Vector3(((offset / 2) - 0.005f) + length, length / 2, (length + offset) / 2); // -0.05 -- stops z fighting, 0.2f -- currently magic number
        paneZ.transform.rotation = Quaternion.Euler(0, -90, 0);


        GameObject paneX = Instantiate(Resources.Load("Pane")) as GameObject; //pane parallel with x axis
        paneX.name = "Pane X axis";
        paneX.transform.parent = parent;
        CreatePaneTicks(max, length, "x", paneX);
        paneX.transform.localScale = new Vector3(length, length, 1);
        paneX.transform.position = new Vector3((length + offset) / 2, length / 2, ((offset / 2) - 0.005f) + length); // -0.05 -- stops z fighting, 0.2f -- currently magic number
        paneX.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public GameObject MakeLabel(string label, GameObject aParent)
    {
        GameObject Text = Instantiate(Resources.Load("Text")) as GameObject;
        Text.name = label;
        Text.GetComponent<TextMesh>().text = label;
        Text.GetComponent<TextMesh>().characterSize = 0.1f;
        Text.GetComponent<TextMesh>().fontSize = 45;
        Text.GetComponent<TextMesh>().color = UnityEngine.Color.black;
        Text.transform.parent = (aParent.transform);
        Text.transform.rotation = Quaternion.Euler(0, 0, 90);
        return Text;
    }

    public void CreateBarTicks(GameObject Bar, float norm_value, float width, float max, float paneHeight)
    {
        Transform Bar_T = Bar.transform;
        float multiple = CalculateMultiple(max);
        float raw_value = norm_value * max;
        float max_tickValue = raw_value - (raw_value % multiple);

        for (int i = 0; i <= (max_tickValue / multiple); i++)
        {
            float tick_height = ((i * multiple / max) * 6.8f);

            if (tick_height + 0.025f < norm_value * 6.8) //account for the extra height of the tick bar
            {
                GameObject tick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tick.GetComponent<MeshRenderer>().material = transparent;
                Destroy(tick.GetComponent<BoxCollider>());
                Transform tick_T = tick.transform;
                tick.name = Bar.name + " tick_" + i.ToString();
                tick_T.localScale = new Vector3(width + 0.01f, 0.05f, width + 0.01f);
                tick_T.localPosition = new Vector3(Bar_T.localPosition.x, tick_height, Bar_T.localPosition.z);
                tick.transform.parent = Bar.transform;
            }
        }
    }

    public void CreatePaneTicks(float max, float paneHeight, string Axis, GameObject aParent)
    {
        //Very basic finding tick marks, with min ticks = 5, max ticks = 10. cheating because dealing with percentage data

        float multiple = CalculateMultiple(max);
        float line_Max = max + (multiple - max % multiple); // find next highest multiple
        float numTicks = line_Max / multiple;

        GameObject lines = new GameObject();
        lines.name = "lines";
        lines.transform.parent = aParent.transform;

        GameObject lineTexts = new GameObject();
        lineTexts.name = "Texts";
        lineTexts.transform.parent = lines.transform;

        //just liek make all the things
        for (int i = 1; i <= numTicks; i++)
        {
            GameObject line = Instantiate(Resources.Load("Line")) as GameObject;
            line.name = "Line " + i.ToString();
            line.transform.parent = lines.transform;
            line.transform.localPosition = new Vector3(0, (i * multiple / max) * 6.8f / paneHeight, 0); 
            //0.68 is max height of bar, manual calculations suck, I know, sorry
            //So here is what the heck is happening up there: 
            // Consider the highest line required, it has to be placed higher than the tallest bar
            // therefore, i*multiple/original_max, in the context of the highest bar,
            // is greater than 1, 1 being the scale of the tallest bar
            // the tallest bar is 6.8 in height, so scale it with that (a*6.8f)
            // then adjust to size of pane, since pane is not scale 10
            // basically, this code is garbage, and I should feel bad about it.

            GameObject valueText = MakeLabel((i * multiple).ToString(), lineTexts);
            valueText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            valueText.transform.localPosition = new Vector3(-0.52f, ((i * multiple / max) * 6.8f / paneHeight) + 0.02f, 0);

            valueText.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
            if (Axis == "z")
            {
                valueText.transform.rotation = Quaternion.Euler(0, 180, 0);
                valueText.transform.localPosition = new Vector3(-0.48f, ((i * multiple / max) * 6.8f / paneHeight) + 0.02f, 0);
            }
            else
            {
                valueText.transform.rotation = Quaternion.Euler(0, 0, 0);
                valueText.transform.localPosition = new Vector3(-0.52f, ((i * multiple / max) * 6.8f / paneHeight) + 0.02f, 0);
            }
        }
        lines.transform.localPosition = new Vector3(0.05f, -0.5f, 0);//final adjustment for all lines
    }

    public GameObject Create10x10(List<List<object>> Input, float scaleWidth, float barWidth)
    {
        float max = FindMax(Input);
        Debug.Log(max);
        float baseLength = 11f - (10f * scaleWidth); //10+extra space - scaling? 11f is magic... used to be 10.25
        float offset = (1f - scaleWidth - .25f); // offset by 1(because first column no cubes) - scaling - width/2
        float baseHeight = 3f;
        //Note: offset / 2 = position of base - half width

        List<List<object>> Input_n = Normalize(Input, max); // Normalize the values for correct bar height

        //Gameobject that holds all objects related to the visualization
        GameObject Vis = new GameObject();
        Vis.name = "Viz";

        //Just, liek, make base
        CreateBase(Vis.transform, baseLength, baseHeight, offset);

        //Just, liek, make panes
        CreatePanes(Vis.transform, baseLength, offset, max);

        //GameObject to hold all the text labels, easier to see in inspector
        GameObject labels = new GameObject();
        labels.name = "Labels";
        labels.transform.parent = (Vis.transform);

        GameObject countryLabels = new GameObject(){ name = "Countries"};
        countryLabels.transform.parent = labels.transform;

        GameObject yearLabels = new GameObject() { name = "Years" };
        yearLabels.transform.parent = labels.transform;

        //GameObject to hold all the bars, easier to see in inspector
        GameObject cubeArray = new GameObject();
        cubeArray.name = "Bars";
        cubeArray.transform.parent = (Vis.transform);

        //Pushed the for loop variables out here because I need year=0 and consistency// do i still need this?

        for (int country = 0; country < 11; country++)
        {

            material = LoadMaterial(country); // Load the correct color for the entire row

            for (int year = 0; year < 11; year++)
            {
                float pushX = country - (country * scaleWidth); //where to place current bar in X axis
                float pushZ = year - (year * scaleWidth); // where to place current bar in the X axis

                if (country == 0 && year == 0)
                {
                    //In the array this is the title; don't really need it.
                }
                else if (country == 0)
                {
                    //Make the year labels
                    //Note: Just a bunch of magic number garbage below
                    GameObject aLabel = MakeLabel(System.Convert.ToString(Input[country][year]), yearLabels);
                    aLabel.transform.localPosition = new Vector3((offset / 2) - 0.005f, -1.1f, pushZ + 0.85f); // -0.05 -- stops z fighting, 0.85f -- currently magic number
                    aLabel.transform.rotation = Quaternion.Euler(0, 90, 90);
                }
                else if (year == 0)
                {
                    //Make the country labels
                    //Note: Just a bunch of magic number garbage below
                    GameObject aLabel = MakeLabel(System.Convert.ToString(Input[country][year]), countryLabels);
                    aLabel.transform.position = new Vector3(pushX + 0.95f, -0.1f, (offset / 2) - 0.005f);// -0.05 -- stops z fighting, 0.95f == currently magic number, offset y by 0.1 because it looks nice
                    aLabel.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube.name = country.ToString() + "," + year.ToString();
                    cube.tag = "Bar";
                    cube.AddComponent<BarCollision>();
                    cube.GetComponent<MeshRenderer>().material = material;
                    float norm_Value = System.Convert.ToSingle(Input_n[country][year]);
                    float height = norm_Value * 6.8f; //6.8 = manually calculated width of entire vis
                    float pushY = height / 2; // since unity scales from center, push up by half of height
                    cube.transform.parent = (cubeArray.transform);
                    cube.transform.localScale = new Vector3(barWidth, height, barWidth);
                    cube.transform.position = new Vector3(pushX, pushY, pushZ);
                    CreateBarTicks(cube, norm_Value, barWidth, max, baseLength);
                }
            }

        }
        cubeArray.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
        // basically 0.75 is magic number, offset all the bars so that you can see the numbers on the glass plane

        return Vis;
    }
}
