using Android.Webkit;
using Java.Interop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace com.dinizdesenvolve.planejar.view
{

    internal class WebAppInterface : Java.Lang.Object
    {
        private List<string> titles;
        private List<double> values;
        public ChartObject mChartObject;
        //private int color;

        public WebAppInterface(string pTitle)
        {
            titles = new List<string>();
            values = new List<double>();
            mChartObject = new ChartObject();
            mChartObject.theme = "theme1";
            mChartObject.title.Add(new ChartTitle { text = pTitle });
            mChartObject.animationEnabled = true;
            // color = 0;

        }

        public void AddChartData(string pType /*bar, line, area, pie, column, spline, splineArea, doughnut */
                               , string[] pLabels
                               , int?[] pValuesX
                               , int?[] pValuesY)
        {

            if ((pLabels.Length < 0) &&
                (pLabels.Length != pValuesY.Length) &&
                (pLabels.Length != pValuesX.Length))
            {
                throw new Exception("Invalid chart parameters.");
            }




            //carrega valores grafico 

            ChartData mChartData = new ChartData();
            mChartData.type = pType;
            
            

            if (pLabels != null)
            {
                for (int x = 0; x < pLabels.Length; x++)
                {
                    var mPoints = new ChartPoints();

                    try
                    {
                        mPoints.label = pLabels[x];


                    }
                    catch (Exception e)
                    {

                    }
                    try
                    {
                        mPoints.x = pValuesX[x];

                    }
                    catch (Exception e)
                    {

                    }

                    try
                    {
                        mPoints.y = pValuesY[x];

                    }
                    catch (Exception e)
                    {

                    }

                    mChartData.dataPoints.Add(mPoints);
                }
            }
            
            mChartObject.data.Add(mChartData);


        }


        public void addItem(string title, decimal value)
        {
            titles.Add(title);
            values.Add(decimal.ToDouble(value));
        }

        public void addTitle(string title)
        {
            titles.Add(title);
        }

        public void addValue(decimal value)
        {
            values.Add(decimal.ToDouble(value));
        }

        [Export("getTitle")]
        [JavascriptInterface]
        public string getTitle(int Position)
        {
            try
            {
                return titles[Position];
            }
            catch (Java.Lang.Exception e)
            {
                return e.Message;
            }

        }


        [Export("getValue")]
        [JavascriptInterface]
        public double getValue(string Position)
        {
            int p = Convert.ToInt32(Position);
            try
            {
                return values[p];
            }
            catch (Java.Lang.Exception e)
            {
                return 0;
            }
        }

        [Export("getValueJson")]
        [JavascriptInterface]
        public string getValueJson()
        {
            //JsonConvert.DeserializeObject<List<Cliente>>(txtJson);
            string retorno = JsonConvert.SerializeObject(mChartObject,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });


            //As per James Newton King: If you create the serializer yourself rather than using JavaScriptConvert there is a NullValueHandling property which you can set to ignore. Here's a sample:
            return retorno;

        }


        internal void AddColor(object tRANSPARENT)
        {
            throw new NotImplementedException();
        }


    };

    class WebChart{
        
        public WebAppInterface dataChart;

        public WebChart(string pTitle) {
            dataChart = new WebAppInterface(pTitle);
        }

        public void loadChart(WebView pWebView)
        {

            pWebView.Settings.JavaScriptEnabled = true;
            pWebView.AddJavascriptInterface(dataChart, "Android");
            pWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);

            string file = "file:///android_asset/chartBarras.html";
            


            pWebView.LoadUrl(file);
            pWebView.SetBackgroundColor(Android.Graphics.Color.Transparent);
        }

    }

    class ChartObject
    {
        public string theme;
        public List<ChartTitle> title;
        public bool? animationEnabled;
        public bool? zoomEnabled;
        public List<ChartData> data;
        public List<ChartAxis> axisX;
        public List<ChartAxis> axisY;

        public ChartObject()
        {
            title = new List<ChartTitle>();
            data = new List<ChartData>();
        }
    };
    class ChartTitle
    {
        public string text;
    };
    class ChartAxis {
        public string valueFormatString;
        public int? interval;
        public string intervalType;
        public int? labelAngle;
        public bool? includeZero;
    }
    class ChartData
    {
        public string type;
        public bool? showInLegend;
        public List<ChartPoints> dataPoints;

        public ChartData()
        {
            dataPoints = new List<ChartPoints>();
        }
    };
    class ChartPoints
    {
        public string label;
        public int? y;
        public int? x;

    };

}