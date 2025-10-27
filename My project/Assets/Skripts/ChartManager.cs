using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;

namespace lab3
{
    public class ChartManager: MonoBehaviour
    {
        
        [SerializeField] StackExperements _stackExperements;
        [SerializeField] private Button _startChartButton;
        [SerializeField] private XCharts.Runtime.LineChart chart;
        private List<int> time;
        private List<int> commands;
        
        public void Init()
        {
            _startChartButton.onClick.AddListener(() =>CreateChart(_stackExperements.MakeStackOnlyExperement(true)));
            
        }

        private void CreateChart(List<Vector2Int> data)
        {
            
            

            // Получаем компонент графика
          
            chart.ClearData();

            // === Настройки осей ===
            var xAxis = chart.GetOrAddChartComponent<XAxis>();
            xAxis.type = Axis.AxisType.Category;
            xAxis.axisName.show = true;
            xAxis.axisName.name = "Количество элементов";

            var yAxis = chart.GetOrAddChartComponent<YAxis>();
            yAxis.type = Axis.AxisType.Value;
            yAxis.axisName.show = true;
            yAxis.axisName.name = "Время (мс)";

            // === Подсказки и легенда ===
            var tooltip = chart.GetOrAddChartComponent<Tooltip>();
            tooltip.show = true;

            var legend = chart.GetOrAddChartComponent<Legend>();
            legend.show = true;

            // === Добавляем серию данных ===
            var series = chart.AddSerie<Line>("Время выполнения");
            series.symbol.show = false;           // без точек
            series.lineType = LineType.Smooth;    // плавная линия
            series.lineStyle.width = 2f;          // толщина
       
            // Примерные данные

            int[] xValues = new int[data.Count];
            int[] yValues = new int[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                xValues[i] = data[i].x;
                
                yValues[i] = data[i].y;
              
            }

            for (int i = 0; i < xValues.Length; i++)
            {
                chart.AddXAxisData(xValues[i].ToString());
                chart.AddData(0, yValues[i]);
                
            }

            chart.RefreshChart();
            
        }   

        
        
        
    }
}