using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XCharts.Runtime;

namespace lab3
{
    public class ChartManager : MonoBehaviour
    {
        [SerializeField] private StackExperements _stackExperements;
        [SerializeField] private Button _startChartButton;
        [SerializeField] private Button _secondChartButton;
        [SerializeField] private LineChart chart;
        
        
        [SerializeField] private QueueExperements _queueExperements;
        [SerializeField] private Button _startChartQueButton;
        [SerializeField] private LineChart _lineChartQue;    
    
        public void Init()
        {
            _startChartButton.onClick.AddListener(CreateBothCharts);
            _startChartQueButton.onClick.AddListener(CreateQueCharts);
        }

        private void CreateQueCharts()
        {// очищаем старые данные
            //
            _lineChartQue.ClearData();
            _lineChartQue.ClearSerieLinks();

            // === Настройка осей ===
            var xAxis = _lineChartQue.GetOrAddChartComponent<XAxis>();
            xAxis.type = Axis.AxisType.Category;
            xAxis.axisName.show = true;
            xAxis.axisName.name = "операции";

            var yAxis = _lineChartQue.GetOrAddChartComponent<YAxis>();
            yAxis.type = Axis.AxisType.Value;
            yAxis.axisName.show = true;
            yAxis.axisName.name = "Время (мкс)";

            var tooltip = _lineChartQue.GetOrAddChartComponent<Tooltip>();
            tooltip.show = true;
            var legend = _lineChartQue.GetOrAddChartComponent<Legend>();
            legend.show = true;

            // === Серия 1 — без Print() ===
            var data1 = _queueExperements.MakeQueueOnlyExperementStandartQue(false);
            var series1 = _lineChartQue.AddSerie<Line>("std без Print()");
            series1.symbol.show = false;
            series1.lineStyle.width = 2f;

            // === Серия 2 — с Print() ===
            var data2 = _queueExperements.MakeQueueOnlyExperementStandartQue(true);
            var series2 = _lineChartQue.AddSerie<Line>("std с Print()");
            series2.symbol.show = false;
            series2.lineStyle.width = 2f;
            // 
            var data3 = _queueExperements.MakeEnqueuePrintExperementsStandart(true);
            var series3 = _lineChartQue.AddSerie<Line>("std Enqueue+Print()");
            series3.symbol.show = false;
            series3.lineStyle.width = 2f;

            // === Добавляем данные ===
            for (int i = 0; i < data1.Count; i++)
            {
                string xLabel = data1[i].x.ToString();
                _lineChartQue.AddXAxisData(xLabel);

                _lineChartQue.AddData(0, data1[i].y); // Без Print()
                _lineChartQue.AddData(1, data2[i].y); // С Print()
                _lineChartQue.AddData(2,data3[i].y);
            }

            _lineChartQue.RefreshChart();
        }
        

        private void CreateBothCharts()
        {
            // очищаем старые данные
            chart.ClearData();
            chart.ClearSerieLinks();

            // === Настройка осей ===
            var xAxis = chart.GetOrAddChartComponent<XAxis>();
            xAxis.type = Axis.AxisType.Category;
            xAxis.axisName.show = true;
            xAxis.axisName.name = "операции";

            var yAxis = chart.GetOrAddChartComponent<YAxis>();
            yAxis.type = Axis.AxisType.Value;
            yAxis.axisName.show = true;
            yAxis.axisName.name = "Время (мкс)";

            var tooltip = chart.GetOrAddChartComponent<Tooltip>();
            tooltip.show = true;
            var legend = chart.GetOrAddChartComponent<Legend>();
            legend.show = true;

            // === Серия 1 — без Print() ===
            var data1 = _stackExperements.MakeStackOnlyExperement();
            var series1 = chart.AddSerie<Line>("Без Print()");
            series1.symbol.show = false;
            series1.lineStyle.width = 2f;

            // === Серия 2 — с Print() ===
            var data2 = _stackExperements.MakeStackOnlyExperement(true);
            var series2 = chart.AddSerie<Line>("С Print()");
            series2.symbol.show = false;
            series2.lineStyle.width = 2f;
            // 
            var data3 = _stackExperements.MakePushPrintExperements(true);
            var series3 = chart.AddSerie<Line>("Push+Print()");
            series2.symbol.show = false;
            series2.lineStyle.width = 2f;

            // === Добавляем данные ===
            for (int i = 0; i < data1.Count; i++)
            {
                string xLabel = data1[i].x.ToString();
                chart.AddXAxisData(xLabel);

                chart.AddData(0, data1[i].y); // Без Print()
                chart.AddData(1, data2[i].y); // С Print()
                chart.AddData(2,data3[i].y);
            }

            chart.RefreshChart();
        }
    }
}
