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

        public void Init()
        {
            _startChartButton.onClick.AddListener(CreateBothCharts);
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
