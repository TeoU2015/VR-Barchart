
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVis : MonoBehaviour {

    public Material material;
    public Material tickMat = Resources.Load<Material>("MATERIALS/legoTicks") as Material;
    public Material textMat;
    public Font font;
    public string[] matNames = {   "White", //text color
                                    "Orange Light", "Orange",
                                    "Red Light", "Red",
                                    "Purple Light", "Purple",
                                    "Blue Light", "Blue",
                                    "Green Light", "Green"};

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

    public float CustomRound(float input)
    { 
        float ret = (float)System.Math.Truncate(input);
        float check = input - ret;

        ret = (check >= 0.5) ? (ret + 1) : ret;

        return ret;
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

    public List<List<object>> legoHeight(List<List<object>> Input, float max)
    {
        List<List<object>> Output = new List<List<object>>();
        float brickHeight = (1f/30f); //for readability

        //Iterate through Normalized values
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
                    float currentValue = System.Convert.ToSingle(Input[i][j]);
                    float temp = currentValue / brickHeight;
                    //round height to nearest 1/3rd lego brick
                    float brickNum = CustomRound(temp);
                    row.Add( (brickNum * brickHeight) );
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

    public void CreateBase(Transform parent)
    {
        float width = 1f;
        float height = 0.35f;
        GameObject bottom = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bottom.name = "Base";
        bottom.transform.parent = parent;
        bottom.transform.localScale = new Vector3(width, height, width);//x,z of 1, y of 3
        bottom.transform.position = new Vector3((width / 2f), -(height / 2f), (width / 2f));//shuffle the center point by half x,y,z.
    }


    public void CreatePanes(Transform parent, float max, bool legoMode)
    {

        float size = 1f;//pane width
        float height = 1.2f; //pane height, larger than tallest bar(>1)

        //make pane that sits on Z axis
        GameObject paneZ = Instantiate(Resources.Load("Pane")) as GameObject; //pane parallel with z axis
        paneZ.name = "Pane Z Axis";
        paneZ.transform.parent = parent;
        paneZ.transform.localScale = new Vector3(size, height, size);//change size of pane
        CreatePaneTicks(max, true, paneZ, legoMode);
        paneZ.transform.position = new Vector3((size), (height / 2f), (size / 2f));
        paneZ.transform.rotation = Quaternion.Euler(0, -90, 0);

        //make pane that sits on X axis
        GameObject paneX = Instantiate(Resources.Load("Pane")) as GameObject; //pane parallel with x axis
        paneX.name = "Pane X axis";
        paneX.transform.parent = parent;
        paneX.transform.localScale = new Vector3(size, height, size);//change size of pane
        CreatePaneTicks(max, false, paneX, legoMode);
        paneX.transform.position = new Vector3((size / 2f), (height / 2f), (size)); // -0.05 -- stops z fighting, 0.2f -- currently magic number
        paneX.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public GameObject MakeLabel(GameObject aParent, string label)
    {
        GameObject Text = Instantiate(Resources.Load("Text")) as GameObject;
        Text.name = label;
        Text.GetComponent<TextMesh>().text = label;
        Text.GetComponent<TextMesh>().characterSize = 0.1f;
        Text.GetComponent<TextMesh>().fontSize = 50;
        Text.GetComponent<TextMesh>().color = UnityEngine.Color.black;
        Text.transform.parent = (aParent.transform);
        Text.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
        Text.transform.rotation = Quaternion.Euler(0, 0, 90);
        
        return Text;
    }

    public void CreateAxisLabels(GameObject aParent, string label, float position, bool isZaxis)
    {
        //because I'm not sure on how font size and game size scale,
        //Here's some magic number to line up the labels! (sorry)
        GameObject aLabel = MakeLabel(aParent, label);
        if (isZaxis)
        {
            aLabel.transform.rotation = Quaternion.Euler(0, 90, 90);
            aLabel.transform.localPosition = new Vector3(-0.01f, -0.11f, position+0.02f); // -0.05 -- stops z fighting, 0.85f -- currently magic number
        }
        else
        {
            aLabel.transform.rotation = Quaternion.Euler(0, 0, -90);
            aLabel.transform.position = new Vector3(position+0.02f, -0.02f, -0.01f);// -0.05 -- stops z fighting, 0.95f == currently magic number, offset y by 0.1 because it looks nice
        }    
    }

    public void CreateBarTicks(GameObject Bar, float height_n, float max, bool legoMode)
    {
        //not sure why I revert back to original heights from normalized
        //been to long to remember if that was important or not...
        Transform Bar_T = Bar.transform;
        float tickYScale = 0.002f;
        float multiple = (legoMode) ? max/30f : CalculateMultiple(max);
        float rawHeight = height_n * max;//extract the orignal height from normalized value
        float maxTickValue = (legoMode) ? rawHeight : ( rawHeight - ( rawHeight % multiple ) );//calculate the largest value for this bar
        int numTicks = (int)CustomRound(maxTickValue / multiple);//calculate number of ticks
        float topCheckValue = (rawHeight - (3f * multiple))/max;
        int iterator;

        for (iterator = 0; iterator <= numTicks; iterator++)
        {
            float tickHeight = ((iterator * multiple / max));
            bool fullBrick = true;
            if (legoMode) { fullBrick = (iterator % 3 == 0) ? true : false; }
            bool topTick = (tickHeight > topCheckValue) ? true : false;

            //tickHeight + 0.0025f < height_n  && 

            if (tickHeight + 0.0025f < height_n && (fullBrick)) //account for the extra height of the tick bar
            {
                GameObject tick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tick.GetComponent<MeshRenderer>().material = tickMat;
                Destroy(tick.GetComponent<BoxCollider>());//remove colliders because we don't need/want them

                Transform tick_T = tick.transform;
                tick.name = Bar.name + " tick_" + iterator.ToString();
                tick_T.localScale = new Vector3(Bar_T.localScale.x + 0.001f, tickYScale, Bar_T.localScale.z + 0.001f);//static height of 0.005f, and adust width to stick out just a bit from bars
                tick_T.localPosition = new Vector3(Bar_T.localPosition.x, tickHeight, Bar_T.localPosition.z);//place at bar and appropriate height
                tick.transform.parent = Bar.transform;
            }
        }

        if (legoMode)
        {
            iterator = numTicks;
            do
            {
                iterator--;
                float tickHeight = ((iterator * multiple / max));

                GameObject tick = GameObject.CreatePrimitive(PrimitiveType.Cube);
                tick.GetComponent<MeshRenderer>().material = tickMat;
                Destroy(tick.GetComponent<BoxCollider>());//remove colliders because we don't need/want them

                Transform tick_T = tick.transform;
                tick.name = Bar.name + "legotick_" + iterator.ToString();
                tick_T.localScale = new Vector3(Bar_T.localScale.x + 0.001f, tickYScale, Bar_T.localScale.z + 0.001f);//static height of 0.005f, and adust width to stick out just a bit from bars
                tick_T.localPosition = new Vector3(Bar_T.localPosition.x, tickHeight, Bar_T.localPosition.z);//place at bar and appropriate height
                tick.transform.parent = Bar.transform;

            } while (iterator % 3 != 0);
        }



    }

    public GameObject CreateBar(GameObject aParent, string name, float height, float width, float xPos, float zPos, float max, bool legoMode)
    {
        float yPos = height / 2f; // since unity scales from center, push up by half of height
        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.name = name;
        cube.tag = "Bar";
        cube.AddComponent<BarCollision>();
        cube.GetComponent<MeshRenderer>().material = material;
        cube.transform.parent = (aParent.transform);
        cube.transform.localScale = new Vector3(width, height, width);
        cube.transform.position = new Vector3(xPos, yPos, zPos);
        CreateBarTicks(cube, height, max, legoMode);

        return cube;
    }

    public void CreatePaneTicks(float max, bool isZAxis, GameObject aParent, bool legoMode)
    {
        
        float multiple = (legoMode) ? (max/10f) :CalculateMultiple(max);
        float line_Max = (legoMode) ? max : max + (multiple - max % multiple); // find next highest multiple
        float numTicks = line_Max / multiple;

        //Create group for lines
        GameObject lines = new GameObject();
        lines.name = "lines";
        lines.transform.parent = aParent.transform;

        //create group for the tick value labels
        GameObject lineTexts = new GameObject();
        lineTexts.name = "Texts";
        lineTexts.transform.parent = lines.transform;

        //For loop to make the ticks
        for (int i = 1; i <= numTicks; i++)
        {
            GameObject line = Instantiate(Resources.Load("Line")) as GameObject; //load in premade line object
            line.name = "Line " + i.ToString();
            line.transform.parent = lines.transform;
            line.transform.localPosition = new Vector3(0, (i * multiple / max), 0);//normalize and plot

            float textValue = (float)System.Math.Round((i * multiple), 1);

            //create the tick value label, some magic numbers to adjust position
            GameObject valueText = MakeLabel(lineTexts, (textValue).ToString());
            valueText.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);//adjust text size
            float yAdjust = 0.02f;//offset text height to center with line
            if (!legoMode)
            {
                valueText.GetComponent<MeshRenderer>().material.color = UnityEngine.Color.white;
            }

            if (isZAxis)
            {
                valueText.transform.rotation = Quaternion.Euler(0, 180, 0);
                valueText.transform.localPosition = new Vector3(-.48f, ((i * multiple / max)) + yAdjust, 0);
                //-0.48 is magic x axis adjustment number, for good distance from line
            }
            else
            {
                valueText.transform.rotation = Quaternion.Euler(0, 0, 0);
                valueText.transform.localPosition = new Vector3(-0.55f, ((i * multiple / max)) + yAdjust, 0);
                //-0.52 is magic z axis adjustment number, differs slightly from Z pane because of letter orientation
            }
        }
        //final adjustment for all lines, 0.05f to stop Z-fighting, -0.5 because lines are postioned center from the parent pane.
        lines.transform.localPosition = new Vector3(0.05f, -0.5f, 0);
    }

    public GameObject CreateChart(List<List<object>> Input, float masterScale, float spaceRatio, bool legoMode)
    {
        //get largest number (raw)
        float max = FindMax(Input);

        //normalize all values
        List<List<object>> Input_n = Normalize(Input, max); // Normalize the values for correct bar height

        //Set all values to nearest lego brick height
        if (legoMode)
        {
            Input_n = legoHeight(Input_n, max);//normalize lego values
            Debug.Log("Brick Interval: " + (max / 30f));
        }

        //Gameobject that holds all objects related to the visualization
        GameObject Vis = new GameObject();
        Vis.name = "Viz";

        //Create base
        CreateBase(Vis.transform);

        //Create panes
        CreatePanes(Vis.transform, max, legoMode);

        //GameObject to hold all the text labels, easier to see in inspector
        GameObject labels = new GameObject();
        labels.name = "Labels";
        labels.transform.parent = (Vis.transform);

        //Group country labels
        GameObject countryLabels = new GameObject(){ name = "Countries"};
        countryLabels.transform.parent = labels.transform;

        //Group year labels
        GameObject yearLabels = new GameObject() { name = "Years" };
        yearLabels.transform.parent = labels.transform;

        //GameObject to hold all the bars, easier to see in inspector
        GameObject cubeArray = new GameObject();
        cubeArray.name = "Bars";
        cubeArray.transform.parent = (Vis.transform);

        //All calculations assume a chart size of "1"
        //scale up or down from there using masterScale
        int xAxisLen = Input.Count; //count number of items in x axis
        int zAxisLen = Input[0].Count; //count number of items in y axis
        int maxLen = xAxisLen > zAxisLen ? xAxisLen : zAxisLen; //get largest length for normalizing positions and scales to 1
        float InitialBarWidth = 1f / (maxLen); //Initial bar width, assumes no space inbetween bars

        //barWidth calculated by using space ratio, although the math has been reduced to be nearly magical
        //Originally: float barWidth = (InitialBarWidth / ( barWidth_i + spaceWidth_i )) * barWidth_i
        //However math checks out, and will scale the bar correctly to the spaceRatio provided
        float barWidth = (InitialBarWidth * spaceRatio) / (spaceRatio + 1f);// new barwidth adjusted by width-spacing ratio provided

        //Create for each loop and counter
        for (int country = 0; country < xAxisLen; country++)
        {
            material = LoadMaterial(country); // Load the correct color for the entire row
            for (int year = 0; year < zAxisLen; year++)
            {
                //bar center position = (width / bars + 1) * i, bars+1 because we only want bars in the center, not the edges
                float xAxisPos = (1f / (maxLen)) * country;
                float zAxisPos = (1f / (maxLen)) * year;//12???????

                if(country==0 && year == 0) {
                    //nothing
                }
                else if (country == 0)
                {
                    //Make the year labels
                    CreateAxisLabels(yearLabels, System.Convert.ToString(Input[country][year]), zAxisPos, true);
                }
                else if (year == 0)
                {
                    //Make the country labels
                    CreateAxisLabels(countryLabels, System.Convert.ToString(Input[country][year]), xAxisPos, false);
                }
                else
                {
                    //Create a bar
                    float height = System.Convert.ToSingle(Input_n[country][year]);
                    string barName = System.Convert.ToString(Input[country][0]) + ", " + System.Convert.ToString(Input[0][year]);
                    if (height > 0)
                    {
                        GameObject bar = CreateBar(cubeArray, barName, height, barWidth, xAxisPos, zAxisPos, max, legoMode);
                    }
                }
            }
        }
        //apply MasterScale value to entire vis
        Vis.transform.localScale = new Vector3(masterScale, masterScale, masterScale);

        return Vis;
    }

    //delete below after complete and change all references in other scales.
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
       // CreateBase(Vis.transform, baseLength, baseHeight, offset);

        //Just, liek, make panes
        //CreatePanes(Vis.transform, baseLength, offset, max);

        //GameObject to hold all the text labels, easier to see in inspector
        GameObject labels = new GameObject();
        labels.name = "Labels";
        labels.transform.parent = (Vis.transform);

        GameObject countryLabels = new GameObject() { name = "Countries" };
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
                    //GameObject aLabel = MakeLabel(System.Convert.ToString(Input[country][year]), yearLabels);
                    //aLabel.transform.localPosition = new Vector3((offset / 2) - 0.005f, -1.1f, pushZ + 0.85f); // -0.05 -- stops z fighting, 0.85f -- currently magic number
                    //aLabel.transform.rotation = Quaternion.Euler(0, 90, 90);
                }
                else if (year == 0)
                {
                    //Make the country labels
                    //Note: Just a bunch of magic number garbage below
                    //GameObject aLabel = MakeLabel(System.Convert.ToString(Input[country][year]), countryLabels);
                    //aLabel.transform.position = new Vector3(pushX + 0.95f, -0.1f, (offset / 2) - 0.005f);// -0.05 -- stops z fighting, 0.95f == currently magic number, offset y by 0.1 because it looks nice
                    //aLabel.transform.rotation = Quaternion.Euler(0, 0, -90);
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
                    //CreateBarTicks(cube, norm_Value, barWidth, max, baseLength);
                }
            }

        }
        cubeArray.transform.localPosition = new Vector3(0.75f, 0, 0.75f);
        // basically 0.75 is magic number, offset all the bars so that you can see the numbers on the glass plane

        return Vis;
    }
}